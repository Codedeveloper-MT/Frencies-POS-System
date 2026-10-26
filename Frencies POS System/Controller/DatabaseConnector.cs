using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace Frencies_POS_System.Controller
{
    internal class DatabaseConnector
    {
        private const string DatabaseName = "CustomersDB";

        // Connection to the master database, used only to create CustomerOrdersDB
        private const string ServerConn =
            @"Server=(localdb)\MSSQLLocalDB;Database=master;Trusted_Connection=True;TrustServerCertificate=True;";

        // Connection to the CustomerOrdersDB database
        private const string DbConn =
            @"Server=(localdb)\MSSQLLocalDB;Database=" + DatabaseName + @";Trusted_Connection=True;TrustServerCertificate=True;";

       
        public void CreateDatabaseAndTable()
        {
            using (var con = new SqlConnection(ServerConn))
            {
                con.Open();
                using (var cmd = new SqlCommand(
                    "IF DB_ID('" + DatabaseName + "') IS NULL CREATE DATABASE [" + DatabaseName + "];", con))
                {
                    cmd.ExecuteNonQuery();
                }
            }

            // Clear pooled connections so the new database is picked up immediately
            SqlConnection.ClearAllPools();

            using (var con = new SqlConnection(DbConn))
            {
                con.Open();
                string sql = @"
                    IF OBJECT_ID('dbo.CustomerOrders','U') IS NULL
                    CREATE TABLE dbo.CustomerOrders (
                        Id INT IDENTITY(1,1) PRIMARY KEY,
                        Username NVARCHAR(100) NOT NULL,
                        Email NVARCHAR(150) NOT NULL,
                        TotalPrice DECIMAL(10,2) NOT NULL DEFAULT 0,
                        TicketNumber INT NOT NULL DEFAULT 0,
                        PayMethod NVARCHAR(50) NOT NULL DEFAULT ''
                    );

                    IF OBJECT_ID('dbo.OrderItems','U') IS NULL
                    CREATE TABLE dbo.OrderItems (
                        Id INT IDENTITY(1,1) PRIMARY KEY,
                        CustomerId INT NOT NULL
                            FOREIGN KEY REFERENCES dbo.CustomerOrders(Id) ON DELETE CASCADE,
                        ItemName NVARCHAR(100) NOT NULL,
                        Price DECIMAL(10,2) NOT NULL
                    );";
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Saves a customer and all their ordered items (all or nothing)
        public void Insert(CustomerOrdersClass customer)
        {
            using (var con = new SqlConnection(DbConn))
            {
                con.Open();
                using (var tx = con.BeginTransaction())
                {
                    int customerId;//fk

                    using (var cmd = new SqlCommand(
                        @"INSERT INTO dbo.CustomerOrders (Username, Email, TotalPrice, TicketNumber, PayMethod)
                          OUTPUT INSERTED.Id
                          VALUES (@u, @e, @t, @n, @p)", con, tx))
                    {
                        cmd.Parameters.AddWithValue("@u", customer.GetName() ?? "");
                        cmd.Parameters.AddWithValue("@e", customer.GetEmail() ?? "");
                        cmd.Parameters.AddWithValue("@t", customer.GetTotalPrice());
                        cmd.Parameters.AddWithValue("@n", customer.GetTicketNumber());
                        cmd.Parameters.AddWithValue("@p", customer.GetPayMethod() ?? "");
                        customerId = (int)cmd.ExecuteScalar();
                    }

                    var orders = customer.GetOrders();
                    if (orders != null)
                    {
                        foreach (var item in orders)
                        {
                            using (var cmd = new SqlCommand(
                                "INSERT INTO dbo.OrderItems (CustomerId, ItemName, Price) VALUES (@c, @i, @pr)",
                                con, tx))
                            {
                                cmd.Parameters.AddWithValue("@c", customerId);
                                cmd.Parameters.AddWithValue("@i", item.Key);
                                cmd.Parameters.AddWithValue("@pr", item.Value);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    tx.Commit();
                }
            }
        }


        public int DeleteByTicket(int ticketNumber)
        {
            using (var con = new SqlConnection(DbConn))
            {
                con.Open();
                using (var tx = con.BeginTransaction())
                {
                    // 1. Delete the ordered items first
                    using (var cmd = new SqlCommand(
                        @"DELETE FROM dbo.OrderItems
                          WHERE CustomerId IN (SELECT Id FROM dbo.CustomerOrders WHERE TicketNumber = @t)",
                        con, tx))
                    {
                        cmd.Parameters.AddWithValue("@t", ticketNumber);
                        cmd.ExecuteNonQuery();
                    }

                    // 2. Delete the order itself
                    int deleted;
                    using (var cmd = new SqlCommand(
                        "DELETE FROM dbo.CustomerOrders WHERE TicketNumber = @t", con, tx))
                    {
                        cmd.Parameters.AddWithValue("@t", ticketNumber);
                        deleted = cmd.ExecuteNonQuery();
                    }

                    tx.Commit();
                    return deleted;
                }
            }
        }

        // Reads all customers back, including their ordered items
        public List<CustomerOrdersClass> GetAll()
        {
            var customers = new Dictionary<int, CustomerOrdersClass>();

            using (var con = new SqlConnection(DbConn))
            {
                con.Open();

                // 1. Customers
                using (var cmd = new SqlCommand(
                    "SELECT Id, Username, Email, TotalPrice, TicketNumber, PayMethod FROM dbo.CustomerOrders ORDER BY Id", con))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customers[reader.GetInt32(0)] = new CustomerOrdersClass(
                            Convert.ToDouble(reader.GetDecimal(3)),   // totalPrice
                            reader.GetInt32(4),                       // ticketNumber
                            reader.GetString(5),                      // payMethod
                            new Dictionary<string, double>(),         // orders (filled below)
                            reader.GetString(1),                      // name
                            reader.GetString(2));                     // email
                    }
                }

                // 2. Their ordered items
                using (var cmd = new SqlCommand(
                    "SELECT CustomerId, ItemName, Price FROM dbo.OrderItems", con))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (customers.TryGetValue(reader.GetInt32(0), out var customer))
                        {
                            customer.GetOrders()[reader.GetString(1)] = Convert.ToDouble(reader.GetDecimal(2));
                        }
                    }
                }
            }

            return new List<CustomerOrdersClass>(customers.Values);
        }
    }
}
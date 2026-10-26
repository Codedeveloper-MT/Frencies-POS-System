using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Manages customer information and order details in the Frencies POS System.
/// Demonstrates encapsulation through private fields and public getter and setter methods,
/// constructor overloading, 
/// </summary>
namespace Frencies_POS_System.Controller
{
    public class CustomerOrdersClass
    {
        private string name = "";
        private string email = "";

        private double totalPrice;
        private int ticketNumber;
        private string payMethod = "";
        private Dictionary<string, double> orders = new Dictionary<string, double>();

        public CustomerOrdersClass(double totalPrice, int customerTicketNumber, string paymentMethod)
        {
            this.totalPrice = totalPrice;
            this.ticketNumber = customerTicketNumber;
            this.payMethod = paymentMethod;
        }

        public CustomerOrdersClass(Dictionary<string, double> customerOrders)
        {
            this.orders = customerOrders;
        }

        public CustomerOrdersClass(string customerName, string customerEmail)
        {
            this.name = customerName;
            this.email = customerEmail;
        }

        public CustomerOrdersClass(double totalPrice, int customerTicketNumber, string paymentMethod,
                                   Dictionary<string, double> customerOrders, string customerName, string customerEmail)
        {
            this.name = customerName;
            this.email = customerEmail;
            this.orders = customerOrders;
            this.payMethod = paymentMethod;
            this.totalPrice = totalPrice;
            this.ticketNumber = customerTicketNumber;
        }

        // ---------------- Setters and getters ----------------

        public void SetName(string name)
        {
            this.name = name;
        }

        public string GetName()
        {
            return name;
        }

        public void SetEmail(string email)
        {
            this.email = email;
        }

        public string GetEmail()
        {
            return email;
        }

        public void SetOrders(Dictionary<string, double> orders)
        {
            this.orders = orders;
        }

        public Dictionary<string, double> GetOrders()
        {
            return orders;
        }

        public void SetTotalPrice(double totalPrice)
        {
            this.totalPrice = totalPrice;
        }

        public double GetTotalPrice()
        {
            return totalPrice;
        }

        public void SetTicketNumber(int ticketNumber)
        {
            this.ticketNumber = ticketNumber;
        }

        public int GetTicketNumber()
        {
            return ticketNumber;
        }

        public void SetPayMethod(string payMethod)
        {
            this.payMethod = payMethod;
        }

        public string GetPayMethod()
        {
            return payMethod;
        }

        // ---------------- Read-only properties for the DataGrid ----------------
        // A DataGrid can only bind to properties (not Get methods), so these
        // simply expose the same data the getters return.


        public int Ticket => ticketNumber;//expression-bodied member

        public string Name => name;

        public string Email => email;

        public string PayMethod => payMethod;

        public string Total => "R" + totalPrice.ToString("F2");

        public string Items => (orders == null || orders.Count == 0)//lamdba method
            ? "(no items)"
            : string.Join(", ", orders.Select(i => i.Key + " (R" + i.Value.ToString("F2") + ")"));

       
        //save to database
        public static void Run(string name, string email, double totalPrice, int ticketNumber,
                               string payMethod, Dictionary<string, double> orders)
        {
            var creator = new DatabaseConnector();

            // creates db and tables if missing
            creator.CreateDatabaseAndTable();
            creator.Insert(new CustomerOrdersClass(totalPrice, ticketNumber, payMethod, orders, name, email));
        }
    }
}
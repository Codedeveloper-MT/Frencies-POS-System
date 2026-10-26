using Frencies_POS_System.Controller;
using System.Windows;
using System.Windows.Controls;

namespace Frencies_POS_System
{
    /// <summary>
    /// Interaction logic for KitchenWindow.xaml
    /// Shows all saved customer orders and lets the kitchen delete an order by ticket number.
    /// </summary>
    public partial class KitchenWindow : Window
    {
        public KitchenWindow()
        {
            InitializeComponent();
        }

        // Loads every saved order from the database into the table
        private void LoadOrders()
        {
            var database = new DatabaseConnector();

            // Makes sure the database and tables exist
            database.CreateDatabaseAndTable();

            List<CustomerOrdersClass> customers = database.GetAll();

            customers.Reverse();   // newest order first
            dgOrders.ItemsSource = customers;

            txtCount.Text = customers.Count == 0
                ? "No orders saved yet."
                : "Total orders: " + customers.Count;
        }

        //view customer orders btn
        private void btnCustomerOrders_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Database error");
            }
        }

        //delete order by ticket number btn
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtTicket.Text.Trim(), out int ticketNumber))
            {
                MessageBox.Show("Please enter a valid ticket number (numbers only).");
                return;
            }

            MessageBoxResult confirm = MessageBox.Show(
                "Delete the order with ticket #" + ticketNumber + "?\nThis cannot be undone.",
                "Confirm delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (confirm != MessageBoxResult.Yes)
            {
                return;
            }

            try
            {
                var database = new DatabaseConnector();
                database.CreateDatabaseAndTable();

                int deleted = database.DeleteByTicket(ticketNumber);

                if (deleted == 0)
                {
                    MessageBox.Show("No order found with ticket #" + ticketNumber + ".");
                    return;
                }

                MessageBox.Show("Order with ticket #" + ticketNumber + " was deleted.");
                txtTicket.Clear();
                LoadOrders();   // refresh the table
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Database error");
            }
        }

        // Clicking a row puts its ticket number in the box, ready to delete
        private void dgOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgOrders.SelectedItem is CustomerOrdersClass selected)
            {
                txtTicket.Text = selected.Ticket.ToString();
            }
        }

        //back btn
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
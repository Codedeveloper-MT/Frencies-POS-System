using Frencies_POS_System.Controller;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Frencies_POS_System
{
    /// <summary>
    /// Manages the order confirmation process by displaying the total price,
    /// generating a ticket number, selecting a payment method, and opening
    /// the PlacingOrdersWindow to continue the order process.
    /// </summary>
    public partial class ConfirmOrderWindow : Window
    {
        double totalPrice = 0.0;
        Dictionary<string, double> orders = new Dictionary<string, double>();
        public ConfirmOrderWindow(double totalPrice, Dictionary<string, double> orders)
        {
            InitializeComponent();
            this.totalPrice = totalPrice;
            this.orders = orders;
            txtToPay.Text = "R" + totalPrice.ToString("F2");
        }

        private void btnPlaceOrder_Click(object sender, RoutedEventArgs e)
        {
            PlaceOrdersInterface tickNumber = new PlacingOrdersClass();
            int tickectNumber = tickNumber.genarateTickectNumber();

            string payMethod = "";

            if (rdoCash.IsChecked == true)
            {
                payMethod = "Cash at counter";
            }
            else
            {
                payMethod = "Card or QR on phone";
            }

            PlacingOrdersWindow placingOrder = new PlacingOrdersWindow(totalPrice, tickectNumber, payMethod, orders);
            placingOrder.Show();

            this.Close();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

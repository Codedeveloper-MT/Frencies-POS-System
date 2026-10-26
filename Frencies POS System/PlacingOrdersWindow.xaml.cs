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
    /// Manages the order placement window by displaying the order number,
    /// total price, and payment instructions. It also allows customers to
    /// send a receipt slip or start a new order.
    /// </summary>
    public partial class PlacingOrdersWindow : Window
    {
        double totalPrice = 0.0;
        int tickectNumber = 0;
        string payMethod = "";
        Dictionary<string, double> orders = new Dictionary<string, double>();
        public PlacingOrdersWindow(double totalPrice, int tickectNumber, string payMethod, Dictionary<string, double> orders)
        {
            InitializeComponent();
            CustomerOrdersClass customerOrders = new CustomerOrdersClass(totalPrice,tickectNumber,payMethod);

            this.totalPrice = customerOrders.GetTotalPrice();
            this.tickectNumber = customerOrders.GetTicketNumber();
            this.payMethod = customerOrders.GetPayMethod();
            this.orders = orders;

            txtOrderNumber.Text = "#" + tickectNumber;

            PlaceOrdersInterface payment = new PlacingOrdersClass();
            string PayMessage = payment.GetPaymentMethods(payMethod, totalPrice);

            txtPayMessage.Text = payMethod;
        }

        private void btnSendSlip_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

            SendReciptSlipWindow slips = new SendReciptSlipWindow(totalPrice,tickectNumber, payMethod,orders);
            slips.Show();
        }

        private void btnNewOrder_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnNewOrder_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

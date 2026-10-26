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
    /// Interaction logic for ViewOrdersWindow.xaml
    /// It formats the order details like a receipt and displays them in the text boxes.
    /// The Close button closes the current window, while the Confirm Order button opens 
    /// the ConfirmOrderWindow and passes the total price for payment confirmation.
    /// </summary>
    public partial class ViewOrdersWindow : Window
    {
        double totalPrice = 0.0;
        Dictionary<string, double> orders = new Dictionary<string, double>();
        public ViewOrdersWindow(double totalPrice, Dictionary<string, double> orders)
        {
            InitializeComponent();
            this.orders = orders;
            this.totalPrice = totalPrice;

            PlaceOrdersInterface texts = new PlacingOrdersClass();//dispay a recept
            string text = texts.CreateReceipt(orders);

            txtOrders.Text = text;
            txtOrderTotal.Text = "R" + totalPrice.ToString("F2");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnConfirmOrder_click(object sender, RoutedEventArgs e)
        {
            ConfirmOrderWindow confirmOrder = new ConfirmOrderWindow(totalPrice, orders);
            confirmOrder.Show();

            this.Close();
        }
    }
}
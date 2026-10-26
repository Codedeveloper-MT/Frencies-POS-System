using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Frencies_POS_System.Controller;

/// <summary>
/// displays the receipt details, including the total price, ticket number, and payment method.
/// It validates the customer's full name and email address before processing the request to send the receipt.
/// If the details are invalid or missing, an error message is displayed.
/// If the details are valid, a success message is shown.
/// The Back button closes the current window.
/// </summary>
namespace Frencies_POS_System
{
    public partial class SendReciptSlipWindow : Window
    {
        double totalPrice = 0.0;
        int tickectNumber = 0;
        string payMethod = "";
        Dictionary<string, double> orders = new Dictionary<string, double>();

        public SendReciptSlipWindow(double totalPrice, int tickectNumber, string payMethod, Dictionary<string, double> orders)
        {
            InitializeComponent();

            
            this.totalPrice = totalPrice;
            this.tickectNumber = tickectNumber;
            this.payMethod = payMethod;
            this.orders = orders;
           

            txtSlipTotal.Text = "R" + totalPrice.ToString("F2");
            txtSlipNumber.Text = "#" + tickectNumber.ToString();
            lblPayMethod.Content = payMethod;
        }

        private void btnSendToEmail_Click(object sender, RoutedEventArgs e)
        {
            string name = txtFullName.Text;
            string email = txtEmail.Text;

            UserValidationsClass customers = new UserValidationsClass();

            if (string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Please enter your full name and email address.");
                return;
            }
            else if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Please enter your full name.");
                return;
            }
            else if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Please enter your email address.");
                return;
            }

            if (!customers.isValidName(name))
            {
                MessageBox.Show("Please enter a valid name.");
                return;
            }

            if (!customers.isValidEmail(email))
            {
                MessageBox.Show("Please enter a valid email address.");
                return;
            }

            
            try
            {
                CustomerOrdersClass.Run(name, email, totalPrice, tickectNumber, payMethod, orders);   // saves to the database
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Database error");
                return;                             // stay on this window if saving failed
            }

            MessageBox.Show("Successfully sent to your email address! and to our kitchen");
            this.Close();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
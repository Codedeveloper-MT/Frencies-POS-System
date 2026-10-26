
using System;
using System.Text.RegularExpressions;
using System.Windows;

/// <summary>
/// Validates customer names and email addresses using conditional
/// statements and regular expressions. Inherits 
/// </summary>
/// 
namespace Frencies_POS_System.Controller
{
    internal class UserValidationsClass : PlacingOrdersClass 
    {
        public bool isValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            return Regex.IsMatch(email, pattern);
        }

        public bool isValidName(string name)
        {
            string msg = "The name must have at least 3 characters!";
            string msg1 = "The name must not contain numbers!";

            if (string.IsNullOrWhiteSpace(name) ||name.Trim().Length < 3)
            {
                MessageBox.Show(msg);
                return false;
            }
            else if (Regex.IsMatch(name, @"\d"))
            {
                MessageBox.Show(msg1);
                return false;
            }

            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Defines a contract for calculating the prices of individual menu items
/// and the total order price in the Frencies POS System.
/// </summary>
namespace Frencies_POS_System.Controller
{
    internal interface PlaceOrdersInterface
    {
        public double calculateItemPrice1(int quantity, double pizzaPrize);
        public double calculateItemPrice2(int quantity, double burgerPrize);
        public double calculateItemPrice3(int quantity, double mealPrize);
        public double calculateItemPrice4(int quantity, double friedPrize);
        public double calculateItemPrice5(int quantity, double kotaPrize);
        public double calculateItemPrice6(int quantity, double chickenPrize);
        
        public double calculateTotalPrice(double item1Prize, double item2Prize, double item3Prize, double item4Prize, double item5Prize, double item6Prize);
        public int genarateTickectNumber();

        public Dictionary<string, double> CreateOrder(double item1Prize,double item2Prize,double item3Prize,double item4Prize,double item5Prize,double item6Prize);

        public string CreateReceipt(Dictionary<string, double> orders);

        public string GetPaymentMethods(string payMethod, double totalPrice);
    }
}

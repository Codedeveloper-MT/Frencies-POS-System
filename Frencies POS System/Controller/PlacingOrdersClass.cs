using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Implements PlaceOrdersInterface to calculate the prices of individual
/// menu items based on quantity and calculate the total cost of the order.
/// </summary>
namespace Frencies_POS_System.Controller
{
    internal class PlacingOrdersClass : PlaceOrdersInterface
    {
       public double calculateItemPrice1(int quantity, double pizzaPrize)
        {
            double item1Prize = pizzaPrize*quantity; 
            return item1Prize;
        }
        public double calculateItemPrice2(int quantity, double burgerPrize)
        {
            double item1Prize = burgerPrize * quantity;
            return item1Prize;
        }
       public double calculateItemPrice3(int quantity, double mealPrize)
        {
            double item1Prize = mealPrize * quantity;
            return item1Prize;
        }
       public double calculateItemPrice4(int quantity, double friedPrize)
        {
            double item1Prize = friedPrize * quantity;
            return item1Prize;
        }
       public double calculateItemPrice5(int quantity, double kotaPrize)
        {
            double item1Prize = kotaPrize * quantity;
            return item1Prize;
        }
       public double calculateItemPrice6(int quantity, double chickenPrize)
        {
            double item1Prize = chickenPrize * quantity;
            return item1Prize;
        }
        public double calculateTotalPrice(double item1Prize, double item2Prize, double item3Prize, double item4Prize, double item5Prize, double item6Prize)
        {
           
            double totalPrice = 0.0;
            totalPrice = item1Prize + item2Prize + item3Prize + item4Prize + item5Prize + item6Prize;//300+0+0+0+0+0=300
            return totalPrice;
        }
        public int genarateTickectNumber()
        {
            int tickectNumber=0;
            Random rm = new Random();
            tickectNumber = rm.Next(3000) + 1;

            return tickectNumber;
        }

       public Dictionary<string, double> CreateOrder(double item1Prize, double item2Prize, double item3Prize, double item4Prize, double item5Prize, double item6Prize)
        {
            Dictionary<string, double> order =new Dictionary<string, double>();

            if (item1Prize > 0)
            {
                order.Add("Mega Pizza", item1Prize);
            }

            if (item2Prize > 0)
            {
                order.Add("Big Mac Burger", item2Prize);
            }

            if (item3Prize > 0)
            {
                order.Add("Family Meal", item3Prize);
            }

            if (item4Prize > 0)
            {
                order.Add("Family Fried", item4Prize);
            }

            if (item5Prize > 0)
            {
                order.Add("Kasi Kota", item5Prize);
            }

            if (item6Prize > 0)
            {
                order.Add("Mega Chicken", item6Prize);
            }

            return order;
        }
        public string CreateReceipt(Dictionary<string, double> orders)
        {
            string line = "--------------------------------------\n";

            string text = "         FRENCIS SUPERMARKET\n";
            text = text + "        Ga-Rankuwa, Gauteng\n";
            text = text + line;

            foreach (var item in orders)
            {
                text = text + item.Key.PadRight(26) + ("R" + item.Value.ToString("F2")).PadLeft(12) + "\n";
            }

            text = text + line;
            text = text + "      Thank you for your order!";

            return text;
        }

        public string GetPaymentMethods(string payMethod, double totalPrice)
        {
             string txtPayMessage = "";
            if (payMethod == "Cash at counter")
            {
                txtPayMessage = "Pay R" + totalPrice.ToString("F2") + " at the hot food counter when your number is called.";
            }
            else
            {
                txtPayMessage = "Pay R" + totalPrice.ToString("F2") + ". Your order is on its way to the kitchen.";
            }
            return txtPayMessage;
        }
    }
}

using Frencies_POS_System.Controller;
using Microsoft.VisualBasic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Frencies_POS_System
{
    /// <summary>
    /// Manages the main POS window, displays menu images, handles item selections
    /// and quantities, calculates item and total prices, and passes order details
    /// to the order viewing window.
    /// </summary> 
    public partial class MainWindow : Window
    {
        private const string ImageFolder = @"C:\Users\thandolwethu\Desktop\C#\Frencies POS System\Frencies POS System\image\";
        double item1Prize = 0.0;
        double item2Prize = 0.0;
        double item3Prize = 0.0;
        double item4Prize = 0.0;
        double item5Prize = 0.0;
        double item6Prize = 0.0;

        double totalPrice = 0.0;

        //item prizes
        double pizzaPrize = 150.00;
        double burgerPrize = 60.00;
        double mealPrize = 110.00;
        double friedPrize = 230.00;
        double kotaPrize = 38.00;
        double chickenPrize = 138.00;
        int quantity = 0;
        public MainWindow()
        {
            InitializeComponent();

            LoadImage(myImage1, "Image1.png");
            LoadImage(myImage2, "Image2.png");
            LoadImage(myImage3, "Image3.png");
            LoadImage(myImage4, "Image4.png");
            LoadImage(myImage5, "Image5.png");
            LoadImage(myImage6, "Image6.png");
        }

        private void LoadImage(Image target, string fileName)
        {
            try
            {
                BitmapImage bmp = new BitmapImage();

                bmp.BeginInit();
                bmp.UriSource = new Uri(ImageFolder + fileName, UriKind.Absolute);

                bmp.CacheOption = BitmapCacheOption.OnLoad;
                bmp.EndInit();

                target.Source = bmp;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not load " + fileName +
                    ":\n" + ex.Message);
            }
        }

        private void btnViewOrders_clicked(object sender, RoutedEventArgs e)
        {
            PlaceOrdersInterface items = new PlacingOrdersClass();
            
            
            
            Dictionary<string, double> order = new Dictionary<string, double>();
            order = items.CreateOrder(item1Prize, item2Prize, item3Prize, item4Prize, item5Prize, item6Prize);

            if (chkBigMacBurger.IsChecked == false && chkKasiKota.IsChecked == false && chkFamilyFried.IsChecked == false && chkFamilyMeal.IsChecked == false && chkMegaPizza.IsChecked == false && chkMegaChicken.IsChecked == false)
            {
                MessageBox.Show("Please select at least one item.");
                return;
            }

            totalPrice = items.calculateTotalPrice(item1Prize, item2Prize, item3Prize, item4Prize, item5Prize, item6Prize);

            
            CustomerOrdersClass customerOrders = new CustomerOrdersClass(order);
            Dictionary<string, double> orders = customerOrders.GetOrders();
       

            ViewOrdersWindow viewOrders = new ViewOrdersWindow(totalPrice, orders);
            viewOrders.Show();
        }
       

        private void chkMegaPizza_Checked(object sender, RoutedEventArgs e)
        {
            string input;
            int quantity;

            input = Microsoft.VisualBasic.Interaction.InputBox(//dialog box
                "Please enter how many Mega Pizzas you want:",
                "Mega Pizza Quantity",
                "1"
            );

            if (int.TryParse(input, out quantity))
            {
                if (quantity > 0)
                {
                    MessageBox.Show("You selected " + quantity + " Mega Pizza(s).");
                    PlaceOrdersInterface pizza = new PlacingOrdersClass();
                    item1Prize = pizza.calculateItemPrice1(quantity, pizzaPrize);
                }
                else
                {
                    MessageBox.Show("Please enter a quantity greater than 0.");
                    chkMegaPizza.IsChecked = false;
                }
            }
            else
            {
                if (input != "")
                {
                    MessageBox.Show("Please enter a valid number.");
                }

                chkMegaPizza.IsChecked = false;
            }
        }

        private void chkMegaPizza_Unchecked(object sender, RoutedEventArgs e)
        {
            item1Prize = 0.0;
        }

        private void chkMegaChicken_Checked(object sender, RoutedEventArgs e)
        {
            string input;
            int quantity;

            input = Microsoft.VisualBasic.Interaction.InputBox(//dialog box
                "Please enter how many Mega Chicken you want:",
                "Mega Chicken Quantity",
                "1"
            );

            if (int.TryParse(input, out quantity))
            {
                if (quantity > 0)
                {
                    MessageBox.Show("You selected " + quantity + " Mega Chicken(s).");
                    PlaceOrdersInterface chicken = new PlacingOrdersClass();
                    item6Prize = chicken.calculateItemPrice6(quantity, chickenPrize);
                }
                else
                {
                    MessageBox.Show("Please enter a quantity greater than 0.");
                    chkMegaChicken.IsChecked = false;
                }
            }
            else
            {
                if (input != "")
                {
                    MessageBox.Show("Please enter a valid number.");
                }

                chkMegaChicken.IsChecked = false;
            }
        }

        private void chkMegaChicken_Unchecked(object sender, RoutedEventArgs e)
        {
            item6Prize = 0.0;
        }

        private void chkKasiKota_Checked(object sender, RoutedEventArgs e)
        {
            string input;
            int quantity;

            input = Microsoft.VisualBasic.Interaction.InputBox(//dialog box
                "Please enter how many Kasi Kota you want:",
                "Kasi Kota Quantity",
                "1"
            );

            if (int.TryParse(input, out quantity))
            {
                if (quantity > 0)
                {
                    MessageBox.Show("You selected " + quantity + " Kasi Kota(s).");
                    PlaceOrdersInterface kota = new PlacingOrdersClass();
                    item5Prize = kota.calculateItemPrice5(quantity, kotaPrize);
                }
                else
                {
                    MessageBox.Show("Please enter a quantity greater than 0.");
                    chkKasiKota.IsChecked = false;
                }
            }
            else
            {
                if (input != "")
                {
                    MessageBox.Show("Please enter a valid number.");
                }

                chkKasiKota.IsChecked = false;
            }
        }

        private void chkKasiKota_Unchecked(object sender, RoutedEventArgs e)
        {
            item5Prize = 0.0;
        }

        private void chkMegaBurger_Checked(object sender, RoutedEventArgs e)
        {
            string input;
            int quantity;

            input = Microsoft.VisualBasic.Interaction.InputBox(//dialog box
                "Please enter how many Big Mac Burgers you want:",
                "Big Mac Burger Quantity",
                "1"
            );

            if (int.TryParse(input, out quantity))
            {
                if (quantity > 0)
                {
                    MessageBox.Show("You selected " + quantity + " Big Mac Burger(s).");
                    PlaceOrdersInterface burger = new PlacingOrdersClass();
                    item2Prize = burger.calculateItemPrice2(quantity, burgerPrize);
                }
                else
                {
                    MessageBox.Show("Please enter a quantity greater than 0.");
                    chkBigMacBurger.IsChecked = false;
                }
            }
            else
            {
                if (input != "")
                {
                    MessageBox.Show("Please enter a valid number.");
                }

                chkBigMacBurger.IsChecked = false;
            }
        }

        private void chkMegaBurger_Unchecked(object sender, RoutedEventArgs e)
        {
            item2Prize = 0.0;
        }

        private void chkFimailyMeal_Checked(object sender, RoutedEventArgs e)
        {
            string input;
            int quantity;

            input = Microsoft.VisualBasic.Interaction.InputBox(//dialog box
                "Please enter how many Fimaly Meal you want:",
                "Fimaly Meal Quantity",
                "1"
            );

            if (int.TryParse(input, out quantity))
            {
                if (quantity > 0)
                {
                    MessageBox.Show("You selected " + quantity + " Fimaly Meal(s).");
                    PlaceOrdersInterface meal = new PlacingOrdersClass();
                    item3Prize = meal.calculateItemPrice3(quantity, mealPrize);
                }
                else
                {
                    MessageBox.Show("Please enter a quantity greater than 0.");
                    chkFamilyMeal.IsChecked = false;
                }
            }
            else
            {
                if (input != "")
                {
                    MessageBox.Show("Please enter a valid number.");
                }

                chkFamilyMeal.IsChecked = false;
            }
        }

        private void chkFimailyMeal_Unchecked(object sender, RoutedEventArgs e)
        {
            item3Prize = 0.0;
        }

        private void chkFimalyFried_Checked(object sender, RoutedEventArgs e)
        {
            string input;
            int quantity;

            input = Microsoft.VisualBasic.Interaction.InputBox(//dialog box
                "Please enter how many Fimaly Fried you want:",
                "Fimaly Fried Quantity",
                "1"
            );

            if (int.TryParse(input, out quantity))
            {
                if (quantity > 0)
                {
                    MessageBox.Show("You selected " + quantity + " Fimaly Fried(s).");
                    PlaceOrdersInterface fried = new PlacingOrdersClass();
                    item4Prize = fried.calculateItemPrice4(quantity, friedPrize);
                }
                else
                {
                    MessageBox.Show("Please enter a quantity greater than 0.");
                    chkFamilyFried.IsChecked = false;
                }
            }
            else
            {
                if (input != "")
                {
                    MessageBox.Show("Please enter a valid number.");
                }

                chkFamilyFried.IsChecked = false;
            }
        }

        private void chkFimalyFried_Unchecked(object sender, RoutedEventArgs e)
        {
            item4Prize = 0.0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            chkBigMacBurger.IsChecked = false;
            chkKasiKota.IsChecked = false;
            chkFamilyFried.IsChecked = false;
            chkFamilyMeal.IsChecked = false;
            chkMegaPizza.IsChecked = false;
            chkMegaChicken.IsChecked = false;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            KitchenWindow viewOrders = new KitchenWindow();
            viewOrders.Show();
        }
    }
}
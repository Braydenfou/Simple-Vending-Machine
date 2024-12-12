using PIIIProject.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PIIIProject.Views
{
    /// <summary>
    /// Interaction logic for PaymentMethodWindow.xaml
    /// </summary>
    public partial class PaymentMethodWindow : Window
    {
        string filePath = "./quantities.txt";

        public ObservableCollection<Item> cartItems { get; } = new ObservableCollection<Item>(); //default property with only a getter, because from this point on we are just displaying the observable collection

        ShoppingCart cartCost = new ShoppingCart();



        public PaymentMethodWindow(ObservableCollection<Item> cartItems, ShoppingCart cartCost)
        {
            InitializeComponent();
            this.cartItems = cartItems; //passing the collection through the constructor
            this.cartCost = cartCost; //passing the total cost object giving us acces to the Total property, which we will use for credit card validation and pass to both the cash and credit receipt pages
        }

        private void NavigateToCredit_Btn(object sender, RoutedEventArgs e)
        {
            const double MIN_CREDIT_COST = 5.00;
            //this will nagivate the user to the credit receipt page
            //validate that the total cost of the cart is > 5
            //redirect to the credit receipt page

            if (cartCost.Total < MIN_CREDIT_COST)
            {
                MessageBox.Show("ERROR: To pay with credit there is a minimun cost of atleast $5, please add more to your cart.");
            }
            else
            {
                CreditReceiptWindow window = new CreditReceiptWindow(cartItems, cartCost); //same logic from main window, opening window
                window.Show();
                foreach (Item item in cartItems)
                {
                    Remove_Quantity_From_File(item);
                }

            }


        }


        private void Cash_Btn(object sender, RoutedEventArgs e)
        {
            double cashInputParsed; ///represents the selected bill parsed to double for use in validation

            //Validation based on the radio button that the user selects

            string selectedCashInput = ((RadioButton)sender).Tag.ToString();

            //parse the tag representing the bill to double to validate that the bill covers the cost of total bill
            double.TryParse(selectedCashInput, out cashInputParsed);

            if(cashInputParsed < cartCost.Total)
            {
                MessageBox.Show($"ERROR: Please choose a bill greater than the total cost. Total Cost: {cartCost.Total}");
            }
            else
            {
                //call the cash receipt window
                CashReceiptWindow window = new CashReceiptWindow(cartItems, cartCost, cashInputParsed); //same logic from main window, opening window
                window.Show();
                foreach (Item item in cartItems)
                {
                    Remove_Quantity_From_File(item);
                }
            }



        }
        public void Remove_Quantity_From_File(Item item)
        {
            //getting all lines from file that contains products and their quantities and putting them in an array
            string[] itemsNQuantities = File.ReadAllLines(filePath);
            const int QUANTITY_INDEX = 1;

            for (int i = 0; i < itemsNQuantities.Length; i++)
            {

                //putting the info of the line in an array
                string[] productInfo = itemsNQuantities[i].Split(',');
                //using trim() to remove white spaces around the word
                if (productInfo[0].Trim() == item.Name)
                {
                    // putting original quantity in a variable
                    int oldQuantity = int.Parse(productInfo[1]);
                    // removing one from the quantity and replacing the old value with the new one
                    int newQuantity = oldQuantity - item.Quantity;
                    productInfo[QUANTITY_INDEX] = newQuantity.ToString();
                    // changing the quantity number in the line
                    itemsNQuantities[i] = string.Join(",", productInfo);
                    //writing the modified line back to the file
                    File.WriteAllLines(filePath, itemsNQuantities);
                    break;
                }
            }
        }
    }
}

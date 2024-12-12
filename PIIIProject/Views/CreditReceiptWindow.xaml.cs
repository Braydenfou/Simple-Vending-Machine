using PIIIProject.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace PIIIProject.Views
{
    /// <summary>
    /// Interaction logic for CreditReceiptWindow.xaml
    /// </summary>
    public partial class CreditReceiptWindow : Window
    {
        //Initialize the observablecollection in this window to display final cart results
        public ObservableCollection<Item> cartItems { get; } = new ObservableCollection<Item>();

        ShoppingCart cartCost = new ShoppingCart();
        public CreditReceiptWindow(ObservableCollection<Item> cartItems, ShoppingCart cartCost)
        {
            InitializeComponent();
            DataContext = this; //simply put this allows the XAML ui to link to the actual code and logic behind event clicks etc.
            this.cartItems = cartItems;
            this.cartCost = cartCost;
            UpdateTotalCostDisplay(); //displaying the total cost from the shopping cart implementation
            UploadReceiptToFile();
        }

        private void UploadReceiptToFile()
        {
            const int MAX_NUMBER = 999;
            // generating random number to use as an order number
            Random randNumber = new Random();
            int orderNumber = randNumber.Next(MAX_NUMBER);
            string outputFile = "./transactions.txt";
            // using a stream writer to write to the transactions.txt file
            StreamWriter outputWriter = new StreamWriter(outputFile,true);
            outputWriter.WriteLine("------------------------------------");
            outputWriter.WriteLine($"Order Number: {orderNumber}");
            outputWriter.WriteLine("Item\tQuantity\tPrice\tTotal");
            foreach(Item item in cartItems)
            {
                outputWriter.WriteLine($"{item.Name}\t{item.Quantity}\t{item.Price}\t{item.ItemTotal}");
            }
            outputWriter.WriteLine($"Total Cost: {cartCost.Total:C}");
            // closing the stream writer after every transaction
            outputWriter.Close();
        }

        private void UpdateTotalCostDisplay()
        {
            totalCostBlock.Text = $"Total Cost: {cartCost.Total:C}";
        }



    }
}

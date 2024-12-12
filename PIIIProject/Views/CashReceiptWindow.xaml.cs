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
    /// Interaction logic for CashReceiptWindow.xaml
    /// </summary>
    public partial class CashReceiptWindow : Window
    {
        //Initialize the observablecollection in this window to display final cart results
        public ObservableCollection<Item> cartItems { get; set; } = new ObservableCollection<Item>();

        ShoppingCart cartCost = new ShoppingCart();

        public CashReceiptWindow(ObservableCollection<Item> cartItems, ShoppingCart cartCost, double cashInputParsed) //passing observable collection through constructor
        {
            InitializeComponent();
            DataContext = this;
            this.cartItems = cartItems;
            this.cartCost = cartCost;
            double billInputed = cashInputParsed;
            UpdateTotalCostDisplay(); //displaying final cost
            DisplayInputtedBillAmount(billInputed);//displaying amount received
            double change = DisplayChangeReturnedToUser(billInputed); //displaying change to user + a call to display billbreakdown method
            BillBreakdown(change);
            UploadReceiptToFile();
        }

        private void UploadReceiptToFile()
        {
            const int MAX_NUM = 999;
            Random randNumber = new Random();
            int orderNumber = randNumber.Next(MAX_NUM);
            string outputFile = "./transactions.txt";
            StreamWriter outputWriter = new StreamWriter(outputFile, true);
            outputWriter.WriteLine("------------------------------------");
            outputWriter.WriteLine($"Order Number: {orderNumber}");
            outputWriter.WriteLine("Item\tQuantity\tPrice\tTotal");
            foreach (Item item in cartItems)
            {
                outputWriter.WriteLine($"{item.Name}\t{item.Quantity}\t{item.Price}\t{item.ItemTotal}");
            }
            outputWriter.WriteLine($"Total Cost: {cartCost.Total:C}");
            outputWriter.WriteLine("------------------------------------");
            outputWriter.Close();
        }

        private void UpdateTotalCostDisplay()
        {
            totalCostBlock.Text = $"Total Cost: {cartCost.Total:C}"; //using C format for the dollar amount display
        }

        private void DisplayInputtedBillAmount(double amountReceived)
        {
            billInputted.Text = $"Amount received: {amountReceived:C}"; //using C format for the dollar amount display
        }

        private double DisplayChangeReturnedToUser(double amountReceived)
        {
            double change;
            change = amountReceived - cartCost.Total;

            changeReturnedToUser.Text = $"Change: {change:C}";

            return change;
        }

        private void BillBreakdown(double change)
        {
            //TYPES OF AMOUNTS
            BillBreakdown oneHundred = new BillBreakdown("One-Hundred dollar bill(s)", 100.0);
            BillBreakdown fifty = new BillBreakdown("Fifty dollar bill(s)", 50.0);
            BillBreakdown twenty = new BillBreakdown("Twenty dollar bill(s)", 20.0);
            BillBreakdown ten = new BillBreakdown("Ten dollar bill(s)", 10.0);
            BillBreakdown five = new BillBreakdown("Five dollar bill(s)", 5.0);
            BillBreakdown quarter = new BillBreakdown("Quarter(s)", 0.25);
            BillBreakdown dime = new BillBreakdown("Dime(s)", 0.10);
            BillBreakdown nickel = new BillBreakdown("Nickel(s)", 0.05);

            BillBreakdown[] amounts = { oneHundred, fifty, twenty, ten, five, quarter, dime, nickel };


            for(int i = 0; i < amounts.Length; i++)
            {
                //using int division we can see how many times the change is devisible by that bill amount
                int count = (int)(change / amounts[i].BillValue);

                //checking if there is remaining change
                if(count > 0)
                {
                    amounts[i].CountOfBill += count; //adding to the count of that specific bill
                    change -= amounts[i].BillValue * count; //finally subtracting the bills amount and repeating for each bill type
                }

            }

            //build a string that holds the bill breakdown to display later, its easier to display rather than looping through an array

            StringBuilder sb = new StringBuilder(); //creation of stringbuilder

            for(int i = 0;i < amounts.Length;i++)
            {
                if (amounts[i].CountOfBill > 0) //looping through bill types checking if they're count of bill is greater than 1, we only want to display bills that were divisible by the change 
                {
                    sb.AppendLine($"{amounts[i].CountOfBill}  {amounts[i].BillType}"); //append the count and bill type to our string which we'll later display
                }
            }

            //display the string builder

            DisplayBillBreakDown(sb);

        }

        private void DisplayBillBreakDown(StringBuilder sb) //this method is inputting the text for billBreakdown, its displaying the string we built above
        {
            billBreakdown.Text =@$"Bill Breakdown: 
{sb}";
        } 

    }
}

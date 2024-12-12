using PIIIProject.Models;
using PIIIProject.Views;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace PIIIProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //Brayden Fournier 6194481
        //Jayson Lartey
        //Simple Vending Machine! Enjoy
        //Project is completed

        //delcaring the Observable collection allowing the user to see whats in their car

        public ObservableCollection<Item> cartItems { get; set; } = new ObservableCollection<Item>(); //trying to create the observable collection for the cart list (So we can view whats in our cart) ESSENTIALLY THE CART
        // An observableCollection is a data collection built in with .NET, its behavior is similar to a list however it has features that notifies when items are added, updates etc. However we chose to take a manual approach to updating the UI and did not implement the INotifyProperty.
        //It was essentially used to store data and allowed us to bind data fromn within the collection to our WPF.
        //Passing with out FetchingObservableCollection method to each class with constructors as shown in demo

        ShoppingCart cartCost = new ShoppingCart(); //setting up implementation to later use to calculate the final cost, we'll pass this through the constuctor to display in receipts
        const double INITIAL_VALUE = 0.00;

        public MainWindow()
        {
            InitializeComponent();
            //to update the ListView with our observable collection we need to set our DataContext to allow for data binding.
            this.DataContext = this; //simply put this allows the XAML ui to link to the actual code and logic behind event clicks etc.
            cartCost.Total = INITIAL_VALUE;

            UpdateTotalCostDisplay();

            //Resetting the quantities every time vending machine starts (for testing purposes)
            //ResetQuantities();

        }

        private void ResetQuantities()
        {
            const int MAX_ITEM_NUMBER = 12;
            const int QUANTITY_INDEX = 1;
            string filePath = "./quantities.txt";
            string[] itemsNQuantities = File.ReadAllLines(filePath);

            for (int i = 0; i < itemsNQuantities.Length; i++)
            {
                //putting the info of the line in an array
                string[] productInfo = itemsNQuantities[i].Split(',');
                if (int.Parse(productInfo[QUANTITY_INDEX].Trim()) != MAX_ITEM_NUMBER)
                {
                    productInfo[QUANTITY_INDEX] = "12";
                    itemsNQuantities[i] = string.Join(",", productInfo);
                }
            }
            File.WriteAllLines(filePath, itemsNQuantities);
        }

        private void AddItem_Btn(object sender, RoutedEventArgs e)
        {
            //logic for adding an item to our observableCollection (the cart)

            //Firstly we're going to focus on distinguishing which product is being selected, with the use of "TAGS" inside of our xaml
            //We're then using the tag property and using ToString to set it to our string.
            string selectedItem = ((Button)sender).Tag.ToString(); //This is setting the selectedItem to the sender object which were casting as a button due to the fact that all 12 items events run this method (They're all buttons).

            Button clickedButton = (Button)sender;


            //I'm going to use a switch case to run specific code which will access our class depending on which item is selected

            //this switch case runs the add product to cart function for all items in the vending machine when they're clicked
            switch (selectedItem)
            {
                case "Skittles":
                    //code if skittles is selected and so on
                    AddProductToCart("Skittles", clickedButton);
                    break;

                case "Snickers":
                    AddProductToCart("Snickers", clickedButton);
                    break;

                case "Reeses":
                    AddProductToCart("Reeses", clickedButton);
                    break;

                case "Kitkats":
                    AddProductToCart("Kitkats", clickedButton);
                    break;

                case "Hersheys":
                    AddProductToCart("Hersheys", clickedButton);
                    break;

                case "Caramilk":
                    AddProductToCart("Caramilk", clickedButton);
                    break;

                case "Maynards":
                    AddProductToCart("Maynards", clickedButton);
                    break;

                case "Doritos":
                    AddProductToCart("Doritos", clickedButton);
                    break;

                case "Lays":
                    AddProductToCart("Lays", clickedButton);
                    break;

                case "Ruffles":
                    AddProductToCart("Ruffles", clickedButton);
                    break;

                case "MountainDew":
                    AddProductToCart("MountainDew", clickedButton);
                    break;

                case "Gatorade":
                    AddProductToCart("Gatorade", clickedButton);
                    break;
            }
        }
        private void AddProductToCart(string productName, Button clickledButton) //the parameter for quantity will have to be changed, and will have to take from the file provided. For now well hardcode for testing purposes
        {
            //Item item1;
            bool sameItem = false;


            //Im going to pass the filled ObservableCollection to the pricing class which will hold the FinalTotal cost



            foreach (Item itemObj in cartItems) //this loop searches through our observableCollection and if the item is already in the car we increase its quanttity by 1
            {
                if (itemObj.Name == productName)
                {
                    sameItem = true;
                    itemObj.Quantity++;
                    itemObj.ItemTotal = itemObj.Price * itemObj.Quantity;
                    UpdateListView();
                    //function that updates the observablecollection
                    break; //once added we break out
                }
            }



            if (!sameItem) //If the item isnt found itll trigger this statement and add the product into our list
            {

                double price = GetPrice(productName);
                int quantity = LoadQuantityFromFile(productName);
                int cartQuantity = quantity - (quantity - 1);

                //inventory validation, if theres no quantitty for the item aka the quantity == 0, display error message + disable button
                if (quantity <= 0)
                {
                    MessageBox.Show("ERROR: The item you selected is out of stock please choose another item");
                    clickledButton.IsEnabled = false;

                }
                else
                {
                    Item item = new Item(productName, price, cartQuantity); //fix hardcoding
                    item.ItemTotal = item.Price * item.Quantity;//total price is being initialized on new Items being added in the cart, which is then updated on each addition of that item
                    cartItems.Add(item);
                    UpdateListView();
                }
            }

            cartCost.CalculateTotal(cartItems);
            UpdateTotalCostDisplay();
        }

        // Function to get price from Vending Machine class
        private double GetPrice(string productName)
        {
            Item[] items = VendingMachine.AddAllItems();
            foreach (Item item in items)
            {
                if (item.Name == productName)
                {
                    double price = item.Price;
                    return price;
                }
            }
            return 0; // if item cannot be found, return 0 indicating an error
        }
        // This function sets quantities of all items through a file
        private int LoadQuantityFromFile(string productName)
        {
            const int QUANTITY_INDEX = 1;

            string filePath = "./quantities.txt";
            string[] itemsNQuantities = File.ReadAllLines(filePath);

            for (int i = 0; i < itemsNQuantities.Length; i++)
            {
                //putting the info of the line in an array
                string[] productInfo = itemsNQuantities[i].Split(',');
                //using trim() to remove white spaces around the word
                if (productInfo[0].Trim() == productName)
                {
                    int quantity = int.Parse(productInfo[QUANTITY_INDEX]);
                    return quantity;
                }
            }
            Console.WriteLine("Product not Found");
            return 0;

        }

        private void UpdateTotalCostDisplay()
        {
            totalCostBlock.Text = $"Total Cost: {cartCost.Total:C}";
        }



        private void UpdateListView() //my work around instead of using INotify this manually updates the ListView
        {
            //logic to update the objects within the cart. Specifially speaking updating properties such as the quantity inside of the cart.
            ListView tempListView = new ListView(); //creation of a new ListView object "templist", which will hold the values such as name,price, quantity etc
            tempListView.ItemsSource = cartListView.ItemsSource; //copying our current list view to a temp allowing us to essentially refresh/update the properties
            cartListView.ItemsSource = null; //we're setting all of the properties of the original list view to null and then reassigning them essentially manually refreshing it every time an item is added, therefore allowing us to update the quantity and the item total aka total cost
            cartListView.ItemsSource = tempListView.ItemsSource; //giving our current list view back its origianl values (aka UPDATED)
        }

        private void CheckOut_Btn(object sender, RoutedEventArgs e)
        {
            const int MIN = 0;

            //CheckOutbutton logic when the user clicks CheckOutBtn

            //Suggested logic, Loop through all items in the cart and calculate the total cost of items in cart

            //Firstly we'll add logic to verify that the cart has atleast 1 item inside

            if (cartItems.Count <= MIN)
            {
                MessageBox.Show("Your cart is empty, please ensure you have selected atleast 1 item");
            }
            else
            {
                //Link the payment method page
                PaymentMethodWindow window = new PaymentMethodWindow(cartItems, cartCost); //opening the window and using the constructor to pass the collection
                window.ShowDialog(); //will display the payment window and allow user to choose the payment method of choice
            }
        }



        private void Clear_Btn(object sender, RoutedEventArgs e)
        {
            //Same as checkout btn, logic for when the user clicks clear
            cartCost.ResetTotal(cartItems); //setting the total to 0 if the user clears the observable collection
            UpdateTotalCostDisplay(); //update the total display to = 0;
            cartItems.Clear(); //this clears the observable collection (aka our users cart)


        }
    }
}
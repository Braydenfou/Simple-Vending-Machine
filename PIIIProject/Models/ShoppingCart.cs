using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIIIProject.Models
{
    public class ShoppingCart
    {

        private double _total = 0.0; //this will hold the final cost


        public double Total
        {
            get { return _total; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Total is below zero huh?");
                _total = value;
            }
        }

        public void CalculateTotal(ObservableCollection<Item> cartItems)
        {
            Total = 0; //every method call it'll reset and recalculate the cost ensuring no added cost by accident
            foreach (Item itemObj in cartItems)
            {
                Total += itemObj.ItemTotal;
            }

        }

        public void ResetTotal(ObservableCollection<Item> cartItems)
        {
            Total = 0.0;
        }
    }
}

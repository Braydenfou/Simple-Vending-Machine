using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIIIProject.Models
{
    public class Item
    {
        #region Backing Fields
        private string _name;
        private double _price;
        private int _quantity;
        private double _itemTotal; //using this so when we have multiple items itll represent the total cost of all duplicates of that item
        #endregion
        #region Properties
        public string Name
        {
            get { return _name; }
            set
            {

                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("name");
                _name = value;
            }
        }
        public double Price
        {
            get { return _price; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("price");
                _price = value;
            }
        }
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Quantity");
                _quantity = value;
            }
        }
        public double ItemTotal
        {
            get { return _itemTotal; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Item Total");
                _itemTotal = value;
            }
        }

        #endregion
        #region Constructors
        public Item()
        {
            Name = "";
            Price = 0.0;
            Quantity = 0;
        }
        public Item(string name, double price, int quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }
        public Item(string name, double price)
        {
            Name = name;
            Price = price;
            Quantity = 0;
        }
        #endregion
    }
}

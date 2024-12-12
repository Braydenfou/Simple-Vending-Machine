using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PIIIProject.Models
{
    internal class VendingMachine
    {
        private const int ITEM_COUNT = 12;
        protected const double LOW_PRICE = 2.00;
        protected const double MED_PRICE = 2.30;
        protected const double HIGH_PRICE = 3.00;
        public enum Product
        {
            Skittles,
            Snickers,
            Reeses,
            Kitkats,
            Hersheys,
            Caramilk,
            Maynards,
            Doritos,
            Lays,
            Ruffles,
            MountainDew,
            Gatorade,
        }
        public static Item[] Items = new Item[ITEM_COUNT];
        public static Item[] AddAllItems()
        {
            //string file = "../../Quantities.txt"; //proper relative link, included in the github
            for (int i = 0; i < Items.Length; i++)
            {
                Items[i] = new Item(((Product)i).ToString(),CalculatePrice(i));
                //if (i <= 6)
                //{
                //    Items[i].Price = LOW_PRICE;
                //}
                //else if (i <= 9)
                //{
                //    Items[i].Price = MED_PRICE;
                //}
                //else
                //{
                //    Items[i].Price = HIGH_PRICE;
                //}
            }
            return Items;
        }

        private static double CalculatePrice(int i)
        {
            if (i <= 6)
            {
                return LOW_PRICE;
            }
            else if (i <= 9)
            {
                return MED_PRICE;
            }
            else
            {
                return HIGH_PRICE;
            }
        }

        public static int[] GetQuantities(string file)
        {
            string[] fileItems = File.ReadAllLines(file);
            if (fileItems.Length != ITEM_COUNT)
                throw new ArgumentOutOfRangeException("items");
            int[] quantities = new int[fileItems.Length];
            for (int i = 0; i < quantities.Length; i++)
            {
                quantities[i] = int.Parse(fileItems[i]);
            }
            return quantities;
        }
        public void PickProduct(string name)
        {
            try
            {

                for (int i = 0; i < Items.Length; i++)
                {
                    if (Items[i].Name == name)
                        Items[i].Quantity--;
                }
            }
            catch (Exception)
            {

                Console.WriteLine("Item could not be found");
            }

        }
    }
}

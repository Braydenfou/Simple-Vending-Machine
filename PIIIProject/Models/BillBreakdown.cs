using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIIIProject.Models
{
    internal class BillBreakdown
    {
        string _billType;
        double _billValue;
        int _countOfBill;

        public BillBreakdown(string billType, double billValue)
        { 
            BillType = billType;
            BillValue = billValue;

        }

        public string BillType
        {
            get { return _billType; }
            set { _billType = value; }
        }

        public double BillValue
        {
            get { return _billValue; }
            set
            {
                if(value  < 0)
                {
                    throw new ArgumentOutOfRangeException("ERROR Value cannot be negative");
                }
                _billValue = value;
            }
        }

        public int CountOfBill
        {
            get { return _countOfBill; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("ERROR Value cannot be negative");
                }
                _countOfBill = value;
            }
        }
    }
}

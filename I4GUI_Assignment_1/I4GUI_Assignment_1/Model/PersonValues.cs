using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using I4GUI_Assignment_1.Annotations;

namespace I4GUI_Assignment_1
{
    public class PersonValue 
    {
        private string date_;
        private double value_;
        
        public  PersonValue()
        { }

        public PersonValue(double value)
        {
            date_ = getCurrentDate();
            Console.WriteLine(date_);
            value_ = value;
        }

        public string Date
        {
            get
            {
                return date_;
            }
            set 
            {
                if (String.IsNullOrEmpty(value))
                {
                    date_ = getCurrentDate();
                }
                else
                {
                    date_ = value;
                }
            }
        }

        public double Value
        {
            get
            {
                return value_;
            }
            set
            {
                value_ = value;
            }
        }

        private string getCurrentDate()
        {
            return DateTime.Now.ToShortDateString();
        }

    }
}

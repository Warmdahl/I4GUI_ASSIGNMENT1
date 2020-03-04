﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I4GUI_Assignment_1
{
    class PersonValue
    {
        private string date_;
        private double value_;

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
                date_ = value;
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
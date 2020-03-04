using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I4GUI_Assignment_1.Model
{
    class Person
    {
        private List<double> values_ = new List<double>();
        private string name_;
        private double currentDebt_;

        public Person(string name, double startValue)
        {
            Name = name;
            Values.Add(startValue);
        }

        public List<double> Values
        {
            get
            {
                return values_;
            }
            set
            {
                values_ = value;
            }
        }

        public string Name
        {
            get
            {
                return name_;
            }
            set 
            {
                name_ = value;
            }
        }

        public double CurrentDebt
        {
            get
            {
                return currentDebt_;
            }
            set
            {
                currentDebt_ = value;
            }
        }
    }
}

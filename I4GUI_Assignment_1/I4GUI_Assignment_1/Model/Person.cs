using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I4GUI_Assignment_1
{
    class Person
    {
        private List<PersonValue> values_ = new List<PersonValue>();
        private string name_;
        private double currentDebt_;


        public Person(string name, double startValue)
        {
            Name = name;
            Values.Add(new PersonValue(startValue));
        }



        public List<PersonValue> Values
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
                currentDebt_ = 0;
                foreach (PersonValue v in values_)
                {
                    currentDebt_ += v.Value;
                }
                return currentDebt_;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using I4GUI_Assignment_1.Annotations;

namespace I4GUI_Assignment_1
{
    class Person : INotifyPropertyChanged
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
                OnPropertyChanged();
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

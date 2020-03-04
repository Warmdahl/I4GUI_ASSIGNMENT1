using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace I4GUI_Assignment_1
{
    class MVVM : INotifyPropertyChanged
    {
        // Oberservable collection for Persons
        private ObservableCollection<Person> persons_;

        private Person currentPerson_;

        public MVVM()
        {
            persons_ = new ObservableCollection<Person>();
            persons_.Add(new Person("Alexander", 0));
            persons_.Add(new Person("Emil", -1000));
            persons_.Add(new Person("Nicolai", -20));
            persons_.Add(new Person("Maja", 100));
        }


        #region Properties

        public ObservableCollection<Person> Persons
        {
            get
            {
                return persons_;
            }
            set
            {
                persons_ = value;
                NotifyPropertyChanged();
            }
        }

        public Person CurrentPerson
        {
            get
            {
                return currentPerson_;
            }
            set
            {
                currentPerson_ = value;
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}

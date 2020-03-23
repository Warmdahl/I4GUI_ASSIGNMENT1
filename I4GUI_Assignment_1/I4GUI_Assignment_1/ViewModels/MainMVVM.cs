using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Prism.Commands;

namespace I4GUI_Assignment_1
{
    class MainMVVM : INotifyPropertyChanged
    {
        // Oberservable collection for Persons
        private ObservableCollection<Person> persons_;

        private Person _currentPerson;

        public MainMVVM()
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
                return _currentPerson;
            }
            set
            {
                _currentPerson = value;
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Commands

        private ICommand addNew_;

        public ICommand AddNew
        {
            get
            {
                return addNew_ ?? (addNew_ = new DelegateCommand(() =>
                {
                    var newPerson = new Person("temp", 0);
                    var addPersonView = new AddPersonView();

                    var addPersonMVVM = new AddPersonMVVM();//subwindodViewModel = new SubwindowMVVM();
                    addPersonView.DataContext = addPersonMVVM;

                    if (addPersonView.ShowDialog() == true)
                    {
                        var name = addPersonMVVM.Name;
                        var startvalue = addPersonMVVM.StartValue;

                        Persons.Add(new Person(name, startvalue));
                    }

                }));
            }

        }


        private ICommand addNewV_;

        public ICommand AddNewV
        {
            get
            {
                return addNewV_ ?? (addNewV_ = new DelegateCommand(() =>
                {
                    var newPersonValue = new PersonValue(0);
                    var addValueView = new AddValueView();//subView2 = new Subwindow2();

                    var addValueMVVM = new AddValueMVVM();
                    addValueView.DataContext = addValueMVVM;

                    if (addValueView.ShowDialog() == true)
                    {
                        newPersonValue.Date = addValueMVVM.Date;
                        newPersonValue.Value = addValueMVVM.Value;

                        CurrentPerson.Values.Add(newPersonValue);

                        CurrentPerson.CurrentDebt += newPersonValue.Value;
                    }
                }));
            }
        }

        #endregion
    }
}

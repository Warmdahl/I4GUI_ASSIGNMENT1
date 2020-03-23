using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Serialization;
using Microsoft.Win32;
using Prism.Commands;

namespace I4GUI_Assignment_1
{
    class MainMVVM : INotifyPropertyChanged
    {
        // Oberservable collection for Persons
        private ObservableCollection<Person> persons_;
        private Person _currentPerson;
        private string filename_ = "";

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

                    var addPersonMVVM = new AddPersonMVVM();
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
                    var addValueView = new AddValueView();

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

        // C O M M A N D S     F O R     D A T A - P E R S I S T I N G //

        // N E W     F I L E
        private ICommand newFileCommand_;
        public ICommand NewFileCommand
        {
            get
            {
                return newFileCommand_ ?? (newFileCommand_ = new DelegateCommand(() =>
                {
                    MessageBoxResult msgResult = MessageBox.Show("Any unsaved changes will be lost, do you wish to continue?", "Warning!", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.No);
                    if (msgResult == MessageBoxResult.Yes)
                    {
                        persons_.Clear();
                        filename_ = "";
                    }
                }));
            }
        }

        
        // E X I T
        private ICommand exit_;
        public ICommand ExitCommand
        {
            get
            {
                return exit_ ?? (exit_ = new DelegateCommand(ExitCommandHandler));  // DelegateCommand initiation first time this is called it will create a bew delegate command since it's null and second time it will return the value.
            }
        }
        void ExitCommandHandler()
        {
            System.Windows.Application.Current.Shutdown();
        }

        // O P E N
        private ICommand open_;
        public ICommand OpenCommand
        {
            get
            {
                return open_ = (open_ = new DelegateCommand(OpenCommand_Execute));
            }
        }
        public void OpenCommand_Execute()
        {
            if (filename_ != "")
            {
                MessageBoxResult msgResult = MessageBox.Show("Any unsaved changes will be lost, do you wish to continue?", "Warning!", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.No);
                if (msgResult == MessageBoxResult.Yes)
                {
                    openCommand_functionality();
                }
            }
            else
            {
                openCommand_functionality();
            }
        }
        private void openCommand_functionality()
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            var tempAgents = new ObservableCollection<Person>();

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    filename_ = openFileDialog.FileName;

                    if (filename_.Contains(".dat"))
                    {
                        //***************** Open as javaScript ***********************///
                        // Source: https://www.youtube.com/watch?v=pqFsFAyiL9I

                        if (File.Exists(filename_))
                        {
                            tempAgents = new JavaScriptSerializer().Deserialize<ObservableCollection<Person>>(File.ReadAllText(filename_));
                        }
                        //******************************************************///
                    }
                    else
                    {
                        //***************** Open as xml ***********************///
                        // Source: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/serialization/how-to-read-object-data-from-an-xml-file

                        // Reads serialized block:
                        XmlSerializer reader = new XmlSerializer(typeof(ObservableCollection<Person>));
                        TextReader file = new StreamReader(filename_);
                        tempAgents = (ObservableCollection<Person>)reader.Deserialize(file);
                        file.Close();

                        //******************************************************///
                    }



                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Unable to open file", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                Persons = tempAgents;
            }
        }


        // S A V E
        private ICommand save_;
        public ICommand SaveCommand
        {
            get
            {
                return save_ ?? (save_ = new DelegateCommand(SaveCommand_Execute, SaveCommand_CanExecute).ObservesProperty(() => Persons.Count));
            }
        }

        public void SaveCommand_Execute()
        {
            if (filename_.Contains(".dat"))
            {
                //***************** Saving as javaScript ***********************///
                // Source: https://www.youtube.com/watch?v=pqFsFAyiL9I
                File.WriteAllText(filename_, new JavaScriptSerializer().Serialize(persons_));

                //******************************************************///
            }
            else
            {
                //***************** Saving as xml ***********************///
                // Source: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/serialization/how-to-write-object-data-to-an-xml-file

                // Create an instance of the XmlSerializer class and specify the type of object to serialize.
                XmlSerializer writer = new XmlSerializer(typeof(ObservableCollection<Person>));
                TextWriter file = new StreamWriter(filename_);

                // Serialize all the agents.
                writer.Serialize(file, persons_);
                file.Close();

                //******************************************************///
            }

        }
        public bool SaveCommand_CanExecute()
        {
            if ((filename_ != "") && (Persons.Count > 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        // S A V E    A S
        // Source: https://www.wpf-tutorial.com/dialogs/the-savefiledialog/
        private ICommand saveAsCommand_;
        public ICommand SaveAsCommand
        {
            get
            {
                return saveAsCommand_ ?? (saveAsCommand_ = new DelegateCommand<string>(SaveAsCommand_Execute));
            }
        }
        public void SaveAsCommand_Execute(string argFilename)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XAML file(*.xaml)|*.xaml|" +
                "Text file(*.txt)|*.txt|" +
                "C# file(*.cs)|*.cs|" +
                "dat file(*.dat)|*.dat";

            saveFileDialog.FileName = filename_;

            if (saveFileDialog.ShowDialog() == true)
            {
                filename_ = saveFileDialog.FileName;
                SaveCommand_Execute();
            }
        }


        #endregion
    }
}

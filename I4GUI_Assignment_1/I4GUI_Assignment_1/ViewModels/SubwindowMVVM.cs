using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace I4GUI_Assignment_1
{
    class SubwindowMVVM : INotifyPropertyChanged
    {
        private string name_;
        private double startvalue_;

        public string Name
        {
            get
            {
                return name_;
            }
            set
            {
                name_ = value;
                NotifyPropertyChanged();
            }
        }

        public double StartValue
        {
            get
            {
                return startvalue_;
            }
            set
            {
                startvalue_ = value;
                NotifyPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}

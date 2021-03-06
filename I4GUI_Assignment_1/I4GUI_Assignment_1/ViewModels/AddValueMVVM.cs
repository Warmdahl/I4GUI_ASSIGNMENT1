﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace I4GUI_Assignment_1
{
    class AddValueMVVM : INotifyPropertyChanged
    {
        private string date_;
        private double value_;

        public string Date
        {
            get
            {
                return date_;
            }
            set
            {
                date_ = value;
                NotifyPropertyChanged();
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
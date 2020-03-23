using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace I4GUI_Assignment_1
{
    /// <summary>
    /// Interaction logic for Subwindow2.xaml
    /// </summary>
    public partial class AddValueView : Window
    {
        public AddValueView()
        {
            InitializeComponent();
        }

        private void CancelBtn_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void SaveBtn_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
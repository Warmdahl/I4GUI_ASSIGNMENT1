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
    /// Interaction logic for Subwindow1.xaml
    /// </summary>
    public partial class AddPersonView : Window
    {
        public AddPersonView()
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
            var viewModel = DataContext as AddPersonMVVM;// SubwindowMVVM;
            if (viewModel.IsValid)
            {
                DialogResult = true;
            }
            else 
            {
                MessageBox.Show("You have empty fields, write Name and start value to continue!", "Error empty fields", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

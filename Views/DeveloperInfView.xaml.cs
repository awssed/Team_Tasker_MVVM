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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TeamTasker.Views
{
    /// <summary>
    /// Логика взаимодействия для DeveloperInfView.xaml
    /// </summary>
    public partial class DeveloperInfView : UserControl
    {
        public DeveloperInfView()
        {
            InitializeComponent();
        
        }
        private void PropertyChanged(object sender,RoutedEventArgs e)
        {
            if(SaveButton!=null)
                SaveButton.IsEnabled = true;
            if(CancelButton!=null)
                CancelButton.IsEnabled = true;
        }
        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            if (SaveButton != null)
                SaveButton.IsEnabled = false;
            if (CancelButton != null)
                CancelButton.IsEnabled = false;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

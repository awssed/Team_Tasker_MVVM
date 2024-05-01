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

namespace TeamTasker.Views
{
    /// <summary>
    /// Логика взаимодействия для TaskView.xaml
    /// </summary>
    public partial class TaskView : Window
    {
        public TaskView()
        {
            InitializeComponent();
            SaveBut.IsEnabled = false;
        }
        public void PropertyChanged(object sender,RoutedEventArgs e)
        {
            SaveBut.IsEnabled = true;
        }
        public void ClickSave(object sender, RoutedEventArgs e)
        {
            SaveBut.IsEnabled = false;
        }
    }
}

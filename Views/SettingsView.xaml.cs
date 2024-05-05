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
    /// Логика взаимодействия для SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
        }
        private void ClickLightTheme(object sender, RoutedEventArgs e)
        {
            this.Resources["ControlLightWidth"] = 50.0;
            this.Resources["ControlLightHeight"] = 50.0;
            this.Resources["ControlDarkHeight"] = 40.0;
            this.Resources["ControlDarkWidth"] = 40.0;
            ResourceDictionary lightTheme = new ResourceDictionary();
            //lightTheme.Source = new Uri("Themes/LightTheme.xaml", UriKind.Relative);
            //Application.Current.Resources.Clear();
            //Application.Current.Resources.MergedDictionaries.Add(lightTheme);
        }
        private void ClickDarkTheme(object sender, RoutedEventArgs e)
        {
            this.Resources["ControlLightWidth"] = 40.0;
            this.Resources["ControlLightHeight"] = 40.0;
            this.Resources["ControlDarkHeight"] = 50.0;
            this.Resources["ControlDarkWidth"] = 50.0;
            ResourceDictionary lightTheme = new ResourceDictionary();
            //lightTheme.Source = new Uri("Themes/DarkTheme.xaml", UriKind.Relative);
            Application.Current.Resources.Clear();
            //Application.Current.Resources.MergedDictionaries.Add(lightTheme);
        }
    }
}

using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TeamTasker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CultureInfo currLang;
        public MainWindow()
        {
            InitializeComponent();
            //докализация

            currLang = App.Language;


            //Заполняем меню смены языка:
            ResourceDictionary lightTheme = new ResourceDictionary();
            lightTheme.Source = new Uri("Themes/LightTheme.xaml", UriKind.Relative);
            //Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(lightTheme);
        }
    }
}
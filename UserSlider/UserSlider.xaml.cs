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

namespace TeamTasker.UserSlider
{
    /// <summary>
    /// Логика взаимодействия для UserSlider.xaml
    /// </summary>
    public partial class UserSlider : UserControl
    {
        public event RoutedEventHandler ValueChanged
        {
            add { AddHandler(ValueChangedEvent, value); }
            remove { RemoveHandler(ValueChangedEvent, value); }
        }

        public static readonly RoutedEvent ValueChangedEvent =
            EventManager.RegisterRoutedEvent(
                "ValueChanged",
                RoutingStrategy.Tunnel,
                typeof(RoutedEventHandler),
                typeof(UserSlider));
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
                "Value",
                typeof(int),
                typeof(UserSlider),
                new FrameworkPropertyMetadata(0,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    new PropertyChangedCallback(OnValueChanged),
                    new CoerceValueCallback(CorrectValue)),
                    new ValidateValueCallback(ValidateSliderValue));
        public UserSlider()
        {
            InitializeComponent();

        }


        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        private static bool ValidateSliderValue(object value)
        {
            int currentValue = (int)value;
            if (currentValue >= 0 && currentValue <= 100)
                return true;
            return false;
        }
        private static object CorrectValue(DependencyObject d, object baseValue)
        {
            int currentValue = (int)baseValue;
            if (currentValue > 100)  // если больше 100, возвращаем 100
                return 100;
            if (currentValue < 0)
                return 0;

            return currentValue; // иначе возвращаем текущее значение
        }
        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UserSlider slider = (UserSlider)d;
            RoutedEventArgs args = new RoutedEventArgs(ValueChangedEvent);
            slider.RaiseEvent(args);
        }
        private void SetNewValue(object sender, RoutedEventArgs e)
        {
            Value = (int)slider.Value;
        }
    }
}

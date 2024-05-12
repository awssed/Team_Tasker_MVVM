using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Логика взаимодействия для RegView.xaml
    /// </summary>
    public partial class RegView : UserControl
    {
        public RegView()
        {
            InitializeComponent();
        }
        private void PasswordChange(object sender,RoutedEventArgs e)
        {
            if (OrigPassword.Password == RepPassword.Password)
            {
                RegButton.IsEnabled = true;
                RepPassword.Background = new SolidColorBrush(Colors.Transparent);

            }
            else
            {
                string hexColor = "#7FFF0000"; // Шестнадцатеричное представление цвета (ARGB)
                Color color = (Color)ColorConverter.ConvertFromString(hexColor);
                Brush brush = new SolidColorBrush(color);
                RepPassword.Background = brush;
                RegButton.IsEnabled = false;
            }
        }
        private void SetValidationError(DependencyObject element, string errorMessage)
        {
            BindingExpression bindingExpression = BindingOperations.GetBindingExpression(element, TextBox.TextProperty);
            if (bindingExpression != null)
            {
                Validation.ClearInvalid(bindingExpression);
                ValidationError validationError = new ValidationError(new CustomValidationRule(), new ValidationResult(false, errorMessage));
                Validation.MarkInvalid(bindingExpression, validationError);
            }
        }

        private void ClearValidationError(DependencyObject element)
        {
            BindingExpression bindingExpression = BindingOperations.GetBindingExpression(element, TextBox.TextProperty);
            if (bindingExpression != null)
            {
                Validation.ClearInvalid(bindingExpression);
            }
        }

        public class CustomValidationRule : ValidationRule
        {
            public override ValidationResult Validate(object value, CultureInfo cultureInfo)
            {
                return ValidationResult.ValidResult;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace TeamTasker.Models
{
    public class ExpiredTaskBorderStyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isCompleted && isCompleted)
            {
                // Если задача завершена, не добавляем стиль границы
                return null;
            }

            if (value is DateTime endTime && endTime < DateTime.Now)
            {
                // Если задача просрочена, применяем стиль границы
                return Application.Current.FindResource("ExpiredTaskBorderStyle");
            }

            // Если задача не завершена и не просрочена, не добавляем стиль границы
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace IndoorPlaygroundSafetyCheck.Converters
{
    public class AnswerToYesNoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string answer = value as string;
            string buttonState = parameter as string;
            return answer == buttonState;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return parameter as string;
            }
            return null; // Or String.Empty or other default value as needed
        }
    }
}

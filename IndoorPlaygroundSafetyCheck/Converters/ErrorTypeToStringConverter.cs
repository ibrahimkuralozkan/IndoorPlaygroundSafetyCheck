using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Globalization;
using System.Windows.Data;
using IndoorPlaygroundSafetyCheck.Enums;

namespace IndoorPlaygroundSafetyCheck.Converters
{
    public class ErrorTypeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case ErrorType.ReadyToUse:
                    return "Ready To Use";
                case ErrorType.Caution:
                    return "Caution";
                case ErrorType.Warning:
                    return "Warning";
                default:
                    return "Unknown";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Optional: Implement this method if you need two-way binding
            throw new NotImplementedException();
        }
    }
}

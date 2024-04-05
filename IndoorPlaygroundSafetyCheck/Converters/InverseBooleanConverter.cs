using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Globalization;
using System.Windows.Data;

namespace IndoorPlaygroundSafetyCheck.Converters
{
    internal class InverseBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Ensure the value is a boolean
            if (value is bool boolValue)
            {
                // Return the inverse
                return !boolValue;
            }
            throw new InvalidOperationException("The value must be a boolean");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // ConvertBack does the same as Convert since it's just inverting a boolean
            if (value is bool boolValue)
            {
                return !boolValue;
            }
            throw new InvalidOperationException("The value must be a boolean");
        }
    }
}

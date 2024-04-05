using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace IndoorPlaygroundSafetyCheck.Converters
{
    public class ErrorTypeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int errorType)
            {
                switch (errorType)
                {
                    case 0:
                        return Brushes.LightGreen; // Ready To Use
                    case 1:
                        return Brushes.Yellow; // Warning
                    case 2:
                        return Brushes.Red; // Error
                    default:
                        return Brushes.LightGray; // Default or unknown
                }
            }
            return Brushes.LightGray; // Default return value if conversion fails or null
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

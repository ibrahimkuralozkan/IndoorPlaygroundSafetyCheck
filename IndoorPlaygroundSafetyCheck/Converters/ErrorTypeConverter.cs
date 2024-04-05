using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace IndoorPlaygroundSafetyCheck.Converters
{
    public class ErrorTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int errorType)
            {
                switch (errorType)
                {
                    case 0:
                        return "Ready To Use";
                    case 1:
                        return "Warning"; // Assuming you want to display something for 1
                    case 2:
                        return "Error"; // Assuming you want to display something for 2
                    // Add more cases as needed
                    default:
                        return "Unknown";
                }
            }

            return "Not Checked"; // Default return value if conversion fails
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
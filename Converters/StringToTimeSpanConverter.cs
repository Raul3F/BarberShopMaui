using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopMaui.Converters
{
    class StringToTimeSpanConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            if (value is string stringValue)
            {
                // Try parsing the string to TimeSpan
                if (TimeSpan.TryParse(stringValue, out TimeSpan result))
                    return result;
                else
                    return null; // Handle invalid string format
            }
            else if (value is TimeSpan timeSpanValue)
            {
                // Convert TimeSpan to string if needed
                return timeSpanValue.ToString();
            }

            return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            if (value is TimeSpan timeSpanValue)
            {
                // Convert TimeSpan to string
                return timeSpanValue.ToString();
            }

            return null;
        }
    }
}

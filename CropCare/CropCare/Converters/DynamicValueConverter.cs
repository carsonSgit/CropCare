using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropCare.Converters
{
    public class DynamicValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";

            string format = parameter as string;
            if (double.TryParse(value.ToString(), out double result))
            {
                return result.ToString(format);
            }

            return value;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                return (string)value == "ON";
            }
            return value;
        }
    }
}

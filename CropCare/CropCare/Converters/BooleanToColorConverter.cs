using System.Globalization;

namespace CropCare.Converters
{
    class BooleanToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is double)
                value = (double)value == 1 ? true : (double)value == -1 ? false : throw new ArgumentException("Invalid value");

            if ((bool)value)
                return Color.FromArgb("#42A765");// Healthy
            else if (!(bool)value)
                return Color.FromArgb("#EA5757");// Unhealthy
            else
                return Color.FromArgb("#A9A9A9");// Unknown
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

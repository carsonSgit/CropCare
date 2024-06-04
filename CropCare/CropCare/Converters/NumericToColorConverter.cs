using CropCare.Models;
using System.Globalization;

namespace CropCare.Converters
{
    public class NumericToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is not HealthyRange range)
                throw new ArgumentException("Parameter must be of type HealthyRange");

            Color color;
            if (double.TryParse(value.ToString(), out double numericValue))
            {
                if (numericValue >= range.LowerHealthyLimit && numericValue <= range.UpperHealthyLimit)
                    color = Color.FromArgb("#42A765");// Healthy
                else if ((numericValue >= range.LowerCautionLimit && numericValue < range.LowerHealthyLimit) || (numericValue > range.UpperHealthyLimit && numericValue <= range.UpperCautionLimit))
                    color = Color.FromArgb("#E08551");// Caution
                else
                    color = Color.FromArgb("#EA5757");// Unhealthy
            }
            else
                color = Color.FromArgb("#A9A9A9");// Unknown

            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

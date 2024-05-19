using CropCare.Models;
using System.Globalization;

namespace CropCare.Converters
{
    public class NumericToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is not HealthyRange range)
                throw new ArgumentException("Parameter must be of type HealthyRange");

            string healthStatus;
            if (double.TryParse(value.ToString(), out double numericValue))
            {
                if (numericValue >= range.LowerHealthyLimit && numericValue <= range.UpperHealthyLimit)
                    healthStatus = "Healthy";// Healthy
                else if ((numericValue >= range.LowerCautionLimit && numericValue < range.LowerHealthyLimit) || (numericValue > range.UpperHealthyLimit && numericValue <= range.UpperCautionLimit))
                    healthStatus = "Caution";// Caution
                else
                    healthStatus = "Critical";// Unhealthy
            }
            else
                healthStatus = "Unkown";// Unkown

            return healthStatus;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
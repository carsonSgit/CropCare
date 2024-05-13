

using System.Globalization;

namespace CropCare.Converters
{
    public class HumidityHealthTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string healthStatus;
            if (double.TryParse(value.ToString(), out double humidityValue))
            {
                if (humidityValue >= 75 && humidityValue <= 95)
                    healthStatus = "Healthy";
                else if (humidityValue >= 65 && humidityValue <= 75 || humidityValue >= 85 && humidityValue <= 95)
                    healthStatus = "Caution";
                else
                    healthStatus = "Unhealthy";
            }
            else
                healthStatus = "Unkown";

            return healthStatus;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

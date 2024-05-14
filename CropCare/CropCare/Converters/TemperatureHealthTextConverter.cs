using System.Globalization;


namespace CropCare.Converters
{
    public class TemperatureHealthTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string healthStatus;
            if (double.TryParse(value.ToString(), out double temperatureValue))
            {
                if (temperatureValue >= 26 && temperatureValue <= 29)
                    healthStatus = "Healthy";
                else if (temperatureValue >= 30 && temperatureValue <= 32 || temperatureValue >= 24 && temperatureValue <= 26)
                    healthStatus = "Caution";
                else
                    healthStatus = "Critical";
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
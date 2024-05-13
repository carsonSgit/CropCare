using System.Globalization;

namespace CropCare.Converters
{
    public class HumidityColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color;
            if (double.TryParse(value.ToString(), out double humidityValue))
            {
                if (humidityValue >= 75 && humidityValue <= 95)
                    color = Color.FromArgb("#42A765");// Healthy
                else if (humidityValue >= 65 && humidityValue <= 75 || humidityValue >= 85 && humidityValue <= 95)
                    color = Color.FromArgb("#E08551");// Caution
                else
                    color = Color.FromArgb("#EA5757");// Unhealthy
            }
            else
                color = Color.FromArgb("#808080");// Unkown

            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

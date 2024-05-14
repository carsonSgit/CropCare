using System.Globalization;

namespace CropCare.Converters
{
    public class MotionColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color;
            try
            {
                if (value.ToString() == "False")
                    color = Color.FromArgb("#42A765");// Healthy
                else if (value.ToString() == "True")
                    color = Color.FromArgb("#EA5757");// Unhealthy
                else
                    color = Color.FromArgb("#A9A9A9");// Unkown
            }
            catch
            {
                color = Color.FromArgb("#A9A9A9");// Unkown
            }
               
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

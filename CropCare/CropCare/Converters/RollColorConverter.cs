using System.Globalization;

namespace CropCare.Converters
{
    public class RollColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color;
            if (double.TryParse(value.ToString(), out double rollValue))
            {
                if (rollValue >= -2.5 && rollValue <= 2.5)
                    color = Color.FromArgb("#42A765");// Healthy
                else if (rollValue >= -5 && rollValue <= -2.5 || rollValue >= 2.5 && rollValue <= 5)
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
using System.Globalization;

namespace CropCare.Converters
{
    public class VibrationHealthTextConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string message;
            try
            {
                if (value.ToString() == "False")
                    message = "No Vibration Detected";// Healthy
                else if (value.ToString() == "True")
                    message = "Vibration Detected";// Unhealthy
                else
                    message = "Status Unkown";// Unkown
            }
            catch
            {
                message = "Status Unkown";// Unkown
            }

            return message;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

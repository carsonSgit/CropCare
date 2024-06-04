

using System.Globalization;

namespace CropCare.Converters
{
    public class DoorOpenHealthTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string message;
            try
            {
                if (value.ToString() == "False")
                    message = "Door Is Open";
                else if (value.ToString() == "True")
                    message = "Door Is Closed";
                else
                    message = "Status Unknown";// Unkown
            }
            catch
            {
                message = "Status Unknown";// Unkown
            }

            return message;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

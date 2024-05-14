
using System.Globalization;

namespace CropCare.Converters
{
    public class LoudnessHealthTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string healthStatus;
            try
            {
                if (value.ToString() == "Quiet")
                    healthStatus = "Healthy";
                else if (value.ToString() == "Noisy")
                    healthStatus = "Caution";
                else if (value.ToString() == "Loud")
                    healthStatus = "Critical";
                else
                    healthStatus = "Unkown";
            }
            catch
            {
                healthStatus = "Unkown";
            }

            return healthStatus;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

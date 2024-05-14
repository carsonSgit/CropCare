using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropCare.Converters
{
    public class PitchAndRollHealthTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string healthStatus;
            if (double.TryParse(value.ToString(), out double pitchValue))
            {
                if (pitchValue >= -2.5 && pitchValue <= 2.5)
                    healthStatus = "Healthy";// Healthy
                else if (pitchValue >= -5 && pitchValue <= -2.5 || pitchValue >= 2.5 && pitchValue <= 5)
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

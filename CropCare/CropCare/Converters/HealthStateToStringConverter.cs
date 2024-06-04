using CropCare.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropCare.Converters
{
    public class HealthStateToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            HealthState state = (HealthState)value;

            switch(state)
            {
                case HealthState.Healthy:
                    return "Healthy";
                case HealthState.Caution:
                    return "Caution";
                case HealthState.Critical:
                    return "Critical";
                case HealthState.Unknown:
                    return "Unknown";
                default:
                    return "Unknown";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

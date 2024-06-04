using CropCare.Models;
using System.Globalization;

namespace CropCare.Converters
{
    class OverviewHealthTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            HealthState state = (HealthState)value;

            switch (state)
            {
                case HealthState.Healthy:
                    return "Healthy";
                case HealthState.Caution:
                    return "Caution";
                case HealthState.Critical:
                    return "Critical";
                case HealthState.Unknown:
                    return "N/A";
                default:
                    return "N/A";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}


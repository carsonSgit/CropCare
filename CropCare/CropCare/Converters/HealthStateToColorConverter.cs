using CropCare.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropCare.Converters
{
    public class HealthStateToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            HealthState state = (HealthState)value;

            switch (state)
            {
                case HealthState.Healthy:
                    return Color.FromArgb("#42A765");
                case HealthState.Caution:
                    return Color.FromArgb("#E08551");
                case HealthState.Critical:
                    return Color.FromArgb("#EA5757");
                case HealthState.Unknown:
                    return Color.FromArgb("#A9A9A9");
                default:
                    return Color.FromArgb("#A9A9A9");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

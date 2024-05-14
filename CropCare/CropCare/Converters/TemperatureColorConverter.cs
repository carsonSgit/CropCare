﻿using System.Globalization;

namespace CropCare.Converters
{
    public class TemperatureColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color;
            if (double.TryParse(value.ToString(), out double temperatureValue))
            {
                if (temperatureValue >= 26 && temperatureValue <= 29)
                    color = Color.FromArgb("#42A765");// Healthy
                else if (temperatureValue >= 30 && temperatureValue <= 32 || temperatureValue >= 24 && temperatureValue <= 26)
                    color = Color.FromArgb("#E08551");// Caution
                else
                    color = Color.FromArgb("#EA5757");// Unhealthy
            }
            else
                color = Color.FromArgb("#A9A9A9");// Unkown

            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

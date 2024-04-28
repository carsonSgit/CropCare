using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropCare.Models
{
    public static class ReadingType
    {
        public const string TEMPERATURE = "temperature";
        public const string HUMIDITY = "humidity";
        public const string LUMINOSITY = "luminosity";
        public const string LOUDNESS = "loudness";
        public const string NOISE = "decibels";
        public const string MOISTURE = "moisture";
        public const string WATERLEVEL = "water level";
        public const string MOTION = "motion";
        public const string LATITUDE = "latitude";
        public const string LONGITUDE = "longitude";
        public const string GPS = "gps";
        public const string PITCH = "pitch";
        public const string ROLL = "roll";
        public const string YAW = "yaw";
        public const string VIBRATION = "vibration";
        public const string DOORLOCK = "doorlock";
    }

    public static class ReadingUnit
    {
        public const string MILLIMITERS = "mm";
        public const string CELCIUS = "°C";
        public const string FAHRENHEIT = "°F";
        public const string HUMIDITY = "% HR";
        public const string UNITLESS = "unitless";
        public const string LUX = "nm";
        public const string LOUDNESS = "unitless"; //Unitless????
        public const string OHMS = "Ω";
        public const string WATERLEVEL = "water level";
        public const string NONE = "";
        public const string DEGREE = "°";
    }


    public class Reading
    {
        public string Type { get; set; }
        public string Unit { get; set; }
        public string Value { get; set; }

        public Reading(string type, string unit, string value)
        {
            Type = type;
            Unit = unit;
            Value = value;
        }
    }
}

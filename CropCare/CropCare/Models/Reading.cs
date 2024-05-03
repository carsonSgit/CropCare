namespace CropCare.Models
{
    // Team Name: CropCare
    // Team Members: Kevin Baggott, Cristiano Fazi and Carson Spriggs-Audet
    // Date: April 29th 2023, 6th Semester
    // Course Name: Application Development and Connected Objects
    // Description: Provides constants for different types of sensor readings.
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

    // Team Name: CropCare
    // Team Members: Kevin Baggott, Cristiano Fazi and Carson Spriggs-Audet
    // Date: April 29th 2023, 6th Semester
    // Course Name: Application Development and Connected Objects
    // Description: Provides constants for different units of sensor readings.
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


    // Team Name: CropCare
    // Team Members: Kevin Baggott, Cristiano Fazi and Carson Spriggs-Audet
    // Date: April 29th 2023, 6th Semester
    // Course Name: Application Development and Connected Objects
    // Description: Represents a sensor reading.
    public class Reading<T>
    {
        /// <summary>
        /// Gets or sets the type of the reading.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the unit of the reading.
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// Gets or sets the value of the reading.
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Reading"/> class with the specified type, unit, and value.
        /// </summary>
        /// <param name="type">The type of the reading.</param>
        /// <param name="unit">The unit of the reading.</param>
        /// <param name="value">The value of the reading.</param>
        public Reading(string type, string unit, T value)
        {
            Type = type;
            Unit = unit;
            Value = value;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"{Value} {Unit}";
        }
    }
}
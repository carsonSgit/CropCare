namespace CropCare.Models
{
    // Team Name: CropCare
    // Team Members: Kevin Baggott, Cristiano Fazi and Carson Spriggs-Audet
    // Date: April 29th 2023, 6th Semester
    // Course Name: Application Development and Connected Objects
    // Description: Provides constants for different types of sensor readings.
    public static class ReadingType
    {
        public const string TEMPERATURE = "TEMPERATURE";
        public const string HUMIDITY = "HUMIDITY";
        public const string LUMINOSITY = "LUMINOSITY";
        public const string LOUDNESS = "LOUDNESS";
        public const string NOISE = "DECIBELS";
        public const string MOISTURE = "MOISTURE";
        public const string WATERLEVEL = "WATERLEVEL";
        public const string MOTION = "MOTION";
        public const string LATITUDE = "LATITUDE";
        public const string LONGITUDE = "LONGITUDE";
        public const string PITCH = "PITCH";
        public const string ROLL = "ROLL";
        public const string VIBRATION = "VIBRATION";
        public const string DOORLOCK = "DOORLOCK";
        public const string CONNECTION_INTERRUPTED = "CONNECTION_INTERRUPTED";
    }

    // Team Name: CropCare
    // Team Members: Kevin Baggott, Cristiano Fazi and Carson Spriggs-Audet
    // Date: April 29th 2023, 6th Semester
    // Course Name: Application Development and Connected Objects
    // Description: Represents a sensor reading.
    public class Reading
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
        public dynamic Value { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Reading<typeparamref name="T"/>"/> class with the specified type, unit, and value.
        /// </summary>
        /// <param name="type">The type of the reading.</param>
        /// <param name="unit">The unit of the reading.</param>
        /// <param name="value">The value of the reading.</param>
        public Reading(string type, string unit, dynamic value)
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
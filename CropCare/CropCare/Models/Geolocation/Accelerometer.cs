using CropCare.Interfaces;

namespace CropCare.Models.Geolocation
{
    /// <summary>
    /// Represents an accelerometer sensor.
    /// </summary>
    public class Accelerometer : ISensor
    {
        /// <summary>
        /// Reads the accelerometer sensor.
        /// </summary>
        /// <returns>A list of readings from the accelerometer sensor.</returns>
        public List<Reading> ReadSensor()
        {
            return new List<Reading>()
            {
                new Reading(ReadingType.PITCH, ReadingUnit.DEGREE, "12.22334"),
                new Reading(ReadingType.ROLL, ReadingUnit.DEGREE, "73.1232123"),
            };
        }
    }
}
using CropCare.Interfaces;

namespace CropCare.Models.Security
{
    /// <summary>
    /// Represents a sensor for detecting vibrations.
    /// </summary>
    public class Vibration : ISensor
    {
        /// <summary>
        /// Reads the sensor to detect vibrations.
        /// </summary>
        /// <returns>A list of readings indicating vibration detection.</returns>
        public List<Reading> ReadSensor()
        {
            return new List<Reading>()
            {
                new Reading(ReadingType.VIBRATION, ReadingUnit.NONE, "Vibration detected"),
            };
        }
    }
}
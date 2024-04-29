using CropCare.Interfaces;

namespace CropCare.Models.Security
{
    /// <summary>
    /// Represents a motion sensor.
    /// </summary>
    public class Motion : ISensor
    {
        /// <summary>
        /// Reads the motion sensor.
        /// </summary>
        /// <returns>A list of readings from the motion sensor.</returns>
        public List<Reading> ReadSensor()
        {
            return new List<Reading>()
            {
                new Reading(ReadingType.MOTION, ReadingUnit.NONE, "Movement Detected"),
            };
        }
    }
}

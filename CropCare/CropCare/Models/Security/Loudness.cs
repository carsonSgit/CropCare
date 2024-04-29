using CropCare.Interfaces;

namespace CropCare.Models.Security
{
    /// <summary>
    /// Represents a loudness sensor.
    /// </summary>
    public class Loudness : ISensor
    {
        /// <summary>
        /// Reads the loudness sensor.
        /// </summary>
        /// <returns>A list of readings from the loudness sensor.</returns>
        public List<Reading> ReadSensor()
        {
            return new List<Reading>()
            {
                new Reading(ReadingType.LOUDNESS, ReadingUnit.LOUDNESS, "100"),
            };
        }
    }
}
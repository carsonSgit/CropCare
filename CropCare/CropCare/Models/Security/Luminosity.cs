using CropCare.Interfaces;

namespace CropCare.Models.Security
{
    /// <summary>
    /// Represents a luminosity sensor.
    /// </summary>
    public class Luminosity : ISensor
    {
        /// <summary>
        /// Reads the luminosity sensor.
        /// </summary>
        /// <returns>A list of readings from the luminosity sensor.</returns>
        public List<Reading> ReadSensor()
        {
            return new List<Reading>()
            {
                new Reading(ReadingType.LUMINOSITY, ReadingUnit.LUX, "230"),
            };
        }
    }
}
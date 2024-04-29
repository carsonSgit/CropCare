using CropCare.Interfaces;

namespace CropCare.Models.Plant
{
    /// <summary>
    /// Represents a soil moisture sensor.
    /// </summary>
    public class SoilMoisture : ISensor
    {
        /// <summary>
        /// Reads the soil moisture sensor.
        /// </summary>
        /// <returns>A list of readings from the soil moisture sensor.</returns>
        public List<Reading> ReadSensor()
        {
            return new List<Reading>()
            {
                new Reading(ReadingType.MOISTURE, ReadingUnit.OHMS, "554"),
            };
        }
    }
}

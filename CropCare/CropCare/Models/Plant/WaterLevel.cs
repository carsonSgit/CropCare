using CropCare.Interfaces;

namespace CropCare.Models.Plant
{
    /// <summary>
    /// Represents a water level sensor.
    /// </summary>
    public class WaterLevel : ISensor
    {
        /// <summary>
        /// Reads the water level sensor.
        /// </summary>
        /// <returns>A list of readings from the water level sensor.</returns>
        public List<Reading> ReadSensor()
        {
            return new List<Reading>()
            {
                new Reading(ReadingType.WATERLEVEL, ReadingUnit.WATERLEVEL, "100"),
            };
        }
    }
}

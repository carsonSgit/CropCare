using CropCare.Interfaces;

namespace CropCare.Models.Plant
{
    /// <summary>
    /// Represents a temperature and humidity sensor.
    /// </summary>
    public class TemperatureHumidity : ISensor
    {
        /// <summary>
        /// Reads the temperature and humidity sensor.
        /// </summary>
        /// <returns>A list of readings from the temperature and humidity sensor.</returns>
        public List<Reading> ReadSensor()
        {
            return new List<Reading>()
            {
                new Reading(ReadingType.TEMPERATURE, ReadingUnit.CELCIUS, "20"),
                new Reading(ReadingType.HUMIDITY, ReadingUnit.HUMIDITY, "10"),
            };
        }
    }
}

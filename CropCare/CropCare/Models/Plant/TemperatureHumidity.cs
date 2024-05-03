using CropCare.Interfaces;

namespace CropCare.Models.Plant
{
    // Team Name: CropCare
    // Team Members: Kevin Baggott, Cristiano Fazi and Carson Spriggs-Audet
    // Date: April 29th 2023, 6th Semester
    // Course Name: Application Development and Connected Objects
    // Description: Represents a temperature and humidity sensor.
    public class TemperatureHumidity : ISensor<int>
    {
        /// <summary>
        /// Reads the temperature and humidity sensor.
        /// </summary>
        /// <returns>A list of readings from the temperature and humidity sensor.</returns>
        public List<Reading<int>> ReadSensor()
        {
            return new List<Reading<int>>()
            {
                new Reading<int>(ReadingType.TEMPERATURE, ReadingUnit.CELCIUS, 20),
                new Reading<int>(ReadingType.HUMIDITY, ReadingUnit.HUMIDITY, 10),
            };
        }
    }
}

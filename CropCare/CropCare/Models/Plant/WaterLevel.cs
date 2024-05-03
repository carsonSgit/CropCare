using CropCare.Interfaces;

namespace CropCare.Models.Plant
{
    // Team Name: CropCare
    // Team Members: Kevin Baggott, Cristiano Fazi and Carson Spriggs-Audet
    // Date: April 29th 2023, 6th Semester
    // Course Name: Application Development and Connected Objects
    // Description: Represents a water level sensor.
    public class WaterLevel : ISensor<int>
    {
        /// <summary>
        /// Reads the water level sensor.
        /// </summary>
        /// <returns>A list of readings from the water level sensor.</returns>
        public List<Reading<int>> ReadSensor()
        {
            return new List<Reading<int>>()
            {
                new Reading<int>(ReadingType.WATERLEVEL, ReadingUnit.WATERLEVEL, 100),
            };
        }
    }
}

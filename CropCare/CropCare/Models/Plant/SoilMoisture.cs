using CropCare.Interfaces;

namespace CropCare.Models.Plant
{
    // Team Name: CropCare
    // Team Members: Kevin Baggott, Cristiano Fazi and Carson Spriggs-Audet
    // Date: April 29th 2023, 6th Semester
    // Course Name: Application Development and Connected Objects
    // Description: Represents a soil moisture sensor.
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

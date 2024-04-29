using CropCare.Interfaces;

namespace CropCare.Models.Security
{
    // Team Name: CropCare
    // Team Members: Kevin Baggott, Cristiano Fazi and Carson Spriggs-Audet
    // Date: April 29th 2023, 6th Semester
    // Course Name: Application Development and Connected Objects
    // Description: Represents a luminosity sensor.
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
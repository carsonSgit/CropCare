using CropCare.Interfaces;

namespace CropCare.Models.Security
{
    // Team Name: CropCare
    // Team Members: Kevin Baggott, Cristiano Fazi and Carson Spriggs-Audet
    // Date: April 29th 2023, 6th Semester
    // Course Name: Application Development and Connected Objects
    // Description: Represents a sensor for detecting vibrations.
    public class Vibration : ISensor<string>
    {
        /// <summary>
        /// Reads the sensor to detect vibrations.
        /// </summary>
        /// <returns>A list of readings indicating vibration detection.</returns>
        public List<Reading<string>> ReadSensor()
        {
            return new List<Reading<string>>()
            {
                new Reading<string>(ReadingType.VIBRATION, ReadingUnit.NONE, "Vibration detected"),
            };
        }
    }
}
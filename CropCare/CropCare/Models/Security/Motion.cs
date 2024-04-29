using CropCare.Interfaces;

namespace CropCare.Models.Security
{
    // Team Name: CropCare
    // Team Members: Kevin Baggott, Cristiano Fazi and Carson Spriggs-Audet
    // Date: April 29th 2023, 6th Semester
    // Course Name: Application Development and Connected Objects
    // Description: Represents a motion sensor.
    public class Motion : ISensor
    {
        /// <summary>
        /// Reads the motion sensor.
        /// </summary>
        /// <returns>A list of readings from the motion sensor.</returns>
        public List<Reading> ReadSensor()
        {
            return new List<Reading>()
            {
                new Reading(ReadingType.MOTION, ReadingUnit.NONE, "Movement Detected"),
            };
        }
    }
}

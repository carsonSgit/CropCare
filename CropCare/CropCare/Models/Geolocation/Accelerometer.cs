using CropCare.Interfaces;

namespace CropCare.Models.Geolocation
{
    // Team Name: CropCare
    // Team Members: Kevin Baggott, Cristiano Fazi and Carson Spriggs-Audet
    // Date: April 29th 2023, 6th Semester
    // Course Name: Application Development and Connected Objects
    // Description: Represents an accelerometer sensor.
    public class Accelerometer : ISensor<double>
    {
        /// <summary>
        /// Reads the accelerometer sensor.
        /// </summary>
        /// <returns>A list of readings from the accelerometer sensor.</returns>
        public List<Reading<double>> ReadSensor()
        {
            return new List<Reading<double>>()
            {
                new Reading<double>(ReadingType.PITCH, ReadingUnit.DEGREE, 12.22334),
                new Reading<double>(ReadingType.ROLL, ReadingUnit.DEGREE, 73.1232123),
            };
        }
    }
}
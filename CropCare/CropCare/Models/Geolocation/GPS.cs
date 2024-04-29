using CropCare.Interfaces;

namespace CropCare.Models.Geolocation
{
    /// <summary>
    /// Represents a GPS sensor.
    /// </summary>
    public class GPS : ISensor
    {
        /// <summary>
        /// Reads the GPS sensor.
        /// </summary>
        /// <returns>A list of readings from the GPS sensor.</returns>
        public List<Reading> ReadSensor()
        {
            return new List<Reading>()
            {
                new Reading(ReadingType.LATITUDE, ReadingUnit.DEGREE, "45.484056"),
                new Reading(ReadingType.LONGITUDE, ReadingUnit.DEGREE, "-73.680421"),
            };
        }
    }
}

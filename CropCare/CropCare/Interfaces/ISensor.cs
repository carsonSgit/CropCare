using CropCare.Models;

namespace CropCare.Interfaces
{
    /// <summary>
    /// Interface for sensor objects.
    /// </summary>
    public interface ISensor<T>
    {
        /// <summary>
        /// Reads sensor data and returns a list of readings.
        /// </summary>
        /// <returns>A list of sensor readings.</returns>
        public List<Reading<T>> ReadSensor();
    }
}
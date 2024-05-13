using CropCare.Models;
using System.Collections.ObjectModel;

namespace CropCare.Interfaces
{
    /// <summary>
    /// Interface for sensor objects.
    /// </summary>
    public interface ISensor
    {
        /// <summary>
        /// Reads sensor data and returns a list of readings.
        /// </summary>
        /// <returns>A list of sensor readings.</returns>
        public ObservableCollection<Reading> Readings { get; }
        public void Refresh();

    }
}
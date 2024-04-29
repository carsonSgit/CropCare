using CropCare.Interfaces;
using Microsoft.Azure.Devices;
using System.ComponentModel;
using Newtonsoft.Json;
using CropCare.Models.Plant;
using CropCare.Models.Security;
using CropCare.Models.Geolocation;

namespace CropCare.Models
{
    /// <summary>
    /// Represents a farm entity.
    /// </summary>
    public class Farm : INotifyPropertyChanged, IHasKey
    {
        /// <summary>
        /// Event that is raised when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the key of the farm.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the name of the farm.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the device ID of the farm.
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// Gets or sets the plant controller associated with the farm.
        /// </summary>
        [JsonIgnore]
        public PlantController PlantController { get; set; }

        /// <summary>
        /// Gets or sets the security controller associated with the farm.
        /// </summary>
        [JsonIgnore]
        public SecurityController SecurityController { get; set; }

        /// <summary>
        /// Gets or sets the geolocation controller associated with the farm.
        /// </summary>
        [JsonIgnore]
        public GeolocationController GeolocationController { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Farm"/> class.
        /// </summary>
        /// <param name="farmName">The name of the farm.</param>
        /// <param name="deviceId">The device ID of the farm.</param>
        public Farm(string farmName, string deviceId)
        {
            Name = farmName;
            DeviceId = deviceId;
            PlantController = new PlantController(deviceId);
            SecurityController = new SecurityController(deviceId);
            GeolocationController = new GeolocationController(deviceId);
        }
    }
}

using CropCare.Interfaces;
using Microsoft.Azure.Devices;
using System.ComponentModel;
using Newtonsoft.Json;
using CropCare.Models.Plant;
using CropCare.Models.Security;
using CropCare.Models.Geolocation;
namespace CropCare.Models
{
    public class Farm : INotifyPropertyChanged, IHasKey
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Key { get; set; }
        public string Name { get; set; }
        public string DeviceId { get; set; }

        [JsonIgnore]
        public PlantController PlantController { get; set; }
        [JsonIgnore]
        public SecurityController SecurityController { get; set; }
        [JsonIgnore]
        public GeolocationController GeolocationController { get; set; }


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

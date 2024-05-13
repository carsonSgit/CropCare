using Azure.Messaging.EventHubs.Consumer;
using CropCare.Interfaces;
using CropCare.Models.Geolocation;
using CropCare.Models.Plant;
using CropCare.Models.Security;
using Newtonsoft.Json;
using Org.Json;
using System.ComponentModel;
namespace CropCare.Models
{
    // Team Name: CropCare
    // Team Members: Kevin Baggott, Cristiano Fazi and Carson Spriggs-Audet
    // Date: April 29th 2023, 6th Semester
    // Course Name: Application Development and Connected Objects
    // Description: Represents a farm entity.
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

        public EventHubConsumerClient Consumer { get; set; }

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
            PlantController = new PlantController();
            SecurityController = new SecurityController();
            GeolocationController = new GeolocationController();
            App.IOTService.MessageReceived += IOTService_MessageReceived;
        }

        private void IOTService_MessageReceived(string deviceId, string data)
        {
            if (deviceId != DeviceId)
            {
                return;
            }
            Console.WriteLine($"{data}");
            Dictionary<string, string> dictionary = null;
            try
            {
                data = data.Replace('\'', '"');
                dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not deserialize data: {ex.Message}");
            }

            try
            {
                foreach (var sensor in PlantController.Sensors)
                {
                    foreach (var reading in sensor.Readings)
                    {
                        if (data.Contains(reading.Type))
                        {
                            reading.Value = dictionary["value"];
                            sensor.Refresh();
                        }
                    }
                }

                foreach (var sensor in SecurityController.Sensors)
                {
                    foreach (var reading in sensor.Readings)
                    {
                        if (data.Contains(reading.Type))
                        {
                            reading.Value = dictionary["value"];
                            sensor.Refresh();
                        }
                    }
                }

                foreach (var sensor in GeolocationController.Sensors)
                {
                    foreach (var reading in sensor.Readings)
                    {
                        if (data.Contains(reading.Type))
                        {
                            reading.Value = dictionary["value"];
                            sensor.Refresh();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not update sensor readings: {ex.Message}");
            }
        }
    }
}


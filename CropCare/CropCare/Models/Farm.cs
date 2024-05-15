using Azure.Messaging.EventHubs.Consumer;
using CropCare.Interfaces;
using CropCare.Models.Controllers;
using Newtonsoft.Json;
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
        /// Path to the icon used to represent this farm
        /// </summary>
        public string IconPath { get; set; }

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
        public Farm(string farmName, string deviceId, string iconPath)
        {
            Name = farmName;
            DeviceId = deviceId;
            PlantController = new PlantController();
            SecurityController = new SecurityController();
            GeolocationController = new GeolocationController();
            IconPath = iconPath;
            App.IOTService.MessageReceived += IOTService_MessageReceived;
        }

        private void IOTService_MessageReceived(string deviceId, string data)
        {
            if (deviceId != DeviceId)
            {
                return;
            }
            Console.WriteLine($"{data}");

            Reading reading = JSONToReading(data);

            try
            {
                var controllers = new BaseController[] { PlantController, SecurityController, GeolocationController };
                foreach (var controller in controllers)
                {
                    if(controller.ValidateReading(reading))
                    {
                        controller.AddReading(reading);
                        controller.GetChart(reading.Type);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not update sensor readings: {ex.Message}");
            }
        }

        private Reading JSONToReading(string json)
        {
            Dictionary<string, string> dictionary = null;
            try
            {
                json = json.Replace('\'', '"');
                dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                return new Reading(dictionary["reading_type"], dictionary["unit"], dictionary["value"]);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not deserialize {json} to Reading Class: {ex.Message}");
            }
            return null;
        }
    }
}


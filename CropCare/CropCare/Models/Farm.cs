using Azure.Messaging.EventHubs.Consumer;
using CropCare.Interfaces;
using CropCare.Models.Controllers;
using CropCare.Services;
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
        /// The property name for the telemetry interval.
        /// </summary>
        public const string TELEMETRY_INTERVAL_PROP = "telemetryInterval";

        private bool _isListening = false;

        private bool _setupComplete = false;

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
        /// Path to the icon used to represent this farm
        /// </summary>
        public string IconPath { get; set; }

        /// <summary>
        /// Gets or sets the telemetry interval of the farm.
        /// </summary>
        [JsonIgnore]
        public float TelemetryInterval { get; set; }

        /// <summary>
        /// Gets or sets the overall health of the plant subsystem.
        /// </summary>
        [JsonIgnore]
        public HealthState PlantControllerOverallHealth { get; set; }

        /// <summary>
        /// Gets or sets the overall health of the security subsystem.
        /// </summary>
        [JsonIgnore]
        public HealthState SecurityControllerOverallHealth { get; set; }

        /// <summary>
        /// Gets or sets the overall health of the geolocation subsystem.
        /// </summary>
        [JsonIgnore]
        public HealthState GeolocationControllerOverallHealth { get; set; }

        /// <summary>
        /// Gets or sets the overall health of the farm.
        /// </summary>
        [JsonIgnore]
        public HealthState OverallHealth { get; set; }

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
            PlantController = new PlantController(DeviceId);
            SecurityController = new SecurityController(DeviceId);
            GeolocationController = new GeolocationController(DeviceId);
            IconPath = iconPath;

            PlantControllerOverallHealth = HealthState.Unknown;
            SecurityControllerOverallHealth = HealthState.Unknown;
            GeolocationControllerOverallHealth = HealthState.Unknown;
            OverallHealth = HealthState.Unknown;
            App.IOTService.ConnectionStopped += GetOverallHealth;
        }

        /// <summary>
        /// Unsubscribes from the IOTService MessageReceived event.
        /// </summary>
        public void StopListeningToHub()
        {
            if (!_isListening)
            {
                return;
            }

            App.IOTService.MessageReceived -= IOTService_MessageReceived;
            _isListening = false;
        }

        /// <summary>
        /// Subscribes from the IOTService MessageReceived event.
        /// </summary>
        public async void StartListeningToHub()
        {
            App.IOTService.MessageReceived += IOTService_MessageReceived;
            _setupComplete = await SetupFarm();
            _isListening = true;
        }

        private async Task<bool> SetupFarm()
        {
            try
            {
                TelemetryInterval = float.Parse(await App.IOTService.GetDesiredPropertyForDeviceAsync(DeviceId, TELEMETRY_INTERVAL_PROP));

                var controllers = new BaseController[] { PlantController, SecurityController, GeolocationController };
                foreach (var controller in controllers)
                {
                    await controller.GetInitialActuatorStates();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not setup farm properly, {ex.Message}");
                return false;
            }
        }

        private async void IOTService_MessageReceived(string deviceId, string data)
        {
            if (deviceId != DeviceId)
            {
                return;
            }
            if(!_setupComplete)
            {
                _setupComplete = await SetupFarm();
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
                        controller.UpdateChart(reading.Type);
                    }
                }
                GetOverallHealth();
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

        /// <summary>
        /// Gets the overall health of the farm.
        /// </summary>
        /// <returns>Overall health of the farm being the worst health state.</returns>
        public void GetOverallHealth()
        {
            PlantControllerOverallHealth = PlantController.GetOverallHealth();
            SecurityControllerOverallHealth = SecurityController.GetOverallHealth();
            GeolocationControllerOverallHealth = GeolocationController.GetOverallHealth();

            var healthStates = new HealthState[] { PlantControllerOverallHealth, SecurityControllerOverallHealth, GeolocationControllerOverallHealth };
            OverallHealth = healthStates.Max();
        }
    }
}


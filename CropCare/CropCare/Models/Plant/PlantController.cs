using System.ComponentModel;
using CropCare.Interfaces;

namespace CropCare.Models.Plant
{
    /// <summary>
    /// Represents a controller for managing plant-related sensors and actuators.
    /// </summary>
    public class PlantController: INotifyPropertyChanged
    {
        /// <summary>
        /// Event raised when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event for the specified property.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Gets the fan associated with the plant controller.
        /// </summary>
        public Fan Fan { get; }

        /// <summary>
        /// Gets the LED associated with the plant controller.
        /// </summary>
        public Led Led { get; }

        /// <summary>
        /// Gets the soil moisture sensor associated with the plant controller.
        /// </summary>
        public SoilMoisture SoilMoisture { get; }

        /// <summary>
        /// Gets the temperature and humidity sensor associated with the plant controller.
        /// </summary>
        public TemperatureHumidity Temperature { get; }

        /// <summary>
        /// Gets the water level sensor associated with the plant controller.
        /// </summary>
        public WaterLevel WaterLevel { get; }

        /// <summary>
        /// Gets or sets the device ID associated with the plant controller.
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// Gets the temperature reading from the temperature and humidity sensor.
        /// </summary>
        public string TemperatureReading => GetTemperatureReading();

        /// <summary>
        /// Gets the health status of the temperature reading.
        /// </summary>
        public string TemperatureHealth => UpdateReadingHealthLabel(TemperatureReading, '°', 30, 10);

        /// <summary>
        /// Gets the humidity reading from the temperature and humidity sensor.
        /// </summary>
        public string HumidityReading => GetHumidityReading();

        /// <summary>
        /// Gets the health status of the humidity reading.
        /// </summary>
        public string HumidityHealth => UpdateReadingHealthLabel(HumidityReading, '%', 80, 20);

        /// <summary>
        /// Gets the moisture reading from the soil moisture sensor.
        /// </summary>
        public string MoistureReading => GetMoistureReading();

        /// <summary>
        /// Gets the health status of the moisture reading.
        /// </summary>
        public string MoistureHealth => UpdateReadingHealthLabel(MoistureReading, 'Ω', 500, 200);

        /// <summary>
        /// Gets the water level reading from the water level sensor.
        /// </summary>
        public string WaterLevelReading => GetWaterLevelReading();

        /// <summary>
        /// Gets the health status of the water level reading.
        /// </summary>
        public string WaterLevelHealth => UpdateReadingHealthLabel(WaterLevelReading, 'w', 80, 20);

        private string _ledState;

        /// <summary>
        /// Gets or sets the state of the LED.
        /// </summary>
        public string LedState
        {
            get => _ledState;
            set
            {
                if (_ledState != value)
                {
                    _ledState = value;
                    OnPropertyChanged(nameof(LedState));
                }
            }
        }

        private string _fanState;

        /// <summary>
        /// Gets or sets the state of the fan.
        /// </summary>
        public string FanState
        {
            get => _fanState;
            set
            {
                if (_fanState != value)
                {
                    _fanState = value;
                    OnPropertyChanged(nameof(FanState));
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlantController"/> class.
        /// </summary>
        /// <param name="deviceId">The device ID associated with the controller.</param>
        public PlantController(string deviceId)
        {
            Fan = new Fan();
            Led = new Led();
            SoilMoisture = new SoilMoisture();
            Temperature = new TemperatureHumidity();
            WaterLevel = new WaterLevel();
            DeviceId = deviceId;
            FanState = UpdateStateHealthLabel(Fan.State);
            LedState = UpdateStateHealthLabel(Led.State);
        }


        /// <summary>
        /// Gets the temperature reading from the temperature and humidity sensor.
        /// </summary>
        /// <returns>The temperature reading.</returns>
        public string GetTemperatureReading() => GetSensorReading(Temperature, ReadingType.TEMPERATURE);

        /// <summary>
        /// Gets the humidity reading from the temperature and humidity sensor.
        /// </summary>
        /// <returns>The humidity reading.</returns>
        public string GetHumidityReading() => GetSensorReading(Temperature, ReadingType.HUMIDITY);

        /// <summary>
        /// Gets the moisture reading from the soil moisture sensor.
        /// </summary>
        /// <returns>The moisture reading.</returns>
        public string GetMoistureReading() => GetSensorReading(SoilMoisture, ReadingType.MOISTURE);

        /// <summary>
        /// Gets the water level reading from the water level sensor.
        /// </summary>
        /// <returns>The water level reading.</returns>
        public string GetWaterLevelReading() => GetSensorReading(WaterLevel, ReadingType.WATERLEVEL);


        private string GetSensorReading<T>(T sensor, string readingType) where T : ISensor
        {
            var reading = sensor.ReadSensor().FirstOrDefault(r => r.Type.Equals(readingType, StringComparison.OrdinalIgnoreCase));
            return reading != null ? $"{reading.Value}{reading.Unit}" : "N/A";
        }

        /// <summary>
        /// Updates the health label based on the sensor reading and specified thresholds.
        /// </summary>
        /// <param name="sensorReading">The sensor reading.</param>
        /// <param name="unitSymbol">The symbol used to separate the value from the unit.</param>
        /// <param name="highThreshold">The threshold for the high range.</param>
        /// <param name="lowThreshold">The threshold for the low range.</param>
        /// <returns>The health status based on the reading and thresholds.</returns>
        public string UpdateReadingHealthLabel(string sensorReading, char unitSymbol, double highThreshold, double lowThreshold)
        {
            string health = "";
            double sensorValue;
            if (double.TryParse(sensorReading.Split(unitSymbol)[0], out sensorValue))
            {
                if (sensorValue > highThreshold)
                {
                    health = "Critical";
                    //healthLbl.TextColor = Colors.Red;
                }
                else if (sensorValue < lowThreshold)
                {
                    health = "Needs Attention";
                    //healthLbl.TextColor = Colors.Red;
                }
                else
                {
                    health = "Healthy";
                    //healthLbl.TextColor = Colors.Green;
                }
            }

            return health;
        }

        /// <summary>
        /// Updates the health label based on the state of the actuator.
        /// </summary>
        /// <param name="actuatorState">The state of the actuator.</param>
        /// <returns>The health status based on the actuator state.</returns>
        public string UpdateStateHealthLabel(string actuatorState)
        {
            string health = "";
            if (actuatorState.Equals(Command.ON.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                health = "On";
                //healthLbl.TextColor = Colors.Green;
            }
            else if (actuatorState.Equals(Command.OFF.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                health = "Off";
                //healthLbl.TextColor = Colors.Red;
            }
            return health;
        }
    }
}

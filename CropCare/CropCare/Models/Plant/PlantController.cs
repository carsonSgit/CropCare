using System.ComponentModel;
using CropCare.Interfaces;

namespace CropCare.Models.Plant
{
    // Team Name: CropCare
    // Team Members: Kevin Baggott, Cristiano Fazi and Carson Spriggs-Audet
    // Date: April 29th 2023, 6th Semester
    // Course Name: Application Development and Connected Objects
    // Description: Represents a controller for managing plant-related sensors and actuators.
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
        public SoilMoistureSensor SoilMoistureSensor { get; }

        /// <summary>
        /// Gets the temperature and humidity sensor associated with the plant controller.
        /// </summary>
        public TemperatureHumiditySensor TemperatureSensor { get; }

        /// <summary>
        /// Gets the water level sensor associated with the plant controller.
        /// </summary>
        public WaterLevelSensor WaterLevelSensor { get; }

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
        public List<ISensor> Sensors { get; set; }  
        /// <summary>
        /// Initializes a new instance of the <see cref="PlantController"/> class.
        /// </summary>
        public PlantController()
        {
            Fan = new Fan();
            Led = new Led();
            SoilMoistureSensor = new SoilMoistureSensor();
            TemperatureSensor = new TemperatureHumiditySensor();
            WaterLevelSensor = new WaterLevelSensor();
            FanState = UpdateStateHealthLabel(Fan.State);
            LedState = UpdateStateHealthLabel(Led.State);

            Sensors = new List<ISensor>()
            {
                SoilMoistureSensor,
                TemperatureSensor,
                WaterLevelSensor
            };
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

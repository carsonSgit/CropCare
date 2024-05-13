using CropCare.Interfaces;
using System.ComponentModel;

namespace CropCare.Models.Security
{
    // Team Name: CropCare
    // Team Members: Kevin Baggott, Cristiano Fazi and Carson Spriggs-Audet
    // Date: April 29th 2023, 6th Semester
    // Course Name: Application Development and Connected Objects
    // Description: Controller for security-related devices and sensors.
    public class SecurityController : INotifyPropertyChanged
    {
        /// <summary>
        /// Event raised when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// The loudness sensor.
        /// </summary>
        public LoudnessSensor LoudnessSensor { get; set; }

        /// <summary>
        /// The motion sensor.
        /// </summary>
        public MotionSensor MotionSensor { get; set; }

        /// <summary>
        /// The vibration sensor.
        /// </summary>
        public VibrationSensor VibrationSensor { get; set; }

        /// <summary>
        /// The door lock actuator and sensor.
        /// </summary>
        public DoorLock DoorLock { get; set; }

        /// <summary>
        /// The door opener actuator.
        /// </summary>
        public DoorOpener DoorOpener { get; set; }

        /// <summary>
        /// The luminosity sensor.
        /// </summary>
        public LuminositySensor LuminositySensor { get; set; }

        /// <summary>
        /// The state of the door opener.
        /// </summary>
        private string _doorOpenerState;
        public string DoorOpenerState
        {
            get => _doorOpenerState;
            set
            {
                if (_doorOpenerState != value)
                {
                    _doorOpenerState = value;
                    OnPropertyChanged(nameof(DoorOpenerState));
                }
            }
        }

        public List<ISensor> Sensors { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityController"/> class.
        /// </summary>
        public SecurityController()
        {
            LoudnessSensor = new LoudnessSensor();
            MotionSensor = new MotionSensor();
            VibrationSensor = new VibrationSensor();
            DoorLock = new DoorLock();
            DoorOpener = new DoorOpener();
            LuminositySensor = new LuminositySensor();

            DoorOpenerState = UpdateStateHealthLabel(DoorOpener.State);

            Sensors = new List<ISensor>()
            {
                LoudnessSensor,
                MotionSensor,
                VibrationSensor,
                LuminositySensor,
            };
        }

        /// <summary>
        /// Updates the health label based on sensor readings.
        /// </summary>
        /// <param name="sensorReading">The sensor reading.</param>
        /// <param name="unitSymbol">The symbol representing the unit of measurement.</param>
        /// <param name="highThreshold">The high threshold for the reading.</param>
        /// <param name="lowThreshold">The low threshold for the reading.</param>
        /// <returns>The health status based on the sensor reading.</returns>
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
        /// Updates the health label based on the state of an actuator.
        /// </summary>
        /// <param name="actuatorState">The state of the actuator.</param>
        /// <returns>The health status based on the actuator state.</returns>
        public string UpdateStateHealthLabel(string actuatorState)
        {
            string health = "";
            if (actuatorState.Equals(Command.ON.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                health = "Open";
                //healthLbl.TextColor = Colors.Green;
            }
            else if (actuatorState.Equals(Command.OFF.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                health = "Closed";
                //healthLbl.TextColor = Colors.Red;
            }
            return health;
        }
    }
}

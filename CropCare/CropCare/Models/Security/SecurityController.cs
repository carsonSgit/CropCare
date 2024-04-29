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
        public Loudness Loudness { get; set; }

        /// <summary>
        /// The motion sensor.
        /// </summary>
        public Motion Motion { get; set; }

        /// <summary>
        /// The vibration sensor.
        /// </summary>
        public Vibration Vibration { get; set; }

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
        public Luminosity Luminosity { get; set; }

        /// <summary>
        /// The ID of the security controller's device.
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// The current reading of luminosity.
        /// </summary>
        public string LuminosityReading => GetLuminosityReading();

        /// <summary>
        /// The health status of luminosity.
        /// </summary>
        public string LuminosityHealth => UpdateReadingHealthLabel(LuminosityReading, 'n', 1000, 500);

        /// <summary>
        /// The current reading of loudness.
        /// </summary>
        public string LoudnessReading => GetLoudnessReading();

        /// <summary>
        /// The health status of loudness.
        /// </summary>
        public string LoudnessHealth => UpdateReadingHealthLabel(LoudnessReading, 'u', 1000, 500);

        /// <summary>
        /// The current reading of motion.
        /// </summary>
        public string MotionReading => GetMotionReading();

        /// <summary>
        /// The health status of motion.
        /// </summary>
        public string MotionHealth => UpdateReadingHealthLabel(MotionReading, ' ', 1000, 500);

        /// <summary>
        /// The current reading of vibration.
        /// </summary>
        public string VibrationReading => GetVibrationReading();

        /// <summary>
        /// The health status of vibration.
        /// </summary>
        public string VibrationHealth => UpdateReadingHealthLabel(VibrationReading, ' ', 1000, 500);

        /// <summary>
        /// The state of the door lock.
        /// </summary>
        public string DoorLockState
        {
            get => GetDoorLockReading();
            set
            {
                if (DoorLock.State != value)
                {
                    DoorLock.State = value;
                    OnPropertyChanged(nameof(DoorLockState));
                }
            }
        }

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

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityController"/> class with the specified device ID.
        /// </summary>
        /// <param name="deviceId">The ID of the security controller's device.</param>
        public SecurityController(string deviceId)
        {
            Loudness = new Loudness();
            Motion = new Motion();
            Vibration = new Vibration();
            DoorLock = new DoorLock();
            DoorOpener = new DoorOpener();
            Luminosity = new Luminosity();
            DeviceId = deviceId;

            DoorOpenerState = UpdateStateHealthLabel(DoorOpener.State);
            DoorLockState = UpdateStateHealthLabel(DoorLock.State);
        }


        /// <summary>
        /// Gets the current reading of loudness.
        /// </summary>
        /// <returns>The current loudness reading.</returns>
        public string GetLoudnessReading() => GetSensorReading(Loudness, ReadingType.LOUDNESS);

        /// <summary>
        /// Gets the current reading of luminosity.
        /// </summary>
        /// <returns>The current luminosity reading.</returns>
        public string GetLuminosityReading() => GetSensorReading(Luminosity, ReadingType.LUMINOSITY);

        /// <summary>
        /// Gets the current reading of motion.
        /// </summary>
        /// <returns>The current motion reading.</returns>
        public string GetMotionReading() => GetSensorReading(Motion, ReadingType.MOTION);

        /// <summary>
        /// Gets the current reading of vibration.
        /// </summary>
        /// <returns>The current vibration reading.</returns>
        public string GetVibrationReading() => GetSensorReading(Vibration, ReadingType.VIBRATION);

        /// <summary>
        /// Gets the current reading of the door lock.
        /// </summary>
        /// <returns>The current door lock reading.</returns>
        public string GetDoorLockReading() => DoorLock.ReadSensor().FirstOrDefault()?.Value;


        private string GetSensorReading<T>(T sensor, string readingType) where T : ISensor
        {
            var reading = sensor.ReadSensor().FirstOrDefault(r => r.Type.Equals(readingType, StringComparison.OrdinalIgnoreCase));
            return reading != null ? $"{reading.Value}{reading.Unit}" : "N/A";
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

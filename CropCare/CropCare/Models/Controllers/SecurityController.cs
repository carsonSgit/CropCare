using System.ComponentModel;

namespace CropCare.Models.Controllers
{
    // Team Name: CropCare
    // Team Members: Kevin Baggott, Cristiano Fazi and Carson Spriggs-Audet
    // Date: April 29th 2023, 6th Semester
    // Course Name: Application Development and Connected Objects
    // Description: Controller for security-related devices and sensors.
    public class SecurityController : BaseController, INotifyPropertyChanged
    {
        private static readonly string[] _readingTypes = new string[] { ReadingType.LOUDNESS, ReadingType.MOTION, ReadingType.VIBRATION, ReadingType.LUMINOSITY, ReadingType.DOOROPEN };
        public string[] ReadingTypes { get => _readingTypes; }
        /// <summary>
        /// Event raised when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Represents latest loudness reading
        /// </summary>
        public Reading Loudness { get; set; }

        /// <summary>
        /// Represents latest motion reading
        /// </summary>
        public Reading Motion { get; set; }

        /// <summary>
        /// Represents latest vibration reading
        /// </summary>
        public Reading Vibration { get; set; }

        /// <summary>
        /// Represents latest luminosity reading
        /// </summary>
        public Reading Luminosity { get; set; }

        /// <summary>
        /// Represents latest door open reading
        /// </summary>
        public Reading DoorOpen { get; set; }

        private bool _isDoorLocked;
        /// <summary>
        /// Represents the state of the door actuator.
        /// </summary>
        public bool IsDoorLocked 
        {
            get
            {
                return this._isDoorLocked;
            }
            set
            {
                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    Console.WriteLine("No Internet Connection");
                }
                else
                {
                    Task.Run(async () => await UpdateActuatorState(Actuator.SERVO, value));
                    this._isDoorLocked = value;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityController"/> class.
        /// </summary>
        public SecurityController(string deviceId) : base(deviceId, _readingTypes)
        {
            Loudness = NO_READING;
            Motion = NO_READING;
            Vibration = NO_READING;
            Luminosity = NO_READING;
            DoorOpen = NO_READING;
        }

        /// <summary>
        /// Adds a reading to the corrosponding property based on the reading type and updates list.
        /// </summary>
        /// <param name="reading">The reading to add</param>
        public override void AddReading(Reading reading)
        {
            base.AddReading(reading);
            switch (reading.Type)
            {
                case ReadingType.LOUDNESS:
                    Loudness = reading;
                    break;
                case ReadingType.MOTION:
                    Motion = reading;
                    break;
                case ReadingType.VIBRATION:
                    Vibration = reading;
                    break;
                case ReadingType.LUMINOSITY:
                    Luminosity = reading;
                    break;
                case ReadingType.DOOROPEN:
                    DoorOpen = reading;
                    break;
            }
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
                }
                else if (sensorValue < lowThreshold)
                {
                    health = "Needs Attention";
                }
                else
                {
                    health = "Healthy";
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

        /// <summary>
        /// Handles the IOTService ConnectionStopped event.
        /// </summary>
        public override void IOTService_ConnectionStopped()
        {
            Loudness = NO_READING;
            Motion = NO_READING;
            Vibration = NO_READING;
            Luminosity = NO_READING;
        }

        /// <summary>
        /// Gets the initial actuator states for the controller.
        /// </summary>
        /// <returns></returns>
        public override async Task GetInitialActuatorStates()
        {
            IsDoorLocked = await GetActuatorState(Actuator.SERVO);
        }
    }
}

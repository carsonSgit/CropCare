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
        public HealthState LoudnessHealth { get; set; }

        /// <summary>
        /// Represents latest motion reading
        /// </summary>
        public Reading Motion { get; set; }
        public HealthState MotionHealth { get; set; }

        /// <summary>
        /// Represents latest vibration reading
        /// </summary>
        public Reading Vibration { get; set; }
        public HealthState VibrationHealth { get; set; }

        /// <summary>
        /// Represents latest luminosity reading
        /// </summary>
        public Reading Luminosity { get; set; }
        public HealthState LuminosityHealth { get; set; }

        /// <summary>
        /// Represents latest door open reading
        /// </summary>
        public Reading DoorOpen { get; set; }
        public HealthState DoorOpenHealth { get; set; }

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
                    _isDoorLocked = !value;  
                }
                else
                {
                    Task.Run(async () => await UpdateActuatorState(Actuator.SERVO, value));
                    this._isDoorLocked = value;
                }
            }
        }

        private Dictionary<string, double[]> _healthyRanges = new Dictionary<string, double[]>
        {
            { ReadingType.LOUDNESS, new double[] { 26, 29, 24, 32} }, // LOWER_HEALTHY, UPPER_HEALTHY, LOWER_CAUTION, UPPER_CAUTION
            { ReadingType.LUMINOSITY, new double[] { 75, 95, 65, 95 } },
        };

        protected override Dictionary<string, double[]> HealthyRanges { get => _healthyRanges; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityController"/> class.
        /// </summary>
        public SecurityController(string deviceId) : base(deviceId, _readingTypes)
        {
            Loudness = NO_READING;
            LoudnessHealth = HealthState.Unknown;
            Motion = NO_READING;
            MotionHealth = HealthState.Unknown;
            Vibration = NO_READING;
            VibrationHealth = HealthState.Unknown;
            Luminosity = NO_READING;
            LuminosityHealth = HealthState.Unknown;
            DoorOpen = NO_READING;
            DoorOpenHealth = HealthState.Unknown;
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
                    LoudnessHealth = ConvertReadingToHealth(reading);
                    break;
                case ReadingType.MOTION:
                    Motion = reading;
                    MotionHealth = ConvertReadingToHealth(reading);
                    break;
                case ReadingType.VIBRATION:
                    Vibration = reading;
                    VibrationHealth = ConvertReadingToHealth(reading);
                    break;
                case ReadingType.LUMINOSITY:
                    Luminosity = reading;
                    LuminosityHealth = ConvertReadingToHealth(reading);
                    break;
                case ReadingType.DOOROPEN:
                    DoorOpen = reading;
                    DoorOpenHealth = ConvertReadingToHealth(reading);
                    break;
            }
        }

        protected override HealthState ConvertReadingToHealth(Reading reading)
        {
            if(reading.Type == ReadingType.LOUDNESS)
            {
                if(reading.Value == "Quiet")
                {
                    return HealthState.Healthy;
                }
                if(reading.Value == "Noisy")
                {
                    return HealthState.Caution;
                }
                else
                {
                    return HealthState.Critical;
                }
            }
            if (reading.Type == ReadingType.DOOROPEN)
            {
                if (reading.Value == "True")
                {
                    return HealthState.Healthy;
                }
                else
                {
                    return HealthState.Critical;
                }
            }
            if (reading.Value.GetType() == typeof(string))
            {
                if(reading.Value == "False")
                {
                    return HealthState.Healthy;
                }
                else
                {
                    return HealthState.Critical;
                }
            }
            return base.ConvertReadingToHealth(reading);
        }

        /// <summary>
        /// Handles the IOTService ConnectionStopped event.
        /// </summary>
        public override void IOTService_ConnectionStopped()
        {
            Loudness = NO_READING;
            LoudnessHealth = HealthState.Unknown;
            Motion = NO_READING;
            MotionHealth = HealthState.Unknown;
            Vibration = NO_READING;
            VibrationHealth = HealthState.Unknown;
            Luminosity = NO_READING;
            LuminosityHealth = HealthState.Unknown;
            DoorOpen = NO_READING;
            DoorOpenHealth = HealthState.Unknown;
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

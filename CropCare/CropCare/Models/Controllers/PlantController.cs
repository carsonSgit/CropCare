using System.ComponentModel;

namespace CropCare.Models.Controllers
{
    // Team Name: CropCare
    // Team Members: Kevin Baggott, Cristiano Fazi and Carson Spriggs-Audet
    // Date: April 29th 2023, 6th Semester
    // Course Name: Application Development and Connected Objects
    // Description: Represents a controller for managing plant-related sensors and actuators.
    public class PlantController : BaseController, INotifyPropertyChanged
    {
        private static readonly string[] _readingTypes = new string[] { ReadingType.TEMPERATURE, ReadingType.HUMIDITY, ReadingType.MOISTURE, ReadingType.WATERLEVEL };

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Represents the types of readings that the controller can have.
        /// </summary>
        public string[] ReadingTypes { get => _readingTypes; }


        private Dictionary<string, double[]> _healthyRanges = new Dictionary<string, double[]>
        {
            { ReadingType.TEMPERATURE, new double[] { 26, 29, 24, 32} }, // LOWER_HEALTHY, UPPER_HEALTHY, LOWER_CAUTION, UPPER_CAUTION
            { ReadingType.HUMIDITY, new double[] { 75, 95, 65, 95 } },
            { ReadingType.MOISTURE, new double[] { 26, 29, 24, 32} },
            { ReadingType.WATERLEVEL, new double[] { 26, 29, 24, 32 } }
        };

        protected override Dictionary<string, double[]> HealthyRanges { get => _healthyRanges; }

        /// <summary>
        /// Represents latest temperature reading
        /// </summary>
        public Reading Temperature { get; set; }
        public HealthState TemperatureHealth { get; set; }

        /// <summary>
        /// Represents latest humidity reading
        /// </summary>
        public Reading Humidity { get; set; }
        public HealthState HumidityHealth { get; set; }

        /// <summary>
        /// Represents latest moisture reading
        /// </summary>
        public Reading Moisture { get; set; }
        public HealthState MoistureHealth { get; set; }

        /// <summary>
        /// Represents latest water level reading
        /// </summary>
        public Reading WaterLevel { get; set; }
        public HealthState WaterLevelHealth { get; set; }

        private bool _isFanOn;
        /// <summary>
        /// Represents the state of the fan actuator.
        /// </summary>
        public bool IsFanOn
        {
            get
            {
                return this._isFanOn;
            }
            set
            {
                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    Console.WriteLine("No Internet Connection");
                    _isFanOn = !value;
                }
                else
                {
                    Task.Run(async () => await UpdateActuatorState(Actuator.FAN, value));
                    this._isFanOn = value;
                }
            }
        }

        private bool _isLedOn;

        /// <summary>
        /// Represents the state of the LED actuator.
        /// </summary>
        public bool IsLedOn
        {
            get
            {
                return this._isLedOn;
            }
            set
            {
                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    Console.WriteLine("No Internet Connection");
                    _isLedOn = !value;
                }
                else
                {
                    Task.Run(async () => await UpdateActuatorState(Actuator.LED, value));
                    this._isLedOn = value;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlantController"/> class.
        /// </summary>
        public PlantController(string deviceId) : base(deviceId, _readingTypes)
        {
            Temperature = NO_READING;
            TemperatureHealth = HealthState.Unknown;
            Humidity = NO_READING;
            HumidityHealth = HealthState.Unknown;
            Moisture = NO_READING;
            MoistureHealth = HealthState.Unknown;
            WaterLevel = NO_READING;
            WaterLevelHealth = HealthState.Unknown;
        }

        /// <summary>
        /// Adds a reading to the corresponding property based on the reading type and updates list.
        /// </summary>
        /// <param name="reading">The reading to add</param>
        public override void AddReading(Reading reading)
        {
            base.AddReading(reading);
            switch (reading.Type)
            {
                case ReadingType.TEMPERATURE:
                    Temperature = reading;
                    TemperatureHealth = ConvertReadingToHealth(reading);
                    break;
                case ReadingType.HUMIDITY:
                    Humidity = reading;
                    HumidityHealth = ConvertReadingToHealth(reading);
                    break;
                case ReadingType.MOISTURE:
                    Moisture = reading;
                    MoistureHealth = ConvertReadingToHealth(reading);
                    break;
                case ReadingType.WATERLEVEL:
                    WaterLevel = reading;
                    WaterLevelHealth = ConvertReadingToHealth(reading);
                    break;
            }
        }

        /// <summary>
        /// Handles the event when the IOTService connection is stopped.
        /// </summary>
        public override void IOTService_ConnectionStopped()
        {
            Temperature = NO_READING;
            TemperatureHealth = HealthState.Unknown;
            Humidity = NO_READING;
            HumidityHealth = HealthState.Unknown;
            Moisture = NO_READING;
            MoistureHealth = HealthState.Unknown;
            WaterLevel = NO_READING;
            WaterLevelHealth = HealthState.Unknown;
        }

        /// <summary>
        /// This method should get the initial actuator states.
        /// </summary>
        /// <returns></returns>
        public override async Task GetInitialActuatorStates()
        {
            IsFanOn = await GetActuatorState(Actuator.FAN);
            IsLedOn = await GetActuatorState(Actuator.LED);
        }
    }
}

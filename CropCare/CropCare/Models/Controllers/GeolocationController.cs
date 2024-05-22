using System.ComponentModel;

namespace CropCare.Models.Controllers
{
    // Team Name: CropCare
    // Team Members: Kevin Baggott, Cristiano Fazi and Carson Spriggs-Audet
    // Date: April 29th 2023, 6th Semester
    // Course Name: Application Development and Connected Objects
    // Description: Represents a controller for geolocation-related sensors and actuators.

    public class GeolocationController : BaseController, INotifyPropertyChanged
    {
        private static readonly string[] _readingTypes = new string[] { ReadingType.LATITUDE, ReadingType.LONGITUDE, ReadingType.PITCH, ReadingType.ROLL };

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Represents the types of readings that the controller can have.
        /// </summary>
        public string[] ReadingTypes { get => _readingTypes; }

        /// <summary>
        /// Represents latest latitude reading
        /// </summary>
        public Reading Latitude { get; set; }

        /// <summary>
        /// Represents latest longitude reading
        /// </summary>
        public Reading Longitude { get; set; }

        /// <summary>
        /// Represents latest pitch reading
        /// </summary>
        public Reading Pitch { get; set; }

        /// <summary>
        /// Represents latest roll reading
        /// </summary>
        public Reading Roll { get; set; }

        private bool _isBuzzerOn;
        /// <summary>
        /// Represents the state of the buzzer actuator.
        /// </summary>
        public bool IsBuzzerOn 
        {
            get
            {
                return this._isBuzzerOn;
            }
            set
            {
                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    Console.WriteLine("No Internet Connection");
                }
                else
                {
                    Task.Run(async () => await UpdateActuatorState(Actuator.BUZZER, value));
                    this._isBuzzerOn = value;
                }
            }
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
                case ReadingType.LATITUDE:
                    Latitude = reading;
                    break;
                case ReadingType.LONGITUDE:
                    Longitude = reading;
                    break;
                case ReadingType.PITCH:
                    Pitch = reading;
                    break;
                case ReadingType.ROLL:
                    Roll = reading;
                    break;
            }
        }

        /// <summary>
        /// Handles the event when the IOTService connection is stopped.
        /// </summary>
        public override void IOTService_ConnectionStopped()
        {
            Pitch = NO_READING;
            Roll = NO_READING;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeolocationController"/> class.
        /// </summary>
        /// <param name="deviceId">The iot device id to target</param>
        public GeolocationController(string deviceId): base(deviceId, _readingTypes)
        {
            Latitude = new Reading(ReadingType.LATITUDE, "°", "0");
            Longitude = new Reading(ReadingType.LONGITUDE, "°", "0");
            Pitch = NO_READING;
            Roll = NO_READING;     
        }

        /// <summary>
        /// Gets the initial actuator states for the controller.
        /// </summary>
        /// <returns></returns>
        public async override Task GetInitialActuatorStates()
        {
            IsBuzzerOn = await GetActuatorState(Actuator.BUZZER);
        }
    }
}
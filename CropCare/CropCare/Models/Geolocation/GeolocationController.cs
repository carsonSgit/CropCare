using CropCare.Interfaces;

namespace CropCare.Models.Geolocation
{
    // Team Name: CropCare
    // Team Members: Kevin Baggott, Cristiano Fazi and Carson Spriggs-Audet
    // Date: April 29th 2023, 6th Semester
    // Course Name: Application Development and Connected Objects
    // Description: Represents a controller for geolocation-related sensors and actuators.
    public class GeolocationController
    {
        /// <summary>
        /// Gets or sets the buzzer actuator.
        /// </summary>
        public Buzzer Buzzer { get; set; }

        /// <summary>
        /// Gets or sets the GPS sensor.
        /// </summary>
        public GPS GPS { get; set; }

        /// <summary>
        /// Gets or sets the accelerometer sensor.
        /// </summary>
        public Accelerometer Accelerometer { get; set; }

        public List<ISensor> Sensors {get; set;}

        /// <summary>
        /// Initializes a new instance of the <see cref="GeolocationController"/> class.
        /// </summary>
        public GeolocationController()
        {
            Buzzer = new Buzzer();
            GPS = new GPS();
            Accelerometer = new Accelerometer();

            Sensors = new List<ISensor>()
            {
                GPS,
                Accelerometer,
            };
        }
    }
}
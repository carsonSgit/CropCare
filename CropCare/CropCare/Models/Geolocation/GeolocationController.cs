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

        /// <summary>
        /// Gets or sets the device ID.
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// Gets the pitch value from the accelerometer sensor.
        /// </summary>
        public double Pitch => Double.Parse(Accelerometer.ReadSensor().FirstOrDefault(r => ReadingType.PITCH == r.Type).Value);

        /// <summary>
        /// Gets the roll value from the accelerometer sensor.
        /// </summary>
        public double Roll => Double.Parse(Accelerometer.ReadSensor().FirstOrDefault(r => ReadingType.ROLL == r.Type).Value);

        /// <summary>
        /// Gets the unit for pitch and roll values.
        /// </summary>
        public string PitchRollUnit => ReadingUnit.DEGREE;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeolocationController"/> class with the specified device ID.
        /// </summary>
        /// <param name="deviceId">The device ID.</param>
        public GeolocationController(string deviceId)
        {
            Buzzer = new Buzzer();
            GPS = new GPS();
            Accelerometer = new Accelerometer();
            DeviceId = deviceId;
        }

        /// <summary>
        /// Gets the latitude value from the GPS sensor.
        /// </summary>
        /// <returns>The latitude value.</returns>
        public double Latitude() => Double.Parse(GPS.ReadSensor().FirstOrDefault(r => ReadingType.LATITUDE == r.Type).Value);

        /// <summary>
        /// Gets the longitude value from the GPS sensor.
        /// </summary>
        /// <returns>The longitude value.</returns>
        public double Longitude() => Double.Parse(GPS.ReadSensor().FirstOrDefault(r => ReadingType.LONGITUDE == r.Type).Value);
    }
}
using CropCare.Interfaces;
using System.Collections.ObjectModel;
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

        public void ToggleBuzzer()
        {
            //Call command for buzzer
            return;
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

        public override void IOTService_ConnectionStopped()
        {
            Pitch = NO_READING;
            Roll = NO_READING;
        }

        public GeolocationController(): base(_readingTypes)
        {
            Latitude = new Reading(ReadingType.LATITUDE, "°", "0");
            Longitude = new Reading(ReadingType.LONGITUDE, "°", "0");
            Pitch = NO_READING;
            Roll = NO_READING;
        }
    }
}
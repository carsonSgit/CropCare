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

        public Reading Latitude { get; set; }

        public Reading Longitude { get; set; }

        public Reading Pitch { get; set; }

        public Reading Roll { get; set; }

        public void ToggleBuzzer()
        {
            //Call command for buzzer
            return;
        }

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

        public GeolocationController(): base(_readingTypes) { }
    }
}
using CropCare.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace CropCare.Models.Geolocation
{
    // Team Name: CropCare
    // Team Members: Kevin Baggott, Cristiano Fazi and Carson Spriggs-Audet
    // Date: April 29th 2023, 6th Semester
    // Course Name: Application Development and Connected Objects
    // Description: Represents a controller for geolocation-related sensors and actuators.

    public class GeolocationController : INotifyPropertyChanged
    {
        private string[] _readingTypes = new string[] { ReadingType.LATITUDE, ReadingType.LONGITUDE, ReadingType.PITCH, ReadingType.ROLL };

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Reading> LatitudeReadings {get; set;}

        public ObservableCollection<Reading> LongitudeReadings { get; set; }

        public ObservableCollection<Reading> PitchReadings { get; set; }

        public ObservableCollection<Reading> RollReadings { get; set; }

        public Reading Latitude { get; set; }

        public Reading Longitude { get; set; }

        public Reading Pitch { get; set; }

        public Reading Roll { get; set; }

        public void ToggleBuzzer()
        {
            //Call command for buzzer
            return;
        }

        public bool ValidateReading(Reading reading)
        {
            if (reading == null)
            {
                return false;
            }
            if(!_readingTypes.Contains(reading.Type))
            {
                return false;
            }
            return true;
        }

        public void AddReading(Reading reading)
        {
            if (ValidateReading(reading))
            {
                Readings.Add(reading);
            }
        }

        public GeolocationController()
        {
            Readings = new ObservableCollection<Reading>();
        }
    }
}
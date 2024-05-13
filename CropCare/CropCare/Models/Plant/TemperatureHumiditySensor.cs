using CropCare.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace CropCare.Models.Plant
{
    // Team Name: CropCare
    // Team Members: Kevin Baggott, Cristiano Fazi and Carson Spriggs-Audet
    // Date: April 29th 2023, 6th Semester
    // Course Name: Application Development and Connected Objects
    // Description: Represents a temperature and humidity sensor.
    public class TemperatureHumiditySensor : ISensor, INotifyPropertyChanged
    {
        private ObservableCollection<Reading> _readings;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Reading> Readings { get => _readings; }

        public double Temperature
        {
            get => _readings[0].Value;
        }
        public string TemperatureUnit
        {
            get => _readings[0].Unit;
        }

        public double Humidity
        {
            get => _readings[1].Value;
        }
        public string HumidityUnit
        {
            get => _readings[1].Unit;
        }

        public TemperatureHumiditySensor()
        {
            _readings = new ObservableCollection<Reading>()
            {
                new Reading(ReadingType.TEMPERATURE, ReadingUnit.CELCIUS, 25),
                new Reading(ReadingType.HUMIDITY, ReadingUnit.HUMIDITY, 50),
            };
        }

        public void Refresh()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Temperature)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Humidity)));
        }
    }
}

using CropCare.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace CropCare.Models.Security
{
    // Team Name: CropCare
    // Team Members: Kevin Baggott, Cristiano Fazi and Carson Spriggs-Audet
    // Date: April 29th 2023, 6th Semester
    // Course Name: Application Development and Connected Objects
    // Description: Represents a luminosity sensor.
    public class LuminositySensor : ISensor, INotifyPropertyChanged
    {
        private ObservableCollection<Reading> _readings;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Reading> Readings { get => _readings; }

        public double Luminosity { get => double.Parse((string)_readings[0].Value); }
        public string LuminosityUnit { get => _readings[0].Unit; }

        public LuminositySensor()
        {
            _readings = new ObservableCollection<Reading>()
            {
                new Reading(ReadingType.LUMINOSITY, ReadingUnit.LUX, 0),
            };
        }

        public void Refresh()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Luminosity)));
        }
    }
}
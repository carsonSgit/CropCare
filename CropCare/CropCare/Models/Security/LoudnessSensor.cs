using CropCare.Interfaces;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace CropCare.Models.Security
{
    // Team Name: CropCare
    // Team Members: Kevin Baggott, Cristiano Fazi and Carson Spriggs-Audet
    // Date: April 29th 2023, 6th Semester
    // Course Name: Application Development and Connected Objects
    // Description: Represents a loudness sensor.
    public class LoudnessSensor : ISensor, INotifyPropertyChanged
    {
        private ObservableCollection<Reading> _readings;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Reading> Readings { get => _readings; }

        public double Loudness { get => double.Parse((string)_readings[0].Value); }
        public string LoudnessUnit { get => _readings[0].Unit; }

        public void Refresh()
        {
            OnPropertyChanged(nameof(Loudness));
        }

        public LoudnessSensor()
        {
            _readings = new ObservableCollection<Reading>()
            {
                new Reading(ReadingType.LOUDNESS, ReadingUnit.LOUDNESS, 0),
            };
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
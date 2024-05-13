using CropCare.Interfaces;
using System.Collections.ObjectModel;

namespace CropCare.Models.Security
{
    // Team Name: CropCare
    // Team Members: Kevin Baggott, Cristiano Fazi and Carson Spriggs-Audet
    // Date: April 29th 2023, 6th Semester
    // Course Name: Application Development and Connected Objects
    // Description: Represents a sensor for detecting vibrations.
    public class VibrationSensor : ISensor
    {
        private ObservableCollection<Reading> _readings;

        public ObservableCollection<Reading> Readings { get => _readings; }

        public bool Vibration { get => _readings[0].Value; }

        public VibrationSensor()
        {
            _readings = new ObservableCollection<Reading>()
            {
                new Reading(ReadingType.VIBRATION, ReadingUnit.NONE, false),
            };
        }

        public void Refresh()
        {
            return;
        }
    }
}
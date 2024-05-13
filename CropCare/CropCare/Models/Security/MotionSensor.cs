using CropCare.Interfaces;
using System.Collections.ObjectModel;

namespace CropCare.Models.Security
{
    // Team Name: CropCare
    // Team Members: Kevin Baggott, Cristiano Fazi and Carson Spriggs-Audet
    // Date: April 29th 2023, 6th Semester
    // Course Name: Application Development and Connected Objects
    // Description: Represents a motion sensor.
    public class MotionSensor : ISensor
    {
        private ObservableCollection<Reading> _readings;

        public ObservableCollection<Reading> Readings { get => _readings; }

        public bool Motion { get => _readings[0].Value; }


        public MotionSensor()
        {
            _readings = new ObservableCollection<Reading>()
            {
                new Reading(ReadingType.MOTION, ReadingUnit.NONE, false),
            };
        }

        public void Refresh()
        {
            return;
        }
    }
}

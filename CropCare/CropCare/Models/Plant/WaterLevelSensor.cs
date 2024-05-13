using CropCare.Interfaces;
using System.Collections.ObjectModel;

namespace CropCare.Models.Plant
{
    // Team Name: CropCare
    // Team Members: Kevin Baggott, Cristiano Fazi and Carson Spriggs-Audet
    // Date: April 29th 2023, 6th Semester
    // Course Name: Application Development and Connected Objects
    // Description: Represents a water level sensor.
    public class WaterLevelSensor : ISensor
    {
        private ObservableCollection<Reading> _readings;

        public ObservableCollection<Reading> Readings { get => _readings; }

        public double WaterLevel
        {
            get => _readings[0].Value;
        }
        public string WaterLevelUnit
        {
            get => _readings[0].Unit;
        }

        public WaterLevelSensor()
        {
            _readings = new ObservableCollection<Reading>()
            {
                new Reading(ReadingType.WATERLEVEL, ReadingUnit.WATERLEVEL, 50),
            };
        }

        public void Refresh()
        {
            return;
        }
    }

}

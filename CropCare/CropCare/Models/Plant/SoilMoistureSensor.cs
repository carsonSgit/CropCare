using CropCare.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace CropCare.Models.Plant
{
    // Team Name: CropCare
    // Team Members: Kevin Baggott, Cristiano Fazi and Carson Spriggs-Audet
    // Date: April 29th 2023, 6th Semester
    // Course Name: Application Development and Connected Objects
    // Description: Represents a soil moisture sensor.
    public class SoilMoistureSensor : ISensor
    {
        private ObservableCollection<Reading> _readings;

        public ObservableCollection<Reading> Readings { get => _readings; }

        public double Moisture
        {
            get => _readings[0].Value;
        }

        public string MoistureUnit
        {
            get => _readings[0].Unit;
        }

        public SoilMoistureSensor()
        {
            _readings = new ObservableCollection<Reading>()
            {
                new Reading(ReadingType.MOISTURE, ReadingUnit.OHMS, 50),
            };
        }

        public void Refresh()
        {
            return;
        }
    }
}

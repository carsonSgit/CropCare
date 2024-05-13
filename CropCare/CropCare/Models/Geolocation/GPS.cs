﻿using CropCare.Interfaces;
using System.Collections.ObjectModel;

namespace CropCare.Models.Geolocation
{
    // Team Name: CropCare
    // Team Members: Kevin Baggott, Cristiano Fazim Carson Spriggs-Audet
    // Date: April 29th 2023, 6th Semester
    // Course Name: Application Development and Connected Objects
    // Description: Represents a GPS sensor.
    public class GPS : ISensor
    {
        private ObservableCollection<Reading> _readings;

        public ObservableCollection<Reading> Readings { get => _readings; }

        public double Latitude
        {
            get => _readings[0].Value;
        }
        public double Longitude
        {
            get => _readings[1].Value;
        }

        public GPS()
        {
            _readings = new ObservableCollection<Reading>()
            {
                new Reading(ReadingType.LATITUDE, ReadingUnit.NONE, 0),
                new Reading(ReadingType.LONGITUDE, ReadingUnit.NONE, 0),
            };
        }

        public void Refresh()
        {
            return;
        }
    }
}

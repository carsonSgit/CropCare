﻿using CropCare.Interfaces;
using System.Collections.ObjectModel;

namespace CropCare.Models.Geolocation
{
    // Team Name: CropCare
    // Team Members: Kevin Baggott, Cristiano Fazi and Carson Spriggs-Audet
    // Date: April 29th 2023, 6th Semester
    // Course Name: Application Development and Connected Objects
    // Description: Represents an accelerometer sensor.
    public class Accelerometer : ISensor
    {
        private ObservableCollection<Reading> _readings;

        public ObservableCollection<Reading> Readings { get => _readings; }

        public double Pitch
        {
            get => _readings[0].Value;
        }

        public string AccelerationUnit
        {
            get => _readings[0].Unit;
        }

        public double Roll
        {
            get => _readings[1].Value;
        }

        public Accelerometer()
        {
            _readings = new ObservableCollection<Reading>()
            {
                new Reading(ReadingType.PITCH, ReadingUnit.DEGREE, 0),
                new Reading(ReadingType.ROLL, ReadingUnit.DEGREE, 0),
            };
        }

        public void Refresh()
        {
            return;
        }
    }
}
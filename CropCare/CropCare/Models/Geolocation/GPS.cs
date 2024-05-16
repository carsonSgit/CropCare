﻿using CropCare.Interfaces;

namespace CropCare.Models.Geolocation
{
    // Team Name: CropCare
    // Team Members: Kevin Baggott, Cristiano Fazim Carson Spriggs-Audet
    // Date: April 29th 2023, 6th Semester
    // Course Name: Application Development and Connected Objects
    // Description: Represents a GPS sensor.
    public class GPS : ISensor<double>
    {
        /// <summary>
        /// Reads the GPS sensor.
        /// </summary>
        /// <returns>A list of readings from the GPS sensor.</returns>
        public List<Reading<double>> ReadSensor()
        {
            return new List<Reading<double>>()
            {
                new Reading<double>(ReadingType.LATITUDE, ReadingUnit.DEGREE, 45.484056),
                new Reading<double>(ReadingType.LONGITUDE, ReadingUnit.DEGREE, -73.680421),
            };
        }
    }
}
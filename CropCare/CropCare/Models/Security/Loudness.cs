﻿using CropCare.Interfaces;

namespace CropCare.Models.Security
{
    // Team Name: CropCare
    // Team Members: Kevin Baggott, Cristiano Fazi and Carson Spriggs-Audet
    // Date: April 29th 2023, 6th Semester
    // Course Name: Application Development and Connected Objects
    // Description: Represents a loudness sensor.
    public class Loudness : ISensor<int>
    {
        /// <summary>
        /// Reads the loudness sensor.
        /// </summary>
        /// <returns>A list of readings from the loudness sensor.</returns>
        public List<Reading<int>> ReadSensor()
        {
            return new List<Reading<int>>()
            {
                new Reading<int>(ReadingType.LOUDNESS, ReadingUnit.LOUDNESS, 100),
            };
        }
    }
}
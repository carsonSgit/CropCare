using CropCare.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropCare.Models.Geolocation
{
    public class GPS : ISensor
    {
        public List<Reading> ReadSensor()
        {
            return new List<Reading>()
            {
                new Reading(ReadingType.LATITUDE, ReadingUnit.DEGREE, "45.484056"),
                new Reading(ReadingType.LONGITUDE, ReadingUnit.DEGREE, "-73.680421"),
            };
        }
    }
}

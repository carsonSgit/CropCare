using CropCare.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropCare.Models.Geolocation
{
    public class Accelerometer : ISensor
    {
        public List<Reading> ReadSensor()
        {
            return new List<Reading>()
            {
                new Reading(ReadingType.PITCH, ReadingUnit.DEGREE, "0"),
                new Reading(ReadingType.ROLL, ReadingUnit.DEGREE, "70"),
            };
        }
    }
}

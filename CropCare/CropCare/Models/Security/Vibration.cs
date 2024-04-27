using CropCare.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropCare.Models.Security
{
    public class Vibration : ISensor
    {
        public List<Reading> ReadSensor()
        {
            return new List<Reading>()
            {
                new Reading(ReadingType.VIBRATION, ReadingUnit.NONE, "Vibration detected"),
            };
        }
    }
}

using CropCare.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropCare.Models.Plant
{
    public class WaterLevel : ISensor
    {
        public List<Reading> ReadSensor()
        {
            return new List<Reading>()
            {
                new Reading(ReadingType.WATERLEVEL, ReadingUnit.WATERLEVEL, "100"),
            };
        }
    }
}

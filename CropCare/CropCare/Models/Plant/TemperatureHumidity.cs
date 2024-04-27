using CropCare.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropCare.Models.Plant
{
    public class TemperatureHumidity : ISensor
    {
        public List<Reading> ReadSensor()
        {
            return new List<Reading>()
            {
                new Reading(ReadingType.TEMPERATURE, ReadingUnit.CELCIUS, "20"),
                new Reading(ReadingType.HUMIDITY, ReadingUnit.HUMIDITY, "10"),
            };
        }
    }
}

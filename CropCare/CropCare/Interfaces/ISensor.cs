using CropCare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropCare.Interfaces
{
    public interface ISensor
    {
        public List<Reading> ReadSensor();
    }
}

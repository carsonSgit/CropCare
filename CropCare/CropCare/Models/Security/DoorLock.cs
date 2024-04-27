using CropCare.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropCare.Models.Security
{
    public class DoorLock : IActuator, ISensor
    {
        public string State { get; set; }

        public bool ControlActuator(Command command)
        {
            if (State == nameof(command))
                return false;

            // send command to iot hub
            State = nameof(command);
            // send command to iot hub

            return true;
        }

        public List<Reading> ReadSensor()
        {
            return new List<Reading>
            {
                new Reading(ReadingType.DOORLOCK, ReadingUnit.NONE, (State == nameof(Command.ON)).ToString())
            };
        }
    }
}

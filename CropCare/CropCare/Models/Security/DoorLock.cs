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
        public DoorLock(Command state = Command.OFF)
        {
            State = state.ToString();
        }

        public bool ControlActuator(Command command)
        {
            if (State == command.ToString())
                return false;

            // send command to iot hub
            State = command.ToString();
            // send command to iot hub

            return true;
        }

        public List<Reading> ReadSensor()
        {
            return new List<Reading>
            {
                new Reading(ReadingType.DOORLOCK, ReadingUnit.NONE, State)
            };
        }
    }
}

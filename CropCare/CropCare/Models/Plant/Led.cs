using CropCare.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropCare.Models.Plant
{
    public class Led : IActuator
    {
        public string State { get; set; }
        public Led(Command state = Command.OFF)
        {
            State = state.ToString();
        }

        public bool ControlActuator(Command command)
        {
            if (State == nameof(command))
                return false;

            // send command to iot hub here
            State = nameof(command);
            // send command to iot hub here

            return true;
        }
    }
}

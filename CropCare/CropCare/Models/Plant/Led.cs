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
            if (State == command.ToString())
                return false;

            // send command to iot hub here
            State = command.ToString();

            return true;
        }
    }
}

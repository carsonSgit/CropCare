using CropCare.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropCare.Models.Plant
{
    public class Fan : IActuator
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
    }
}

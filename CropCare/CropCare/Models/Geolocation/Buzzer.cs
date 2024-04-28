using CropCare.Interfaces;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropCare.Models.Geolocation
{
    public class Buzzer : IActuator, INotifyPropertyChanged
    {
        public string State { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool ControlActuator(Command command)
        {
            if (State == command.ToString())
                return false;

            // send command to iot hub here
            State = command.ToString();
            // send command to iot hub here
            
            return true;
        }

        public Buzzer()
        {
            State = Command.OFF.ToString();
        }
    }
}

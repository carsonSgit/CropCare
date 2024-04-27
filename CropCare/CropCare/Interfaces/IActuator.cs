using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CropCare.Models;
using Command = CropCare.Models.Command;

namespace CropCare.Interfaces
{
    public interface IActuator
    {
        string State { get; set; }
        bool ControlActuator(Command command);
    }
}

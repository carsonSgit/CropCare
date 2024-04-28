using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropCare.Models.Security
{
    public class SecurityController
    {
        public Loudness Loudness { get; set; }
        public Motion Motion { get; set; }
        public Vibration Vibration { get; set; }
        public DoorLock DoorLock { get; set; }
        public DoorOpener DoorOpener { get; set; }
        public Luminosity Luminosity { get; set; }
        public string DeviceId { get; set; }

        public SecurityController(string deviceId)
        {
            Loudness = new Loudness();
            Motion = new Motion();
            Vibration = new Vibration();
            DoorLock = new DoorLock();
            DoorOpener = new DoorOpener();
            Luminosity = new Luminosity();
            DeviceId = deviceId;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropCare.Models.Geolocation
{
    public class GeolocationController
    {
        public Buzzer Buzzer { get; set; }
        public GPS GPS { get; set; }
        public Accelerometer Accelerometer { get; set; }

        public GeolocationController()
        {
            Buzzer = new Buzzer();
            GPS = new GPS();
            Accelerometer = new Accelerometer();
        }
    }
}

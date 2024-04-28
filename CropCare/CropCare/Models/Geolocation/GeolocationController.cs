using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public string DeviceId { get; set; }

        public double Pitch => Double.Parse(Accelerometer.ReadSensor().FirstOrDefault(r => ReadingType.PITCH == r.Type).Value);
        public double Roll => Double.Parse(Accelerometer.ReadSensor().FirstOrDefault(r => ReadingType.ROLL == r.Type).Value);
        public string PitchRollUnit => ReadingUnit.DEGREE;


        public double Latitude() => Double.Parse(GPS.ReadSensor().FirstOrDefault(r => ReadingType.LATITUDE == r.Type).Value);
        public double Longitude() => Double.Parse(GPS.ReadSensor().FirstOrDefault(r => ReadingType.LONGITUDE == r.Type).Value);


        public GeolocationController(string deviceId)
        {
            Buzzer = new Buzzer();
            GPS = new GPS();
            Accelerometer = new Accelerometer();
            DeviceId = deviceId;
        }
    }
}

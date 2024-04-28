using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CropCare.Interfaces;
using System.Threading.Tasks;
using System.ComponentModel;

namespace CropCare.Models.Security
{
    public class SecurityController : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Loudness Loudness { get; set; }
        public Motion Motion { get; set; }
        public Vibration Vibration { get; set; }
        public DoorLock DoorLock { get; set; }
        public DoorOpener DoorOpener { get; set; }
        public Luminosity Luminosity { get; set; }
        public string DeviceId { get; set; }
        public string LuminosityReading => GetLuminosityReading();
        public string LuminosityHealth => UpdateReadingHealthLabel(LuminosityReading, 'n', 1000, 500);
        public string LoudnessReading => GetLoudnessReading();
        public string LoudnessHealth => UpdateReadingHealthLabel(LoudnessReading, 'u', 1000, 500);
        public string MotionReading => GetMotionReading();
        public string MotionHealth => UpdateReadingHealthLabel(MotionReading, ' ', 1000, 500);
        public string VibrationReading => GetVibrationReading();
        public string VibrationHealth => UpdateReadingHealthLabel(VibrationReading, ' ', 1000, 500);

        public string DoorLockState
        {
            get => GetDoorLockReading();
            set
            {
                if (DoorLock.State != value)
                {
                    DoorLock.State = value;
                    OnPropertyChanged(nameof(DoorLockState));
                }
            }
        }
        private string _doorOpenerState;
        public string DoorOpenerState
        {
            get => _doorOpenerState;
            set
            {
                if (_doorOpenerState != value)
                {
                    _doorOpenerState = value;
                    OnPropertyChanged(nameof(DoorOpenerState));
                }
            }
        }

        public SecurityController(string deviceId)
        {
            Loudness = new Loudness();
            Motion = new Motion();
            Vibration = new Vibration();
            DoorLock = new DoorLock();
            DoorOpener = new DoorOpener();
            Luminosity = new Luminosity();
            DeviceId = deviceId;

            DoorOpenerState = UpdateStateHealthLabel(DoorOpener.State);
            DoorLockState = UpdateStateHealthLabel(DoorLock.State);
        }

        public string GetLoudnessReading() => GetSensorReading(Loudness, ReadingType.LOUDNESS);
        public string GetLuminosityReading() => GetSensorReading(Luminosity, ReadingType.LUMINOSITY);
        public string GetMotionReading() => GetSensorReading(Motion, ReadingType.MOTION);
        public string GetVibrationReading() => GetSensorReading(Vibration, ReadingType.VIBRATION); 
        public string GetDoorLockReading() => DoorLock.ReadSensor().FirstOrDefault()?.Value;


        private string GetSensorReading<T>(T sensor, string readingType) where T : ISensor
        {
            var reading = sensor.ReadSensor().FirstOrDefault(r => r.Type.Equals(readingType, StringComparison.OrdinalIgnoreCase));
            return reading != null ? $"{reading.Value}{reading.Unit}" : "N/A";
        }

        public string UpdateReadingHealthLabel(string sensorReading, char unitSymbol, double highThreshold, double lowThreshold)
        {
            string health = "";
            double sensorValue;
            if (double.TryParse(sensorReading.Split(unitSymbol)[0], out sensorValue))
            {
                if (sensorValue > highThreshold)
                {
                    health = "Critical";
                    //healthLbl.TextColor = Colors.Red;
                }
                else if (sensorValue < lowThreshold)
                {
                    health = "Needs Attention";
                    //healthLbl.TextColor = Colors.Red;
                }
                else
                {
                    health = "Healthy";
                    //healthLbl.TextColor = Colors.Green;
                }
            }

            return health;
        }

        public string UpdateStateHealthLabel(string actuatorState)
        {
            string health = "";
            if (actuatorState.Equals(Command.ON.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                health = "Open";
                //healthLbl.TextColor = Colors.Green;
            }
            else if (actuatorState.Equals(Command.OFF.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                health = "Closed";
                //healthLbl.TextColor = Colors.Red;
            }
            return health;
        }
    }
}

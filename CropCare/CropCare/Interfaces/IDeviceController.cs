using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropCare.Interfaces
{
    public interface IDeviceController
    {
        public int GetSoilMoistureAsync();
        public int GetWaterLevelAsync();
        public int GetLuminosityAsync();
        public bool GetLightAsync();
        public bool GetDoorLockedAsync();
        public bool GetDoorOpenAsync();
        public int GetPitchAsync();
        public int GetRollAsync();
        public string GetLocationAsync();
        public bool GetVibrationAsync();
        public int GetNoiseAsync();
        public bool GetFanStateAsync();
        public bool GetBuzzerStateAsync();
        public int GetTemperatureAsync();
        public int GetHumidityAsync();

        public void SetLightAsync(bool state);
        public void SetDoorLockedAsync(bool state);
        public void SetDoorOpenAsync(bool state);
        public void SetFanStateAsync(bool state);
        public void SetBuzzerStateAsync(bool state);
    }
}

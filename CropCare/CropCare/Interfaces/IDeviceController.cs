namespace CropCare.Interfaces
{
    /// <summary>
    /// Interface for controlling devices.
    /// </summary>
    public interface IDeviceController
    {
        // Methods to get sensor readings asynchronously
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

        // Methods to set actuator states asynchronously
        public void SetLightAsync(bool state);
        public void SetDoorLockedAsync(bool state);
        public void SetDoorOpenAsync(bool state);
        public void SetFanStateAsync(bool state);
        public void SetBuzzerStateAsync(bool state);
    }
}

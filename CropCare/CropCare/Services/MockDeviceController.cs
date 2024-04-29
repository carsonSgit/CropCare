using CropCare.Interfaces;

namespace CropCare.Services
{
    class MockDeviceController : IDeviceController
    {
        public bool GetBuzzerStateAsync()
        {
            return false;
        }

        public bool GetDoorLockedAsync()
        {
            return false;
        }

        public bool GetDoorOpenAsync()
        {
            return true;
        }

        public bool GetFanStateAsync()
        {
            return true;
        }

        public int GetHumidityAsync()
        {
            throw new NotImplementedException();
        }

        public bool GetLightAsync()
        {
            return true;
        }

        public string GetLocationAsync()
        {
            return "blah";
        }

        public int GetLuminosityAsync()
        {
            throw new NotImplementedException();
        }

        public int GetNoiseAsync()
        {
            throw new NotImplementedException();
        }

        public int GetPitchAsync()
        {
            throw new NotImplementedException();
        }

        public int GetRollAsync()
        {
            throw new NotImplementedException();
        }

        public int GetSoilMoistureAsync()
        {
            throw new NotImplementedException();
        }

        public int GetTemperatureAsync()
        {
            throw new NotImplementedException();
        }

        public bool GetVibrationAsync()
        {
            return true;
        }

        public int GetWaterLevelAsync()
        {
            throw new NotImplementedException();
        }

        public void SetBuzzerStateAsync(bool state)
        {
            throw new NotImplementedException();
        }

        public void SetDoorLockedAsync(bool state)
        {
            throw new NotImplementedException();
        }

        public void SetDoorOpenAsync(bool state)
        {
            throw new NotImplementedException();
        }

        public void SetFanStateAsync(bool state)
        {
            throw new NotImplementedException();
        }

        public void SetLightAsync(bool state)
        {
            throw new NotImplementedException();
        }
    }
}

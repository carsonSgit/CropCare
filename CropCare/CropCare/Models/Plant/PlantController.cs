using System;
using System.ComponentModel;
using System.Linq;
using CropCare.Interfaces;
using CropCare.Models.Plant;

namespace CropCare.Models.Plant
{
    public class PlantController: INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Fan Fan { get; }
        public Led Led { get; }
        public SoilMoisture SoilMoisture { get; }
        public TemperatureHumidity Temperature { get; }
        public WaterLevel WaterLevel { get; }

        public string TemperatureReading => GetTemperatureReading();
        public string TemperatureHealth => UpdateReadingHealthLabel(TemperatureReading, '°', 30, 10);
        public string HumidityReading => GetHumidityReading();
        public string HumidityHealth => UpdateReadingHealthLabel(HumidityReading, '%', 80, 20);
        public string MoistureReading => GetMoistureReading();
        public string MoistureHealth => UpdateReadingHealthLabel(MoistureReading, 'Ω', 500, 200);
        public string WaterLevelReading => GetWaterLevelReading();
        public string WaterLevelHealth => UpdateReadingHealthLabel(WaterLevelReading, 'w', 80, 20);
        private string _ledState;
        public string LedState
        {
            get => _ledState;
            set
            {
                if (_ledState != value)
                {
                    _ledState = value;
                    OnPropertyChanged(nameof(LedState));
                }
            }
        }
        private string _fanState;
        public string FanState
        {
            get => _fanState;
            set
            {
                if (_fanState != value)
                {
                    _fanState = value;
                    OnPropertyChanged(nameof(FanState));
                }
            }
        }



        public PlantController()
        {
            Fan = new Fan();
            Led = new Led();
            SoilMoisture = new SoilMoisture();
            Temperature = new TemperatureHumidity();
            WaterLevel = new WaterLevel();

            FanState = UpdateStateHealthLabel(Fan.State);
            LedState = UpdateStateHealthLabel(Led.State);
        }

        public string GetTemperatureReading() => GetSensorReading(Temperature, ReadingType.TEMPERATURE);

        public string GetHumidityReading() => GetSensorReading(Temperature, ReadingType.HUMIDITY);

        public string GetMoistureReading() => GetSensorReading(SoilMoisture, ReadingType.MOISTURE);

        public string GetWaterLevelReading() => GetSensorReading(WaterLevel, ReadingType.WATERLEVEL);

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
                health= "On";
                //healthLbl.TextColor = Colors.Green;
            }
            else if (actuatorState.Equals(Command.OFF.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                health = "Off";
                //healthLbl.TextColor = Colors.Red;
            }
            return health;
        }
    }
}

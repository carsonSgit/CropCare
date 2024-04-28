using System;
using System.Linq;
using CropCare.Interfaces;
using CropCare.Models.Plant;

namespace CropCare.Models.Plant
{
    public class PlantController
    {
        public Fan Fan { get; }
        public Led Led { get; }
        public SoilMoisture SoilMoisture { get; }
        public TemperatureHumidity Temperature { get; }
        public WaterLevel WaterLevel { get; }
        public string DeviceId { get; set; }

        public PlantController(string deviceId)
        {
            Fan = new Fan();
            Led = new Led();
            SoilMoisture = new SoilMoisture();
            Temperature = new TemperatureHumidity();
            WaterLevel = new WaterLevel();
            DeviceId = deviceId;
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

        public void UpdateReadingHealthLabel(string sensorReading, Label healthLbl, char unitSymbol, double highThreshold, double lowThreshold)
        {
            double sensorValue;
            if (double.TryParse(sensorReading.Split(unitSymbol)[0], out sensorValue))
            {
                if (sensorValue > highThreshold)
                {
                    healthLbl.Text = "High";
                    healthLbl.TextColor = Colors.Red;
                }
                else if (sensorValue < lowThreshold)
                {
                    healthLbl.Text = "Low";
                    healthLbl.TextColor = Colors.Red;
                }
                else
                {
                    healthLbl.Text = "Normal";
                    healthLbl.TextColor = Colors.Green;
                }
            }
        }
        public void UpdateStateHealthLabel(string actuatorState, Label healthLbl)
        {
            if (actuatorState.Equals(Command.ON.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                healthLbl.Text = "On";
                healthLbl.TextColor = Colors.Green;
            }
            else if (actuatorState.Equals(Command.OFF.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                healthLbl.Text = "Off";
                healthLbl.TextColor = Colors.Red;
            }
        }
    }
}

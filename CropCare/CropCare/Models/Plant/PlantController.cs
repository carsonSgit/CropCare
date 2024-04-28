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

        public PlantController()
        {
            Fan = new Fan();
            Led = new Led();
            SoilMoisture = new SoilMoisture();
            Temperature = new TemperatureHumidity();
            WaterLevel = new WaterLevel();
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
    }
}

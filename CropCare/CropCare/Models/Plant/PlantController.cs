using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropCare.Models.Plant
{
    public class PlantController
    {
        public Fan Fan { get; set; }
        public Led Led { get; set; }
        public SoilMoisture SoilMoisture { get; set; }
        public TemperatureHumidity Temperature { get; set; }
        public WaterLevel WaterLevel { get; set; }

        public PlantController()
        {
            Fan = new Fan();
            Led = new Led();
            SoilMoisture = new SoilMoisture();
            Temperature = new TemperatureHumidity();
            WaterLevel = new WaterLevel();
        }
    }
}

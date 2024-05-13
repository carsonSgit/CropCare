using System.ComponentModel;
using CropCare.Interfaces;

namespace CropCare.Models.Controllers
{
    // Team Name: CropCare
    // Team Members: Kevin Baggott, Cristiano Fazi and Carson Spriggs-Audet
    // Date: April 29th 2023, 6th Semester
    // Course Name: Application Development and Connected Objects
    // Description: Represents a controller for managing plant-related sensors and actuators.
    public class PlantController : BaseController, INotifyPropertyChanged
    {
        private static readonly string[] _readingTypes = new string[] { ReadingType.TEMPERATURE, ReadingType.HUMIDITY, ReadingType.MOISTURE, ReadingType.WATERLEVEL };

        public event PropertyChangedEventHandler PropertyChanged;

        public Reading Temperature { get; set; }

        public Reading Humidity { get; set; }

        public Reading Moisture { get; set; }

        public Reading WaterLevel { get; set; }

        public void ToggleFan()
        {
            //Call command for fan
            return;
        }

        public void ToggleLed()
        {
            //Call command for led
            return;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlantController"/> class.
        /// </summary>
        public PlantController() : base(_readingTypes) { }

        public override void AddReading(Reading reading)
        {
            base.AddReading(reading);
            switch (reading.Type)
            {
                case ReadingType.TEMPERATURE:
                    Temperature = reading;
                    break;
                case ReadingType.HUMIDITY:
                    Humidity = reading;
                    break;
                case ReadingType.MOISTURE:
                    Moisture = reading;
                    break;
                case ReadingType.WATERLEVEL:
                    WaterLevel = reading;
                    break;
            }
        }

        /// <summary>
        /// Updates the health label based on the sensor reading and specified thresholds.
        /// </summary>
        /// <param name="sensorReading">The sensor reading.</param>
        /// <param name="unitSymbol">The symbol used to separate the value from the unit.</param>
        /// <param name="highThreshold">The threshold for the high range.</param>
        /// <param name="lowThreshold">The threshold for the low range.</param>
        /// <returns>The health status based on the reading and thresholds.</returns>
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

        /// <summary>
        /// Updates the health label based on the state of the actuator.
        /// </summary>
        /// <param name="actuatorState">The state of the actuator.</param>
        /// <returns>The health status based on the actuator state.</returns>
        public string UpdateStateHealthLabel(string actuatorState)
        {
            string health = "";
            if (actuatorState.Equals(Command.ON.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                health = "On";
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

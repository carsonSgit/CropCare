using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Maui;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using SkiaSharp;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace CropCare.Models.Controllers
{
    public abstract class BaseController
    {
        private string[] _readingTypes;
        
        protected string _invokeGetActuatorStates = "get_actuator_states";

        protected readonly Reading NO_READING = new Reading(ReadingType.NODATA, String.Empty, "NO DATA");
        /// <summary>
        /// A dictionary of readings for the controller.
        /// </summary>
        public Dictionary<string, ObservableCollection<Reading>> Readings { get; set; }

        /// <summary>
        /// Set the healthy ranges for each reading type.
        /// </summary>
        protected abstract Dictionary<string, double[]> HealthyRanges { get; }

        /// <summary>
        /// Lower and upper limits for healthy and caution ranges.
        /// </summary>
        public const byte LOWER_HEALTHY_LIMIT = 0;
        public const byte UPPER_HEALTHY_LIMIT = 1;
        public const byte LOWER_CAUTION_LIMIT = 2;
        public const byte UPPER_CAUTION_LIMIT = 3;


        /// <summary>
        /// A dictionary of charts for the controller.
        /// </summary>
        public Dictionary<string, CartesianChart> Charts { get; set; }
        /// <summary>
        /// The device ID of the controller.
        /// </summary>
        protected string DeviceId { get; set; }

        /// <summary>
        /// Validates that a reading can be added to the controller.
        /// </summary>
        /// <param name="reading">The reading to be checked.</param>
        /// <returns>True if the reading can be added else false.</returns>
        public bool ValidateReading(Reading reading)
        {
            if (reading == null)
            {
                return false;
            }
            if (!_readingTypes.Contains(reading.Type))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Adds a reading to the controller.
        /// </summary>
        /// <param name="reading">The reading to add.</param>
        public virtual void AddReading(Reading reading)
        {
            Readings[reading.Type].Add(reading);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController"/> class.
        /// </summary>
        /// <param name="deviceId">The iot device id to target</param>
        /// <param name="readingTypes">A list of reading types as strings to be added to the controller.</param>
        public BaseController(string deviceId, string[] readingTypes)
        {
            DeviceId = deviceId;
            _readingTypes = readingTypes;
            Readings = new Dictionary<string, ObservableCollection<Reading>>();
            Charts = new Dictionary<string, CartesianChart>();
            foreach (string type in readingTypes)
            {
                Readings.Add(type, new ObservableCollection<Reading>());
                Charts.Add(type, new CartesianChart());
            }
            App.IOTService.ConnectionStopped += IOTService_ConnectionStopped;
        }

        /// <summary>
        /// This handler is called when the IOTService connection is stopped.
        /// </summary>
        public abstract void IOTService_ConnectionStopped();

        /// <summary>
        /// This method updates the chart for the given reading type.
        /// </summary>
        /// <param name="readingType">The reading type to target which chart to update.</param>
        public virtual void UpdateChart(string readingType)
        {
            LineSeries<DateTimePoint>[] series =
            {
                new LineSeries<DateTimePoint>
                {
                    Values = this.Readings[readingType].Select(x => new DateTimePoint(x.TimeStamp, double.Parse(x.Value))),
                    Name = this.Readings[readingType][0].Type + " Over Time",
                    Stroke = new SolidColorPaint(SKColor.Parse("#123c1f")) { StrokeThickness = 3 },
                    Fill = new SolidColorPaint(SKColor.Parse("#d8e2d6")),
                    GeometrySize = 0,
                    GeometryStroke = null,
                    LineSmoothness = 0.95
                }
            };

            Axis[] yAxis = {
                new Axis
                {
                    MinLimit = 0,
                    MaxLimit = (int)(this.Readings[readingType].Select(x => double.Parse(x.Value)).Max() + 10),
                    Name = this.Readings[readingType][0].Type + " (" + this.Readings[readingType][0].Unit + ")",
                    NamePaint = new SolidColorPaint(SKColor.Parse("#4a8e49")),
                    TicksPaint = new SolidColorPaint(SKColor.Parse("#4a8e49")),
                    LabelsPaint = new SolidColorPaint(SKColor.Parse("#4a8e49"))
                }
            };

            Axis[] xAxis =
            {
                new DateTimeAxis
                (
                    TimeSpan.FromSeconds(5),
                    value => value.ToString("MM-dd")
                )
                {
                    Name = "Time",
                    NamePaint = new SolidColorPaint(SKColor.Parse("#4a8e49")),
                    TicksPaint = new SolidColorPaint(SKColor.Parse("#4a8e49")),
                    LabelsPaint = new SolidColorPaint(SKColor.Parse("#4a8e49")),
                    MinLimit = Readings[readingType][Math.Max(Readings[readingType].Count - 10, 0)].TimeStamp.Ticks
                }
            };

            LabelVisual chartTitle = new LabelVisual
            {
                Text = this.Readings[readingType][0].Type + " Over Time",
                TextSize = 18,
                Padding = new LiveChartsCore.Drawing.Padding(15),
                Paint = new SolidColorPaint(SKColor.Parse("#4a8e49"))
            };

            var cartesianChart = new CartesianChart
            {
                Series = series,
                YAxes = yAxis,
                XAxes = xAxis,
                Title = chartTitle
            };

            Charts[readingType] = cartesianChart;
        }

        /// <summary>
        /// Converts a reading to a health state enum.
        /// </summary>
        /// <param name="reading">The reading to check against.</param>
        /// <returns>Health state depending on that readings health ranges.</returns>
        protected virtual HealthState ConvertReadingToHealth(Reading reading)
        {
            if(!double.TryParse(reading.Value, out double dValue))
            {
                return HealthState.Unknown;
            }

            if (dValue >= HealthyRanges[reading.Type][LOWER_HEALTHY_LIMIT]
                && dValue <= HealthyRanges[reading.Type][UPPER_HEALTHY_LIMIT])
            {
                return HealthState.Healthy;
            }
            if (dValue >= HealthyRanges[reading.Type][LOWER_CAUTION_LIMIT]
                && dValue <= HealthyRanges[reading.Type][UPPER_CAUTION_LIMIT])
            {
                return HealthState.Caution;
            }
            else
            {
                return HealthState.Critical;
            }
        }

        /// <summary>
        /// This method should get the initial actuator states.
        /// </summary>
        /// <returns></returns>
        public abstract Task GetInitialActuatorStates();

        /// <summary>
        /// This method should get the overall health of the controller.
        /// </summary>
        /// <returns>The worst healthstate of the sensors</returns>
        public abstract HealthState GetOverallHealth();

        /// <summary>
        /// This method queries iot hub to get the specified actuator state.
        /// </summary>
        /// <param name="actuatorType"></param>
        /// <returns>True if the actuator is on else false.</returns>
        protected async Task<bool> GetActuatorState(string actuatorType)
        {
            try
            {
                const string METHOD = "get_single_actuator_state";

                string payload = $"{{\"target\":\"{actuatorType}\"}}";
                var result = (await App.IOTService.InvokeDirectMethod(DeviceId, METHOD, payload)).GetPayloadAsJson();

                var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(result);
                result = dict["value"].ToString();

                return Actuator.StringToBool(result);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Updates the actuator state.
        /// </summary>
        /// <param name="actuatorType">The target actuator type to update.</param>
        /// <param name="state">Whether the actuator should be set on or off.</param>
        /// <returns></returns>
        protected async Task<bool> UpdateActuatorState(string actuatorType, bool state)
        {
            const string METHOD = "control_actuator";
            string stateString = String.Empty;

            if (actuatorType == Actuator.SERVO)
            {
                stateString = state ? "1" : "-1";
            }
            else
            {
                stateString = state ? "ON" : "OFF";
            }
            
            string payload = $"{{\"target\":\"{actuatorType}\", \"value\":\"{stateString}\"}}";

            var result = await App.IOTService.InvokeDirectMethod(DeviceId, METHOD, payload);
            return state;
        }
    }
}

﻿using LiveChartsCore.Defaults;
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

        public Dictionary<string, ObservableCollection<Reading>> Readings { get; set; }
        public Dictionary<string, CartesianChart> Charts { get; set; }

        protected string DeviceId { get; set; }

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

        public virtual void AddReading(Reading reading)
        {
            Readings[reading.Type].Add(reading);
        }

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
        /// <param name="readingType"></param>
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
                    LabelsPaint = new SolidColorPaint(SKColor.Parse("#4a8e49"))
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

        public abstract Task GetInitialActuatorStates();

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
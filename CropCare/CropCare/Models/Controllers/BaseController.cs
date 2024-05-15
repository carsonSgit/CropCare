using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Maui;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using LiveChartsCore.VisualElements;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropCare.Models.Controllers
{
    public abstract class BaseController
    {
        private string[] _readingTypes;

        protected readonly Reading NO_READING = new Reading(ReadingType.NODATA, String.Empty, "NO DATA");

        public Dictionary<string, ObservableCollection<Reading>> Readings { get; set; }
        public Dictionary<string, CartesianChart> Charts { get; set; }

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

        public BaseController(string[] readingTypes)
        {
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
                    Stroke = new SolidColorPaint(SKColors.Blue) { StrokeThickness = 3 },
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
                    Name = this.Readings[readingType][0].Type + " (" + this.Readings[readingType][0].Unit + ")"
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
                    Name = "Time"
                }
            };

            LabelVisual chartTitle = new LabelVisual
            {
                Text = this.Readings[readingType][0].Type + " Over Time",
                TextSize = 18,
                Padding = new LiveChartsCore.Drawing.Padding(15),
                Paint = new SolidColorPaint(SKColors.DarkSlateGray)
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
    }
}

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
            foreach (string type in readingTypes)
            {
                Readings.Add(type, new ObservableCollection<Reading>());
            }
            App.IOTService.ConnectionStopped += IOTService_ConnectionStopped;
        }

        /// <summary>
        /// This handler is called when the IOTService connection is stopped.
        /// </summary>
        public abstract void IOTService_ConnectionStopped();

        public virtual CartesianChart GetChart(string readingType, string title, string xTitle, string yTitle)
        {
            LineSeries<DateTimePoint>[] series =
            {
                new LineSeries<DateTimePoint>
                {
                    Values = this.Readings[readingType].Select(x => new DateTimePoint(x.TimeStamp, double.Parse(x.Value))),
                    Name = title,
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
                    MaxLimit = 100,
                    Name = yTitle
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
                    Name = xTitle
                }
            };

            LabelVisual chartTitle = new LabelVisual
            {
                Text = title,
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

            return cartesianChart;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropCare.Models.Controllers
{
    public abstract class BaseController
    {
        private string[] _readingTypes;

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
    }
}

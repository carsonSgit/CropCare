using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using LiteDB;
using Newtonsoft.Json;

namespace CropCare.Models.Controllers
{
    public abstract class BaseController
    {
        private string[] _readingTypes;
        
        protected string _invokeGetActuatorStates = "get_actuator_states";

        protected readonly Reading NO_READING = new Reading(ReadingType.NODATA, String.Empty, "NO DATA");

        public Dictionary<string, ObservableCollection<Reading>> Readings { get; set; }

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
        public abstract Task GetInitialActuatorStates();

        protected async Task<bool> GetActuatorState(string actuatorType)
        {
            const string METHOD = "get_single_actuator_state";

            string payload = $"{{\"target\":\"{actuatorType}\"}}";
            var result = (await App.IOTService.InvokeDirectMethod(DeviceId, METHOD, payload)).GetPayloadAsJson();
            
            var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(result);
            result = dict["value"].ToString();

            return Actuator.StringToBool(result);
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
            return result.Status == 200;
        }
    }
}

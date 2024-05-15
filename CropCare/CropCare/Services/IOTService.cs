using Azure.Messaging.EventHubs.Consumer;
using Microsoft.Azure.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropCare.Services
{
    public class IOTService
    {
        public event Action<string, string> MessageReceived;
        public event Action ConnectionStopped;

        private int retryDelay = 5000;
        private ServiceClient _serviceClient;
        
        private ServiceClient ServiceClient
        {
            get
            {
                if (_serviceClient == null)
                {
                    string connectionString = App.Settings.IOTHubConnectionString;
                    _serviceClient = ServiceClient.CreateFromConnectionString(connectionString);
                }
                return _serviceClient;
            }
        }

        public IOTService()
        {
            Task.Run(() => StartReceivingMessagesAsync());
        }

        public async Task<CloudToDeviceMethodResult> InvokeDirectMethod(string deviceId, string methodName, string payload)
        {
            CloudToDeviceMethod method = new CloudToDeviceMethod(methodName)
            {
                ResponseTimeout = TimeSpan.FromSeconds(30)
            };

            try
            {
                method.SetPayloadJson(payload);
                Console.WriteLine($"Invoking method '{methodName}' on device '{deviceId}' with payload '{payload}'.");
                CloudToDeviceMethodResult result = await ServiceClient.InvokeDeviceMethodAsync(deviceId, method);
                Console.WriteLine("Method invoked successfully.");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while invoking method: {ex.Message}");
                throw;
            }
        }
    

        public async Task StartReceivingMessagesAsync()
        {
            string connectionString = App.Settings.EventHubConnectionString;
            string eventHubName = App.Settings.EventHubName;
            await using var consumer = new EventHubConsumerClient(EventHubConsumerClient.DefaultConsumerGroupName, connectionString, eventHubName);
            while (true)
            {
                try
                {
                    await foreach (PartitionEvent partitionEvent in consumer.ReadEventsAsync(false))
                    {
                        string data = Encoding.UTF8.GetString(partitionEvent.Data.Body.ToArray());
                        string deviceId = partitionEvent.Data.SystemProperties["iothub-connection-device-id"].ToString();
                        MessageReceived?.Invoke(deviceId, data);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    ConnectionStopped?.Invoke();
                    await Task.Delay(retryDelay);
                }
            }
        }
    }
}

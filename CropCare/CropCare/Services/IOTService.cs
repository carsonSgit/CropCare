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
        /// <summary>
        /// The event that is raised when a message is received from Azure IOT Hub.
        /// </summary>
        public event Action<string, string> MessageReceived;
        /// <summary>
        /// The event that is raised when the connection to Azure IOT Hub is interrupted for any reason.
        /// </summary>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="IOTService"/> class and starts receiving messages.
        /// </summary>
        public IOTService()
        {
            Task.Run(() => StartReceivingMessagesAsync());
        }

        /// <summary>
        /// Sends a direct method to a device.
        /// </summary>
        /// <param name="deviceId">The device id to send the direct method to.</param>
        /// <param name="methodName">The method name to invoke.</param>
        /// <param name="payload">The payload formatted as json.</param>
        /// <returns>The CloudToDeviceMethodResult of the direct method</returns>
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
    
        /// <summary>
        /// Starts receiving messages from Azure IOT Hub.
        /// </summary>
        /// <returns></returns>
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

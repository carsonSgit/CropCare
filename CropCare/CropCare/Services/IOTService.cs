using Azure.Messaging.EventHubs.Consumer;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
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

        private RegistryManager _registryManager;
        private RegistryManager RegistryManager
        {
            get
            {
                if (_registryManager == null)
                {
                    string connectionString = App.Settings.IOTHubConnectionString;
                    _registryManager = RegistryManager.CreateFromConnectionString(connectionString);
                }
                return _registryManager;
            }
        }

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

        /// <summary>
        /// Gets a desired property for a device.
        /// </summary>
        /// <param name="deviceId">The device id to get the property for.</param>
        /// <param name="property">The property name to get.</param>
        /// <returns>The property value, if an error occured returns null.</returns>
        public async Task<string> GetDesiredPropertyForDeviceAsync(string deviceId, string property)
        {
            try
            {
                Twin twin = await RegistryManager.GetTwinAsync(deviceId);
                return twin.Properties.Desired[property];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving the twin for device '{deviceId}': {ex.Message}");
                return null;
            }
        }
        /// <summary>
        /// Sets a desired property for a device.
        /// </summary>
        /// <param name="deviceId">The device id to set property for.</param>
        /// <param name="property">The property name to change</param>
        /// <param name="value">The value to change the property to.</param>
        /// <returns>True if property was set else false.</returns>
        public async Task<bool> SetDesiredPropertyForDeviceAsync(string deviceId, string property, dynamic value)
        {
            try
            {
                Twin twin = await RegistryManager.GetTwinAsync(deviceId);
                twin.Properties.Desired[property] = value;

                await RegistryManager.UpdateTwinAsync(twin.DeviceId, twin, twin.ETag);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while set the twin for device '{deviceId}': {ex.Message}");
                return false;
            }
        }
    }
}

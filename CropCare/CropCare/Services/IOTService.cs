using Azure.Messaging.EventHubs.Consumer;
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

        public IOTService()
        {
            Task.Run(() => StartReceivingMessagesAsync());
        }

        public async Task StartReceivingMessagesAsync()
        {
            string connectionString = App.Settings.EventHubConnectionString;

            await using var consumer = new EventHubConsumerClient(EventHubConsumerClient.DefaultConsumerGroupName, connectionString, "cropcare");
            await foreach (PartitionEvent partitionEvent in consumer.ReadEventsAsync(false))
            {
                string data = Encoding.UTF8.GetString(partitionEvent.Data.Body.ToArray());

                string deviceId = partitionEvent.Data.SystemProperties["iothub-connection-device-id"].ToString();
                MessageReceived?.Invoke(deviceId, data);
            }
        }
    }
}

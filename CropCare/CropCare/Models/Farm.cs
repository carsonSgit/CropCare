using CropCare.Interfaces;
using Microsoft.Azure.Devices;
using System.ComponentModel;

namespace CropCare.Models
{
    public enum MethodName
    {
        set_door_lock,
        get_door_lock,
        set_door_open,
        get_door_open,
        set_light,
        get_light,
        set_fan,
        get_fan,
        // Add more methods here
    }

    public class Farm : INotifyPropertyChanged, IHasKey
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Key { get; set; }
        public string Name { get; set; }
        public string DeviceId { get; set; }

        public Farm(string farmName)
        {
            Name = farmName;
        }

        public async Task InvokeMethodAsync(MethodName methodName, string parametersJSON)
        {
            var methodInvocation = new CloudToDeviceMethod(nameof(methodName))
            {
                ResponseTimeout = TimeSpan.FromSeconds(30),
            };
            methodInvocation.SetPayloadJson(parametersJSON);

            Console.WriteLine($"Invoking direct method for device: {DeviceId}");

            // Invoke the direct method asynchronously and get the response from the simulated device.
            CloudToDeviceMethodResult response = await App.IOTHubClient.InvokeDeviceMethodAsync(DeviceId, methodInvocation);

            Console.WriteLine($"Response status: {response.Status}, payload:\n\t{response.GetPayloadAsJson()}");

        }
    }
}

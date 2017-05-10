using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Client.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTHubDemo
{
    class Program
    {
        static RegistryManager registryManager;
        static string connectionString;

        static DeviceClient device1Client;
        static DeviceClient device2Client;

        static string iotHubUri;

        static string device1Key = "{device 1 key}";
        static string device1Name = "car1";

        static string device2Key = "{device 2 key}";
        static string device2Name = "car2";

        static void Main(string[] args)
        {
            connectionString = ConfigurationManager.AppSettings["IotHubConnectionString"];
            iotHubUri = ConfigurationManager.AppSettings["IotHubUri"];

            Console.WriteLine("Registering device");
            registryManager = RegistryManager.CreateFromConnectionString(connectionString);
            AddDeviceAsync().Wait();
            Console.ReadLine();

            Console.WriteLine("Simulating devices\n");
            device1Client = DeviceClient.Create(iotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey(device1Name, device1Key), Microsoft.Azure.Devices.Client.TransportType.Http1);
            device2Client = DeviceClient.Create(iotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey(device2Name, device2Key), Microsoft.Azure.Devices.Client.TransportType.Http1);

            SendDeviceToCloudMessagesAsync().Wait();
            Console.ReadLine();
        }

        private static async Task AddDeviceAsync()
        {
            Device device1;
            Device device2;
            try
            {
                device1 = await registryManager.AddDeviceAsync(new Device(device1Name));
                device2 = await registryManager.AddDeviceAsync(new Device(device2Name));
            }
            catch (Exception)
            {
                device1 = await registryManager.GetDeviceAsync(device1Name);
                device2 = await registryManager.GetDeviceAsync(device2Name);
            }
            Console.WriteLine("Device 1 key: {0}", device1.Authentication.SymmetricKey.PrimaryKey);
            Console.WriteLine("Device 2 key: {0}", device2.Authentication.SymmetricKey.PrimaryKey);

            device1Key = device1.Authentication.SymmetricKey.PrimaryKey;
            device2Key = device2.Authentication.SymmetricKey.PrimaryKey;
        }

        private static async Task SendDeviceToCloudMessagesAsync()
        {
            double avgSpeed = 50;
            Random rand = new Random();

            while (true)
            {
                double currentSpeed = avgSpeed + rand.NextDouble() * 8 - 4;

                var telemetryDataPoint = new
                {
                    deviceId = device1Name,
                    currentSpeed = currentSpeed,
                    timestamp = DateTime.Now
                };
                var messageString = JsonConvert.SerializeObject(telemetryDataPoint);
                var message = new Microsoft.Azure.Devices.Client.Message(Encoding.ASCII.GetBytes(messageString));

                await device1Client.SendEventAsync(message);
                Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);

                Task.Delay(1000).Wait();

                currentSpeed = avgSpeed + rand.NextDouble() * 8 - 4;

                var telemetryDataPoint2 = new
                {
                    deviceId = device2Name,
                    currentSpeed = currentSpeed,
                    timestamp = DateTime.Now
                };
                messageString = JsonConvert.SerializeObject(telemetryDataPoint2);
                message = new Microsoft.Azure.Devices.Client.Message(Encoding.ASCII.GetBytes(messageString));

                await device2Client.SendEventAsync(message);
                Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);

                Task.Delay(1000).Wait();
            }
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace InventoryClient
{
    class Worker
    {
        private static string MQTTServer;
        private static string Topic;

        public Worker(String mqttServer,String topic)
        {
            MQTTServer = mqttServer;
            Topic = topic;
        }

        public void DoJob()
        {
            RunAsync().GetAwaiter().GetResult();
        }

        public static async Task RunAsync()
        {
            try
            {
                var hardware = new Hardware();
                // Create a new Inventory ,,Be carfull exception where record equ null.
                Inventory inventoryItems = new Inventory
                {
                    SN = hardware.GetSn(),
                    CPU = hardware.GetCpu(),
                    RAM = hardware.GetRam(),
                    VideoController = hardware.GetVideoController(),
                    DISK = hardware.GetDisk(),
                    Brand = hardware.GetBrand(),
                    Model = hardware.Get_model(),
                    OS = hardware.Get_OS(),
                    NetworkAdapter = hardware.GetNetworkAdapter(),
                    SoundCard = hardware.GetSoundDevice(),
                    Monitor = hardware.GetMonitor(),
                    HostName = hardware.GetHostName(),
                    IPAddr = hardware.GetIpAddr(),
                    LastCheckin = hardware.GetLastCheckIn()
                };

                //await SendAsyncPostRequest(inventoryItems);
                string payload = JsonConvert.SerializeObject(inventoryItems, Formatting.Indented, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd hh:mm:ss" });

                Task.Run(async () => await MQTTClient.Publish_Application_Message(MQTTServer,Topic, payload)).Wait();

#if DEBUG
                Console.WriteLine(inventoryItems.CPU);
                Console.WriteLine(inventoryItems.RAM);
                Console.WriteLine(inventoryItems.VideoController);
                Console.WriteLine(inventoryItems.Model);
                Console.WriteLine(inventoryItems.Monitor);
                Console.WriteLine(inventoryItems.DISK);
                Console.WriteLine(inventoryItems.NetworkAdapter);
                Console.WriteLine(inventoryItems.SoundCard);
                Console.WriteLine(inventoryItems.HostName);
                Console.WriteLine(inventoryItems.IPAddr);
                Console.WriteLine(inventoryItems.LastCheckin);
                //Console.WriteLine(output);
#endif
            }
            catch (Exception e)
            {
                Console.WriteLine("oops..catch exception." + e.Message + e.StackTrace);
            }

        }

    }
}

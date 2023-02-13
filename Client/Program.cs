using System;
using System.Net;

namespace InventoryClient
{

    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Please Specify MQTT Sever Address and topic");
                return;
            }
            try
            {
                var worker = new Worker(args[0], args[1]);
                worker.DoJob();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Inventory Scan failed with: {e.Message},{e.StackTrace}");
            }
        }
    }
}

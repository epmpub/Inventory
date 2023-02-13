using System;
using System.Collections.Generic;
using System.Management;
using System.Linq;

namespace InventoryClient
{
    public class Network
    {
        public Guid Guid { get; private set; } = Guid.NewGuid();
        public string ComputerName { get; set; }
        public string IPAddr { get; set; }
        public string MacAddr { get; set; }
        public List<Network> DoScan()
        {
            var systemInfo = new List<Network>();

            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = True");

                foreach (ManagementObject queryObj in searcher.Get())
                {

                    String[] arrIPAddress = (String[])(queryObj["IPAddress"]);
                    foreach (String arrValue in arrIPAddress)
                    {
                        //Console.WriteLine("IPAddress: {0}", arrValue);
                        systemInfo.Add(new Network
                        {
                            ComputerName = queryObj["DNSHostName"] + "",
                            IPAddr = arrIPAddress[0] + "",
                            MacAddr = queryObj["MACAddress"] + ""
                        });
                    }
                }
            }
            catch (ManagementException e)
            {
                Console.WriteLine("An error occurred while querying for WMI data: " + e.Message);
            }
            return systemInfo;
        }
    }
}


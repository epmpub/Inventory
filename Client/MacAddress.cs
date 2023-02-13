using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace InventoryClient
{
    public class MacAddr
    {
        public string Caption { get; set; }
        public string Mac { get; set; }
        public List<MacAddr> GetMac()
        {
            var MacList = new List<MacAddr>();

            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_NetworkAdapterConfiguration");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    MacList.Add(new MacAddr
                    {
                        Caption = queryObj["Caption"] + "",
                        Mac = queryObj["MACAddress"] + ""
                    });
                }
            }
            catch (ManagementException e)
            {
                Console.WriteLine("An error occurred while querying for WMI data: " + e.Message);
            }
            return MacList;

        }
    }
}

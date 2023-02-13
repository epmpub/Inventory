using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace InventoryClient
{
    public class VOS
    {
        public Guid Guid { get; private set; } = Guid.NewGuid();
        public string Caption { get; set; }
        public string OSArchitecture { get; set; }

        public List<VOS> GetOSinfo()
        {
            var OSINFO = new List<VOS>();

            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_OperatingSystem");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    OSINFO.Add(
                        new VOS
                        {
                            Caption = queryObj["Caption"] + " ",
                            OSArchitecture = queryObj["OSArchitecture"] + " "
                        });
                }

            }

            catch (ManagementException e)
            {
                Console.WriteLine("An error occurred while querying for WMI data: " + e.Message);
            }

            return OSINFO;
        }
    }
}

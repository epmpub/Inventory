using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;

namespace InventoryClient
{
    class Hardware
    {
        private static readonly string SN = "select * from  Win32_BIOS";
        private static readonly string WMI_CPU = "select * from  Win32_Processor";
        private static readonly string RAM = "select * from  Win32_ComputerSystem";
        private static readonly string DiskDrive = "select * from  Win32_DiskDrive";
        private static readonly string Brand = "SELECT * FROM Win32_BIOS";

        public String GetVideoController()
        {
            string VideoController = null;
            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_VideoController");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    //Console.WriteLine("-----------------------------------");
                    //Console.WriteLine("Win32_VideoController instance");
                    //Console.WriteLine("-----------------------------------");
                    //Console.WriteLine("Caption: {0}", queryObj["Caption"]);
                    VideoController = queryObj["Caption"].ToString();
                }
            }
            catch (ManagementException e)
            {
                Console.WriteLine("An error occurred while querying for WMI data: " + e.Message);
            }
            return VideoController;
        }

        public String GetNetworkAdapter()
        {
            String NetworkAdapters = null;
            var mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            var moc = mc.GetInstances();

            foreach (var mo in moc)
            {
                if ((bool)mo["ipEnabled"])
                {
                    NetworkAdapters += (mo["Caption"].ToString()) + ",";
                }
            }

            return NetworkAdapters;
        }
        // win10 work ,win7 not work!!!!
        public String GetMonitor1()
        {
            IEnumerable<string> EnumerateMonitors() =>
                new ManagementObjectSearcher(@"root\wmi", "SELECT * FROM WmiMonitorID").Get()
                    .OfType<ManagementBaseObject>()
                    .Select(mo => string.Join(
                        string.Empty,
                        ((ushort[])(mo["UserFriendlyName"] ?? "No description"))
                            .Select(x => Encoding.ASCII.GetString(new[] { (byte)x }).Replace("\0", string.Empty))));
            return (string.Join(", ", EnumerateMonitors()));

        }

        // TODO : win7 TEST.
        public String GetMonitor()
        {
            string s = null;

            string monitor = null;
            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\WMI",
                    "SELECT * FROM WmiMonitorID");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    if (queryObj["UserFriendlyName"] == null)
                    {
                        //Console.WriteLine("1 - UserFriendlyName: {0}", queryObj["UserFriendlyName"]);

                    }
                    else
                    {
                        UInt16[] arrUserFriendlyName = (UInt16[])(queryObj["UserFriendlyName"]);
                        foreach (UInt16 arrValue in arrUserFriendlyName)
                        {
                            //Console.WriteLine("2 - UserFriendlyName: {0}", arrValue);
                            char c = Convert.ToChar(arrValue);
                            if (c != '\0')
                            {
                                s += c.ToString();

                            }
                        }
                    }
                }
                Console.WriteLine(s);
                return s;

            }
            catch (ManagementException e)
            {
                Console.WriteLine("An error occurred while querying for WMI data: " + e.Message);
            }


            return monitor;

        }






        public String GetSoundDevice()
        {
            String SoundCard = null;
            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_SoundDevice");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    //Console.WriteLine("-----------------------------------");
                    //Console.WriteLine("Win32_SoundDevice instance");
                    //Console.WriteLine("-----------------------------------");
                    //Console.WriteLine("Caption: {0}", queryObj["Caption"]);
                    SoundCard += queryObj["Caption"].ToString() + "/";
                }
            }
            catch (ManagementException e)
            {
                Console.WriteLine("An error occurred while querying for WMI data: " + e.Message);
            }
            return SoundCard;
        }

        public String Get_OS()
        {
            String Caption = null;

            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_OperatingSystem");

                foreach (ManagementObject queryObj in searcher.Get())
                {

                    Caption = queryObj["Caption"].ToString() + " " +queryObj["OSArchitecture"].ToString();
                }

            }

            catch (ManagementException e)
            {
                Console.WriteLine("An error occurred while querying for WMI data: " + e.Message);
            }

            return Caption;
        }

        public String Get_model()
        {

            string pc_model = null;

            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_ComputerSystem");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    //Console.WriteLine("-----------------------------------");
                    //Console.WriteLine("Win32_ComputerSystem instance");
                    //Console.WriteLine("-----------------------------------");
                    pc_model = queryObj["Model"].ToString();
                }
            }
            catch (ManagementException e)
            {
                Console.WriteLine("An error occurred while querying for WMI data: " + e.Message);
            }
            return pc_model;
        }

        public String GetSn()
        {
            String Sn = null;

            foreach (var wmi in new ManagementObjectSearcher(SN).Get())
            {
                Sn = wmi["SerialNumber"].ToString();
            }
            return Sn;
        }

        public String GetCpu()
        {
            String Cpu = null;
            foreach (var wmi in new ManagementObjectSearcher(WMI_CPU).Get())
            {
                Cpu = wmi["Name"].ToString();
            }
            return Cpu;
        }

        public String GetRamSize()
        {
            String Ram = null;
            foreach (var wmi in new ManagementObjectSearcher(RAM).Get())
            {
                String ramSize = wmi["TotalPhysicalMemory"].ToString();
                float size = float.Parse(ramSize);
                float RSize = size / 1000 / 1024 / 1024;
                Ram = Math.Round(RSize, 0).ToString() + "GB";
            }
            return Ram;

        }

        public String GetRam()
        {
            string ram = null;
            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_PhysicalMemory");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    //Console.WriteLine("-----------------------------------");
                    //Console.WriteLine("Win32_PhysicalMemory instance");
                    //Console.WriteLine("-----------------------------------");
                    //Console.WriteLine("Manufacturer: {0}", queryObj["Manufacturer"]);
                    ram += queryObj["Manufacturer"].ToString() + ",";
                }
            }
            catch (ManagementException e)
            {
                Console.WriteLine("An error occurred while querying for WMI data: " + e.Message);
            }

            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_PhysicalMemory");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    //Console.WriteLine("-----------------------------------");
                    //Console.WriteLine("Win32_PhysicalMemory instance");
                    //Console.WriteLine("-----------------------------------");
                    //Console.WriteLine("Capacity: {0}", queryObj["Capacity"]);
                    String Capacity = queryObj["Capacity"].ToString();
                    float size = float.Parse(Capacity);
                    float RSize = size / 1024 / 1024 / 1024;
                    ram += RSize.ToString() + "GB,";
                }
            }
            catch (ManagementException e)
            {
                Console.WriteLine("An error occurred while querying for WMI data: " + e.Message);
            }


            return ram;
        }

        public String GetDiskSize()
        {
            //TODO:多硬盘处理，

            var DiskDrivers = new ManagementObjectSearcher(DiskDrive).Get();
            StringBuilder StrDisk = new StringBuilder();
            foreach (var wmi in DiskDrivers)
            {
                String diskSize = wmi["size"].ToString();
                float size = float.Parse(diskSize);
                float RSize = size / 1000 / 1000 / 1000;
                StrDisk.Append(Math.Round(RSize, 0).ToString() + "GB" +",");
            }
            return StrDisk.ToString();
        }

        public String GetDisk()
        {
            string disks = null;
            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_DiskDrive");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    //Console.WriteLine("-----------------------------------");
                    //Console.WriteLine("Win32_DiskDrive instance");
                    //Console.WriteLine("-----------------------------------");
                    //Console.WriteLine("Caption: {0}", queryObj["Caption"]);
                    disks += queryObj["Caption"].ToString() + ",";

                }
            }
            catch (ManagementException e)
            {
                Console.WriteLine("An error occurred while querying for WMI data: " + e.Message);
            }
            return disks;
        }


        public String GetBrand()
        {
            String BrandName = null;
            foreach (var wmi in new ManagementObjectSearcher(Brand).Get())
            {
                BrandName = wmi["Manufacturer"].ToString();
            }
            return BrandName;
        }

        public DateTime GetLastCheckIn()
        {
            DateTime LastCheckin = DateTime.Now;
            return LastCheckin;
        
        }

        public String GetHostName()
        {
            string hostname = null;
            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_ComputerSystem");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    hostname =  queryObj["Name"].ToString();
                }
            }
            catch (ManagementException e)
            {
                Console.WriteLine("An error occurred while querying for WMI data: " + e.Message);
            }

            return hostname;

        }

        public String GetIpAddr()
        {
            string ipAddr = null;
            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = True");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    if (queryObj["IPAddress"] == null)
                        Console.WriteLine("IPAddress: {0}", queryObj["IPAddress"]);
                    else
                    {
                        String[] arrIPAddress = (String[])(queryObj["IPAddress"]);
                        foreach (String arrValue in arrIPAddress)
                        {
                            //Console.WriteLine("IPAddress: {0}", arrValue);
                            ipAddr += arrValue.ToString() + ",";
                        }
                    }
                    //Console.WriteLine("IPEnabled: {0}", queryObj["IPEnabled"]);
                }
            }
            catch (ManagementException e)
            {
                Console.WriteLine("An error occurred while querying for WMI data: " + e.Message);
            }

            return ipAddr;


    }



    }
}

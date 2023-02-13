using System;
using System.Collections.Generic;
using System.Management;

namespace InventoryClient
{
    public class Vprocess
    {
        private List<Proc> ProccessList = new List<Proc>();
        internal ICollection<Proc> GetProcessList()
        {
            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_Process");

                foreach (ManagementObject _Process in searcher.Get())
                {
                    if (_Process["CommandLine"] != null)
                    {
                        //Console.WriteLine(_Process["Caption"].ToString());
                        //Console.WriteLine(_Process["CommandLine"].ToString().Replace('/', '_'));
                        ProccessList.Add(new Proc { Caption = _Process["Caption"].ToString(), CommandLine = _Process["CommandLine"].ToString() });
                    }


                }
            }
            catch (ManagementException e)
            {
                Console.WriteLine("An error occurred while querying for WMI data: " + e.Message);
            }
            return ProccessList;
        }
    }
}

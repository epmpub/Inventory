using System.Management.Automation;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;
using System.Collections;

namespace InventoryClient
{
    public class SoftWare
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string InstallDate { get; set; }
        public List<SoftWare> GetSoftWare(string regPath)
        {

            using (PowerShell PowerShellInstance = PowerShell.Create())
            {
                PowerShellInstance.AddScript(regPath);
                Collection<PSObject> PSOutput = PowerShellInstance.Invoke();
                var softlist = new List<SoftWare>();
                softlist.Clear();

                foreach (var outputItem in PSOutput)
                {
                    try
                    {
                        var software = new SoftWare
                        {
                            Name = (outputItem.Members["DisplayName"] != null ? outputItem.Members["DisplayName"].Value + "" : "unknow"),
                            Version = (outputItem.Members["DisplayVersion"] != null ? outputItem.Members["DisplayVersion"].Value + "" : "unknow"),
                            InstallDate = (outputItem.Members["InstallDate"] != null ? outputItem.Members["InstallDate"].Value + "" : "unknow")

                        };


                        softlist.Insert(softlist.Count, software);

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message + e.StackTrace);

                    }

                }

                return softlist;

            }
        }


        public List<SoftWare> DoScan()
        {
            var p = new SoftWare();
            string regPath = @"Get-ItemProperty HKLM:\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\* | Select-Object DisplayName ,DisplayVersion,InstallDate | where-object {$_.displayname -ne $null}";
            List<SoftWare> softList = p.GetSoftWare(regPath);

            string regPath2 = @"Get-ItemProperty HKLM:\Software\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\* | Select-Object DisplayName ,DisplayVersion,InstallDate | where-object {$_.displayname -ne $null}";
            softList.AddRange(p.GetSoftWare(regPath2));

            return softList;

        }

        //For DEBUG
        //foreach (var item in l)
        //{
        //    Console.WriteLine(item.Name);
        //}

    }

}

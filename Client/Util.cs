using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;

namespace InventoryClient
{
    class Util
    {
        private  string _ReportUrl;

        public Util(string ReportUrl )
        {
            this._ReportUrl = ReportUrl;
        }

        public static string GetMD5HashFromFile(string fileName)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(fileName))
                {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", string.Empty);
                }
            }
        }

        public void SetupApp()
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";

            //p.StartInfo.Arguments = "/c schtasks /Create /SC MINUTE /MO 15 /RU system /TN Inventory_Collection /TR:c:\\software\\cmdb_agent\\client.exe /F";
            p.StartInfo.Arguments = string.Format("/c schtasks /Create /SC MINUTE /MO {0} /RU system /TN VIPInventory /TR:c:\\software\\cmdb_agent\\client.exe /F",(new Random()).Next(35,60).ToString());
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.Start();
            p.WaitForExit();
        }

    }
}


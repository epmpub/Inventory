using System;
using System.Collections.Generic;
using System.Management;

namespace InventoryClient
{
    public class User
    {
        public Guid Guid { get; private set; } = Guid.NewGuid();
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string UserDesc { get; set; }

        public static String LoginName = String.Format("select * from  Win32_Process WHERE Name like '{0}%{1}'", "explorer", "exe");



        public String GetUserName()
        {
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            return userName;
        }


        //add FullName :
        // create 2021/3/3
        private String QueryFullName(string UserName)
        {
            string str = UserName;
            string[] r = str.Split('\\');
            string _userName = r[1];
            string queryStr = "SELECT * FROM Win32_UserAccount WHERE Name = " + "'" + _userName + "'" + " AND Domain = 'VIPSHOP' ";
            string _FullName = null;
            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2", queryStr);

                foreach (ManagementObject queryObj in searcher.Get())
                {

                    _FullName = queryObj["FullName"].ToString();
                }
            }
            catch (ManagementException e)
            {
                Console.WriteLine(("An error occurred while querying for WMI data: " + e.Message));
            }
            return _FullName;
        }

        public List<User> GetUserInfo()
        {
            var USERINFO = new List<User>();
            USERINFO.Add(
                new User
                {
                    UserName = GetUserName(),
                    FullName = QueryFullName(GetUserName()),
                    UserDesc = Get_UserDesc()
                });

            //for debug
            //foreach (var item in USERINFO)
            //{
            //    Console.WriteLine(item.UserName);
            //    Console.WriteLine(item.FullName);
            //    Console.WriteLine(item.UserDesc);
            //}

            return USERINFO;
        }


        private String QueryUserDesc(string UserName)
        {
            string _userName = UserName;
            string queryStr = "SELECT * FROM Win32_UserAccount WHERE Name = " + "'" + _userName + "'" + " AND Domain = 'VIPSHOP' ";
            string UserDesc = null;
            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2", queryStr);

                foreach (ManagementObject queryObj in searcher.Get())
                {

                    UserDesc = queryObj["Description"].ToString();
                }
            }
            catch (ManagementException e)
            {
                Console.WriteLine(("An error occurred while querying for WMI data: " + e.Message));
            }
            return UserDesc;
        }

        public string Get_UserDesc()
        {
            string ExplorerUser = GetExplorerUser();

            string user = ExplorerUser.Replace("VIPSHOP\\", "");

            return QueryUserDesc(user);
        }


        public String GetExplorerUser()
        {
            var query = new ObjectQuery(
                "SELECT * FROM Win32_Process WHERE Name = 'explorer.exe'");

            var explorerProcesses = new ManagementObjectSearcher(query).Get();

            foreach (ManagementObject mo in explorerProcesses)
            {
                string[] ownerInfo = new string[2];
                mo.InvokeMethod("GetOwner", (object[])ownerInfo);

                return String.Concat(ownerInfo[1], @"\", ownerInfo[0]);
            }

            return string.Empty;
        }



    }
}
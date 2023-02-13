using System.IO;
using System.Net;
using System.IO.Compression;
using System;

namespace VIPClient
{
    class Updater
    {

        private static readonly string Url = "http://gd2push01.corp.vipshop.com:9596/cmdb/";
        private static readonly string SaveAsFileName = "c:\\software\\cmdb_agent.zip";
        private static readonly string MainFileName = "C:\\software\\cmdb_agent\\client.exe";
        private static readonly string ExtractZipFolderName = "c:\\software";
        private static readonly string ClientFolder = "c:\\software\\cmdb_agent";
        //private static readonly string OldFolder = "C:\\software\\cmdb_agent_"+ (new Random()).Next(10001).ToString();
        private static readonly string OldFolder = "C:\\software\\cmdb_agent_OLD";




        internal void CheckUpdate()
        {
            //if exist mainfile;
            if (File.Exists(MainFileName))
            {
                if (!MD5Match())
                {
                    try
                    {
                        //Directory.Delete(OldFolder, true);
                        //Directory.Move(ClientFolder, OldFolder);
                        DownloadAndUnzipClient();
                        //Directory.Delete(OldFolder, true);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"[x] Error:Copy File error with {e.Message}");
                    }
                }
                Console.WriteLine("[x] Client is latest version.");

            }


            //if not exist mainfile;
            if (!Directory.Exists(ClientFolder) )
            {
                DownloadAndUnzipClient();
            }

            File.Delete(SaveAsFileName);

        }



        private static void DownloadAndUnzipClient()
        {
            var webClient = new WebClient();
            webClient.DownloadFile(Url+ "cmdb_agent.zip", SaveAsFileName);
            ZipFile.ExtractToDirectory(SaveAsFileName, ExtractZipFolderName);
        }

        public static bool MD5Match()
        {
            var webClient = new WebClient();
            string localMD5 = Util.GetMD5HashFromFile(MainFileName);
            string ServMD5 = webClient.DownloadString(Url + "MD5.txt");
            return localMD5 == ServMD5;
        }

    }

}

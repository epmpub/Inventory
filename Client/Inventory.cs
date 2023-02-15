using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InventoryClient
{
    public class Inventory
    {
        [Key]
        public string SN { get; set; }
        public string CPU { get; set; }
        public string RAM { get; set; }
        public string VideoController { get; set; }
        public string DISK { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string OS { get; set; }
        public DateTime LastCheckin { get; set; }
        public string SoundCard { get;  set; }
        public string Monitor { get;  set; }
        public string NetworkAdapter { get;  set; }

        public string HostName { get; set; }
        public string IPAddr { get; set; }
    }
}

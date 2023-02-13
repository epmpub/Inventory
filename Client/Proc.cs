using System;
using System.ComponentModel.DataAnnotations;

namespace InventoryClient
{
    public class Proc
    {
        public Guid Guid { get; private set; } = Guid.NewGuid();
        public string Caption { get; set; }
        public string CommandLine { get; set; }
    }
}
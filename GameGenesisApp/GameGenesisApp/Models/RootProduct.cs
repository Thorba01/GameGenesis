using System;
using System.Collections.Generic;
using System.Text;

namespace GameGenesisApp.Models
{
    public class RootProduct
    {
        public Product Products { get; set; }
        public bool Success { get; set; } = false;
        public string Message { get; set; }
    }
}

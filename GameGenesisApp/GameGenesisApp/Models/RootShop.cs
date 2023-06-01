using System;
using System.Collections.Generic;
using System.Text;

namespace GameGenesisApp.Models
{
    public class RootShop
    {
        public List<Product> products { get; set; }
        public bool success { get; set; } = false;
        public string message { get; set; }
    }
}

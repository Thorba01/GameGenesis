using System;
using System.Collections.Generic;
using System.Text;

namespace GameGenesisApp.Models
{
    public class RootBasket
    {
        public List<Basket> Baskets { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}

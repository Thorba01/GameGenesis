﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GameGenesisApp.Models
{
    public class RootlistProduct
    {
        public List<Product> Products { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}

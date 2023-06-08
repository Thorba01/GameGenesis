using System;
using System.Collections.Generic;
using System.Text;

namespace GameGenesisApp.Models
{
    public class RootLibrary
    {
        public List<Library> Libraries { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}

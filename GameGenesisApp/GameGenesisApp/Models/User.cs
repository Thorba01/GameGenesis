using System;
using System.Collections.Generic;
using System.Text;

namespace GameGenesisApp.Models
{
    public class User
    {
        public string email { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public DateTime birthdate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace GameGenesisApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Name";
        public string Description { get; set; } = "Description";
        public User Author { get; set; }
        public float Price { get; set; }
    }
}

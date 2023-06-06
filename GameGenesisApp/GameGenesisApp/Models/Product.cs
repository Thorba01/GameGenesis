using System;
using System.Collections.Generic;
using System.Text;

namespace GameGenesisApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string name { get; set; } = "Name";
        public string Description { get; set; } = "Description";
        public User Author { get; set; }
        public float Price { get; set; }
        public List<Category> Categories { get; set; }
        public List<Image> Images { get; set; }
    }
}

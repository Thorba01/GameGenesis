using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace GameGenesisApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string name { get; set; } = "Name";
        public string Description { get; set; } = "Description";
        public User Author { get; set; }
        public long Price { get; set; }
        public List<Category> Categories { get; set; }
        public ObservableCollection<Image> Images { get; set; }
    }
}

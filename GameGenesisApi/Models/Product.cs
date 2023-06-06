using GameGenesisApi.Dtos.Category;

namespace GameGenesisApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Name";
        public string Description { get; set; } = "Description";
        public User Author { get; set; }
        public float Price { get; set; }
        public List<ProductCategory>? ProductCategories { get; set; } 
        public List<Image>? Images { get; set; }
        public List<Library>? Libraries { get; set; }
        public List<Shop>? Shops { get; set; }
        public List<Basket>? Baskets { get; set; }

    }
}

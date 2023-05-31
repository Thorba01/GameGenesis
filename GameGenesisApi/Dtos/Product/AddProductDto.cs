using GameGenesisApi.Models;

namespace GameGenesisApi.Dtos.Product
{
    public class AddProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public float Price { get; set; }

    }
}

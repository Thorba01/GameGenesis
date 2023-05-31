using GameGenesisApi.Dtos.Category;
using GameGenesisApi.Models;

namespace GameGenesisApi.Dtos.Product
{
    public class GetProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public float Price { get; set; }
        public List<GetCategoryDto>? Categories { get; set; }
    }
}

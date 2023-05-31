using System.Net.Http.Headers;

namespace GameGenesisApi.Models
{
    public class Basket
    {
        public int Id { get; set; }
        public Account Account { get; set; }
        public int AccountId { get; set; }
        public List<Product>? Products { get; set; }
    }
}

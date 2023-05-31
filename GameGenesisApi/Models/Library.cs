namespace GameGenesisApi.Models
{
    public class Library
    {
        public int Id { get; set; }
        public Account Account { get; set; }
        public int AccountId { get; set; }
        public List<Product>? Products { get; set; }
    }
}

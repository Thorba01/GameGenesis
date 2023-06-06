namespace GameGenesisApi.Models
{
    public class Image
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
    }
}

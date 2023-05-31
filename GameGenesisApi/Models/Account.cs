namespace GameGenesisApi.Models
{
    public class Account
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; }
        public Basket Basket { get; set; } = new Basket();
        public Library Library { get; set; } = new Library();
    }
}

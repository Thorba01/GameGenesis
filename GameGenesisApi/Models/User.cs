namespace GameGenesisApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public bool EmailVerified { get; set; } = false;
        public byte[] PasswordHash { get; set; } = new byte[0];
        public byte[] PasswordSalt { get; set; } = new byte[0];
        public DateTime Birthdate { get; set; }
        public bool IsAdmin { get; set; } = false;
        public Account Account { get; set; } = new Account();
    }
}

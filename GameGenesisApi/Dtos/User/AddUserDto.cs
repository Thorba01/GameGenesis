namespace GameGenesisApi.Dtos.User
{
    public class AddUserDto
    {
        public string Email { get; set; } = string.Empty;
        public bool EmailVerified { get; set; } = false;
        public string Password { get; set; } = string.Empty;
        public DateTime Birthdate { get; set; }
        public bool IsAdmin { get; set; } = false;
    }
}

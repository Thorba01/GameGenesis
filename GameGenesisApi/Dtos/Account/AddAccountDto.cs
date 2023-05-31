namespace GameGenesisApi.Dtos.Account
{
    public class AddAccountDto
    {
        public bool IsActive { get; set; } = true;
        public int UserId { get; set; }
        public int BasketId { get; set; }
        public int LibraryId { get; set; }
    }
}

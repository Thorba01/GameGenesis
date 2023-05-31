namespace GameGenesisApi.Models
{
    public class ServiceAccountResponse<T>
    {
        public T? Accounts { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}

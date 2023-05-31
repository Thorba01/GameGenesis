namespace GameGenesisApi.Models
{
    public class ServiceShopResponse<T>
    {
        public T? Shops { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}

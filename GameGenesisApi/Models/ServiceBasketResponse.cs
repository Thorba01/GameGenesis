namespace GameGenesisApi.Models
{
    public class ServiceBasketResponse<T>
    {
        public T? Baskets { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}

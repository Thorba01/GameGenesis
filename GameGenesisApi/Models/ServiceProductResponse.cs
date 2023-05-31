namespace GameGenesisApi.Models
{
    public class ServiceProductResponse<T>
    {
        public T? Products { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}

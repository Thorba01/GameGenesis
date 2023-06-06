namespace GameGenesisApi.Models
{
    public class ServiceImageResponse<T>
    {
        public T? Images { get; set; }
        public bool Success { get; set; } = false;
        public string Message { get; set; } = string.Empty;
    }
}

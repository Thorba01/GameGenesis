namespace GameGenesisApi.Models
{
    public class ServiceCategoriesResponse<T>
    {
        public T? Categories { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}

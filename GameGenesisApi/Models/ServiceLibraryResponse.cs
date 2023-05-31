namespace GameGenesisApi.Models
{
    public class ServiceLibraryResponse<T>
    {
        public T? Libraries { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}

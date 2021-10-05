namespace CompilerApp.DTOs
{
    public class CompilerResponseDTO
    {
        public string? Output { get; set; }
        public int StatusCode { get; set; }
        public string? Memory { get; set; }
        public string? CpuTime { get; set; }
        public string? Error { get; set; }
    }
}

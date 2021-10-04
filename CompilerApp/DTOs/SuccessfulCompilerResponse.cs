namespace CompilerApp.DTOs
{
    public class SuccessfulCompilerResponse
    {
        public string Output { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public string? Memory { get; set; }
        public string? CpuTime { get; set; }
    }
}

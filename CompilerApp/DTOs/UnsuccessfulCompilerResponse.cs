namespace CompilerApp.DTOs
{
    public class UnsuccessfulCompilerResponse
    {
        public string Error { get; set; } = string.Empty;
        public int StatusCode { get; set; }
    }
}

namespace CompilerApp.Entities
{
    public class CompilationSummary
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SourceCode { get; set; } = string.Empty;
        public string? Output { get; set; }
        public int StatusCode { get; set; }
        public string? Memory { get; set; }
        public string? CpuTime { get; set; }
        public string? Error { get; set; }
    }
}

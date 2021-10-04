namespace CompilerApp.DTOs
{
    public class CompilerRequestDTO
    {
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
        public string Script { get; set; } = string.Empty;
        public string StdIn { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public string VersionIndex { get; set; } = string.Empty;
    }
}

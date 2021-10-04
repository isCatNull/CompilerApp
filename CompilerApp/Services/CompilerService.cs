using CompilerApp.DTOs;
using CompilerApp.Interfaces;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text;

namespace CompilerApp.Services
{
    public class CompilerService : ICompilerService
    {
        private readonly HttpClient _httpClient;

        private readonly JsonSerializerOptions _serialezitionOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        private readonly JsonSerializerOptions _deserialiazitionOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public CompilerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task CompileAsync(CompilerRequestDTO compilerRequestDTO)
        {
            var content = new StringContent(
                content: JsonSerializer.Serialize(compilerRequestDTO, _serialezitionOptions),
                encoding: Encoding.UTF8,
                mediaType: "application/json");

            var response = await _httpClient.PostAsync("https://api.jdoodle.com/v1/execute", content);

            if(response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var successfulResponse = JsonSerializer.Deserialize<SuccessfulCompilerResponse>(responseContent, _deserialiazitionOptions);
            }
            else
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var unsuccessfulResponse = JsonSerializer.Deserialize<UnsuccessfulCompilerResponse>(responseContent, _deserialiazitionOptions);
            }
        }
    }
}

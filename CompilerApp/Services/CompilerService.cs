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

        private readonly JsonSerializerOptions _serializationOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        private readonly JsonSerializerOptions _deserializationOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public CompilerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CompilerResponseDTO> CompileAsync(CompilerRequestDTO compilerRequestDTO)
        {
            var content = new StringContent(
                content: JsonSerializer.Serialize(compilerRequestDTO, _serializationOptions),
                encoding: Encoding.UTF8,
                mediaType: "application/json");

            var response = await _httpClient.PostAsync("https://api.jdoodle.com/v1/execute", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return DeserializeSuccessfulResponse(responseContent);
            }
            else
            {
                return DeserializeUnsuccessfulResponse(responseContent);
            }
        }

        private CompilerResponseDTO DeserializeSuccessfulResponse(string responseContent)
        {
            var response = JsonSerializer.Deserialize<SuccessfulCompilerResponse>(responseContent, _deserializationOptions);

            return new CompilerResponseDTO
            {
                Output = response.Output,
                StatusCode = response.StatusCode,
                Memory = response.Memory,
                CpuTime = response.CpuTime
            };
        }

        private CompilerResponseDTO DeserializeUnsuccessfulResponse(string responseContent)
        {
            var response = JsonSerializer.Deserialize<UnsuccessfulCompilerResponse>(responseContent, _deserializationOptions);

            return new CompilerResponseDTO
            {
                Error = response.Error,
                StatusCode = response.StatusCode
            };
        }
    }
}

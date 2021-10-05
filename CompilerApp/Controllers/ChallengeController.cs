using CompilerApp.Data;
using CompilerApp.DTOs;
using CompilerApp.Entities;
using CompilerApp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace ConstrolerApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChallengeController : ControllerBase
    {
        private readonly ICompilerService _compilerService;
        private readonly DataContext _dataContext;
        private readonly CredentialsDTO _credentials;

        public ChallengeController(
            ICompilerService compilerService, IOptions<CredentialsDTO> credentials, DataContext dataContext)
        {
            _compilerService = compilerService;
            _dataContext = dataContext;
            _credentials = credentials.Value;
        }

        [HttpPost("submitTask")]
        public async Task<OkObjectResult> SubmitTask(ChallengeFormDTO challengeForm)
        {
            var request = MapCompilerRequest(challengeForm);
            var response = await _compilerService.CompileAsync(request);

            var summary = MapCompilationSummary(challengeForm, response);
            _dataContext.Summaries!.Add(summary);
            await _dataContext.SaveChangesAsync();

            return Ok(response);
        }

        private CompilerRequestDTO MapCompilerRequest(ChallengeFormDTO challengeFormDTO)
        {
            return new CompilerRequestDTO
            {
                ClientId = _credentials.ClientId,
                ClientSecret = _credentials.ClientSecret,
                Language = "python3",
                Script = challengeFormDTO.SourceCode,
                StdIn = string.Empty,
                VersionIndex = "3"
            };
        }

        private CompilationSummary MapCompilationSummary(
            ChallengeFormDTO challengeForm, CompilerResponseDTO compilerResponse)
        {
            return new CompilationSummary
            {
                Name = challengeForm.Name,
                SourceCode = challengeForm.SourceCode,
                Output = compilerResponse.Output,
                StatusCode = compilerResponse.StatusCode,
                Memory = compilerResponse.Memory,
                CpuTime = compilerResponse.CpuTime,
                Error = compilerResponse.Error
            };
        }
    }
}

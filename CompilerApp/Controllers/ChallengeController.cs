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

        public ChallengeController(ICompilerService compilerService, IOptions<CredentialsDTO> credentials, DataContext dataContext)
        {
            _compilerService = compilerService;
            _dataContext = dataContext;
            _credentials = credentials.Value;
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

        [HttpPost("submitTask")]
        public async Task<OkObjectResult> SubmitTask(ChallengeFormDTO challengeForm)
        {
            var compilerRequest = MapCompilerRequest(challengeForm);
            var response = await _compilerService.CompileAsync(compilerRequest);

            var compilationSummary = new CompilationSummary
            {
                Name = challengeForm.Name,
                SourceCode = challengeForm.SourceCode,
                Output = response.Output,
                StatusCode = response.StatusCode,
                Memory = response.Memory,
                CpuTime = response.CpuTime,
                Error = response.Error
            };

            _dataContext.Summaries.Add(compilationSummary);
            await _dataContext.SaveChangesAsync();

            return Ok(response);
        }

        
    }
}

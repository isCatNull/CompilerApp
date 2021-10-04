using CompilerApp.DTOs;
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
        private readonly CredentialsDTO _credentials;

        public ChallengeController(ICompilerService compilerService, IOptions<CredentialsDTO> credentials)
        {
            _compilerService = compilerService;
            _credentials = credentials.Value;
        }

        private CompilerRequestDTO MapCompilerRequest(ChallengeFormDTO challengeFormDTO)
        {
            return new CompilerRequestDTO 
            { 
                ClientId = _credentials.ClientId,
                ClientSecret = _credentials.ClientSecret, 
                Language = "csharp",
                Script = challengeFormDTO.SourceCode,
                StdIn = string.Empty,
                VersionIndex = "3"
            };
        }

        [HttpPost("submitTask")]
        public async Task<IActionResult> SubmitTask(ChallengeFormDTO challengeForm)
        {
            var compilerRequest = MapCompilerRequest(challengeForm);
            await _compilerService.CompileAsync(compilerRequest);

            return Ok();
        }
    }
}

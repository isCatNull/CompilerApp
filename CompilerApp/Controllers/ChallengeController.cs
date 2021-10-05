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

            return Ok(response);
        }

        
    }
}

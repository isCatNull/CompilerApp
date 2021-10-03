using CompilerApp.DTOs;
using Microsoft.AspNetCore.Mvc;


namespace ConstrolerApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChallengeController : ControllerBase
    {
        [HttpPost("submitTask")]
        public IActionResult SubmitTask(ChallengeFormDTO challengeForm)
        {
            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyntaxSugar.Application.Interface;

namespace SyntaxSugar.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class HelpController : ControllerBase    
    {
        private readonly IAiRequestService aiRequestService;
        public HelpController(IAiRequestService aiRequestService)
        {
            this.aiRequestService = aiRequestService;
        }

        [HttpGet("GetHints")]
        public async Task<IActionResult> GetHints(Guid problemId)
        {
            if (problemId == Guid.Empty)
            {
                return BadRequest("Invalid problem ID.");
            }
            var hints = await aiRequestService.GetHintsAsync(problemId);
            if (hints == null || !hints.Any())
            {
                return NotFound("No hints available for this problem.");
            }
            return Ok(hints);
        }

        [HttpGet("ClassifyProblem")]
        public async Task<IActionResult> ClassifyProblem(Guid problemId, string userDoubt)
        {
            if (problemId == Guid.Empty || string.IsNullOrEmpty(userDoubt))
            {
                return BadRequest("Invalid problem ID or user doubt.");
            }
            var classification = await aiRequestService.ClassifyProblem(problemId, userDoubt);
            if (string.IsNullOrEmpty(classification))
            {
                return NotFound("Classification could not be generated at this time.");
            }
            return Ok(classification);
        }

    }
}

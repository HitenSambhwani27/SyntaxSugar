using Microsoft.AspNetCore.Mvc;
using SyntaxSugar.Application.DTOs;
using SyntaxSugar.Application.Interface;
using SyntaxSugar.Core.Entities;
using System.Security.Claims;

namespace SyntaxSugar.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProblemController : ControllerBase
    {
        private readonly IProblemService _problemService;
        public ProblemController(IProblemService problemService)
        {
            this._problemService = problemService;
        }
        private Guid? GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (Guid.TryParse(userIdClaim, out Guid userId))
            {
                return userId;
            }
            return null;
        }

        [HttpGet("GetProblems")]
        public async Task<IActionResult> GetProblems()
        {
            var userId = GetCurrentUserId();
            var res = await _problemService.GetProblemsAsync(userId);
            return Ok(res);
        }

        [HttpGet("GetProblemById")]
        public async Task<IActionResult> GetProblemById(Guid problemId)
        {
            var userId = GetCurrentUserId();
            var problem = _problemService.GetProblemByIdAsync(problemId, userId);
            if (problem == null)
            {
                return NotFound();
            }
            return Ok(problem);
        }
        [HttpGet("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _problemService.GetCategoriesAsync();
            if (categories == null || !categories.Any())
            {
                return NotFound("No categories found.");
            }
            return Ok(categories);
        }
        [HttpGet("Gettags")]
        public async Task<IActionResult> GetTags()
        {
            var tags = await _problemService.GetTagsAsync();
            if (tags == null || !tags.Any())
            {
                return NotFound("No tags found.");
            }
            return Ok(tags);
        }
    }
}
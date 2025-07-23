using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyntaxSugar.Application.DTOs;
using SyntaxSugar.Application.Interface;
using SyntaxSugar.Core.Interfaces;
using System.Security.Claims;

namespace SyntaxSugar.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminProblemService _problemService;

        public AdminController(IAdminProblemService problemService)
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

        [HttpPost("AddProblem")]
        public async Task<IActionResult> AddProblem(AdminProblemDto problemDetails)
        {
            if(problemDetails == null || string.IsNullOrEmpty(problemDetails.Name) || string.IsNullOrEmpty(problemDetails.Description))
            {
                return BadRequest("Invalid problem data.");
            }
            var problemId = await _problemService.AddProblemAsync(problemDetails);
            if (problemId == Guid.Empty)
            {
                return BadRequest("Problem could not be added.");
            }
            return Ok(problemId);
        }

        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory(CreateCategoryDto categoryDto)
        {
            if (categoryDto == null || string.IsNullOrEmpty(categoryDto.Name))
            {
                return BadRequest("Invalid category data.");
            }
            var categoryId = await _problemService.AddCategoryAsync(categoryDto);
            if (categoryId == Guid.Empty)
            {
                return BadRequest("Category could not be added.");
            }
            return Ok(categoryId);
        }

        [HttpPost("AddTag")]
        public async Task<IActionResult> AddTags(CreateTagDto tagDto)
        {
            if (tagDto == null || string.IsNullOrEmpty(tagDto.Name))
            {
                return BadRequest("Invalid tag data.");
            }
            var tagId = await _problemService.AddTagAsync(tagDto);
            if (tagId == Guid.Empty)
            {
                return BadRequest("Tag could not be added.");
            }
            return Ok(tagId);
        }
    }
}

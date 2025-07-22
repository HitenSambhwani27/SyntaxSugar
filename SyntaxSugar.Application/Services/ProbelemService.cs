using SyntaxSugar.Application.DTOs;
using SyntaxSugar.Application.Interface;
using SyntaxSugar.Core.Entities;
using SyntaxSugar.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxSugar.Application.Services
{
    public class ProbelemService: IProblemService
    {
        private readonly IProblemRepository _problemRepository;
        private readonly ICacheService _cacheService;
        private readonly IUserProblemRepository _userProblemRepository;

        public ProbelemService(IProblemRepository problemRepository, ICacheService cacheService, IUserProblemRepository userProblemRepository)
        {
            this._problemRepository = problemRepository;
            this._cacheService = cacheService;
            this._userProblemRepository = userProblemRepository;
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
          const string cacheKey = "categories:allProblems";
            var categories = await _cacheService.GetAsync<IEnumerable<CategoryDto>>(cacheKey);

            if(categories != null)
            {
                return categories;
            }
            var categoryEntities = await _problemRepository.GetCategoriesAsync();
            var categoryDtos = categoryEntities.Select(c => new CategoryDto { Id = c.Id, Name = c.Name }).ToList();

            await _cacheService.SetAsync(cacheKey, categoryDtos, TimeSpan.FromHours(12));
            return categoryDtos;
        }

        public async Task<ProblemDetailsDto> GetProblemByIdAsync(Guid problemId, Guid? userId)
        {
            string cacheKey = $"Problem: {problemId}";
            var cacheProblem = _cacheService.GetAsync<ProblemDetailsDto>(cacheKey);

            ProblemDetailsDto problemDto;
            if (cacheProblem != null)
            {
                problemDto = await cacheProblem;
            }
            else
            {
                var problemDb = await _problemRepository.GetProblemByIdAsync(problemId);
                if (problemDb == null)
                {
                    return null;
                }
                problemDto = MapToProblemDetailsDto(problemDb);

                await _cacheService.SetAsync(cacheKey, problemDto, TimeSpan.FromHours(12));
            }

            if (userId != null)
            {
                var userProblemStatus = await _userProblemRepository.GetUserProblemStatusAsync(userId.Value, problemId);
                problemDto.IsSolved = userProblemStatus?.IsSolved;
            }
            return problemDto;
        }

        private ProblemDetailsDto MapToProblemDetailsDto(Problem problem)
        {
            return new ProblemDetailsDto
            {
                Id = problem.Id,
                Name = problem.Name,
                Difficulty = problem.Difficulty,
                Category = problem.Category?.Name,
                Tags = problem.ProblemTags.Select(pt => pt.Tag?.Name).ToList()
            };
        }
        private ProblemDTO MapToProblemSummaryDto(Problem problem)
        {
            return new ProblemDTO
            {
                Id = problem.Id,
                Name = problem.Name,
                Difficulty = problem.Difficulty,
                Category = problem.Category?.Name,
                Tags = problem.ProblemTags.Select(pt => pt.Tag?.Name).ToList()
            };
        }

        public async Task<IEnumerable<ProblemDTO>> GetProblemsAsync(Guid? userId)
        {
            var problemsDb = await _problemRepository.GetProblemsAsync();
            var problemDtos = problemsDb.Select(p => MapToProblemSummaryDto(p)).ToList();

            return problemDtos;
        }

        public async Task<IEnumerable<TagDto>> GetTagsAsync()
        {
            const string cacheKey = "tags:all";
            var cachedTags = await _cacheService.GetAsync<IEnumerable<TagDto>>(cacheKey);

            if (cachedTags != null)
            {
                return cachedTags; 
            }

            var tagsDb = await _problemRepository.GetTagsAsync();
            var tagDtos = tagsDb.Select(t => new TagDto { Id = t.Id, Name = t.Name }).ToList();

            await _cacheService.SetAsync(cacheKey, tagDtos, TimeSpan.FromHours(12));

            return tagDtos;
        }
    }
}

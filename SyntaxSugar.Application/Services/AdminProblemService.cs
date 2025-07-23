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
    public class AdminProblemService : IAdminProblemService
    {
        private readonly ICacheService _cacheService;
        private readonly IProblemWriteRepository _problemWriteRepository;
        public AdminProblemService(ICacheService cacheService, IProblemWriteRepository problemWriteRepository)
        {
            this._cacheService = cacheService;
            this._problemWriteRepository = problemWriteRepository;
        }
        public async Task<Guid> AddCategoryAsync(CreateCategoryDto categoryDto)
        {
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = categoryDto.Name
            };

            await _problemWriteRepository.AddCategoryAsync(category);

            await _cacheService.DeleteAsync("categories:allProblems");
            return category.Id;
        }

        public async Task<Guid> AddProblemAsync(AdminProblemDto problemDetailsDto)
        {
            var problem = new Problem
            {
                Id = Guid.NewGuid(),
                Name = problemDetailsDto.Name,
                Description = problemDetailsDto.Description,
                Difficulty = problemDetailsDto.Difficulty,
                CategoryId = problemDetailsDto.CategoryId,
            };

            foreach (var tagId in problemDetailsDto.TagIds)
            {
                problem.ProblemTags.Add(new ProblemTag
                {
                    ProblemId = problem.Id,
                    TagId = tagId
                });

            }
            await _problemWriteRepository.AddProblemAsync(problem);
            return problem.Id;

        }

        public async Task<Guid> AddTagAsync(CreateTagDto createTagDto)
        {
            var tag = new Tag { Id = Guid.NewGuid(), Name = createTagDto.Name };
            await _problemWriteRepository.AddTagAsync(tag);

            await _cacheService.DeleteAsync("tags:all");

            return tag.Id;
        }

        public Task<Guid> UpdateProblemAsync(Guid problemId)
        {
            throw new NotImplementedException();
        }
    }
}

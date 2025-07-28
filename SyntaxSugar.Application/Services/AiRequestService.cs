using SyntaxSugar.Application.Interface;
using SyntaxSugar.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxSugar.Application.Services
{
    public class AiRequestService : IAiRequestService
    {
        private readonly ICacheService _cacheService;
        private readonly IProblemRepository _problemWriteRepository;
        private readonly IAiService _aiService;
        public AiRequestService(IAiService aiService, ICacheService cacheService, IProblemRepository problemWriteRepository)
        {
            this._aiService = aiService;
            this._cacheService = cacheService;
            this._problemWriteRepository = problemWriteRepository;
        }
        public async Task<string> ClassifyProblem(Guid problemId, string userDoubt)
        {
           var key = $"ProblemClassification_{problemId}_{userDoubt}";
           var cachedClassification = await _cacheService.GetAsync<string>(key);
            if (cachedClassification != null)
            {
                return cachedClassification;
            }
            var problem = await _problemWriteRepository.GetProblemByIdAsync(problemId);
            var classification = await _aiService.ClassifyProblem(problem.Name, problem.Description, userDoubt);
            if (string.IsNullOrEmpty(classification))
            {
                return "Sorry, classification could not be generated at this time.";
            }
            await _cacheService.SetAsync(key, classification, TimeSpan.FromHours(1));
            return classification;
        }

        public async Task<List<string>> GetHintsAsync(Guid problemId)
        {
            string key = $"Hints_{problemId}";
            var cachedHints = await  _cacheService.GetAsync<List<string>>(key);
            if (cachedHints != null)
            {
                return cachedHints;
            }
            var problem = await _problemWriteRepository.GetProblemByIdAsync(problemId);
            if (problem == null)
            {
                return new List<string> { "Problem not found." };
            }
            var hints = await _aiService.GetHintsAsync(problem.Name, problem.Description);
            if (hints == null || !hints.Any())
            {
                return new List<string> { "No hints available." };
            }
            await _cacheService.SetAsync(key, hints, TimeSpan.FromDays(30));
            return hints;
        }
    }
}

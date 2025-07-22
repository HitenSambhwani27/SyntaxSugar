using SyntaxSugar.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxSugar.Application.Interface
{
    public interface IProblemService
    {
        Task<IEnumerable<ProblemDTO>> GetProblemsAsync(Guid? userId);
        Task<ProblemDetailsDto> GetProblemByIdAsync(Guid problemId, Guid? userId);
        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
        Task<IEnumerable<TagDto>> GetTagsAsync();
    }
}

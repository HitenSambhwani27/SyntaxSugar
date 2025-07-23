using SyntaxSugar.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxSugar.Application.Interface
{
    public interface IAdminProblemService
    {
        Task<Guid> AddProblemAsync(AdminProblemDto problemDetailsDto);
        Task<Guid> UpdateProblemAsync(Guid problemId);
        Task<Guid> AddCategoryAsync(CreateCategoryDto createCategoryDto);
        Task<Guid> AddTagAsync(CreateTagDto tagDto);
    }
}

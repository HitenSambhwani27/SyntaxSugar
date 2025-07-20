using SyntaxSugar.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxSugar.Core.Interfaces
{
    public interface IProblemWriteRepository
    {
        Task AddProblemAsync(Problem problem);
        Task UpdateProblemAsync(Problem problem);
        Task DeleteProblemAsync(Guid id);
        Task AddCategoryAsync(Category category);   
        Task UpdateCategoryAsync(Category category);
        Task AddTagAsync(Tag tag);
        Task UpdateTagAsync(Tag tag);


    }
}

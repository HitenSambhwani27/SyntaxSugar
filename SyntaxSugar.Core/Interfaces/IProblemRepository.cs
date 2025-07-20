using SyntaxSugar.Core.Entities;
using SyntaxSugar.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxSugar.Core.Interfaces
{
    public  interface IProblemRepository
    {
        Task<IEnumerable<Problem>> GetProblemsAsync();
        Task<Problem> GetProblemByIdAsync(Guid id);
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<IEnumerable<Tag>> GetTagsAsync();
        Task<UserProblemStatus> GetUserProblemStatusAsync(Guid userId, Guid problemId);


    }
}

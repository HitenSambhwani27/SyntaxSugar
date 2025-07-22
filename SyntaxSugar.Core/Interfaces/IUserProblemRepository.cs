using SyntaxSugar.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxSugar.Core.Interfaces
{
    public interface IUserProblemRepository
    {
        Task AddOrUpdateUserProblemStatusAsync(UserProblemStatus status);
        Task<UserProblemStatus> GetUserProblemStatusAsync(Guid userId, Guid problemId);
    }
}

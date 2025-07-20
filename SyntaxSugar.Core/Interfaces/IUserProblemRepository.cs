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
        Task AddUserProblemAsync(Guid userId, Guid problemId);
        Task UpdateUserProblemStatus(Guid userId, Guid problemId, UserProblemStatus status);
    }
}

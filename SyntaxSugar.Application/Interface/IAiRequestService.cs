using SyntaxSugar.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxSugar.Application.Interface
{
    public interface IAiRequestService
    {
        Task<List<string>> GetHintsAsync(Guid problemId);

        Task<string> ClassifyProblem(Guid problemId, string userDoubt);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxSugar.Core.Interfaces
{
    public interface IAiService
    {
        Task<List<string>> GetHintsAsync(string problemName, string problemDescription);

        Task<string> ClassifyProblem(string problemName, string problemDescription, string userDoubt);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxSugar.Core.Interfaces
{
    public interface IPasswordService
    {
        string HashedPassword(string password);
        bool VerifyPassword(string password, string HashedPassword);
    }
}

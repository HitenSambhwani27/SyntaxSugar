using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxSugar.Core.Interfaces
{
    public  interface IBlacklistTokenService
    {
        Task BlackListTokens(string token);
        Task<bool> IsTokenBlacklisted(string token);
    }
}

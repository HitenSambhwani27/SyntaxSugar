﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxSugar.Core.Interfaces
{
    public interface ITokenService
    {
         string GetToken( Guid id, string name, string role);
    }
}

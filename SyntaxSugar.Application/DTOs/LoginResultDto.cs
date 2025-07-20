using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxSugar.Application.DTOs
{
    public class LoginResultDto
    {
        public bool Succeeded;
        public string LoginToken { get; set; }

        public string Error { get; set; }

    }
}

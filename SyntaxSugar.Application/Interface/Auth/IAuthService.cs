using SyntaxSugar.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxSugar.Application.Interface.Auth
{
    public interface IAuthService
    {
        Task<bool> RegisterUserAsync (RegisterUserDTO userDTO);

        Task<LoginResultDto> LoginUserAsync (LoginUserDTO userDTO);

        Task LogoutUserAsync(string token);

    }
}

using SyntaxSugar.Application.DTOs;
using SyntaxSugar.Application.Interface;
using SyntaxSugar.Core.Entities;
using SyntaxSugar.Core.Enums;
using SyntaxSugar.Core.Interfaces;
using SyntaxSugar.Infrastructure.Data;
using SyntaxSugar.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxSugar.Application.Services
{
    public  class AuthService: IAuthService
    {
        private IUserRepository _userRepository;
        private readonly IPasswordService _passwordHasher;
        private readonly ITokenService _tokenService;
        private readonly IBlacklistTokenService _blacklistTokenService;

        public AuthService(IUserRepository userRepository, IPasswordService passwordHasher, ITokenService tokenService, IBlacklistTokenService blacklistTokenService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _blacklistTokenService = blacklistTokenService;
        }
        public async Task<bool> RegisterUserAsync(RegisterUserDTO userDTO)
        {
            try
            {
                if (await _userRepository.GetUserByEmailAsync(userDTO.Email) != null)
                {
                    return false; 
                }
                    var user = new User
                    {
                        Id = Guid.NewGuid(),
                        Username = userDTO.UserName,
                        Email = userDTO.Email,
                        PasswordHash = _passwordHasher.HashedPassword(userDTO.Password),
                        Role = Role.Admin 
                    };
                    await _userRepository.AddUserAsync(user);
                    return true;
                

            }
            catch ( Exception exception)
            {
                
            }
            return false;
        }
        public async Task<LoginResultDto> LoginUserAsync(LoginUserDTO userDTO)
        {
           if( await _userRepository.GetUserByNameAsync(userDTO.UserName) == null)
            {
                return new LoginResultDto
                {
                    Succeeded = false,
                    Error = "Incorrect Username or Username not found",
                };
            }
            var user = await _userRepository.GetUserByNameAsync(userDTO.UserName);
            if(!_passwordHasher.VerifyPassword(user.PasswordHash, userDTO.Password))
            {
                return new LoginResultDto
                {
                    Succeeded = false,
                    Error = "Incorrect Password",
                };
            }
            var token =  _tokenService.GetToken(user.Id, user.Username, user.Role.ToString());

            return new LoginResultDto
            {
                Succeeded = true,
                LoginToken = token,
            };

        }

        public async Task LogoutUserAsync(string token)
        {
            if (!await _blacklistTokenService.IsTokenBlacklisted(token))
            {
                _blacklistTokenService.BlackListTokens(token);
            }

        }

    }
 }


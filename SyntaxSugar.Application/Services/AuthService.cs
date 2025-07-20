using SyntaxSugar.Application.DTOs;
using SyntaxSugar.Application.Interface.Auth;
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

        public AuthService(IUserRepository userRepository, IPasswordService passwordHasher, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
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
                        Role = Role.User // Assuming default role is User
                    };
                    await _userRepository.AddUserAsync(user);
                    return true;
                

            }
            catch ( Exception exception)
            {
                // Log exception (not implemented here)
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
            var token =  _tokenService.GetToken(user.Id, user.Username);

            return new LoginResultDto
            {
                Succeeded = true,
                LoginToken = token,
            };

        }

    }
 }


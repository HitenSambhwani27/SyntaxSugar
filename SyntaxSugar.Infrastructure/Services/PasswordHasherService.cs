﻿using Microsoft.AspNetCore.Identity;
using SyntaxSugar.Core.Entities;
using SyntaxSugar.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxSugar.Infrastructure.Services
{
    public class PasswordHasherServices : IPasswordService
    {
        private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();
        public string HashedPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password cannot be null or empty", nameof(password));
            }
            return _passwordHasher.HashPassword(null, password);
        }

        public bool VerifyPassword( string HashedPassword, string password)
        {
            var result = _passwordHasher.VerifyHashedPassword(null, HashedPassword, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SyntaxSugar.Application.DTOs
{
    public class RegisterUserDTO
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

    }
}

using SyntaxSugar.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxSugar.Application.DTOs
{
    public class AdminProblemDto
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        public Difficulty Difficulty { get; set; }

        public List<Guid> TagIds { get; set; } = new List<Guid>();

    }
    public class CreateCategoryDto
    {
        [Required]
        public string Name { get; set; }
    }

    public class CreateTagDto
    {
        [Required]
        public string Name { get; set; }
    }
}

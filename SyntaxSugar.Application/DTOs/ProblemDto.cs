using SyntaxSugar.Core.Entities;
using SyntaxSugar.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxSugar.Application.DTOs
{
    public class ProblemDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Difficulty Difficulty { get; set; }
        public string Category { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
        public bool? IsSolved { get; set; }

    }
}

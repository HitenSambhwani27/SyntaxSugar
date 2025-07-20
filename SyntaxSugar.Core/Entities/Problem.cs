using SyntaxSugar.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxSugar.Core.Entities
{
    public class Problem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Difficulty Difficulty { get; set; }
        public Guid CategoryId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Category Category { get; set; }
        public ICollection<ProblemTag> ProblemTags { get; set; } = new List<ProblemTag>();
        public ICollection<UserProblemStatus> UserProblemStatuses { get; set; } = new List<UserProblemStatus>();



    }
}

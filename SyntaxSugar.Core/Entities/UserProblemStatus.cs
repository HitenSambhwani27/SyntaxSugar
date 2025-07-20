using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxSugar.Core.Entities
{
    public class UserProblemStatus
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid ProblemId { get; set; }
        public Problem Problem { get; set; }
        public DateTime? LastAttemptedAt { get; set; }
        public bool IsSolved { get; set; }
        public int AttemptsCount { get; set; } = 0;
    }
}

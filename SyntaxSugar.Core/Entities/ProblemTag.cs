using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxSugar.Core.Entities
{
    public class ProblemTag
    {
        public Guid ProblemId { get; set; }
        public Problem Problem { get; set; }

        public Guid TagId { get; set; }
        public Tag Tag { get; set; }
    }
}

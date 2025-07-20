using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxSugar.Core.Entities
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<ProblemTag> ProblemTags { get; set; } = new List<ProblemTag>();

    }
}

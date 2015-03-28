using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Project
    {
        public Project()
        {
            Periods = new List<Period>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<Period> Periods { get; set; }
    }
}

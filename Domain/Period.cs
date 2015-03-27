using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Period
    {
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }

        public ICollection<EmployeeInRole> EmployeesInRoles { get; set; }
        public ICollection<RoleWage> RoleWagesInPeriod { get; set; }
    }
}

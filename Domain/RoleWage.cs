using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class RoleWage
    {
        public EmployeeRole Role { get; set; }

        public Decimal HourlyWage { get; set; }
    }
}

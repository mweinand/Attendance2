using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Attendance.Web.Models.Clock
{
    public class CurrentEmployeesViewModel
    {
        public List<Attendance.Core.Domain.Employee> Employees { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
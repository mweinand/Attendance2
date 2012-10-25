using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Attendance.Web.Models.Employee
{
    public class EmployeeIndexModel
    {
        public IEnumerable<Core.Domain.Employee> Employees { get; set; }
    }
}
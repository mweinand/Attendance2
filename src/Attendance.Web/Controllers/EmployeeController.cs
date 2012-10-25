using Attendance.Core.Repositories;
using Attendance.Web.Models.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Attendance.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeController()
        {
            employeeRepository = new EmployeeRepository();
        }

        //
        // GET: /Employee/

        public ActionResult Index()
        {
            var model = new EmployeeIndexModel();

            model.Employees = employeeRepository.GetEmployeesInCompany("1");

            return View(model);
        }

    }
}

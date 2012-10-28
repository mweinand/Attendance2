using Attendance.Core.Domain;
using Attendance.Core.Infrastructure;
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
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork<Employee> _employeeUnitOfWork;

        public EmployeeController(IEmployeeRepository employeeRepository, IUnitOfWork<Employee> employeeUnitOfWork)
        {
            _employeeRepository = employeeRepository;
            _employeeUnitOfWork = employeeUnitOfWork;
        }

        //
        // GET: /Employee/

        public ActionResult Index()
        {
            var model = new EmployeeIndexModel();

            model.Employees = _employeeRepository.GetEmployeesInCompany("1");

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new EmployeeViewModel();
            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult Create(EmployeeInputModel inputModel)
        {
            var newEmployee = new Employee();
            newEmployee.CompanyId = "1";
            newEmployee.SerialNumber = inputModel.Serial1;
            newEmployee.FirstName = inputModel.FirstName;
            newEmployee.LastName = inputModel.LastName;

            _employeeUnitOfWork.Initialize();
            _employeeUnitOfWork.Insert(newEmployee);
            _employeeUnitOfWork.Execute();

            return RedirectToAction("Index");
        }

    }
}

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

        [HttpGet]
        public ActionResult Edit(string id)
        {
            var model = new EmployeeViewModel();
            var employee = _employeeRepository.FindEmployeeById("1", id);
            if (employee == null)
            {
                return new HttpNotFoundResult();
            }
            model.FirstName = employee.FirstName;
            model.LastName = employee.LastName;
            model.Serial1 = employee.SerialNumber;
            model.Serial2 = employee.SerialNumber;
            model.OriginalSerial = employee.SerialNumber;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EmployeeInputModel inputModel)
        {
            var employee = _employeeRepository.FindEmployeeById("1", inputModel.OriginalSerial);
            if (employee == null)
            {
                return new HttpNotFoundResult();
            }

            if (inputModel.OriginalSerial != inputModel.Serial1)
            {
                _employeeUnitOfWork.Initialize();
                _employeeUnitOfWork.Delete(employee);
                _employeeUnitOfWork.Execute();

				employee = new Employee();
				employee.CompanyId = "1";
            }

            employee.SerialNumber = inputModel.Serial1;
            employee.FirstName = inputModel.FirstName;
            employee.LastName = inputModel.LastName;

			_employeeUnitOfWork.Initialize();
            _employeeUnitOfWork.InsertOrReplace(employee);
            _employeeUnitOfWork.Execute();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Import()
        {
            var model = new ImportViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Import(ImportInputModel inputModel)
        {
            var existingEmployees = _employeeRepository.GetEmployeesInCompany("1");
            var rows = inputModel.ImportText.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            _employeeUnitOfWork.Initialize();

            foreach (var row in rows)
            {
                var parts = row.Split(',');
                var serial = parts[0];
                if (parts.Length != 3 || existingEmployees.Any(e => e.SerialNumber == serial))
                {                    
                    continue;
                }

                var newEmployee = new Employee();
                newEmployee.CompanyId = "1";
                newEmployee.SerialNumber = serial;
                newEmployee.FirstName = parts[1];
                newEmployee.LastName = parts[2];

                _employeeUnitOfWork.Insert(newEmployee);

            }
            _employeeUnitOfWork.Execute();
            return RedirectToAction("Index");
        }
    }
}

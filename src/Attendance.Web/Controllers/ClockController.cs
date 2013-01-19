using Attendance.Core.Domain;
using Attendance.Core.Infrastructure;
using Attendance.Core.Repositories;
using Attendance.Web.Models.Clock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Attendance.Web.Controllers
{
	[Authorize]
    public class ClockController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork<Employee> _employeeUnitOfWork;
        private readonly IUnitOfWork<Punch> _punchUnitOfWork;

        public ClockController(IEmployeeRepository employeeRepository, IUnitOfWork<Employee> employeeUnitOfWork, IUnitOfWork<Punch> punchUnitOfWork)
        {
            _employeeRepository = employeeRepository;
            _employeeUnitOfWork = employeeUnitOfWork;
            _punchUnitOfWork = punchUnitOfWork;
        }

        //
        // GET: /Clock/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CurrentEmployees()
        {
            var model = new CurrentEmployeesViewModel();

            model.Employees = _employeeRepository.GetEmployeesInCompany("1")
				.Where(e => e.TimeIn.HasValue)
				.OrderBy(e => e.LastName).ThenBy(e => e.FirstName)
				.ToList();

            return PartialView(model);
        }

        [HttpPost]
        public JsonResult Punch(PunchInputModel inputModel)
        {
            var employee = _employeeRepository.FindEmployeeById("1", inputModel.Serial);
            var response = new PunchResponseModel();

            if (employee == null)
            {
                response.Result = (int)PunchResult.Error;
                return Json(response);
            }

            response.Result = (int)PunchResult.Success;
            response.Name = String.Format("{0} {1}", employee.FirstName, employee.LastName);

            // already logged in?
            if (employee.TimeIn.HasValue)
            {
                var punch = new Punch();
                punch.RowKey = Guid.NewGuid().ToString();
                punch.SerialNumber = employee.SerialNumber;
                punch.TimeIn = employee.TimeIn.Value;
                punch.TimeOut = DateTime.UtcNow;
                _punchUnitOfWork.Initialize();
                _punchUnitOfWork.Insert(punch);
                _punchUnitOfWork.Execute();
                employee.TimeIn = null;
                response.Direction = -1;
            }
            else
            {
				employee.TimeIn = DateTime.UtcNow;
                response.Direction = 1;
            }

            _employeeUnitOfWork.Initialize();
            _employeeUnitOfWork.InsertOrReplace(employee);
            _employeeUnitOfWork.Execute();

            return Json(response);
        }

    }
}

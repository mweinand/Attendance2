using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Attendance.Core.Domain;
using Attendance.Core.Infrastructure;
using Attendance.Core.Repositories;
using Attendance.Core.Services;
using Attendance.Web.Models.Report;

namespace Attendance.Web.Controllers
{
	[Authorize]
    public class ReportController : Controller
    {
		private readonly IPunchRepository _punchRepository;
		private readonly IEmployeeRepository _employeeRepository;
		private readonly ITardyService _tardyService;
		private readonly IUnitOfWork<Punch> _punchUnitOfWork;

		public ReportController(IPunchRepository punchRepository, IEmployeeRepository employeeRepository, ITardyService tardyService, IUnitOfWork<Punch> punchUnitOfWork)
		{
			_punchRepository = punchRepository;
			_employeeRepository = employeeRepository;
			_tardyService = tardyService;
			_punchUnitOfWork = punchUnitOfWork;
		}

        //
        // GET: /Report/

        public ActionResult Index()
        {
            return View();
        }

		public ActionResult Employee(string id)
		{
			var viewModel = new EmployeeReportViewModel();

			var employee = _employeeRepository.FindEmployeeById("1", id);

			viewModel.Name = String.Format("{0} {1}", employee.FirstName, employee.LastName);
			viewModel.Punches.AddRange(_punchRepository.GetPunchesForEmployee(id).OrderByDescending(p => p.TimeOut).Select(p => new EmployeePunchModel(p)));

			// Tardy Check
			foreach (var punch in viewModel.Punches)
			{
				_tardyService.MarkTardy(punch);
			}
			
			viewModel.TotalTardy = viewModel.Punches.Sum(p => p.TardyTime.TotalMinutes);
			viewModel.TotalHours = viewModel.Punches.Sum(p => (p.TimeOut - p.TimeIn).TotalHours);

			return View(viewModel);
		}

		public ActionResult TooShort()
		{
			var viewModel = new TooShortReportViewModel();
			viewModel.Punches.AddRange(_punchRepository.GetPunchesUnderTime("1", TimeSpan.FromMinutes(5)).OrderByDescending(p => p.TimeOut));

			return View(viewModel);
		}

		public ActionResult DeletePunch(string serialNr, string rowKey)
		{
			var punch = _punchRepository.Get(serialNr, rowKey);
			if(punch == null)
			{
				return RedirectToAction("TooShort");
			}

			_punchUnitOfWork.Initialize();
			_punchUnitOfWork.Delete(punch);
			_punchUnitOfWork.Execute();
			return RedirectToAction("TooShort");
		}

		public ActionResult TooLong()
		{
			var viewModel = new TooLongReportViewModel();
			viewModel.Punches.AddRange(_punchRepository.GetPunchesOverTime("1", TimeSpan.FromHours(12)).OrderByDescending(p => p.TimeOut));

			return View(viewModel);
		}

		public ActionResult UpdateTime(string serialNr, string rowKey, double hours)
		{
			var punch = _punchRepository.Get(serialNr, rowKey);
			if (punch == null)
			{
				return RedirectToAction("TooLong");
			}

			punch.TimeOut = punch.TimeIn.AddHours(hours);

			_punchUnitOfWork.Initialize();
			_punchUnitOfWork.InsertOrReplace(punch);
			_punchUnitOfWork.Execute();
			return RedirectToAction("TooLong");
		}
    }
}

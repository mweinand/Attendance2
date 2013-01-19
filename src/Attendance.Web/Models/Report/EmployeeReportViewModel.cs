using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Attendance.Core.Domain;

namespace Attendance.Web.Models.Report
{
	public class EmployeeReportViewModel
	{
		public EmployeeReportViewModel()
		{
			this.Punches = new List<EmployeePunchModel>();
		}

		public string Name { get; set; }

		public List<EmployeePunchModel> Punches
		{ 
			get; 
			private set; 
		}

		public double TotalTardy { get; set; }
		public double TotalHours { get; set; }
	}
}
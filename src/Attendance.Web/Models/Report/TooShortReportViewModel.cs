using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Attendance.Core.Domain;

namespace Attendance.Web.Models.Report
{
	public class TooShortReportViewModel
	{
		public TooShortReportViewModel()
		{
			Punches = new List<Punch>();
		}
		public List<Punch> Punches { get; private set; }
	}
}
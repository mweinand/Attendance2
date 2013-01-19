using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Attendance.Core.Domain
{
	public class EmployeePunchModel
	{
		public EmployeePunchModel()
		{
		}

		public EmployeePunchModel(Punch source)
		{
			this.TimeIn = TimeZoneInfo.ConvertTimeFromUtc(source.TimeIn, TimeZoneInfo.Local);
			this.TimeOut = TimeZoneInfo.ConvertTimeFromUtc(source.TimeOut, TimeZoneInfo.Local);			
		}

		public DateTime TimeIn { get; set; }
		public DateTime TimeOut { get; set; }

		public bool IsTardy { get; set;}
		public TimeSpan TardyTime { get; set; }
		public string TardyClass
		{
			get { return IsTardy ? "tardy" : null; }
		}
	}
}
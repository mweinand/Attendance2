using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Attendance.Core.Domain;

namespace Attendance.Core.Services
{
	public interface ITardyService
	{
		void MarkTardy(EmployeePunchModel model);
	}
	public class TardyService : ITardyService
	{
		public void MarkTardy(EmployeePunchModel model)
		{
			TimeSpan startTime;
			switch(model.TimeIn.DayOfWeek)
			{
				case DayOfWeek.Monday:
				case DayOfWeek.Tuesday:
				case DayOfWeek.Wednesday:
				case DayOfWeek.Thursday:
					startTime = TimeSpan.FromHours(18);
					break;
				case DayOfWeek.Saturday:
					startTime = TimeSpan.FromHours(8);
					break;
				default:
					startTime = TimeSpan.FromHours(48);
					break;
			}

			model.IsTardy = model.TimeIn.TimeOfDay > startTime;
			if(model.IsTardy)
			{
				model.TardyTime = model.TimeIn.TimeOfDay - startTime;
			}
		}
	}
}

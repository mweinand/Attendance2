using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Attendance.Web.Models.Clock
{
    public enum PunchResult
    {
        Error = 0,
        Success = 1,
        EarlyWarning = 2
    }
}
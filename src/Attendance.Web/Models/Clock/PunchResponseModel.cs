using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Attendance.Web.Models.Clock
{
    public class PunchResponseModel
    {
        public int Result { get; set; }
        public string Name { get; set; }
        public int Direction { get; set; }
    }
}
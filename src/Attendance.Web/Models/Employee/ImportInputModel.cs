using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Attendance.Web.Models.Employee
{
    public class ImportInputModel
    {
        [Required]
        public string ImportText { get; set; }
    }
}
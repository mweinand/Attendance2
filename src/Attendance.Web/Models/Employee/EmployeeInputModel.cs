using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Attendance.Web.Models.Employee
{
    public class EmployeeInputModel
    {
        [Required]
        public string Serial1 { get; set; }
        [Compare("Serial1")]
        public string Serial2 { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [HiddenInput]
        public string OriginalSerial { get; set; }
    }
}
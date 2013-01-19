using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Attendance.Web.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
		[HttpGet]
        public ActionResult Login()
        {
            return View();
        }

		[HttpPost]
		public ActionResult Login(string password)
		{
			if(password == "23818")
			{
				FormsAuthentication.SetAuthCookie("user", false);
				return RedirectToAction("Index", "Clock");
			}
			return RedirectToAction("Login");
		}

		public ActionResult Logout()
		{
			FormsAuthentication.SignOut();
			return RedirectToAction("Index", "Home");
		}

    }
}

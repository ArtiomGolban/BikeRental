using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BikeRental.Web.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Account
        public ActionResult Index()
        {
            SessionStatus();

            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }
    }
}
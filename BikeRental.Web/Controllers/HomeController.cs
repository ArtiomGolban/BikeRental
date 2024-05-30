using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BikeRental.Web.Models;

namespace BikeRental.Web.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            SessionStatus();

            return View();
        }

        public ActionResult About()
        {
            SessionStatus();

            return View();
        }

        public ActionResult Contact()
        {
            SessionStatus();

            return View();
        }

        public ActionResult Listing()
        {
            SessionStatus();

            return View();
        }
    }


}
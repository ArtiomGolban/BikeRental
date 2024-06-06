using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BikeRental.Web.Models;
using BikeRental.Web.Services;

namespace BikeRental.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly BikeService _bikeService;

        public HomeController() : base()
        {
            _bikeService = new BikeService();
        }

        // GET: Home
        public ActionResult Index()
        {
            SessionStatus();

            var bikes = _bikeService.GetAll();

            return View(bikes);
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

            var bikes = _bikeService.GetAll();

            return View(bikes);
        }
    }


}
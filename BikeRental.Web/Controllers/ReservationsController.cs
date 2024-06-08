using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BikeRental.Domain.Enums;
using BikeRental.Web.Extension;
using BikeRental.Web.Models;
using BikeRental.Web.Models.Admin;
using BikeRental.Web.Services;

namespace BikeRental.Web.Controllers
{
    public class ReservationsController : BaseController
    {
        private readonly BikeService _bikeService;
        private readonly ReservationService _reservationService;

        public ReservationsController() : base()
        {
            _bikeService = new BikeService();
            _reservationService = new ReservationService();
        }

        // GET: create reservation form
        public ActionResult Create(int id)
        {
            SessionStatus();

            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("Index", "Login");
            }

            var bike = _bikeService.GetById(id);

            return View(bike);
        }

        // POST: create reservation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReservationCreate newReservation)
        {
            SessionStatus();

            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("Index", "Login");
            }

            if (ModelState.IsValid)
            {
                var res = _reservationService.AddReservation(System.Web.HttpContext.Current.GetMySessionObject(), newReservation);

                if (res == null)
                {
                    ModelState.AddModelError("Reservation", "Error");
                }
                else
                {
                    return RedirectToAction("MyList");
                }
            }

            var bike = _bikeService.GetById(newReservation.BikeId);

            return View(bike);
        }

        public ActionResult MyList()
        {
            SessionStatus();

            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("Index", "Login");
            }

            var reservations = _reservationService.GetAllByUser(System.Web.HttpContext.Current.GetMySessionObject());

            return View(reservations);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cancel(int id)
        {
            SessionStatus();

            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("Index", "Login");
            }

            _reservationService.DeleteForUserById(System.Web.HttpContext.Current.GetMySessionObject(), id);

            return RedirectToAction("MyList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Pay(int id)
        {
            SessionStatus();

            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("Index", "Login");
            }

            var user = System.Web.HttpContext.Current.GetMySessionObject();
            var reservation = _reservationService.GetForUserById(user, id);


            if (user.Balance < reservation.TotalPrice)
            {
                ModelState.AddModelError("Reservation", "Error");
            }
            else
            {
                _reservationService.PayForUserById(user, id);
            }

            return RedirectToAction("MyList");
        }


    }


}
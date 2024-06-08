using System.Web.Mvc;
using BikeRental.Domain.Entities.User;
using BikeRental.Domain.Enums;
using BikeRental.Web.Services;
using BikeRental.Web.Extension;
using BikeRental.Web.Models.Admin;
using BikeRental.Web.Services;
using BikeRental.Web.Models;

namespace BikeRental.Web.Controllers
{
    public class AdminReservationController : BaseController
    {
        private readonly ReservationService _reservationService;

        public AdminReservationController() : base()
        {
            _reservationService = new ReservationService();
        }

        public ActionResult List()
        {
            SessionStatus();

            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("Index", "Login");
            }

            if (System.Web.HttpContext.Current.GetMySessionObject().Level != UserRole.Admin)
            {
                return new HttpStatusCodeResult(403, "Forbidden");
            }

            var reservations = _reservationService.GetAll();

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

            if (System.Web.HttpContext.Current.GetMySessionObject().Level != UserRole.Admin)
            {
                return new HttpStatusCodeResult(403, "Forbidden");
            }

            _reservationService.DeleteById(id);

            return RedirectToAction("List");
        }

        public ActionResult Edit(int id)
        {
            SessionStatus();

            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("Index", "Login");
            }

            if (System.Web.HttpContext.Current.GetMySessionObject().Level != UserRole.Admin)
            {
                return new HttpStatusCodeResult(403, "Forbidden");
            }

            var reservation = _reservationService.GetById(id);

            return View(reservation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ReservationEdit reservationEdit)
        {
            SessionStatus();

            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("Index", "Login");
            }

            if (System.Web.HttpContext.Current.GetMySessionObject().Level != UserRole.Admin)
            {
                return new HttpStatusCodeResult(403, "Forbidden");
            }

            var reservation = _reservationService.UpdateReservation(id, reservationEdit);

            return View(reservation);
        }

    }
}
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
    public class AdminController : BaseController
    {
        // GET: Admin
        public ActionResult Index()
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

            return View();
        }
        
    }
}
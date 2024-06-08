using BikeRental.Web.Extension;
using BikeRental.Web.Models.Admin;
using BikeRental.Web.Services;
using BikeRental.Web.Models;
using BikeRental.Domain.Enums;
using System.Web.Mvc;

namespace BikeRental.Web.Controllers
{
    public class AccountController : BaseController
    {

        private readonly UserService _userService;

        public AccountController() : base()
        {
            _userService = new UserService();
        }

        // GET: Admin
        public ActionResult Index()
        {
            SessionStatus();

            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TopUp(decimal topUpAmount)
        {
            SessionStatus();

            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("Index", "Login");
            }

            _userService.TopUpBalance(System.Web.HttpContext.Current.GetMySessionObject(), topUpAmount);


            return RedirectToAction("Index", "Account");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEmail(string email)
        {
            SessionStatus();

            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("Index", "Login");
            }

            _userService.EditEmail(System.Web.HttpContext.Current.GetMySessionObject().Id, email);


            return RedirectToAction("Index", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(string password)
        {
            SessionStatus();

            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("Index", "Login");
            }

            _userService.ChangePassword(System.Web.HttpContext.Current.GetMySessionObject().Id, password);


            return RedirectToAction("Index", "Account");
        }
    }
}
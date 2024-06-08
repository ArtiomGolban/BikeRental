using System;
using BikeRental.BusinessLogic.Interfaces;
using System.Web.Mvc;
using BikeRental.BusinessLogic;
using BikeRental.Domain.Entities.User;
using BikeRental.Web.Models;
using System.Web;

namespace BikeRental.Web.Controllers
{
    public class LoginController : BaseController
    {
        // GET: Index
        public ActionResult Index()
        {
            SessionStatus();

            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] == "login")
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UserLogin login)
        {
            if (ModelState.IsValid)
            {
                ULoginData data = new ULoginData
                {
                    Username = login.Username,
                    Password = login.Password
                };

                var userLogin = _session.UserLogin(data);

                if (userLogin.Status)
                {
                    HttpCookie cookie = _session.GenCookie(login.Username);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);

                    // Redirect to home page after successful login
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("error", "Invalid credentials");
                    return RedirectToAction("Index", "Login");
                }
            }

            return View();
        }

        // GET: Logout
        public ActionResult Logout()
        {
            SessionStatus();

            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] == "login")
            {
                SessionDestroy();
            }

            return RedirectToAction("Index", "Home");
        }

        // GET: Register
        public ActionResult Register()
        {
            SessionStatus();

            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] == "login")
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(ULoginData register)
        {
            if (ModelState.IsValid)
            {
                var userRegister = _session.UserRegister(register);

                if (userRegister.Status)
                {
                    // Redirect to home page after successful login
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("error", "Registration failed");
                    return View(register);
                }
            }

            return View();
        }
    }
}

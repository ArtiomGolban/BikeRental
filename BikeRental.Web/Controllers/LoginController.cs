using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BikeRental.BusinessLogic;
using BikeRental.BusinessLogic.Interfaces;
using BikeRental.Domain.Entities.User;
using BikeRental.Web.Models;

namespace BikeRental.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly ISession _session;

        public LoginController()
        {
            var bl = new BusinessLogic.BusinessLogic();

            _session = bl.GetSessionBL();
        }

        // GET: Index
        public ActionResult Index()
        {
            return View();
        }

    // GET: Register
    public ActionResult Register()
    {
        return View();
    }

    public LoginController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
        }


    // POST: Register
    [HttpPost]
    public ActionResult Register(ULoginData register)
    {
        if (ModelState.IsValid)
        {
            ULoginData data = new ULoginData
            {
                Username = register.Username,
                Email = register.Email,
                Password = register.Password
            };

            var userRegister = _session.UserRegister(data);

            if (userRegister.Status)
            {
                // Redirect to login page after successful registration
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ModelState.AddModelError("error", "Registration failed");
                return View(register);
            }
        }

     
        return View(register);
    }


    // POST: Login
    [HttpPost]
    public ActionResult Login(UserLogin login)
    {
        if (ModelState.IsValid)

            return View(new UserLogin());
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin login)

        {
            ULoginData data = new ULoginData
            {

                Username = login.Username,
                Password = login.Password
            };

                var userLogin = _session.UserLogin(data);

                if (userLogin.Status) { 
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("error", "Invalid data");
    
                    return RedirectToAction("Index", "Home");
                }
            }

            return RedirectToAction("Index");
        }
    }
}
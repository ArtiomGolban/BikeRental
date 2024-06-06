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
    public class AdminUsersController : BaseController
    {
        private readonly UserService _userService;

        public AdminUsersController() : base()
        {
            _userService = new UserService();
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

            var users = _userService.GetAllUsers();

            return View(users);
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

            var user = _userService.GetById(id);

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UserEdit userEdit)
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

            var user = _userService.UpdateUser(id, userEdit);

            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
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

            _userService.DeleteUser(id);

            return RedirectToAction("List");
        }


        public ActionResult Add()
        {
            return View();
        }

        // POST: Admin/AddUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(UserAdd addUser)
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

            if (ModelState.IsValid)
            {
                var user = _userService.AddUser(addUser);

                if (user == null)
                {
                    ModelState.AddModelError("User", "User with this username already exists");
                }
                else
                {
                    return RedirectToAction("List", "AdminUsers");
                }
            }

            // If model state is not valid or user already exists, return to the AddUser view
            return View(addUser);
        }



    }
}
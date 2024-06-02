using System.Web.Mvc;
using BikeRental.Domain.Entities.User;
using BikeRental.Domain.Enums;
using BikeRental.Web.Services;
using BikeRental.Web.Extension;
using BikeRental.Web.Models.Admin;
using BikeRental.Web.Services;
using BikeRental.BusinessLogic.Core;
using BikeRental.Web.Models;

namespace BikeRental.Web.Controllers
{
    public class AdminController : BaseController
    {
        private readonly UserService _userService;
        private readonly AdminApi _adminApi;

        public AdminController() : base()
        {
            _userService = new UserService();
            _adminApi = new AdminApi();
        }

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

        public ActionResult UsersList()
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


        public ActionResult UserEdit(int id)
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
        public ActionResult UserEdit(int id, UserEdit userEdit)
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
        public ActionResult DeleteUser(int id)
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

            return RedirectToAction("UsersList");
        }


        public ActionResult AddUser()
        {
            return View();
        }

        // POST: Admin/AddUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUser(UserAdd addUser)
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
                // Convert UserAdd to ULoginData
                var userLoginData = new ULoginData
                {
                    Username = addUser.Name,
                    Password = addUser.Password,
                    Email = addUser.Email
                };

                // Use AdminApi to add user
                var result = _adminApi.AddUserLogic(userLoginData);

                if (result.Status)
                {
                    // Get the id of the newly added user from the result
                    int newUserId = result.UserId; // Assuming AddUserLogic returns the new user's id

                    // Redirect to UserEdit with the id of the newly added user
                    return RedirectToAction("UsersList", "Admin", new { id = newUserId });
                }
                else
                {
                    // User with the same username already exists, add a model error
                    ModelState.AddModelError("Username", "User with this username already exists");
                }
            }

            // If model state is not valid or user already exists, return to the AddUser view
            return View(addUser);
        }



    }
}
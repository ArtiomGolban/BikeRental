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
        public class AdminBikesController : BaseController
        {
            private readonly BikeService _bikeService;

            public AdminBikesController() : base()
            {
                _bikeService = new BikeService();
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

                var bikes = _bikeService.GetAll();

                return View(bikes);
            }

            public ActionResult Add()
            {
                return View();
            }

            // POST: Admin/AddUser
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Add(BikeAdd addBike)
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
                    var bike = _bikeService.AddBike(addBike);

                    if (bike == null)
                    {
                        ModelState.AddModelError("Bike", "Error");
                    }
                    else
                    {
                        return RedirectToAction("List", "AdminBikes");
                    }
                }

                // If model state is not valid or user already exists, return to the AddUser view
                return View(addBike);
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

                var bike = _bikeService.GetById(id);

                return View(bike);

            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit(int id, BikeEdit bikeEdit)
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

                var bike = _bikeService.UpdateBike(id, bikeEdit);

                return View(bike);
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

                _bikeService.DeleteBike(id);

                return RedirectToAction("List");
            }

        }
    }
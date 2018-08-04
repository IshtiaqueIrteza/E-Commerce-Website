using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMEntity;
using PMRepository;
using System.IO;

namespace PMApp.Controllers
{
    public class RegistrationController : Controller
    {
        private UserRepository repo = new UserRepository();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(User user)
        {
            if (ModelState.IsValid)
            {
                string valid = repo.RegistrationValidate(user);

                if (valid == "valid")
                {
                    TempData["msg"] = "Registration completed successfully";
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    ViewBag.errorMessage = "User already exists !! Check username or email";
                    return View(user);
                }
            }
            else
            {
                return View(user);
            }
        }

    }
}

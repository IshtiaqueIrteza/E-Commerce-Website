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
    public class LoginController : Controller
    {
        private UserRepository repo = new UserRepository();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string Username, string Password)
        {
            if (Username != "" && Password != "")
            {
                User user = repo.LoginValidate(Username, Password);

                if (user != null) //user login
                {
                    Session["userid"] = user.UserId;
                    Session["username"] = user.Username;
                    Session["usertype"] = "user";

                    return RedirectToAction("BookIndex", "User");
                }
                else if (Username == "admin" && Password == "admin") //admin login
                {
                    Session["usertype"] = "admin";

                    return RedirectToAction("Index", "Book");
                }
                else
                {
                    ViewBag.loginError = "Invalid username or password";
                    return View();
                }
            }
            else
            {
                if (Username == "" && Password == "")
                {
                    ViewBag.ErrorMessage = "Username & Password Required !!";
                }
                else if (Username == "")
                {
                    ViewBag.ErrorMessage = "Username Required !!";
                }
                else if (Password == "")
                {
                    ViewBag.username = Username;
                    ViewBag.ErrorMessage = "Password Required !!";
                }

                return View();
            }
        }

    }
}

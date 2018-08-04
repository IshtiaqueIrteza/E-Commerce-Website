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
    public class BookController : BaseController
    {
        private BookRepository repo = new BookRepository();
        private UserRepository userRepo = new UserRepository();

        public ActionResult Index()
        {
            return View(this.repo.GetAll());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Book b, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var guid = Guid.NewGuid().ToString();
                    var path = Path.Combine(Server.MapPath("~/uploads"), guid + fileName);
                    file.SaveAs(path);
                    string fl = path.Substring(path.LastIndexOf("\\"));
                    string[] split = fl.Split('\\');
                    string newpath = split[1];
                    string imagepath = "/uploads/" + newpath;
                    b.ImagePath = imagepath;
                }
                else
                {
                    b.ImagePath = null;
                }

                if (this.repo.Insert(b))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.errorMessage = "This item (name) already exists !!";
                    return View(b);
                }
                
            }
            else
            {
                return View(b);
            }
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            Book b = this.repo.Get(id);
            return View(b);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Book b = this.repo.Get(id);
            return View(b);
        }

        [HttpPost]
        public ActionResult Edit(Book b, HttpPostedFileBase file, string image)
        {
            bool warning = false;

            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var guid = Guid.NewGuid().ToString();
                    var path = Path.Combine(Server.MapPath("~/uploads"), guid + fileName);
                    file.SaveAs(path);
                    string fl = path.Substring(path.LastIndexOf("\\"));
                    string[] split = fl.Split('\\');
                    string newpath = split[1];
                    string imagepath = "/uploads/" + newpath;
                    b.ImagePath = imagepath;
                }
                else
                {
                    warning = true;
                }

                //query
                if (this.repo.Update(b, warning)) //positive result
                {
                    return RedirectToAction("Index");
                }
                else //negative result
                {
                    ViewBag.errorMessage = "This item (name) already exists !!";
                    b.ImagePath = image;
                    return View(b);
                }
                
            }
            else
            {
                b.ImagePath = image;
                return View(b);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Book b = this.repo.Get(id);
            return View(b);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            this.repo.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Logout(string logout)
        {
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult TopUser()
        {
            return View(this.userRepo.ReturnTopUsers());
        }

        [HttpPost]
        public ActionResult ViewUserTransaction()
        {
            return View(this.userRepo.ReturnUserTransaction());
        }
    }
}

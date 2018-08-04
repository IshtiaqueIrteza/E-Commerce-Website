using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PMApp.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string url = Request.Url.ToString();
            ViewBag.d = url;

            if (Session["usertype"] == null)
            {
                Response.Redirect("http://localhost:33917/");
            }
            else if (Session["usertype"].ToString() == "user")
            {
                if (url == "http://localhost:33917/Cloth" || url == "http://localhost:33917/Cloth/" || url == "http://localhost:33917/Cloth/Create" || url == "http://localhost:33917/Cloth/Create/" || url == "http://localhost:33917/Cloth/Edit" || url == "http://localhost:33917/Cloth/Edit/" || url == "http://localhost:33917/Cloth/Delete" || url == "http://localhost:33917/Cloth/Delete/" || url == "http://localhost:33917/Book" || url == "http://localhost:33917/Book/" || url == "http://localhost:33917/Book/Create" || url == "http://localhost:33917/Book/Create/" || url == "http://localhost:33917/Book/Edit" || url == "http://localhost:33917/Book/Edit/" || url == "http://localhost:33917/Book/Delete" || url == "http://localhost:33917/Book/Delete/" || url == "http://localhost:33917/Book/TopUser" || url == "http://localhost:33917/Book/TopUser/" || url == "http://localhost:33917/Book/ViewUserTransaction" || url == "http://localhost:33917/Book/ViewUserTransaction/")
                {
                    Response.Redirect("http://localhost:33917/User/BookIndex/");
                }
            }
            else
            {
                if (url == "http://localhost:33917/User/BookIndex" || url == "http://localhost:33917/User/BookIndex/" || url == "http://localhost:33917/User/ClothIndex" || url == "http://localhost:33917/User/ClothIndex/" || url == "http://localhost:33917/User/ViewCart" || url == "http://localhost:33917/User/ViewCart/")
                {
                    Response.Redirect("http://localhost:33917/Book/");
                }
            }
       
            base.OnActionExecuting(filterContext);
        }

    }
}

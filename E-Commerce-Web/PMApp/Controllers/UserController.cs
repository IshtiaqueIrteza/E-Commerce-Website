using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMEntity;
using PMRepository;
using System.IO;
using System.Collections.Concurrent;

namespace PMApp.Controllers
{
    public class UserController : BaseController
    {
        private ProductDBContext context = new ProductDBContext();
        private BookRepository repoBook = new BookRepository();
        private ClothRepository repoCloth = new ClothRepository();

        public ActionResult BookIndex(string add, string[] quantity)
        {
            if (add != null)
            {
                add = add.Remove(0, 15);
                int a = Convert.ToInt32(add) - 1; //for array indexing (a-1)
                int j;

                if (Int32.TryParse(quantity[a], out j))
                {
                    int b = Convert.ToInt32(quantity[a]); //actual quantity

                    if (b < 1)
                    {
                        ViewBag.errorMessage = "Please insert proper quantity !!";
                    }
                    else
                    {
                        string s = "Book" + add; //create variable name
                        Session[s] = b.ToString(); //Book1 = 2(quantity)
                    }
                }
                else
                {
                    ViewBag.errorMessage = "Please insert proper quantity !!";
                }
            } //ViewBag.e = Session["Book1"];

            return View(this.repoBook.GetAll());
        }

        [HttpPost]
        public ActionResult ViewCart(string shop)
        {
            double total = 0;

            string totalBookId = "";
            string totalClothId = "";

            string totalBookQuantity = "";
            string totalClothQuantity = "";

            //Book checking process

            int bookCount = this.context.Books.Count();

            List<Book> bookList = new List<Book>();

            for (int i = 1; i <= bookCount; i++)
            {
                string id = "Book" + i.ToString();

                if (Session[id] != null)
                {
                    Book b = this.repoBook.Get(i);

                    b.TotalPrice = (Convert.ToInt32(Session[id]) * b.Price);
                    total += b.TotalPrice;
                    b.Quantity = Convert.ToInt32(Session[id]);

                    totalBookId += ((b.Id).ToString()+","); //gererating all book id's for database
                    totalBookQuantity += (b.Quantity + ","); //gererating all book quantities for database

                    bookList.Add(b);
                }
            }

            ViewBag.bList = bookList; //Actual list passing to the cshtml

            //Cloth checking process

            int clothCount = this.context.Cloths.Count();

            List<Cloth> clothList = new List<Cloth>();

            for (int i = 1; i <= clothCount; i++)
            {
                string id = "Cloth" + i.ToString();

                if (Session[id] != null)
                {
                    Cloth c = this.repoCloth.Get(i);

                    c.TotalPrice = (Convert.ToInt32(Session[id]) * c.Price);
                    total += c.TotalPrice;
                    c.Quantity = Convert.ToInt32(Session[id]);

                    totalClothId += ((c.Id).ToString() + ","); //gererating all cloth id's for database
                    totalClothQuantity += (c.Quantity + ","); //gererating all cloth quantities for database

                    clothList.Add(c);
                }
            } 

            if (shop != null)
            {
                if (total != 0)
                {
                    int userId = Convert.ToInt32(Session["userId"]);

                    Transaction transaction = new Transaction();

                    transaction.BookId = totalBookId;
                    transaction.BookQuantity = totalBookQuantity;
                    transaction.ClothId = totalClothId;
                    transaction.ClothQuantity = totalClothQuantity;
                    transaction.TotalPrice = total;
                    transaction.UserId = userId;
                    transaction.DateTime = DateTime.UtcNow;

                    this.context.Transactions.Add(transaction);
                    this.context.SaveChanges();

                    User u = this.context.Users.FirstOrDefault(m => m.UserId == userId);

                    //Update the user's total spent money

                    double val = u.TotalSpent;
                    val += total;

                    u.TotalSpent = val;

                    this.context.SaveChanges();

                    TempData["success"] = "Transaction Succesfull";

                    //Destroying session
                    string tmpName = Session["username"].ToString();
                    string tmpId = Session["userid"].ToString();
                    Session.Clear();
                    Session["username"] = tmpName;
                    Session["userid"] = tmpId;
                    Session["usertype"] = "user";

                    return RedirectToAction("BookIndex", "User");
                }
                else
                {
                    ViewBag.warning = "Please add some product first";
                }
            }
            else
            {
                ViewBag.cList = clothList; //Actual list passing to the cshtml
                ViewBag.Total = total; //Passing total cost
            }

            //ViewBag.c = count;
            return View(this.repoBook.GetAll());
        }

        public ActionResult ClothIndex(string add, string[] quantity)
        {
            if (add != null)
            {
                add = add.Remove(0, 15);
                int a = Convert.ToInt32(add) - 1; //for array indexing (a-1)
                int j;

                if (Int32.TryParse(quantity[a], out j))
                {
                    int b = Convert.ToInt32(quantity[a]); //actual quantity

                    if (b < 1)
                    {
                        ViewBag.errorMessage = "Please insert proper quantity !!";
                    }
                    else
                    {
                        string s = "Cloth" + add; //create variable name
                        Session[s] = b.ToString(); //Book1 = 2(quantity)
                    }
                }
                else
                {
                    ViewBag.errorMessage = "Please insert proper quantity !!";
                }
            } //ViewBag.e = Session["Book1"];

            return View(this.repoCloth.GetAll());
        }

        public ActionResult Logout(string logout)
        {
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}

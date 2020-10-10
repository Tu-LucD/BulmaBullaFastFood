using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BulmaBullaFastFood.Models;

namespace BulmaBullaFastFood.Controllers
{
    public class HomeController : Controller
    {
        private BBFastFoodDBEntities db = new BBFastFoodDBEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "What is Bulma and Bulla Fast Food?";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact us with your questions and concerns.";

            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Message = "Login to make your orders";

            return View();
        }

        public ActionResult CreateAccount()
        {
            ViewBag.Message = "Create your new account";
            return View();
        }

        [HttpPost]
        public ActionResult CreateAccount(Account accountToCreate)
        {
            if (!ModelState.IsValid)
                return View();

            db.Accounts.Add(accountToCreate);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
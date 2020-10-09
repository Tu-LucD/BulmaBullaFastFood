using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BulmaBullaFastFood.Controllers
{
    public class HomeController : Controller
    {
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
    }
}
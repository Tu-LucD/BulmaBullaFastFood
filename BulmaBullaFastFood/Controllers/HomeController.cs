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
    }
}
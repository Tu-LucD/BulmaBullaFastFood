using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
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

        public ActionResult Menu()
        {
            ViewBag.Message = "Admire our vast collection of culinary treats";
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

            // Call the SendEmail Method
            SendEmail(accountToCreate);

            return RedirectToAction("Index");
        }

        // Private method to send a confirmation email to the new account's email.
        private void SendEmail(Account account)
        {
            try
            {
                // Create a message object using MailMessage
                MailMessage message = new MailMessage("technotransact@gmail.com", 
                                                        account.email, 
                                                        "CONFIRMATION",
                                                        "Hello, you are receiving this email because we have confirmed your email into our database! " +
                                                        "<br>Username: " + account.username +
                                                        "<br>Password: " + account.password +
                                                        "<br><br>Thank you!");
                // Allow specify whether or not we can attach/send html in the body of the mail
                message.IsBodyHtml = true;

                // Specify the smtpClient which handles all the email sending for us
                // Each mailServer as its own smtp server and port definition
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                // Gmail requires client have enabled SSL
                client.EnableSsl = true;
                // Set client credentials used to authenticate the sender("email", "password")
                client.Credentials = new System.Net.NetworkCredential("technotransact@gmail.com", "techno98741");
                // Effectively send message
                client.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
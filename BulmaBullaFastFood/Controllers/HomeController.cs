﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using BulmaBullaFastFood.Models;
using Microsoft.Ajax.Utilities;

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

        public ActionResult SoulmatePage()
        {
            ViewBag.Message = "Find your soulmates";
            return View();
        }

        public ActionResult EditProfile(int id)
        {
            ViewBag.Message = "Update Your Profile";
            return View(db.Accounts.Where(x => x.Id == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult EditProfile(int id, Account account)
        {
            try
            {
                db.Entry(account).State = EntityState.Modified; // Grab modification
                db.SaveChanges(); // To persist the change

                return RedirectToAction("ViewProfile");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeleteProfile(int id)
        {
            ViewBag.Message = "Do you want to delete your profile?";
            return View(db.Accounts.Where(x => x.Id == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult DeleteProfile(int id, Account account)
        {
            try
            {
                    
                account = db.Accounts.Where(x => x.Id == id).FirstOrDefault();
                    
                db.Accounts.Remove(account);
                    
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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

        public ActionResult ViewProfile()
        {
            ViewBag.Message = "This is your profile: " + Session["UserName"];
            List<Account> li = new List<Account>();

            string i = Session["UserID"].ToString();
            int id = Convert.ToInt32(i);
            var account = db.Accounts.Where(x => x.Id == id).FirstOrDefault();
            li.Add(account);
            return View(li);
        }

        public ActionResult ViewPreviousOrders()
        {
            ViewBag.Message = "These are your previous orders, " + Session["UserName"] + ".";
            string i = Session["UserID"].ToString();
            int id = Convert.ToInt32(i);
            return View(db.Orders.Where(x => x.customerId == id).ToList());
        }

        public ActionResult GenderS()
        {
            ViewBag.Message = "These are your soulmates";
            string i = Session["UserID"].ToString();
            string gender = Session["Gender"].ToString();
            int id = Convert.ToInt32(i);
            return View(db.Orders.Where(x => x.gender == gender && x.customerId != id).ToList());
        }

        public ActionResult CityS()
        {
            ViewBag.Message = "These are your soulmates";
            string i = Session["UserID"].ToString();
            string city = Session["City"].ToString();
            int id = Convert.ToInt32(i);
            return View(db.Orders.Where(x => x.city == city && x.customerId != id).ToList());
        }

        public ActionResult FoodDrinkS(string searchString)
        {
            ViewBag.Message = "Search for soulmates through food or drinks you've purchased";
            string i = Session["UserID"].ToString();
            int id = Convert.ToInt32(i);

            var food = from m in db.Orders
                       where m.customerId != id
                       select m;
            if (!string.IsNullOrEmpty(searchString))
            {
                food = food.Where(s => s.food.Contains(searchString) || s.drink.Contains(searchString));
            }
            return View(food.ToList());
        }

        public ActionResult UserDashboard()
        {
            if (Session["UserID"] != null)
            {
                ViewBag.Message = "WELCOME " + Session["UserName"].ToString() + "!";
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public ActionResult Login(Account account)
        {
            if (ModelState.IsValid)
            {
                using (BBFastFoodDBEntities db = new BBFastFoodDBEntities())
                {
                    Account obj = (Account)db.Accounts.Where(a => a.username.Equals(account.username) && a.password.Equals(account.password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.Id.ToString();
                        Session["UserName"] = obj.username.ToString();
                        Session["Gender"] = obj.gender.ToString();
                        Session["City"] = obj.city.ToString();
                        return RedirectToAction("UserDashboard");
                    }
                }
            }
            return View(account);
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
            Session["UserID"] = accountToCreate.Id.ToString();
            Session["UserName"] = accountToCreate.username.ToString();
            Session["Gender"] = accountToCreate.gender.ToString();
            Session["City"] = accountToCreate.city.ToString();

            return RedirectToAction("UserDashboard");
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

        public ActionResult Payment()
        {
            ViewBag.Message = "Confirm your Order";
            List<Order> li = new List<Order>();

            string i = Session["UserID"].ToString();
            int id = Convert.ToInt32(i);
            Order order = db.Orders.AsEnumerable().Where(x => x.customerId == id).Last();
            li.Add(order);
            return View(li);
        }
    }
}
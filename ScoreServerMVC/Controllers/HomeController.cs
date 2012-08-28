using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScoreServerMVC.Models;

namespace ScoreServerMVC.Controllers
{
    public class HomeController : Controller
    {
        ScoreDbContext db = new ScoreDbContext();

        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to Pet Rancher!";
            return View();
            //return View("../Score/Index");
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Logon()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Logon(Users user)
        {
            ViewBag.Title = "Logon Post";
            return View();
          /*  if (ModelState.IsValid)
            {
                ViewBag.Title = "ModelState Valid";
                return View(user);
            }
            ViewBag.Title = "ModelState NOT Valid";
            return View(); */
        } 

        public ActionResult Register()
        {
            ViewBag.Message = "Welcome to the Login Page.";
            var v = ViewData.Model = db.Users.ToList();
            var user = new Users();
            return View(user);

        }

        [HttpPost]
        public ActionResult Register(Users user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View("User");
            }
            catch{ return View();}

        }

        public ActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Create(ScoreServerMVC.Models.Users.RegistrationViewModel user)
        {
            ///
            /// This is where we check to make sure the modelmatches what we wrote in RegsirationViewModel
            /// and if it does we need to convert it back to the Users model so we can enter a new user into the database.
            /// Probbaly not the most ideal way of doing this, but a straight cast did not work.
            /// Also find another way of verifying of user is active: I.E: send out email with link to click to activate useraccount
            ///
            if (ModelState.IsValid)
            {
                
                ViewBag.Title = "Success!";
                Users newUser = new Users();
                newUser.Username = user.Username;
                newUser.firstName = user.firstName;
                newUser.lastName = user.lastName;
                newUser.email = user.email;
                //newUser.password = user.password;
                newUser.password = newUser.SetPassword(user.password);
                db.Users.Add(newUser);
                db.SaveChanges();
                return View();
            }
            ViewBag.Title = "FAILED!";    
            return View(user);
        }
    }
}

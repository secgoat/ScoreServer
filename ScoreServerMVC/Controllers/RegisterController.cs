using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScoreServerMVC.Models;

namespace ScoreServerMVC.Controllers
{
    public class RegisterController : Controller
    {
        ScoreDbContext db = new ScoreDbContext();
        //
        // GET: /Register/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ScoreServerMVC.Models.Users.RegistrationViewModel user)
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
                newUser.password = newUser.SetPassword(user.password);
                db.Users.Add(newUser);
                db.SaveChanges();
                ViewBag.Message = "You have succesfully been registered!";
                return View("../Home/Index");
            }
            ViewBag.Title = "FAILED!";
            return View(user);
        }

    }
}

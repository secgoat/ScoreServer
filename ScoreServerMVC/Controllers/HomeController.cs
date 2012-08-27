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
                //TODO: insert logic here?
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
    }
}

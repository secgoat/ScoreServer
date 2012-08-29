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

       
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ScoreServerMVC.Controllers
{
    public class LogOffController : Controller
    {
        //
        // GET: /LogOff/

        public ActionResult Index()
        {
            FormsAuthentication.SignOut();
            return View();

        }

    }
}

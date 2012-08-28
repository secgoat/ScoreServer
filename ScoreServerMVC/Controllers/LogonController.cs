using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ScoreServerMVC.Controllers
{
    public class LogonController : Controller
    {
        //
        // GET: /Logon/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LogonModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Username == "bob" && model.Password == "bob") //simulate DB call where username and password valid
                {
                    FormsAuthentication.SetAuthCookie(model.Username, false); //set non persistant cookie,
                    return RedirectToAction("Index", "Home"); //return to home page
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Username or Password"); //if not valid or not authenticated return error
                }
            }
           
            return View();
        }
    }
}

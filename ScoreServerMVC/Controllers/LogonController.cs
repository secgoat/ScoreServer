using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ScoreServerMVC.Models;
using System.Data.SqlServerCe;

namespace ScoreServerMVC.Controllers
{
    public class LogonController : Controller
    {
        ScoreDbContext db = new ScoreDbContext();
        //
        // GET: /Logon/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LogonModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {

                var userList = db.Users.Where(u => u.Username.Equals(model.Username));
                //Users user = db.Users.Select() (model.Username); //TODO: need to figure out how to select a user row by ID based on username
                if (userList != null)
                {
                    userList.Cast<Users>();
                    foreach (Users user in userList)
                    {
                        if (user.ValidatePassword(model.Password))
                        {
                            FormsAuthentication.SetAuthCookie(model.Username, false); //set non persistant cookie,
                            //return RedirectToAction("Index", "Home"); //return to home page
                            if (returnUrl != null)
                                return Redirect(returnUrl);
                            else
                                return View("../Home/Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Invalid Username or Password"); //if not valid or not authenticated return error
                        }
                    }
                }
               /* if (model.Username == "bob" && model.Password == "bob") //simulate DB call where username and password valid
                {
                    FormsAuthentication.SetAuthCookie(model.Username, false); //set non persistant cookie,
                    return RedirectToAction("Index", "Home"); //return to home page
                }*/
               
            }
           
            return View();
        }
    }
}

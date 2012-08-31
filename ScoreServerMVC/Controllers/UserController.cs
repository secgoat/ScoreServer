using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScoreServerMVC.Models;
using System.Web.Security;

namespace ScoreServerMVC.Controllers
{ 
    public class UserController : Controller
    {
        private ScoreDbContext db = new ScoreDbContext();
        private FormsIdentity id;// use this to get the user identity.
        private FormsAuthenticationTicket ticket; //use this ot pull acopy of the ticket 
        // GET: /User/
        public ViewResult Index(string returnUrl)
        {

            id = (FormsIdentity)User.Identity;
            ticket = id.Ticket;
            if (ticket.UserData == "1")
            {
                return View(db.Users.ToList());
            }
          
            else
            {
                ViewBag.Error = "Unathorized Access! I'm afraid I Can't Do that Dave!";
                return View("Error");
              
            }
        }

        //
        // GET: /User/Details/5

        public ViewResult Details(int id)
        {
            Users usermodel = db.Users.Find(id);
            return View(usermodel);
        }

        //
        // GET: /User/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /User/Create

        [HttpPost]
        public ActionResult Create(Users usermodel)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(usermodel);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(usermodel);
        }
        
        //
        // GET: /User/Edit/5
 
        public ActionResult Edit(int id)
        {
            Users usermodel = db.Users.Find(id);
            return View(usermodel);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        public ActionResult Edit(Users usermodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usermodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usermodel);
        }

        //
        // GET: /User/Delete/5
 
        public ActionResult Delete(int id)
        {
            Users usermodel = db.Users.Find(id);
            return View(usermodel);
        }

        //
        // POST: /User/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Users usermodel = db.Users.Find(id);
            db.Users.Remove(usermodel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
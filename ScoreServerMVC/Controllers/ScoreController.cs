using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScoreServerMVC.Models;

namespace ScoreServerMVC.Controllers
{
    public class ScoreController : Controller
    {
        ScoreDbContext db = new ScoreDbContext();

        //
        // GET: /Score/
        [Authorize]
        public ActionResult Index()
        {
            var scores = from s in db.Scores
                         where s.Points > 0
                         select s;

            return View(scores.ToList());
        }

        [HttpPost]
        //http://stackoverflow.com/questions/5401501/how-to-post-data-to-specific-url-using-webclient-in-c-sharp
        //http://www.eworldui.net/blog/post/2008/05/ASPNET-MVC---Using-Post2c-Redirect2c-Get-Pattern.aspx
        public ActionResult Index(string name, int points, DateTime date)
        {
            ViewBag.name = name;
            ViewBag.points = points;
            ViewBag.date = date;

            Score newScore = new Score();
            newScore.Name = name;
            newScore.Points = points;
            newScore.Date = date;
            
            db.Scores.Add(newScore);
            db.SaveChanges();

            var scores = from s in db.Scores
                         where s.Points > 1
                         select s;

            return View(scores.ToList());
        }

       
    }
}

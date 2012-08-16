using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ScoreServerMVC.Models
{
    public class Score
    {
        public int ScoreID { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
        public DateTime Date { get; set; }

   }

    public class ScoreDbContext : DbContext
    {
        public DbSet<Score> Scores { get; set; }

    }
}
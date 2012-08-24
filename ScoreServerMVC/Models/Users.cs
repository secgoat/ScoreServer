using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ScoreServerMVC.Models
{
    public class Users
    {
        [Key]
        public int playerID { get; set; }
        public String email { get; set; }
        public String password { get; set; }
        public int admin { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 3)]
        [Display(Name = "User Name")]
        [RegularExpression(@"(\S)+", ErrorMessage = " White Space is not allowed in User Names")]
        [ScaffoldColumn(false)]
        public String Username { get; set;}

        [Required]
        [StringLength(15, MinimumLength = 3)]
        [Display(Name = "First Name")]
        public String firstName { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 3)]
        [Display(Name = "Last Name")]
        public String lastName { get; set; }

        public class ScoreDbContextUsers : DbContext
        {
            public DbSet<Score> Users { get; set; }

        }
    }
}
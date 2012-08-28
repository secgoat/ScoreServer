using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ScoreServerMVC.Controllers
{
    public class LogonModel
    {
        [Required]
        public String Username { get; set; }
        
        [Required]
        public String Password { get; set; }

    }
}

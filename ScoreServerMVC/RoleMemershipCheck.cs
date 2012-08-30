using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ScoreServerMVC.Models;

//TODO: check the cookie I think this one has the basics on this http://stackoverflow.com/questions/1149996/storing-more-information-using-formsauthentication-setauthcookie

namespace ScoreServerMVC
{
    public static class RoleMemershipCheck
    {
        ScoreDbContext db = new ScoreDbContext();

        public static bool IsAdmin(string userName)
        {

            return false; 
        }

    }
}
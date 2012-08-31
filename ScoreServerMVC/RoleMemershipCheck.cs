using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ScoreServerMVC.Models;
using System.Web.Security;

//TODO: check the cookie I think this one has the basics on this http://stackoverflow.com/questions/1149996/storing-more-information-using-formsauthentication-setauthcookie
//http://stackoverflow.com/questions/7220184/read-form-authentication-cookie-from-asp-net-code-behind
namespace ScoreServerMVC
{
    public class RoleMemershipCheck
    {
        ScoreDbContext db = new ScoreDbContext();

        public bool IsAdmin(string userName)
        {
            var userList = db.Users.Where(u => u.Username.Equals(userName));
            if (userList != null)
            {
                userList.Cast<Users>();
                foreach (Users user in userList)
                {
                    if (user.admin == 1)
                    {
                        return true;
                    }
                }
            }
            return false; 
        }

    }
}
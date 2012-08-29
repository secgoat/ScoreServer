using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ScoreServerMVC.Models
{
    public class Users
    {

        const String ConstantSalt = "fg809rTyu099#!"; //use this to salt password hashes
       // private String passwordSalt; // this is the generated salt. if null is set below in get
       /* private String PasswordSalt 
        {
            get { return (Guid.NewGuid().ToString("N")); }
            set { PasswordSalt = value; }
        }*/

        [Key]
        public int playerID { get; set; }

        [Required]
        [Display(Name = "Email")]
        public String email { get; set; }
        
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
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


        public String SetPassword(string pwd)
        {
            string password = GetHashedPassword(pwd);
            return password;
        }

        private String GetHashedPassword(string pwd)
        {
            using (var sha = SHA256.Create())
            {
                var computedHash = sha.ComputeHash(Encoding.Unicode.GetBytes(pwd + ConstantSalt));
                return Convert.ToBase64String(computedHash);
            }
        }

        public bool ValidatePassword(string maybePwd)
        {
            if (password == null)
                return true;
            return password == GetHashedPassword(maybePwd);
        }

        public class ScoreDbContextUsers : DbContext
        {
            public DbSet<Score> Users { get; set; }

        }

        public class RegistrationViewModel
        {
            /// <summary>
            /// I needed to create a Registration View model so that I could validate the passwords, as User class
            /// did not have a ConfirmPassword field in the database the check while using the model failed.
            /// Created this new model with the needed fields and password checks. once Posted haveto convert 
            /// back to user model instad of registrationview model
            /// </summary>
            [Required]
            [StringLength(15, MinimumLength = 3)]
            [Display(Name = "User Name")]
            [RegularExpression(@"(\S)+", ErrorMessage = " White Space is not allowed in User Names")]
            [ScaffoldColumn(false)]
            public String Username { get; set; }

            [Required]
            [StringLength(15, MinimumLength = 3)]
            [Display(Name = "First Name")]
            public String firstName { get; set; }

            [Required]
            [StringLength(15, MinimumLength = 3)]
            [Display(Name = "Last Name")]
            public String lastName { get; set; }

            [Required]
            [Display(Name = "Email")]
            public String email { get; set; }

            [Required]
            [Display(Name = "Password")]
            [DataType(DataType.Password)]
            public String password { get; set; }
            
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Re-enter Password")]
            [Compare("password", ErrorMessage = "Passwords do not match.")]
            public String comparePassword { get; set; }
        }
       
    }
}
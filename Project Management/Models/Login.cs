using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_Management.Models
{
    public class Login
    {
        [Display(Name = "Enter Email/Username")]
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool isAuthenticated(Login user)
        {
            using (dbProjectManagementEntities db = new dbProjectManagementEntities())
            {
                var authentic = db.tblUsers.Where(m => m.UserEmail == user.Email && m.UserPassword == user.Password).SingleOrDefault();
                if (authentic != null)
                {
                    return true;
                }
            }

            return false;
        }

        public int Authenticate(Login user)
        {
            using (dbProjectManagementEntities db = new dbProjectManagementEntities())
            {
                var auth = db.tblUsers.Where(m => m.UserEmail == user.Email && m.UserPassword == user.Password).SingleOrDefault();
                if (auth == null)
                {
                    return 0;
                }
                else if (auth != null && auth.UserStatus == false)
                {
                    return -1;
                }
                else
                {
                    return auth.UserId;
                }
            }

        }

        public string UserName(int id)
        {
            using (dbProjectManagementEntities db = new dbProjectManagementEntities())
            {
                var use = db.tblUsers.Where(m => m.UserId == id).SingleOrDefault();
                return use.UserUserName;
            }
        }

    }
}
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
            using (dbProjectManagementEntities2 db = new dbProjectManagementEntities2())
            {
                var authentic = db.tblUsers.Where(m => m.UserEmail == user.Email && m.UserPassword == user.Password).SingleOrDefault();
                if (authentic != null)
                {
                    return true;
                }   
            }

            return false;
        }


    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Web.Mvc;
using System.IO;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.Web.Routing;

namespace Project_Management.Models
{
    public class Users
    {
        

        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [DataType(DataType.Date)]
        [Required]
        [Display(Name = "Enter DOB")]


        public DateTime DOB { get; set; }
        [Required(ErrorMessage = "Required")]
        public Gender UserGender { get; set; }
        [DisplayName("UploadImage")]
        [Required(ErrorMessage = "Required")]
        public string ProfileImage { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Bio { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Company { get; set; }

        public string  ActivationGuid { get; set; }

        public HttpPostedFileBase ImageUpload { get; set; }





        public enum Gender
        {
            Male,
            Female,
            Others
        }

        public void Register(Users NewUser,string Filename)
        {
            using (dbProjectManagementEntities2 db = new dbProjectManagementEntities2())
            {
                tblUser CreateUser = new tblUser();
                CreateUser.UserName = NewUser.Name;
                CreateUser.UserDOB = NewUser.DOB;
                CreateUser.UserEmail = NewUser.Email;
                CreateUser.UserPassword = NewUser.Password;
                CreateUser.UserUserName = NewUser.Username;
                CreateUser.UserCompany = NewUser.Company;
                CreateUser.UserBio = NewUser.Bio;
                CreateUser.UserGender = NewUser.UserGender.ToString();
                CreateUser.UserStatus = false;
                CreateUser.GUID = NewUser.ActivationGuid;



                NewUser.ImageUpload.SaveAs(Filename);
                string Filename1 = Path.GetFileNameWithoutExtension(NewUser.ImageUpload.FileName);
                string extension = Path.GetExtension(NewUser.ImageUpload.FileName);

                Filename1 = Filename1 + extension;
                CreateUser.UserProfileImage = "~/UserProfileImage/" + Filename1;

                db.tblUsers.Add((CreateUser));
                db.SaveChanges();
                

               
            }



        }

        public void ActivationEmail(Users NewUser, string part)
        {
            using (dbProjectManagementEntities2 db = new dbProjectManagementEntities2())
            {
                tblUser CreatedUser =  new tblUser();
                //var employee = db.tblUsers.SingleOrDefault(m => m.UserEmail == NewUser.Email);

                using (MailMessage mm = new MailMessage("projectmanagementcommunity@gmail.com", NewUser.Email))
                {
                  //  UrlHelper url = new UrlHelper();

                    mm.Subject = "Account Activation";
                    string body = "Hello " + NewUser.Username + ",";
                    body += "<br /><br />Please click the following link to activate your account";
                    body += part;   //"<br /><a href = '" + string.Format("{0}://{1}/Home/Activation/{2}", Request.Url.Scheme, Request.Url.Authority, employee.GUID) + "'>Click here to activate your account.</a>";
                    body += "<br /><br />Thanks";
                    mm.Body = body;
                    mm.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential("projectmanagementcommunity@gmail.com", "Akshay@123");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mm);
                }

            }
        }

      


    }

}
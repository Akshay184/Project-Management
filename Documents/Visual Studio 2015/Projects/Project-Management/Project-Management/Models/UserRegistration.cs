using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.IO;
using System.Data.Entity;

namespace Project_Management.Models
{
    [System.Runtime.InteropServices.Guid("E73A310F-97A7-44F9-8F50-6371B995C5BD")]
    public class UserRegistration
    {
        [Required(ErrorMessage = "Enter Your Name")]
        [Display(Name = "Enter Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Enter Your Date of Birth")]
        [Display(Name = "Enter DOB")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Enter Your Gender")]
        [Display(Name = "Enter Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Upload your profile image")]
        [Display(Name = "upload file")]
        public string ProfileImage { get; set; }

        

        [Required(ErrorMessage = "Enter Your Email")]
        [Display(Name = "Enter Email Address")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "Enter Your Password")]
        [Display(Name = "Enter Password")]
        [DataType(DataType.Password)]
        //[RegularExpression("^ (?=.*?[A - Z])(?=.*?[a - z])(?=.*?[0 - 9])(?=.*?[#?!@$%^&*-]).{8,}$")]
        public string UserPassword { get; set; }

        [Required(ErrorMessage = "Enter Your Bio")]
        [Display(Name = "Enter Bio")]
        public string UserBio { get; set; }

        [Required(ErrorMessage = "Enter Your organisation")]
        [Display(Name = "Enter organisation")]
        public string UserOrganisation { get; set; }

        // [Remote("IsUserNameAvailable","Home",ErrorMessage = "User Name already exist")]
        [Required(ErrorMessage = "Enter Your Username")]
        [Display(Name = "Enter Ussername")]
        [StringLength(maximumLength: 8, MinimumLength = 5, ErrorMessage = "Username must be maximum 8 and minimum 5 characters long")]
        public string UserUserName { get; set; }

        public string ActivationGuid { get; set; }
        

        public HttpPostedFileBase ImageUpload { get; set; }
       // public string UserStatus { get; set; }



        public void NewUser(UserRegistration NewUser,string Filename)
        {
            


            using (dbProjectManagementEntities db = new dbProjectManagementEntities())
            {
                tblUser CreateUser = new tblUser();
                CreateUser.UserName = NewUser.UserName;
                CreateUser.UserDOB = NewUser.DOB;
                CreateUser.UserGender = NewUser.Gender;
                CreateUser.UserProfileImage = NewUser.ProfileImage;
                CreateUser.UserEmail = NewUser.UserEmail;
                CreateUser.UserPassword = NewUser.UserPassword;
                CreateUser.UserUserName = NewUser.UserName;
                CreateUser.UserOrganisation = NewUser.UserOrganisation;
                CreateUser.UserBio = NewUser.UserBio;
                CreateUser.UserStatus = false;
                CreateUser.GUID = NewUser.ActivationGuid;
                string a = NewUser.ActivationGuid;


                NewUser.ImageUpload.SaveAs(Filename);
                string Filename1 = Path.GetFileNameWithoutExtension(NewUser.ImageUpload.FileName);
                string extension = Path.GetExtension(NewUser.ImageUpload.FileName);
                Filename1 = Filename1 + extension;

                CreateUser.UserProfileImage = "~/ProfileImage/" + Filename1;

                db.tblUsers.Add((CreateUser));
                db.SaveChanges();


            }

            
        }

        public void ActivationEmail(UserRegistration NewUser,string part)
        {
            using (dbProjectManagementEntities db = new dbProjectManagementEntities())
            {
                tblUser CreatedUser = new tblUser();
                using (MailMessage mm = new MailMessage("Aman.klmishra1715.am@gmail.com", NewUser.UserEmail))
                {
                    mm.Subject = "Account Activation";
                    string body = "Hello" + NewUser.UserName + ",";
                    body += "<br/><br/>please click the following link to activate your account";
                    body += part;
                    body += "<br/><br />Thanks";
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
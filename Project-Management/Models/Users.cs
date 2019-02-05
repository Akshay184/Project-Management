using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.IO;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.Collections.Generic;

namespace Project_Management.Models
{
    public class Users
    {


        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [DataType(DataType.Date)]
       
        [Display(Name = "Enter DOB")]


        public DateTime? DOB { get; set; }
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

        public string Company { get; set; }
        public string Bio { get; set; }
       

        public string ActivationGuid { get; set; }

        public HttpPostedFileBase ImageUpload { get; set; }





        public enum Gender
        {
            Male,
            Female,
            Others
        }

        public void Register(Users NewUser, string Filename)
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
                tblUser CreatedUser = new tblUser();
                using (MailMessage mm = new MailMessage("projectmanagementcommunity@gmail.com", NewUser.Email))
                {

                    mm.Subject = "Account Activation";
                    string body = "Hello " + NewUser.Username + ",";
                    body += "<br /><br />Please click the following link to activate your account";
                    body += part;
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

        public Users Profile(int id)
        {
            using (dbProjectManagementEntities2 db = new dbProjectManagementEntities2())
            {
                
               Users UserProfile = new Users();
                var Profile = db.tblUsers.Where(m => m.UserId == id).SingleOrDefault();
               
                UserProfile.Name = Profile.UserName;
                UserProfile.DOB = Profile.UserDOB;
                UserProfile.Email = Profile.UserEmail;
                UserProfile.Username = Profile.UserUserName;
                UserProfile.DOB = Profile.UserDOB;
                UserProfile.Bio = Profile.UserBio;
                UserProfile.Company = Profile.UserCompany;
                return UserProfile;
            }
        }

        public void Edit(Users ToEdit, int id)
        {
            using (dbProjectManagementEntities2 db = new dbProjectManagementEntities2())
            {

                var existingUser = db.tblUsers.SingleOrDefault(s => s.UserId == id);
                existingUser.UserName = ToEdit.Name;
                existingUser.UserDOB = ToEdit.DOB;
                existingUser.UserGender = ToEdit.UserGender.ToString();
                existingUser.UserBio = ToEdit.Bio;
                existingUser.UserCompany = ToEdit.Company;
                db.Entry(existingUser).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public List<tblUser> Organistaion(int id)
        {
            using (dbProjectManagementEntities2 db = new dbProjectManagementEntities2())
            {
                var authentic = db.tblUsers.Where(m => m.UserId == id).SingleOrDefault();
                return db.tblUsers.Where(m => m.UserCompany == authentic.UserCompany).ToList();
            }
        }
    }

}
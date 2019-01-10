using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Web.Mvc;


namespace Project_Management.Models
{
    public class Users
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)] public DateTime DOB { get; set; }
        public Gender UserGender { get; set; }
        public string ProfileImage { get; set; }
        public string Email { get; set; }
        [DataType(DataType.Password)] public string Password { get; set; }
        public string Username { get; set; }
        public string Bio { get; set; }
        public string Company { get; set; }





        public enum Gender
        {
            Male,
            Female,
            Others
        }

        public void Register(Users NewUser)
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
                db.tblUsers.Add((CreateUser));
                db.SaveChanges();

            }
        }
    }

}
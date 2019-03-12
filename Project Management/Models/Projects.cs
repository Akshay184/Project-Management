using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_Management.Models
{
    public class Projects
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public int Admin { get; set; }



        public void AddProject(Projects CreateProject, int id)
        {
            using (dbProjectManagementEntities db = new dbProjectManagementEntities())
            {
                tblProject ToAdd = new tblProject();
                ToAdd.ProjectName = CreateProject.Name;
                ToAdd.ProjectDescription = CreateProject.Bio;
                ToAdd.ProjectAdminId = id;
                db.tblProjects.Add(ToAdd);
                db.SaveChanges();

            }
        }

        public List<tblProject> ListProject(int id)
        {
            using (dbProjectManagementEntities db = new dbProjectManagementEntities())
            {
                return db.tblProjects.Where(m => m.ProjectAdminId == id).ToList();
            }
        }



    }
}

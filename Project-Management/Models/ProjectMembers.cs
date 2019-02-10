using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace Project_Management.Models
{
    public class ProjectMembers
    {
        public int Id { get; set; }
        public Users UserId { get; set; }
        public Projects ProjectId { get; set; }
        public bool Status { get; set; }


        public int ProjectMember(int id, string ProjectName)
        {
            using (dbProjectManagementEntities2 db = new dbProjectManagementEntities2())
            {
                var project = db.tblProjects.Where(m => m.ProjectName == ProjectName).FirstOrDefault();
                var addedUser = db.tblProjectMembers.Where(m => m.ProjectId == project.ProjectId).SingleOrDefault(m => m.UserId == id);
                if (addedUser == null)
                {
                    tblProjectMember add = new tblProjectMember();
                    add.UserId = id;
                    add.Status = null;
                    add.ProjectId = project.ProjectId;
                    db.tblProjectMembers.Add(add);
                    db.SaveChanges();
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        public void AddAdmin(int id, string ProjectName)
        {
            using (dbProjectManagementEntities2 db = new dbProjectManagementEntities2())
            {
                var project = db.tblProjects.Where(m => m.ProjectName == ProjectName).SingleOrDefault();
                
                    tblProjectMember add = new tblProjectMember();
                    add.UserId = id;
                    add.Status = true;
                    add.ProjectId = project.ProjectId;
                    db.tblProjectMembers.Add(add);
                    db.SaveChanges();
                    
                
               
            }
        }


        public void Requets(int id)
        {
            using (dbProjectManagementEntities2 db = new dbProjectManagementEntities2())
            {
                var request = db.tblProjectMembers.Where(m => m.UserId == id).SingleOrDefault(m => m.Status == null);


            }

        }

        public List<tblProject> Groups(int id)
        {
            using (dbProjectManagementEntities2 db = new dbProjectManagementEntities2())
            {
                var request = db.tblProjectMembers.Where(m => m.UserId == id).ToList();
               List<tblProject> res =new  List<tblProject>();
                var a=0;
                foreach (var project in request)
                {
                  res.AddRange( db.tblProjects.Where(m => m.ProjectId == project.ProjectId));
                    
                }

                return res;
            }
        }
    }


}

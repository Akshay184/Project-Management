using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_Management.Models
{
    public class Messages
    {
        public int MessageId { get; set; }
        public int FromUser_Id { get; set; }
        public int ToProject { get; set; }
        public int ToWorkspace { get; set; }
        public string Message { get; set; }
        public DateTime TimeStrap { get; set; }
        public bool Status { get; set; }



        public void AddMessage(int id, string _message, int ProjectId)
        {
            using (dbProjectManagementEntities2 db = new dbProjectManagementEntities2())
            {
                tblMessage message = new tblMessage();
                message.From_User = id;
                message.To_Project = ProjectId;
                message.To_Workspace = null;
                message.Message = _message;
                message.TimeStrap = DateTime.Now;
                db.tblMessages.Add(message);
                db.SaveChanges();
            }
        }

        public List<tblMessage> GetMessgae( int ProjectId)
        {
            using (dbProjectManagementEntities2 db = new dbProjectManagementEntities2())
            {
                return  db.tblMessages.Where(m => m.To_Project == ProjectId).ToList();
            }
        }

    }
}
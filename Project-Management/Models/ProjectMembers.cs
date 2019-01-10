using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_Management.Models
{
    public class ProjectMembers
    {
        public int Id { get; set; }
        public Users UserId{ get; set; }
        public Projects ProjectId { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_Management.Models
{
    public class Workspace
    {
        public int Id { get; set; }
        public Users Admin { get; set; }
        public Projects ProjectId { get; set; }
    }
}
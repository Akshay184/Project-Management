//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project_Management
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblMessage
    {
        public int MessageId { get; set; }
        public Nullable<int> From_User { get; set; }
        public Nullable<int> To_Project { get; set; }
        public Nullable<int> To_Workspace { get; set; }
        public Nullable<System.DateTime> TimeStrap { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
    }
}

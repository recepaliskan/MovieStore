//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MovieStoreV4.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_MovieUser
    {
        public int ID { get; set; }
        public int MovieID { get; set; }
        public int UserID { get; set; }
    
        public virtual tbl_Movie tbl_Movie { get; set; }
        public virtual tbl_User tbl_User { get; set; }
    }
}
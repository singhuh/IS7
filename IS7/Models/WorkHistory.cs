//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IS7.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class WorkHistory
    {
        public string MNumber { get; set; }
        public string CompanyName { get; set; }
        public string TitleName { get; set; }
        public System.DateTime StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<decimal> FTE { get; set; }
    
        public virtual Company Company { get; set; }
        public virtual Title Title { get; set; }
        public virtual User User { get; set; }
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace u22565991_HW03.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class borrows
    {
        public int borrowId { get; set; }
        public Nullable<int> studentId { get; set; }
        public Nullable<int> bookId { get; set; }
        public Nullable<System.DateTime> takenDate { get; set; }
        public Nullable<System.DateTime> broughtDate { get; set; }
    
        public virtual books books { get; set; }
        public virtual students students { get; set; }
    }
}

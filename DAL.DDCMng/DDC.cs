//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.DDCMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class DDC
    {
        public DDC()
        {
            this.DDCDetail = new HashSet<DDCDetail>();
        }
    
        public int DDCID { get; set; }
        public string Season { get; set; }
    
        public virtual ICollection<DDCDetail> DDCDetail { get; set; }
    }
}
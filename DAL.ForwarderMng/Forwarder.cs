//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.ForwarderMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class Forwarder
    {
        public Forwarder()
        {
            this.ForwarderImage = new HashSet<ForwarderImage>();
            this.ForwarderPIC = new HashSet<ForwarderPIC>();
        }
    
        public int ForwarderID { get; set; }
        public string ForwarderUD { get; set; }
        public string ForwarderNM { get; set; }
        public string Address { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string PIC { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
        public Nullable<int> CountryID { get; set; }
        public string TaxCode { get; set; }
        public string Name { get; set; }
        public string JobTitle { get; set; }
        public Nullable<int> InternalDepartmentID { get; set; }
        public string Skype { get; set; }
        public string Comment { get; set; }
    
        public virtual ICollection<ForwarderImage> ForwarderImage { get; set; }
        public virtual ICollection<ForwarderPIC> ForwarderPIC { get; set; }
    }
}

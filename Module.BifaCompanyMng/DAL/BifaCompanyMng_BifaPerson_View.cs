//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.BifaCompanyMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class BifaCompanyMng_BifaPerson_View
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BifaCompanyMng_BifaPerson_View()
        {
            this.BifaCompanyMng_BifaEmailAddress_View = new HashSet<BifaCompanyMng_BifaEmailAddress_View>();
            this.BifaCompanyMng_BifaTelephone_View = new HashSet<BifaCompanyMng_BifaTelephone_View>();
        }
    
        public int BifaPersonID { get; set; }
        public string FullName { get; set; }
        public string CallingName { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public Nullable<int> BifaCompanyID { get; set; }
        public Nullable<int> PositionGroupID { get; set; }
        public string PositionGroupNM { get; set; }
        public string Remark { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string EmployeeNM { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    
        public virtual BifaCompanyMng_BifaCompany_View BifaCompanyMng_BifaCompany_View { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BifaCompanyMng_BifaEmailAddress_View> BifaCompanyMng_BifaEmailAddress_View { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BifaCompanyMng_BifaTelephone_View> BifaCompanyMng_BifaTelephone_View { get; set; }
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactoryMng2.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactoryMng2_FactorySearchResult_View
    {
        public FactoryMng2_FactorySearchResult_View()
        {
            this.FactoryMng2_FactorySampleTechnical_View = new HashSet<FactoryMng2_FactorySampleTechnical_View>();
            this.FactoryMng2_FactoryResponsiblePerson_View = new HashSet<FactoryMng2_FactoryResponsiblePerson_View>();
            this.FactoryMng2_FactoryDirector_View = new HashSet<FactoryMng2_FactoryDirector_View>();
            this.FactoryMng2_FactoryRawMaterialSupplier_View = new HashSet<FactoryMng2_FactoryRawMaterialSupplier_View>();
            this.FactoryMng2_FactorySupplier_View = new HashSet<FactoryMng2_FactorySupplier_View>();
            this.FactoryMng2_FactoryManager_View = new HashSet<FactoryMng2_FactoryManager_View>();
            this.FactoryMng2_FactoryPricing_View = new HashSet<FactoryMng2_FactoryPricing_View>();
        }
    
        public int FactoryID { get; set; }
        public string FactoryUD { get; set; }
        public string FactoryName { get; set; }
        public Nullable<int> SupplierID { get; set; }
        public string City { get; set; }
        public Nullable<int> LocationID { get; set; }
        public string LocationNM { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string TaxCode { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string UpdatorName { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string LogoImage_DisplayURL { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FactoryOfficialName { get; set; }
        public string CurrentSeason { get; set; }
        public decimal CurrentTurnover { get; set; }
        public decimal CurrentTotalShipped40HC { get; set; }
        public string PrevSeason { get; set; }
        public decimal PrevTurnover { get; set; }
        public decimal PrevTotalShipped40HC { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsPotential { get; set; }
        public string GoogleMapURL { get; set; }
        public string Website { get; set; }
    
        public virtual ICollection<FactoryMng2_FactorySampleTechnical_View> FactoryMng2_FactorySampleTechnical_View { get; set; }
        public virtual ICollection<FactoryMng2_FactoryResponsiblePerson_View> FactoryMng2_FactoryResponsiblePerson_View { get; set; }
        public virtual ICollection<FactoryMng2_FactoryDirector_View> FactoryMng2_FactoryDirector_View { get; set; }
        public virtual ICollection<FactoryMng2_FactoryRawMaterialSupplier_View> FactoryMng2_FactoryRawMaterialSupplier_View { get; set; }
        public virtual ICollection<FactoryMng2_FactorySupplier_View> FactoryMng2_FactorySupplier_View { get; set; }
        public virtual ICollection<FactoryMng2_FactoryManager_View> FactoryMng2_FactoryManager_View { get; set; }
        public virtual ICollection<FactoryMng2_FactoryPricing_View> FactoryMng2_FactoryPricing_View { get; set; }
    }
}

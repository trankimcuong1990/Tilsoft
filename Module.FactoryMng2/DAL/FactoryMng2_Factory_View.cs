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
    
    public partial class FactoryMng2_Factory_View
    {
        public FactoryMng2_Factory_View()
        {
            this.FactoryMng2_FactoryImage_View = new HashSet<FactoryMng2_FactoryImage_View>();
            this.FactoryMng2_FactoryInHouseTest_View = new HashSet<FactoryMng2_FactoryInHouseTest_View>();
            this.FactoryMng2_FactoryExpectedCapacity_View = new HashSet<FactoryMng2_FactoryExpectedCapacity_View>();
            this.FactoryMng2_MonthlyCapacityCurrentSeason_View = new HashSet<FactoryMng2_MonthlyCapacityCurrentSeason_View>();
            this.FactoryMng2_MonthlyCapacityPreviousSeason_View = new HashSet<FactoryMng2_MonthlyCapacityPreviousSeason_View>();
            this.FactoryMng2_FactoryDirector_View = new HashSet<FactoryMng2_FactoryDirector_View>();
            this.FactoryMng2_FactoryResponsiblePerson_View = new HashSet<FactoryMng2_FactoryResponsiblePerson_View>();
            this.FactoryMng2_FactorySampleTechnical_View = new HashSet<FactoryMng2_FactorySampleTechnical_View>();
            this.FactoryMng2_FactoryTurnover_View = new HashSet<FactoryMng2_FactoryTurnover_View>();
            this.FactoryMng2_FactoryRawMaterialSupplier_View = new HashSet<FactoryMng2_FactoryRawMaterialSupplier_View>();
            this.FactoryMng2_Appointment_View = new HashSet<FactoryMng2_Appointment_View>();
            this.FactoryMng2_FactoryCertificate_View = new HashSet<FactoryMng2_FactoryCertificate_View>();
            this.FactoryMng2_FactorySupplier_View = new HashSet<FactoryMng2_FactorySupplier_View>();
            this.FactoryMng2_FactoryGallery_View = new HashSet<FactoryMng2_FactoryGallery_View>();
            this.FactoryMng2_FactoryBusinessCard_View = new HashSet<FactoryMng2_FactoryBusinessCard_View>();
            this.FactoryMng2_FactoryContactQuickInfo_View = new HashSet<FactoryMng2_FactoryContactQuickInfo_View>();
            this.FactoryMng2_FactoryDocument_View = new HashSet<FactoryMng2_FactoryDocument_View>();
            this.FactoryMng2_FactoryProductGroup_View = new HashSet<FactoryMng2_FactoryProductGroup_View>();
            this.FactoryMng2_FactoryManager_View = new HashSet<FactoryMng2_FactoryManager_View>();
            this.FactoryMng2_FactoryPricing_View = new HashSet<FactoryMng2_FactoryPricing_View>();
        }
    
        public int FactoryID { get; set; }
        public string FactoryUD { get; set; }
        public string FactoryName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string TaxCode { get; set; }
        public string PIC { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Description { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
        public Nullable<int> LocationID { get; set; }
        public string UpdatorName { get; set; }
        public Nullable<bool> IsProducer { get; set; }
        public Nullable<bool> IsTrader { get; set; }
        public Nullable<bool> IsPacking { get; set; }
        public Nullable<bool> IsAssembler { get; set; }
        public Nullable<bool> IsPrivate { get; set; }
        public Nullable<bool> IsJointVenture { get; set; }
        public Nullable<bool> IsState { get; set; }
        public Nullable<bool> IsStockCompany { get; set; }
        public string ProductDescription { get; set; }
        public string ManufacturingProcessDescription { get; set; }
        public string TotalPlantSquareMeters { get; set; }
        public string NumberOfBuildings { get; set; }
        public string YearEstablished { get; set; }
        public string NumberOfWorkers { get; set; }
        public string OperatingHours { get; set; }
        public string OperatingDays { get; set; }
        public string ProductLeadtime { get; set; }
        public string WarehouseCapacity { get; set; }
        public string LogoImage { get; set; }
        public string LogoImage_DisplayURL { get; set; }
        public string CurrentSeason { get; set; }
        public Nullable<decimal> CurrentTurnOver { get; set; }
        public string SampleLeadtime { get; set; }
        public string OnePreYrSeason { get; set; }
        public Nullable<decimal> OnePreYrTurnover { get; set; }
        public string TwoPreYrSeason { get; set; }
        public Nullable<decimal> TwoPreYrTurnover { get; set; }
        public string ThreePreYrSeason { get; set; }
        public Nullable<decimal> ThreePreYrTurnover { get; set; }
        public string FactoryOfficialName { get; set; }
        public string SpecialRemark { get; set; }
        public string FactoryAddress { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsPotential { get; set; }
        public string GoogleMapURL { get; set; }
        public string Website { get; set; }
        public Nullable<int> SupplierID { get; set; }
        public Nullable<decimal> ExportCost20DC { get; set; }
        public Nullable<decimal> ExportCost40DC { get; set; }
        public Nullable<decimal> ExportCost40HC { get; set; }
        public string DIBDNumber { get; set; }
    
        public virtual ICollection<FactoryMng2_FactoryImage_View> FactoryMng2_FactoryImage_View { get; set; }
        public virtual ICollection<FactoryMng2_FactoryInHouseTest_View> FactoryMng2_FactoryInHouseTest_View { get; set; }
        public virtual ICollection<FactoryMng2_FactoryExpectedCapacity_View> FactoryMng2_FactoryExpectedCapacity_View { get; set; }
        public virtual ICollection<FactoryMng2_MonthlyCapacityCurrentSeason_View> FactoryMng2_MonthlyCapacityCurrentSeason_View { get; set; }
        public virtual ICollection<FactoryMng2_MonthlyCapacityPreviousSeason_View> FactoryMng2_MonthlyCapacityPreviousSeason_View { get; set; }
        public virtual ICollection<FactoryMng2_FactoryDirector_View> FactoryMng2_FactoryDirector_View { get; set; }
        public virtual ICollection<FactoryMng2_FactoryResponsiblePerson_View> FactoryMng2_FactoryResponsiblePerson_View { get; set; }
        public virtual ICollection<FactoryMng2_FactorySampleTechnical_View> FactoryMng2_FactorySampleTechnical_View { get; set; }
        public virtual ICollection<FactoryMng2_FactoryTurnover_View> FactoryMng2_FactoryTurnover_View { get; set; }
        public virtual ICollection<FactoryMng2_FactoryRawMaterialSupplier_View> FactoryMng2_FactoryRawMaterialSupplier_View { get; set; }
        public virtual ICollection<FactoryMng2_Appointment_View> FactoryMng2_Appointment_View { get; set; }
        public virtual ICollection<FactoryMng2_FactoryCertificate_View> FactoryMng2_FactoryCertificate_View { get; set; }
        public virtual ICollection<FactoryMng2_FactorySupplier_View> FactoryMng2_FactorySupplier_View { get; set; }
        public virtual ICollection<FactoryMng2_FactoryGallery_View> FactoryMng2_FactoryGallery_View { get; set; }
        public virtual ICollection<FactoryMng2_FactoryBusinessCard_View> FactoryMng2_FactoryBusinessCard_View { get; set; }
        public virtual ICollection<FactoryMng2_FactoryContactQuickInfo_View> FactoryMng2_FactoryContactQuickInfo_View { get; set; }
        public virtual ICollection<FactoryMng2_FactoryDocument_View> FactoryMng2_FactoryDocument_View { get; set; }
        public virtual ICollection<FactoryMng2_FactoryProductGroup_View> FactoryMng2_FactoryProductGroup_View { get; set; }
        public virtual ICollection<FactoryMng2_FactoryManager_View> FactoryMng2_FactoryManager_View { get; set; }
        public virtual ICollection<FactoryMng2_FactoryPricing_View> FactoryMng2_FactoryPricing_View { get; set; }
    }
}

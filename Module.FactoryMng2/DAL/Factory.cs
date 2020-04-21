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
    
    public partial class Factory
    {
        public Factory()
        {
            this.FactoryCertificate = new HashSet<FactoryCertificate>();
            this.FactoryDirector = new HashSet<FactoryDirector>();
            this.FactoryExpectedCapacity = new HashSet<FactoryExpectedCapacity>();
            this.FactoryImage = new HashSet<FactoryImage>();
            this.FactoryInHouseTest = new HashSet<FactoryInHouseTest>();
            this.FactoryResponsiblePerson = new HashSet<FactoryResponsiblePerson>();
            this.FactorySampleTechnical = new HashSet<FactorySampleTechnical>();
            this.FactoryTurnover = new HashSet<FactoryTurnover>();
            this.FactoryRawMaterialSupplier = new HashSet<FactoryRawMaterialSupplier>();
            this.FactoryCapacity = new HashSet<FactoryCapacity>();
            this.FactorySupplier = new HashSet<FactorySupplier>();
            this.FactoryGallery = new HashSet<FactoryGallery>();
            this.FactoryBusinessCard = new HashSet<FactoryBusinessCard>();
            this.FactoryContactQuickInfo = new HashSet<FactoryContactQuickInfo>();
            this.FactoryDocument = new HashSet<FactoryDocument>();
            this.FactoryProductGroup = new HashSet<FactoryProductGroup>();
            this.FactoryManager = new HashSet<FactoryManager>();
            this.FactoryPricing = new HashSet<FactoryPricing>();
        }
    
        public int FactoryID { get; set; }
        public string FactoryUD { get; set; }
        public string FactoryName { get; set; }
        public string FactoryOfficialName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string TaxCode { get; set; }
        public string PIC { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Description { get; set; }
        public Nullable<int> SupplierID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
        public Nullable<int> LocationID { get; set; }
        public Nullable<int> ProductSpecificID { get; set; }
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
        public string SampleLeadTime { get; set; }
        public string WarehouseCapacity { get; set; }
        public string LogoImage { get; set; }
        public string SpecialRemark { get; set; }
        public string FactoryAddress { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsPotential { get; set; }
        public string GoogleMapURL { get; set; }
        public string Website { get; set; }
        public Nullable<decimal> ExportCost20DC { get; set; }
        public Nullable<decimal> ExportCost40DC { get; set; }
        public Nullable<decimal> ExportCost40HC { get; set; }
        public string DIBDNumber { get; set; }
    
        public virtual ICollection<FactoryCertificate> FactoryCertificate { get; set; }
        public virtual ICollection<FactoryDirector> FactoryDirector { get; set; }
        public virtual ICollection<FactoryExpectedCapacity> FactoryExpectedCapacity { get; set; }
        public virtual ICollection<FactoryImage> FactoryImage { get; set; }
        public virtual ICollection<FactoryInHouseTest> FactoryInHouseTest { get; set; }
        public virtual ICollection<FactoryResponsiblePerson> FactoryResponsiblePerson { get; set; }
        public virtual ICollection<FactorySampleTechnical> FactorySampleTechnical { get; set; }
        public virtual ICollection<FactoryTurnover> FactoryTurnover { get; set; }
        public virtual ICollection<FactoryRawMaterialSupplier> FactoryRawMaterialSupplier { get; set; }
        public virtual ICollection<FactoryCapacity> FactoryCapacity { get; set; }
        public virtual ICollection<FactorySupplier> FactorySupplier { get; set; }
        public virtual ICollection<FactoryGallery> FactoryGallery { get; set; }
        public virtual ICollection<FactoryBusinessCard> FactoryBusinessCard { get; set; }
        public virtual ICollection<FactoryContactQuickInfo> FactoryContactQuickInfo { get; set; }
        public virtual ICollection<FactoryDocument> FactoryDocument { get; set; }
        public virtual ICollection<FactoryProductGroup> FactoryProductGroup { get; set; }
        public virtual ICollection<FactoryManager> FactoryManager { get; set; }
        public virtual Factory Factory1 { get; set; }
        public virtual Factory Factory2 { get; set; }
        public virtual Factory Factory11 { get; set; }
        public virtual Factory Factory3 { get; set; }
        public virtual ICollection<FactoryPricing> FactoryPricing { get; set; }
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactoryWarehouse.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactoryWarehouse_FactoryWarehouse_View
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FactoryWarehouse_FactoryWarehouse_View()
        {
            this.FactoryWarehouse_FactoryWarehousePallet_View = new HashSet<FactoryWarehouse_FactoryWarehousePallet_View>();
        }
    
        public int FactoryWarehouseID { get; set; }
        public string FactoryWarehouseUD { get; set; }
        public string FactoryWarehouseNM { get; set; }
        public string Description { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatorNM { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
        public string CompanyUD { get; set; }
        public string CompanyNM { get; set; }
        public string ShortName { get; set; }
        public Nullable<int> BranchID { get; set; }
        public string BranchUD { get; set; }
        public string BranchNM { get; set; }
        public Nullable<int> ResponsibleBy { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FactoryWarehouse_FactoryWarehousePallet_View> FactoryWarehouse_FactoryWarehousePallet_View { get; set; }
    }
}

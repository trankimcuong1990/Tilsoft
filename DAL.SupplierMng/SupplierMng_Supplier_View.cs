//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.SupplierMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class SupplierMng_Supplier_View
    {
        public SupplierMng_Supplier_View()
        {
            this.SupplierMng_Factory_View = new HashSet<SupplierMng_Factory_View>();
        }
    
        public int SupplierID { get; set; }
        public string SupplierUD { get; set; }
        public string SupplierNM { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Website { get; set; }
        public string PIC { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public Nullable<int> ManufacturerCountryID { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
        public string UpdatorName { get; set; }
        public Nullable<int> PaymentTermID { get; set; }
        public Nullable<int> DeliveryTermID { get; set; }
        public string BankAccountNo { get; set; }
        public string BankAddress { get; set; }
        public string BankBeneficiary { get; set; }
        public string BankName { get; set; }
        public string BankSwiftCode { get; set; }
        public string ShortAddress { get; set; }
    
        public virtual ICollection<SupplierMng_Factory_View> SupplierMng_Factory_View { get; set; }
    }
}

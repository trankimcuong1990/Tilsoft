//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.ClientMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class ClientMng_ClientContractDetail_View
    {
        public int ClientContractDetailID { get; set; }
        public Nullable<int> ClientContractID { get; set; }
        public Nullable<int> SaleOrderID { get; set; }
        public string ProformaInvoiceNo { get; set; }
    
        public virtual ClientMng_ClientContract_View ClientMng_ClientContract_View { get; set; }
    }
}

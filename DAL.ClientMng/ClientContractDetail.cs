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
    
    public partial class ClientContractDetail
    {
        public int ClientContractDetailID { get; set; }
        public Nullable<int> SaleOrderID { get; set; }
    
        public virtual ClientContract ClientContract { get; set; }
    }
}
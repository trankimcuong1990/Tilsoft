//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.OrderQuantityCheckingRpt
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderQuantityCheckingRpt_SaleOrderDetailSparepart_View
    {
        public OrderQuantityCheckingRpt_SaleOrderDetailSparepart_View()
        {
            this.OrderQuantityCheckingRpt_FactoryOrderSparepartDetail_View = new HashSet<OrderQuantityCheckingRpt_FactoryOrderSparepartDetail_View>();
        }
    
        public int SaleOrderDetailSparepartID { get; set; }
        public string ClientUD { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public Nullable<int> OrderQnt { get; set; }
        public string Season { get; set; }
    
        public virtual ICollection<OrderQuantityCheckingRpt_FactoryOrderSparepartDetail_View> OrderQuantityCheckingRpt_FactoryOrderSparepartDetail_View { get; set; }
    }
}
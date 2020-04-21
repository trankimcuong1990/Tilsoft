using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WorkOrder.DTO
{
    public class WorkOrderSearchDTO
    {
        public int? WorkOrderID { get; set; }
        public string WorkOrderUD { get; set; }
        public string OpSequenceNM { get; set; }
        public string ProductArticleCode { get; set; }
        public string ProductDescription { get; set; }
        public string ModelUD { get; set; }        
        public int? Quantity { get; set; }
        public string ClientUD { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string CreatedDate { get; set; }
        public bool? IsCreatedBOM { get; set; }
        public string CreatedDateBOM { get; set; }
        public bool? IsConfirmed { get; set; }
        public string StartDate { get; set; }
        public string FinishDate { get; set; }
        public string UpdatedDate { get; set; }

        public string ConfirmerName { get; set; }
        public string ConfirmedDate { get; set; }
        public string UpdatorName { get; set; }
        public string SetStatusDate { get; set; }
        public string SetStatustorName { get; set; }

        public int? WorkOrderStatusID { get; set; }
        public string WorkOrderStatusNM { get; set; }
        public bool HasTransaction { get; set; }
        public int? WorkOrderTypeID { get; set; }
        public string PreOrderDescription { get; set; }
        public bool IsSelected { get; set; }
        public string WorkOrderPreOrderUD { get; set; }
        
        public string MaterialGroups { get; set; }
        public bool HasPurchaseRequest { get; set; }
        public bool IsCreatePRGroup { get; set; }
    }
}

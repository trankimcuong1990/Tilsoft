using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionCosting.DTO
{
    public class WorkOrderDTO
    {
        public int WorkOrderID { get; set; }
        public string WorkOrderUD { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }
        public bool? IsConfirmed { get; set; }
        public string StartDate { get; set; }
        public string FinishDate { get; set; }
        public decimal? WastagePercent { get; set; }
        public string ClientUD { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public string OPSequenceNM { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ProductArticleCode { get; set; }
        public string ProductDescription { get; set; }
        public string WorkOrderTypeNM { get; set; }
        public string WorkOrderStatusNM { get; set; }
        public string SampleOrderInfo { get; set; }
        public string PreOrderDescription { get; set; }
    }
}

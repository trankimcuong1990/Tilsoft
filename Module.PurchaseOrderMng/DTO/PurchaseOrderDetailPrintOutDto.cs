using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseOrderMng.DTO
{
    public class PurchaseOrderDetailPrintOutDto
    {
        public int PurchaseOrderDetailID { get; set; }
        public int? PurchaseOrderID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string UnitNM { get; set; }
        public string ClientUD { get; set; }
        public decimal? OrderQnt { get; set; }
        public decimal? UnitPrice { get; set; }
        public string ETA { get; set; }
        public string ProductImage { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string Remark { get; set; }
        public decimal? Amount { get; set; }
        public long RowIndex { get; set; }
        public string ID { get; set; }
    }
}

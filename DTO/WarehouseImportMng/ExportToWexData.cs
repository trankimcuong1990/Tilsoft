using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.WarehouseImportMng
{
    public class ExportToWexData
    {
        public object RowIndex { get; set; }
        public string ReceiptNo { get; set; }
        public string OrderType { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int? ColliQnt { get; set; }
        public string ContainerNo { get; set; }
        public string ContainerReceivedDate { get; set; }
        public string ContainerReceivedTime { get; set; }
        public string ColliEANCode { get; set; }
        public string ColliDescription { get; set; }
        public string ClientUD { get; set; }
        public string ProformaInvoiceNo { get; set; }

    }
}

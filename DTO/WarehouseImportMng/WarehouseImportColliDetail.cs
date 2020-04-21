using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.WarehouseImportMng
{
    public class WarehouseImportColliDetail
    {
        public int? WarehouseImportColliDetailID { get; set; }
        public int? WarehouseImportProductDetailID { get; set; }
        public int? ProductColliID { get; set; }
        public int? Quantity { get; set; }
        public int? WarehouseImportID { get; set; }
        public int? RefQnt { get; set; }
        public int? WexQnt { get; set; }

        public string ReceiptNo { get; set; }
        public object RowIndex { get; set; }
        public string OrderType { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int? ColliQnt { get; set; }
        public string ContainerNo { get; set; }
        public string ColliEANCode { get; set; }
        public string ClientUD { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ContainerReceivedDate { get; set; }
        public string ContainerReceivedTime { get; set; }
        public string ColliDescription { get; set; }
    }
}

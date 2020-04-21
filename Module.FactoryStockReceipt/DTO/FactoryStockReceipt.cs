using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Module.FactoryStockReceipt.DTO
{
    public class FactoryStockReceipt
    {
        public int? FactoryStockReceiptID { get; set; }
        public int? FactoryID { get; set; }
        public int? ReceiptTypeID { get; set; }
        public string ReceiptNo { get; set; }
        public string ReceiptDate { get; set; }
        public string Remark { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string CreatorName { get; set; }
        public string UpdatorName { get; set; }
        public List<FactoryStockReceiptDetail> FactoryStockReceiptDetails { get; set; }

        public string CreatorName2 { get; set; }
        public string UpdatorName2 { get; set; }
    }
}
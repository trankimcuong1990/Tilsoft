using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Module.FactoryStockReceipt.DTO
{
    public class FactoryStockReceiptSearch
    {
        public int? FactoryStockReceiptID { get; set; }
        public int? FactoryID { get; set; }
        public int? ReceiptTypeID { get; set; }
        public string ReceiptNo { get; set; }
        public string ReceiptDate { get; set; }
        public string Remark { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public string CreatorName { get; set; }
        public int CreatorID { get; set; }
        public string UpdatorName { get; set; }
        public int UpdatorID { get; set; }
        public string FactoryUD { get; set; }
        public string ReceiptTypeText { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryMaterialReceipt.DTO
{
    public class FactoryMaterialReceiptSearch
    {
        public int? FactoryMaterialReceiptID { get; set; }
        public string ReceiptNo { get; set; }
        public string Season { get; set; }
        public string ReceiptDate { get; set; }
        public string Remark { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public string ReceiptTypeText { get; set; }
        public string CreatorName { get; set; }
        public int CreatorID { get; set; }
        public string UpdatorName { get; set; }
        public int UpdatorID { get; set; }
        public string FactoryTeamNM { get; set; }
        public string BaseOnTypeText { get; set; }
        public string DeliverName { get; set; }
        public string DeliverAddress { get; set; }
        public string StockName { get; set; }
        public string StockAddress { get; set; }
        public string Reason { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverAddress { get; set; }
    }
}

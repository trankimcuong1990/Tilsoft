using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryMaterialReceipt.DTO
{
    public class FactoryMaterialReceipt
    {
        public int? FactoryMaterialReceiptID { get; set; }
        public int? ReceiptTypeID { get; set; }
        public string ReceiptNo { get; set; }
        public string Season { get; set; }
        public string ReceiptDate { get; set; }
        public int? FactoryTeamID { get; set; }
        public string Remark { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public int? BaseOnTypeID { get; set; }
        public int? FactoryStepID { get; set; }
        public int? FactoryGoodsProcedureID { get; set; }


        public string DeliverName { get; set; }
        public string DeliverAddress { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverAddress { get; set; }
        public string StockName { get; set; }
        public string StockAddress { get; set; }
        public string Reason { get; set; }

        public string FactoryStepNM { get; set; }
        public string FactoryGoodsProcedureNM { get; set; }
        public string ReceiptTypeText { get; set; }
        public string CreatorName { get; set; }
        public string UpdatorName { get; set; }
        public List<FactoryMaterialReceiptDetail> FactoryMaterialReceiptDetails { get; set; }

        public string CreatorName2 { get; set; }
        public string UpdatorName2 { get; set; }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryFinishedProductReceipt.DTO
{
    public class FactoryFinishedProductReceipt
    {
        public int? FactoryFinishedProductReceiptID { get; set; }
        public int? ReceiptTypeID { get; set; }
        public int? ProductTypeID { get; set; }
        public string ReceiptNo { get; set; }
        public string Season { get; set; }
        public string ReceiptDate { get; set; }
        public int? ImportFromTeamID { get; set; }
        public int? ExportToTeamID { get; set; }
        public int? FactoryGoodsProcedureID { get; set; }
        public int? FactoryStepID { get; set; }
        public string Remark { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public bool? IsOutsource { get; set; }
        public int? BaseOnTypeID { get; set; }
        public int? InProgressOrBuyDirectlyID { get; set; }
        public string ReceiptTypeText { get; set; }
        public string BaseOnTypeText { get; set; }
        public string InProgressOrBuyDirectlyText { get; set; }
        public string ProductTypeText { get; set; }
        public string CreatorName { get; set; }
        public string UpdatorName { get; set; }
        public string FactoryTeamNM { get; set; }
        public string FactoryStepNM { get; set; }
        public string FactoryGoodsProcedureNM { get; set; }
        public List<FactoryFinishedProductReceiptDetail> FactoryFinishedProductReceiptDetails { get; set; }
    }
}

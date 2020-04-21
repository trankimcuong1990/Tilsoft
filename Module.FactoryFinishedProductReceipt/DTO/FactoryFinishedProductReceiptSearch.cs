using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryFinishedProductReceipt.DTO
{
    public class FactoryFinishedProductReceiptSearch
    {
        public int? FactoryFinishedProductReceiptID { get; set; }
        public int? ReceiptTypeID { get; set; }
        public string ReceiptTypeText { get; set; }
        public int? ProductTypeID { get; set; }
        public string ProductTypeText { get; set; }
        public string ReceiptNo { get; set; }
        public string Season { get; set; }
        public string ReceiptDate { get; set; }
        public int? ImportFromTeamID { get; set; }
        public string ImportFromTeamNM { get; set; }
        public int? ExportToTeamID { get; set; }
        public string ExportToTeamNM { get; set; }
        public string Remark { get; set; }
        public string CreatorName { get; set; }
        public int CreatorID { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatorName { get; set; }
        public int UpdatorID { get; set; }
        public string UpdatedDate { get; set; }
        public string FactoryStepNM { get; set; }
        public bool? IsOutsource { get; set; }
        public string FactoryGoodsProcedureNM { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseOrderMng.DTO
{
    public class PurchaseOrderSearchDto
    {
        public int? PurchaseOrderID { get; set; }
        public string Season { get; set; }
        public string PurchaseOrderUD { get; set; }
        public string PurchaseOrderDate { get; set; }
        public int? CompanyID { get; set; }
        public int? FactoryRawMaterialID { get; set; }
        public string FactoryRawMaterialUD { get; set; }
        public string FactoryRawMaterialShortNM { get; set; }
        public string FactoryRawMaterialOfficialNM { get; set; }
        public string Currency { get; set; }
        public int? PurchaseRequestID { get; set; }
        public string PurchaseRequestUD { get; set; }
        public string ETA { get; set; }
        public string AttachedFile { get; set; }
        public string Remark { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdaterName { get; set; }
        public string UpdatedDate { get; set; }
        public int? ApprovedBy { get; set; }
        public string ApprovedDate { get; set; }
        public string ApproverName { get; set; }
        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }
        public int? PurchaseOrderStatusID { get; set; }
        public decimal? Amount { get; set; }
    }
}

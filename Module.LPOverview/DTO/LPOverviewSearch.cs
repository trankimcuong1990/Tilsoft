using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.LPOverview.DTO
{
    public class LPOverviewSearch
    {
        public int? LabelingPackagingID { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string Season { get; set; }
        public string ClientOrderNumber { get; set; }
        public string SaleNM { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string FactoryUD { get; set; }
        public string LDS { get; set; }
        public string LPStatusNM { get; set; }
        public string VNRepName { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? DateDifff { get; set; }
        //Approve infor
        public string NameApproval { get; set; }
        public string ApprovedDate { get; set; }
        public int? ApprovedBy { get; set; }
        public int? SaleID { get; set; }
    }
}

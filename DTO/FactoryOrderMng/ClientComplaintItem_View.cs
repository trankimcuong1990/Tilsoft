using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.FactoryOrderMng
{
    public class ClientComplaintItem_View
    {
        public int ClientComplaintItemID { get; set; }
        public int ClientComplaintID { get; set; }
        public int? LoadingPlanDetailID { get; set; }
        public int? QuantityOfComplaint { get; set; }
        public string ComplaintDescription { get; set; }
        public int? FactoryOrderDetailID { get; set; }
        public string ArticleCode { get; set; }
        public string FactoryUD { get; set; }
        public string FactoryOrderUD { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string FactoryOrderDetailDescription { get; set; }
        public string LDS { get; set; }
        public decimal? OriginalSellingPrice { get; set; }
        public decimal? TotalSaleValue { get; set; }
        public string CreatedPINumberSolution { get; set; }
    }
}

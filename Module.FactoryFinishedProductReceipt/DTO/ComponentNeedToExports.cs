using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryFinishedProductReceipt.DTO
{
    public class ComponentNeedToExports
    {
        public List<ComponentNeedToExport> Data { get; set; }
        public int TotalRows { get; set; }
    }

    public class ComponentNeedToExport
    {
        public object KeyID { get; set; }
        public int? FactoryOrderDetailID { get; set; }
        public int? FactoryFinishedProductOrderNormID { get; set; }
        public int? FactoryFinishedProductID { get; set; }
        public int? FromGoodsProcedureID { get; set; }
        public string StepProgress { get; set; }
        public int? Quantity { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string FactoryUD { get; set; }
        public string LDS { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ClientUD { get; set; }
        public string FactoryFinishedProductUD { get; set; }
        public string FactoryFinishedProductNM { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryFinishedProductReceipt.DTO
{
    public class ComponentNeedToImport_Orders_BuyDirectlies
    {
        public List<ComponentNeedToImport_Orders_BuyDirectly> Data { get; set; }
        public int TotalRows { get; set; }
    }

    public class ComponentNeedToImport_Orders_BuyDirectly
    {
        public object KeyID { get; set; }
        public int? FactoryFinishedProductID { get; set; }
        public int? FactoryOrderDetailID { get; set; }
        public int? FactoryFinishedProductOrderNormID { get; set; }
        public string FactoryUD { get; set; }
        public string LDS { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ClientUD { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string FactoryFinishedProductUD { get; set; }
        public string FactoryFinishedProductNM { get; set; }       
    }
}

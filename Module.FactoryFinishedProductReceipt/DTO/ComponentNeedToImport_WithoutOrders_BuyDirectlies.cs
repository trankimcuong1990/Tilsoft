using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryFinishedProductReceipt.DTO
{
    public class ComponentNeedToImport_WithoutOrders_BuyDirectlies
    {
        public List<ComponentNeedToImport_WithoutOrders_BuyDirectly> Data { get; set; }
        public int TotalRows { get; set; }
    }
    public class ComponentNeedToImport_WithoutOrders_BuyDirectly
    {
        public object KeyID { get; set; }
        public int? FactoryFinishedProductID { get; set; }
        public int? ProductID { get; set; }
        public string FactoryFinishedProductUD { get; set; }
        public string FactoryFinishedProductNM { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }        
    }
}

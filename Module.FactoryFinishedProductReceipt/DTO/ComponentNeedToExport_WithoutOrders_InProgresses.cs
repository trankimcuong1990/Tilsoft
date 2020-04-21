using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryFinishedProductReceipt.DTO
{
    public class ComponentNeedToExport_WithoutOrders_InProgresses
    {
        public List<ComponentNeedToExport_WithoutOrders_InProgress> Data { get; set; }
        public int TotalRows { get; set; }
    }

    public class ComponentNeedToExport_WithoutOrders_InProgress
    {
        public object KeyID { get; set; }
        public int? FactoryFinishedProductID { get; set; }
        public int? ProductID { get; set; }
        public int? FromGoodsProcedureID { get; set; }
        public string StepProgress { get; set; }
        public string FactoryFinishedProductUD { get; set; }
        public string FactoryFinishedProductNM { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; } 
    }
}

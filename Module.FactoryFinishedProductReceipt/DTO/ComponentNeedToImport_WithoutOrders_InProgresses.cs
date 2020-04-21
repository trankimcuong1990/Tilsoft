using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryFinishedProductReceipt.DTO
{
    public class ComponentNeedToImport_WithoutOrders_InProgresses
    {
        public List<ComponentNeedToImport_WithoutOrders_InProgress> Data { get; set; }
        public int TotalRows { get; set; }
    }
    public class ComponentNeedToImport_WithoutOrders_InProgress
    {
        public object KeyID { get; set; }
        public int? FactoryTeamID { get; set; }
        public int? FactoryStepID { get; set; }
        public int? FactoryGoodsProcedureID { get; set; }
        public int? FactoryFinishedProductID { get; set; }
        public int? ProductID { get; set; }
        public int? FactoryMaterialNormID { get; set; }
        public int? Quantity { get; set; }
        public string FactoryFinishedProductUD { get; set; }
        public string FactoryFinishedProductNM { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string FactoryTeamNM { get; set; }
        public string FactoryStepNM { get; set; }
        public string FactoryGoodsProcedureNM { get; set; }
    }
}

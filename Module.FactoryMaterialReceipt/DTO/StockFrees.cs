using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryMaterialReceipt.DTO
{
    public class StockFrees
    {
        public List<StockFree> Data { get; set; }
        public int TotalRows { get; set; }
    }

    public class StockFree
    {
        public object KeyID { get; set; }
        public int? FactoryMaterialID { get; set; }
        public int? FactoryAreaID { get; set; }
        public decimal? TotalStockQnt { get; set; }
        public decimal? TotalImportedQnt { get; set; }
        public decimal? TotalExportedQnt { get; set; }
        public string FactoryMaterialUD { get; set; }
        public string FactoryMaterialNM { get; set; }
        public string FactoryAreaNM { get; set; }
        public string UnitNM { get; set; }
    }
}

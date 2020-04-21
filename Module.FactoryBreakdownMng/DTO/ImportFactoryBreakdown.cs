using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryBreakdownMng.DTO
{
    public class ImportFactoryBreakdown
    {
        public int FactoryBreakdownID { get; set; }
        public decimal? IndicatedPrice { get; set; }
        public string PackingDimensionL { get; set; }
        public string PackingDimensionW { get; set; }
        public string PackingDimensionH { get; set; }
        public string CushionDimensionL { get; set; }
        public string CushionDimensionW { get; set; }
        public string CushionDimensionH { get; set; }
        public string Remark { get; set; }
        public List<DTO.ImportFactoryBreakdownDetail> FactoryBreakdownDetail { get; set; }
    }
    public class ImportFactoryBreakdownDetail
    {
        public int? FactoryBreakdownID { get; set; }
        public int? FactoryBreakdownCategoryID { get; set; }
        public decimal? Quantity { get; set; }
        public string UnitNM { get; set; }
    }
}

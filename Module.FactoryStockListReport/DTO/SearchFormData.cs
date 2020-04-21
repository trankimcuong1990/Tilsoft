using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryStockListReport.DTO
{
    public class SearchFormData
    {
        public List<FactoryStockList> Data { get; set; }
        public List<Support.DTO.Factory> Factories { get; set; }

        public int? TotalStockQnt { get;set; }
        public decimal? TotalStockQntIn40HC { get; set; }
        public int? TotalProducedQnt { get; set; }
        public int? TotalNotPackedQnt { get; set; }
        public int? TotalPackedQnt { get; set; }

        public int? SubTotalStockQnt { get; set; }
        public decimal? SubTotalStockQntIn40HC { get; set; }
        public int? SubTotalProducedQnt { get; set; }
        public int? SubTotalNotPackedQnt { get; set; }
        public int? SubTotalPackedQnt { get; set; }
    }
}

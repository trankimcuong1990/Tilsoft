using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QCReport.DTO
{
    public class InitSearchForm
    {
        public List<DTO.FactoryOrderDetailSearchResult> Data { get; set; }
        public int? TotalRows { get; set; }
    }
}

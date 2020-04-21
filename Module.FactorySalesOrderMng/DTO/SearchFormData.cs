using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactorySalesOrderMng.DTO
{
   public class SearchFormData
    {
        public List<FactorySalesOrderSearchDTO> lstFactorySalesOrder { get; set; }
        public List<RawMaterial> lstListRawMaterial { get; set; }
        public List<Status> lstStatus { get; set; }
        public List<FactorySaleQuotation> lstSaleQuotation  { get; set; }
    }
}

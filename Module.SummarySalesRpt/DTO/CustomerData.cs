using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SummarySalesRpt.DTO
{
    public class CustomerData
    {
        public int? FactoryRawMaterialID { get; set; }
        public int FactorySaleOrderID { get; set; }
        public string DocumentDate { get; set; }
        public string FactorySaleOrderUD { get; set; }
        public string FactoryRawMaterialOfficialNM { get; set; }
        public int? CompanyID { get; set; }
    }
}

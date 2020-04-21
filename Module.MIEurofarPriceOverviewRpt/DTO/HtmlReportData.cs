using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MIEurofarPriceOverviewRpt.DTO
{
    public class HtmlReportData
    {
        public List<DTO.CustomerData> CustomerData { get; set; }
        public List<DTO.SupplierData> SupplierData { get; set; }
        public List<DTO.SaleData> SaleData { get; set; }
    }
}

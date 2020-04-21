using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.MISaleManagementRpt
{
    public class ReportData
    {
        public decimal ExchangeRate { get; set; }
        public decimal USDContainerValue { get; set; }
        public decimal EURContainerValue { get; set; }

        public List<AllSaleProformaByMonth> AllSaleProformaByMonths { get; set; }
        public List<ConfirmedSaleProformaByMonth> ConfirmedSaleProformaByMonths { get; set; }
        public List<DDCProformaBySale> DDCProformaBySales { get; set; }
        public ReportData()
        {
            AllSaleProformaByMonths = new List<AllSaleProformaByMonth>();
            ConfirmedSaleProformaByMonths = new List<ConfirmedSaleProformaByMonth>();
            DDCProformaBySales = new List<DDCProformaBySale>();
            ExchangeRate = 0;
            USDContainerValue = 0;
            EURContainerValue = 0;
        }
    }
}

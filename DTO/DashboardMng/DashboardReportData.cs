using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DashboardMng
{
    public class DashboardReportData
    {
        // TAB 1
        public List<DDC> DDCs { get; set; }
        public List<Production> Productions { get; set; }
        public List<Proforma> Proformas { get; set; }

        // TAB 2
        public List<ConfirmedContainerPerMonth> ConfirmedContainerPerMonths { get; set; }
        public List<ConfirmedContainerPerSeason> ConfirmedContainerPerSeasons { get; set; }
        public List<AllProformaByMonth> AllProformaByMonths { get; set; }
        public List<ConfirmedProformaByMonth> ConfirmedProformaByMonths { get; set; }

        // TAB 3
        public List<AllCummulative> AllCummulatives { get; set; }
        public List<ConfirmedCummulative> ConfirmedCummulatives { get; set; }

        // TAB 4
        public List<Margin> Margins { get; set; }

        // TAB 5
        public List<AllSaleProformaByMonth> AllSaleProformaByMonths { get; set; }
        public List<ConfirmedSaleProformaByMonth> ConfirmedSaleProformaByMonths { get; set; }
        public List<DDCProformaBySale> DDCProformaBySales { get; set; }

        // TAB 6
        public List<SaleByClient> SaleByClients { get; set; }
        public List<SaleByClientAndSale> SaleByClientAndSales { get; set; }
        public List<SaleByCountry> SaleByCountrys { get; set; }
        public List<SaleByCountryAndSale> SaleByCountryAndSales { get; set; }
        public List<ShippedByCountry> ShippedByCountrys { get; set; }
    }
}

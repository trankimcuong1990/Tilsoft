using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.MIOverviewRpt
{
    public class ReportData
    {
        public decimal ExchangeRate { get; set; }
        public decimal USDContainerValue { get; set; }
        public decimal EURContainerValue { get; set; }

        public List<DDC> DDCs { get; set; }
        public List<DDC> DDCNextSeasons { get; set; }
        public List<Production> Productions { get; set; }
        public List<Proforma> Proformas { get; set; }

    }
}

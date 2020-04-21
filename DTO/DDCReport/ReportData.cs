using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DDCReport
{
    public class ReportData
    {
        public List<DTO.DDCReport.ClientInfo> ClientInfos { get; set; }
        public decimal? EURContainerValue { get; set; }
        public decimal? USDContainerValue { get; set; }
        public decimal? ExchangeRate { get; set; }
        public decimal? ExchangeRateLastSeason { get; set; }
        public List<DTO.Support.Season> Seasons { get; set; }

    }
}

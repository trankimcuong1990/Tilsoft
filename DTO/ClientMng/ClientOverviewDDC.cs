using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng
{
    public class ClientOverviewDDC
    {
        public int ClientID { get; set; }
        public string Season { get; set; }
        public decimal? ExchangeRate { get; set; }
        public decimal? OrderQnt40HC { get; set; }
        public decimal? AllOrderTotalUSD { get; set; }
        public decimal? AllOrderTotalEUR { get; set; }
        public decimal? AllOrderConvertoEUR { get; set; }
        public decimal? ConfirmedOrderQnt40HC { get; set; }
        public decimal? ConfirmedOrderTotalUSD { get; set; }
        public decimal? ConfirmedOrderTotalEUR { get; set; }
        public decimal? ConfirmedOrderConvertoEUR { get; set; }
        public decimal? CITotalQnt40HC { get; set; }
        public decimal? CITotalUSD { get; set; }
        public decimal? CITotalEUR { get; set; }
        public decimal? CITotalConvertToEUR { get; set; }

        public decimal? ExpectionUSD { get; set; }
        public decimal? ExpectionEUR { get; set; }
        public decimal? TotalExpectionConvertEUR { get; set; }
        public decimal? ConvertUSD2EURCont { get; set; }
    }
}

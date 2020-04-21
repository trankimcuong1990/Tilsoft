using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.DashboardMng
{
    public class DDCProformaBySale
    {
        public string SaleUD { get; set; }

        public decimal? DDCAmountUSD { get; set; }

        public decimal? DDCAmountEUR { get; set; }

        public decimal? AllTotalUSDAmount { get; set; }

        public decimal? AllTotalEURAmount { get; set; }

        public decimal? ConfirmedTotalUSDAmount { get; set; }

        public decimal? ConfirmedTotalEURAmount { get; set; }

    }
}
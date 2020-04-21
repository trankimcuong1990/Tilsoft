using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng
{
    public class ClientEstimatedAdditionalCostDTO
    {
        public int ClientEstimatedAdditionalCostID { get; set; }
        public int? ClientID { get; set; }
        public string Season { get; set; }
        public decimal? InterestCostPercent { get; set; }
        public decimal? InspectionCostUSD { get; set; }
        public decimal? LCCostPercent { get; set; }
        public decimal? SampleCostUSD { get; set; }
        public decimal? SparepartCostUSD { get; set; }
        public decimal? EstTransportationPerContainerEUR { get; set; }
        public bool? IsConfirmed { get; set; }
        public int? ConfirmedBy { get; set; }
        public string ConfirmNM { get; set; }
        public string ConfirmedDate { get; set; }
        public decimal? DefaultCommissionPercent { get; set; }
        public decimal? BonusPercent { get; set; }
        public decimal? LCPercent { get; set; }
        public decimal? InterestPercent { get; set; }
        public decimal? DamagePercent { get; set; }
        public bool IsChange { get; set; }
    }
}

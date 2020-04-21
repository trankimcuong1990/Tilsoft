using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Agents.DTO
{
    public class AmountByClientAndSeason
    {
        public string Season { get; set; }
        public Nullable<int> ClientID { get; set; }
        public Nullable<decimal> AmountPI { get; set; }
        public Nullable<decimal> ComAmountPI { get; set; }
        public Nullable<decimal> ComPercentPI { get; set; }
        public Nullable<decimal> AmountCI { get; set; }
        public Nullable<decimal> ComAmountCI { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public Nullable<decimal> DefaultCommissionPercent { get; set; }
        public Nullable<decimal> Delta8Percent { get; set; }
        public Nullable<decimal> Delta8Amount { get; set; }
        public long KeyID { get; set; }
    }
}

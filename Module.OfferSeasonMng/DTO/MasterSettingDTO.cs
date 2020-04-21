using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OfferSeasonMng.DTO
{
    public class MasterSettingDTO
    {
        public string Season { get; set; }
        public Nullable<decimal> ExchangeRate { get; set; }
        public Nullable<decimal> InterestPercent { get; set; }
        public Nullable<decimal> LCPercent { get; set; }
        public Nullable<decimal> DamagePercent { get; set; }
        public Nullable<int> SparePartLoadability40HC { get; set; }
        public Nullable<decimal> EstUSDContValue { get; set; }
        public Nullable<decimal> EstEURContValue { get; set; }
    }
}

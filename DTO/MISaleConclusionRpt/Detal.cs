using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.MISaleConclusionRpt
{
    public class Delta
    {
        public string Season { get; set; }
        public Nullable<int> ClientID { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public Nullable<decimal> Delta5Amount { get; set; }
        public Nullable<decimal> Delta5Percent { get; set; }
        public Nullable<decimal> PiConfirmedAmount { get; set; }
        public Nullable<decimal> CiAmount { get; set; }
        public Nullable<decimal> PurchasingAmountUSD { get; set; }
        public Nullable<decimal> Delta5PercentOf100 { get; set; }
        public Nullable<decimal> PiPercentOf100 { get; set; }
        public Nullable<decimal> CiPercentOf100 { get; set; }

    }
}
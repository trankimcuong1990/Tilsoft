using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.DDCMng
{
    public class DDCDetail
    {
        public int DDCDetailID { get; set; }
        public int? ClientID { get; set; }
        public decimal? MinUSD { get; set; }
        public decimal? AvgUSD { get; set; }
        public decimal? MaxUSD { get; set; }
        public decimal? MinEUR { get; set; }
        public decimal? AvgEUR { get; set; }
        public decimal? MaxEUR { get; set; }
        public string Remark { get; set; }
        public decimal? WickerContQty { get; set; }
        public decimal? WickerPromoContQty { get; set; }
        public decimal? WoodAcaciaContQty { get; set; }
        public decimal? WoodTeakContQty { get; set; }
        public decimal? ChinaContQty { get; set; }
        public decimal? IndoContQty { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
    }
}
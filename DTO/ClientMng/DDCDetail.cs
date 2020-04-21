using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ClientMng
{
    public class DDCDetail
    {
        [Display(Name = "DDCDetailID")]
        public int? DDCDetailID { get; set; }

        [Display(Name = "DDCID")]
        public int? DDCID { get; set; }

        [Display(Name = "ClientID")]
        public int? ClientID { get; set; }

        [Display(Name = "MinUSD")]
        public decimal? MinUSD { get; set; }

        [Display(Name = "AvgUSD")]
        public decimal? AvgUSD { get; set; }

        [Display(Name = "MaxUSD")]
        public decimal? MaxUSD { get; set; }

        [Display(Name = "MinEUR")]
        public decimal? MinEUR { get; set; }

        [Display(Name = "AvgEUR")]
        public decimal? AvgEUR { get; set; }

        [Display(Name = "MaxEUR")]
        public decimal? MaxEUR { get; set; }

        [Display(Name = "WickerContQty")]
        public decimal? WickerContQty { get; set; }

        [Display(Name = "WickerPromoContQty")]
        public decimal? WickerPromoContQty { get; set; }

        [Display(Name = "WoodAcaciaContQty")]
        public decimal? WoodAcaciaContQty { get; set; }

        [Display(Name = "WoodTeakContQty")]
        public decimal? WoodTeakContQty { get; set; }

        [Display(Name = "ChinaContQty")]
        public decimal? ChinaContQty { get; set; }

        [Display(Name = "IndoContQty")]
        public decimal? IndoContQty { get; set; }

        [Display(Name = "Remark")]
        public string Remark { get; set; }

        public string Season { get; set; }

    }
}
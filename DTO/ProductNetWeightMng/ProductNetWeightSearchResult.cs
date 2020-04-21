using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ProductNetWeightMng
{
    public class ProductNetWeightSearchResult
    {
        public int ProductNetWeightID { get; set; }

        [Display(Name = "IsConfirmed")]
        public bool IsConfirmed { get; set; }

        [Display(Name = "ModelNM")]
        public string ModelNM { get; set; }

        [Display(Name = "MaterialOptionNM")]
        public string MaterialOptionNM { get; set; }

        [Display(Name = "FrameMaterialNM")]
        public string FrameMaterialNM { get; set; }

        [Display(Name = "CushionOptionNM")]
        public string CushionOptionNM { get; set; }

        [Display(Name = "NetWeight")]
        public decimal NetWeight { get; set; }

        [Display(Name = "UpdatorName")]
        public string UpdatorName { get; set; }

        [Display(Name = "UpdatedDate")]
        public string UpdatedDate { get; set; }

        [Display(Name = "ConfirmerName")]
        public string ConfirmerName { get; set; }

        [Display(Name = "ConfirmedDate")]
        public string ConfirmedDate { get; set; }

        [Display(Name = "ConcurrencyFlag")]
        public string ConcurrencyFlag { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.MISaleByMaterialRpt
{
    public class AllMaterial
    {
        public string MaterialNM { get; set; }
        public decimal? TotalCont { get; set; }
        public decimal? TotalAmount { get; set; }
        public string Season { get; set; }
    }
}
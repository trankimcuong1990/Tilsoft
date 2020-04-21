using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Module.FactoryOrderNorm.DTO
{
    public class FactoryOrderNormSearch
    {
        public int? FactoryOrderNormID { get; set; }
        public int? ClientID { get; set; }
        public int? ProductID { get; set; }
        public string Season { get; set; }
        public string ClientUD { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
    }
}
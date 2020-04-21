using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.QuotationMng
{
    public class FactoryOrderSearchResult
    {
        public bool IsSelected { get; set; }
        public int FactoryOrderID { get; set; }
        public string Season { get; set; }
        public string FactoryOrderUD { get; set; }
        public string ProductionStatus { get; set; }
        public string LDS { get; set; }
        public string FactoryUD { get; set; }
        public int? FactoryID { get; set; }
    }
}
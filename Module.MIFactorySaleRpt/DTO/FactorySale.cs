using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MIFactorySaleRpt.DTO
{
    public class FactorySale
    {
        public string FactoryID { get; set; }
        public string FactoryUD { get; set; }
        public string LocationNM { get; set; }
        public string ManufacturerCountryNM { get; set; }
        public string FactoryName { get; set; }
        public string ProductSpecificNM { get; set; }
        public decimal? CapacityAllWeek { get; set; }
        public decimal? EST { get; set; }
        public decimal? NumContOrder { get; set; }
        public decimal? NumContShipped { get; set; }
        public decimal? NumContTobeShipped { get; set; }
        public decimal? FactoryProformaInvoiceTotalAmount { get; set; }
        public decimal? FactoryProformaInvoiceTotalCont { get; set; }
        public decimal? FactoryInvoiceTotalAmount { get; set; }
        public decimal? FactoryInvoiceTotalCont { get; set; }
    }
}

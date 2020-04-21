using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SummaryPurchasesRpt.DTO
{
    public class SupplierOfReceivingData
    {
        public int? FactoryRawMaterialID { get; set; }
        public string FactoryRawMaterialOfficialNM { get; set; }
        public string FactoryRawMaterialShortNM { get; set; }
        public long PrimaryID { get; set; }
        public decimal? TotalAmountOfSupplier { get; set; }
    }
}

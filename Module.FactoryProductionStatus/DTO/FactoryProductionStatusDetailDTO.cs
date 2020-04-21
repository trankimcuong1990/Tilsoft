using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryProductionStatus.DTO
{
    public class FactoryProductionStatusDetailDTO
    {
        public int? FactoryProductionStatusDetailID { get; set; }
        public int? FactoryProductionStatusID { get; set; }
        public int? FactoryOrderID { get; set; }
        public decimal? ProducedContainerQnt { get; set; }
        public string Remark { get; set; }
        public string ClientUD { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string LDS { get; set; }
        public decimal? TotalContOrder { get; set; }
        public decimal? TotalContProducedLastWeeks { get; set; }
        public int? TotalOutputProducedLastWeeks { get; set; }
        public int? TotalOutputProducedThisWeek { get; set; }
        public List<FactoryProductionStatusOrderDetailDTO> FactoryProductionStatusOrderDetailDTOs { get; set; }

    }
}

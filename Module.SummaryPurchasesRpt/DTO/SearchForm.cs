using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SummaryPurchasesRpt.DTO
{
    public class SearchForm
    {
        public List<DTO.ReceivingNoteReportData> ReceivingNoteReportDatas { get; set; } 
        public List<DTO.SupplierOfReceivingData> SupplierOfReceivingDatas { get; set; }
    }
}

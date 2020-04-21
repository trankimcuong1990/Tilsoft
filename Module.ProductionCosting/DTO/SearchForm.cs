using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionCosting.DTO
{
    public class SearchForm
    {
        public DTO.ProductionCostingPrintData ProductionCostingData { get; set; }
        public List<TotalReceivingNote> TotalReceivingNotes { get; set; }
    }
}

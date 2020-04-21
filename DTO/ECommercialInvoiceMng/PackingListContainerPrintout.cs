using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ECommercialInvoiceMng
{
    public class PackingListContainerPrintout
    {
        public List<PackingListPrintout> PackingListPrintouts { get; set; }
        public List<PackingListDetailPrintout> PackingListDetailPrintouts { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WarehouseInvoiceGrossMarginRpt.DTO
{
    public class InitFormData
    {
        public List<Support.DTO.Season> SupportSeason { get; set; }

        public InitFormData()
        {
            SupportSeason = new List<Support.DTO.Season>();
        }
    }
}

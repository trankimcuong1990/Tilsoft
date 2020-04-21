using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.StorageCardRpt.DTO
{
    public class SearchForm
    {
        public decimal? FinalBeginning { get; set; }

        public List<DTO.StorageCard> StorageCards { get; set; }
        public decimal? TotalPurchaseOrderQnt { get; set; }
        public string PurchaseOrderIDs { get; set; }
    }
}

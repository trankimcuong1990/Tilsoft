using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.PurchasingInvoiceMng
{
    public class EditSupportList
    {
        public List<Support.Season> Seasons { get; set; }
        public List<DTO.PurchasingInvoiceMng.PriceDifferenceInvoiceSetting> PriceDifferenceInvoiceSettings { get; set; }
        public object FactoryCostTypes { get; set; }
        public List<DTO.PurchasingInvoiceMng.CompanyDTO> companyDTOs { get; set; }
    }
}

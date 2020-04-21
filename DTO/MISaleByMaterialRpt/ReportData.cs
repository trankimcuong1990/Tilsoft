using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.MISaleByMaterialRpt
{
    public class ReportData
    {
        public decimal ExchangeRate { get; set; }
        public decimal USDContainerValue { get; set; }
        public decimal EURContainerValue { get; set; }

        public List<AllMaterial> AllMaterials { get; set; }
        public List<Wood> Woods { get; set; }

        public List<ProformaInvoiceAllMaterial> ProformaInvoiceAllMaterials { get; set; }
        public List<ProformaInvoiceWood> ProformaInvoiceWoods { get; set; }
    }
}

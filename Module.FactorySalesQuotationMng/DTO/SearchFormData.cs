using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactorySalesQuotationMng.DTO
{
    public class SearchFormData
    {
        public List<FactorySaleQuotationSearchDTO> lstFactorySaleQuotation { get; set; }
        public List<RawMaterial> lstListRawMaterial { get; set; }
        public List<Status> lstStatus { get; set; }
        public List<FactorySaleQuotation> lstSaleQuotation { get; set; }
    }
    public class FactorySaleQuotation
    {
        public int FactorySaleQuotationID { get; set; }
        public string FactorySaleQuotationUD { get; set; }
        public int? CompanyID { get; set; }
        public int Id { get; set; }

        public string Value { get; set; }

        public string Label { get; set; }
    }

}

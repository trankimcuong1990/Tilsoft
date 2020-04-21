using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactorySaleInvoice.DTO
{
    public class SearchFormData
    {

        public List<FactorySaleInvoiceSearchDTO> Data { get; set; }
        public List<FactorySaleInvoiceStatusDTO> FactorySaleInvoiceStatusDTOs { get; set; }
        public List<DTO.FactoryRawMaterialDTO> FactoryRawMaterialDTOs { get; set; }
        public List<Support.DTO.Season> Seasons { get; set; }
        public TotalAmountDTO TotalAmountDTO { get; set; }

        public SearchFormData()
        {
            Data = new List<FactorySaleInvoiceSearchDTO>();
            FactorySaleInvoiceStatusDTOs = new List<FactorySaleInvoiceStatusDTO>();
            FactoryRawMaterialDTOs = new List<FactoryRawMaterialDTO>();
            Seasons = new List<Support.DTO.Season>();
            TotalAmountDTO = new TotalAmountDTO();
        }
    }
}

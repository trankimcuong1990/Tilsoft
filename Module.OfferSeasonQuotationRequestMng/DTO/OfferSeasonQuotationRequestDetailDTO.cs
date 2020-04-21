using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Module.OfferSeasonQuotationRequestMng.DTO
{
    public class OfferSeasonQuotationRequestDetailDTO
    {
        public int OfferSeasonQuotationRequestDetailID { get; set; }
        public int? OfferSeasonQuotationRequestID { get; set; }
        public int? FactoryID { get; set; }
        public string FactoryUD { get; set; }

        [MaxLength(255, ErrorMessage = "Remark can not exceed 255 chars!")]
        public string Remark { get; set; }        
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
    }
}

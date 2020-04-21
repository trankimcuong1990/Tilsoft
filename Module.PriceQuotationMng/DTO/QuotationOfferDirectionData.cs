using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PriceQuotationMng.DTO
{
    public class QuotationOfferDirectionData
    {
        public int ConstantEntryID { get; set; }
        public int? QuotationOfferDirectionID { get; set; }
        public string QuotationOfferDirectionNM { get; set; }
        public int? DisplayOrder { get; set; }
    }
}

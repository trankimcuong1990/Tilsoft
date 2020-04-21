using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OfferSeasonMng.DTO
{
    public class OfferLineDTO
    {
        public int OfferLineID { get; set; }
        public Nullable<int> OfferSeasonDetailID { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public string OfferUD { get; set; }
        public List<SaleOrderDetailDTO> SaleOrderDetailDTOs { get; set; }
        public OfferLineDTO()
        {
            SaleOrderDetailDTOs = new List<SaleOrderDetailDTO>();
        }
    }
}

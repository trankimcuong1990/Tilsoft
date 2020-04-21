using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.BackSaleOrderMng
{
    public class BackOrder
    {
        public int BackOrderID { get; set; }
        public int? OfferID { get; set; }
        public int? SaleOrderID { get; set; }
        public string Season { get; set; }
        public int? ClientID { get; set; }
        public int? SaleID { get; set; }
        public int? PaymentTermID { get; set; }
        public int? DeliveryTermID { get; set; }
        public int? VATPercent { get; set; }
        public string Currency { get; set; }
        public string OrderType { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public List<BackSaleOrderMng.BackOrderDetail> BackOrderDetails { get; set; }

    }
}

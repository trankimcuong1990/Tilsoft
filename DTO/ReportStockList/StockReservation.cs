using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReportStockList
{
    public class StockReservation
    {
        public string KeyID { get; set; }
        public int? GoodsID { get; set; }
        public string ProductType { get; set; }
        public int? ProductStatusID { get; set; }
        public int? ClientID { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public int? ReservationQnt { get; set; }
        public decimal? ReservationQntIn40HC { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.EurofarServiceMng.DTO
{
    public class WarehouseReservationStockBy_NLBL
    {
        public string KeyID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int? ReservationQnt { get; set; }

    }
}

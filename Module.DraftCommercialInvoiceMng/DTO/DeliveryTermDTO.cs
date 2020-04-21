using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DraftCommercialInvoiceMng.DTO
{
    public class DeliveryTermDTO
    {
        public int DeliveryTermID { get; set; }
        public string DeliveryTermNM { get; set; }
        public int? DeliveryTypeID { get; set; }
        public string DeliveryTypeNM { get; set; }
    }
}

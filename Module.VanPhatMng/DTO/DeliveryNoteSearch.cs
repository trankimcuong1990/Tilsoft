using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.VanPhatMng.DTO
{
    public class DeliveryNoteSearch
    {
        public int? DeliveryNoteID { get; set; }
        public string DeliveryNoteUD { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverAddress { get; set; }
        public string Description { get; set; }
        public string WorkCenterNM { get; set; }
        public bool? IsPrinted { get; set; }

        public List<DTO.DeliveryNoteDetailSearch> deliveryNoteDetailSearches { get; set; }
    }
}

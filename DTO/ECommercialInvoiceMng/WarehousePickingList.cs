using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
namespace DTO.ECommercialInvoiceMng
{
    public class WarehousePickingList
    {
        public int? WarehousePickingListID { get; set; }
        public string CMRNo { get; set; }
        public int? ClientID { get; set; }
        public List<WarehousePickingListDetail> WarehousePickingListDetails { get; set; }
    }
}

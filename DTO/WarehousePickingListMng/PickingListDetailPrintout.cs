using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.WarehousePickingListMng
{
    public class PickingListDetailPrintout
    {
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string WarehouseAreaUD { get; set; }
        public int? Quantity { get; set; }
        public int? PickedQnt { get; set; }
        public string Unit { get; set; }
        public string IsChecked { get; set; }
                
    }
}

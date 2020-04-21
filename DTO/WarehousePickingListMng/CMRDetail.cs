using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.WarehousePickingListMng
{
    public class CMRDetail
    {
        public int? RowIndex { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }
        public string Colli { get; set; }
        public string PackagingMethodNM { get; set; }
    }
}

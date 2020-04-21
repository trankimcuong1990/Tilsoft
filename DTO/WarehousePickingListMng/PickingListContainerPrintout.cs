using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.WarehousePickingListMng
{
    public class PickingListContainerPrintout
    {
        public List<PickingListPrintout> PickingListPrintouts { get; set; }
        public List<PickingListDetailPrintout> PickingListDetailPrintouts { get; set; }
    }
}

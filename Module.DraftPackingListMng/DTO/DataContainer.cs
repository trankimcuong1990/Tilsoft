using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DraftPackingListMng.DTO
{
    public class DataContainer
    {
        public DTO.DraftPackingListDTO DraftPackingListData { get; set; }

        public DataContainer()
        {
            DraftPackingListData = new DraftPackingListDTO();
        }
    }
}

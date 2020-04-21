using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DeliveryNote2.DTO
{
    public class SearchFormData
    {
        public List<DeliveryNoteSearchDTO> Data { get; set; }
        public List<SupportFactoryWareHouseList> supportFactoryWareHouseLists { get; set; }
        public List<StatusTypeDTO> StatusTypes { get; set; }

    }
}

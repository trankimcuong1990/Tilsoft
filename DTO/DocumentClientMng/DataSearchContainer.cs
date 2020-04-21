using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DocumentClientMng
{
    public class DataSearchContainer
    {
        public List<DocumentClientSearchResult> DocumentClientSearchResults { get; set; }
        public List<DeliveryStatus> DeliveryStatuss { get; set; }
        public List<DTO.Support.Season> Seasons { get; set; }
    }
}

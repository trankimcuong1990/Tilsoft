using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.OfferMng
{
    public class OfferStatusDTO
    {
        public int ConstantEntryID { get; set; }
        public int? TrackingStatusID { get; set; }
        public string TrackingStatusNM { get; set; }
        public int? DisplayOrder { get; set; }
    }
}

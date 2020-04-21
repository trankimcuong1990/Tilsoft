using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OfferSeasonMng.DTO
{
    public class OfferSeasonDetailRemarkDTO
    {
        public int OfferSeasonDetailRemarkID { get; set; }
        public Nullable<int> OfferSeasonDetailID { get; set; }
        public string Remark { get; set; }
        public Nullable<int> RemarkBy { get; set; }
        public string RemarkDate { get; set; }
        public string Remarktor { get; set; }
    }
}

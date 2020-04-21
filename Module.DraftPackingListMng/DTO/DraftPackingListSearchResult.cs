using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DraftPackingListMng.DTO
{
    public class DraftPackingListSearchResult
    {
        public int DraftPackingListID { get; set; }
        public string DraftPackingListUD { get; set; }
        public string PackingListDate { get; set; }
        public string InvoiceNo { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string CreatorName { get; set; }
        public string UpdatorName { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public int? creatorID { get; set; }
        public int? updatorID { get; set; }
    }
}

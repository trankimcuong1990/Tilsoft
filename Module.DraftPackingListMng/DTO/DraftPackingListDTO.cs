using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DraftPackingListMng.DTO
{
    public class DraftPackingListDTO
    {
        public int DraftPackingListID { get; set; }
        public string DraftPackingListUD { get; set; }
        public string PackingListDate { get; set; }
        public int? DraftCommercialInvoiceID { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string InvoiceNo { get; set; }
        public string ClientUD { get; set; }
        public string ClientInvoiceNo { get; set; }
        public int? AccountNo { get; set; }
        public string RefNo { get; set; }
        public string LCRefNo { get; set; }
        public string Conditions { get; set; }
        public string Remark { get; set; }
        public string CreatorName { get; set; }
        public string UpdatorName { get; set; }

        public List<DraftPackingListDetail> DraftPackingListDetail { get; set; }
        public List<DraftPackingListSparepartDetail> DraftPackingListSparepartDetail { get; set; }

        public DraftPackingListDTO()
        {
            DraftPackingListDetail = new List<DraftPackingListDetail>();
            DraftPackingListSparepartDetail = new List<DraftPackingListSparepartDetail>();
        }
    }
}

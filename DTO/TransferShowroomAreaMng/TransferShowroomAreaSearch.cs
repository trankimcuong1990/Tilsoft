using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.TransferShowroomAreaMng
{
    public class TransferShowroomAreaSearch
    {
        public int TransferShowroomAreaID { get; set; }
        public int? ShowroomItemID { get; set; }
        public int? FromShowroomAreaID { get; set; }
        public int? ToShowroomAreaID { get; set; }
        public DateTime? TransferDate { get; set; }
        public int? TransferBy { get; set; }
        public string Remark { get; set; }
        public int? Quantity { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string FromAreaUD { get; set; }
        public string ToAreaUD { get; set; }
        public string TransferName { get; set; }
        public int TransferID { get; set; }
        public string TransferDateString {
            get {
                if (TransferDate.HasValue) return TransferDate.Value.ToString("dd/MM/yyyy");
                else
                    return string.Empty;
            }
        }
    }
}

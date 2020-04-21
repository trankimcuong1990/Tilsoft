using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CashBookReceiptMng.DTO
{
    public class CashBookDocumentData
    {
        public int CashBookDocumentID { get; set; }
        public int? CashBookID { get; set; }
        public string FileUD { get; set; }
        public string Description { get; set; }
        public string FileLocation { get; set; }
        public string ThumbnailLocation { get; set; }
        public bool HasChange { get; set; }
        public string NewFile { get; set; }
    }
}

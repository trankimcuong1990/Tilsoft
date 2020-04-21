using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.FactoryOrderMng
{
    public class EmailNotificationConfirmed
    {
        public int FactoryOrderID { get; set; }
        public int? ModelID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string Season { get; set; }
        public int? NumberOfPiece { get; set; }
        public int? ProductTypeID { get; set; }
        public string ProductTypeNM { get; set; }
        public int? TrackingStatusID { get; set; }
        public string FactoryOrderUD { get; set; }
        public string ProformaInvoiceNo { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.StorageCardRpt.DTO
{
    public class StorageCard
    {
        public long PrimaryID { get; set; }

        public string DocumentDate { get; set; }

        public string ImportDocumentNo { get; set; }

        public string ExportDocumentNo { get; set; }

        public string Description { get; set; }

        public decimal? Receiving { get; set; }

        public decimal? Delivering { get; set; }

        public decimal? Ending { get; set; }

        public int? ReceivingNoteID { get; set; }

        public int? DeliveryNoteID { get; set; }

        public string ViewName { get; set; }

        public int? DocumentTypeID { get; set; }

        public int? WarehouseTransferID { get; set; }

        public string RecevingNoteUnitNM { get; set; }
        public string DeliveryNoteUnitNM { get; set; }
        public string ProductionUnitNM { get; set; }
        public string Remark { get; set; }
    }
}

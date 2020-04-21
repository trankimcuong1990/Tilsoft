using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DeliveryNote2.DTO
{
    public class DeliveryNotePrintoutDTO
    {
        public string ReceiptNo { get; set; }
        public string ReceiptDate { get; set; }
        public string DeliverName { get; set; }
        public string DeliverAddress { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverAddress { get; set; }
        public string StockName { get; set; }
        public string StockAddress { get; set; }
        public string Reason { get; set; }
        public int? DayReceipt { get; set; }
        public int? MonthReceipt { get; set; }
        public int? YearReceipt { get; set; }

        public List<DeliveryNoteDetailPrintoutDTO> DeliveryNoteDetailPrintoutDTOs { get; set; }
    }
}

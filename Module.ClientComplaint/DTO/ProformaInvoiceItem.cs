using System.Collections.Generic;

namespace Module.ClientComplaint.DTO
{
    public class ProformaInvoiceItem
    {
        public int SaleOrderID { get; set; }
        public string FactoryOrderUD { get; set; }
        public int? ClientID { get; set; }
        public string Season { get; set; }
        public string PIReceivedDate { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string LDS { get; set; }

    }

    public class ProformaInvoiceItems
    {
        public List<ProformaInvoiceItem> Data { get; set; }
        public int TotalRows { get; set; }
    }
}

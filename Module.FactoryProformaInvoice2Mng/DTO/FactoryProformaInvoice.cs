using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryProformaInvoice2Mng.DTO
{
    public class FactoryProformaInvoice
    {
        public int FactoryProformaInvoiceID { get; set; }
        public string Season { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public int? ClientID { get; set; }
        public int? FactoryID { get; set; }
        public string AttachedFile { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public bool? IsFurnindoConfirmed { get; set; }
        public int? FurnindoConfirmedBy { get; set; }
        public string FurnindoConfirmedDate { get; set; }
        public string FurnindoConfirmedRemark { get; set; }
        public bool? IsFactoryConfirmed { get; set; }
        public int? FactoryConfirmedBy { get; set; }
        public string FactoryConfirmedDate { get; set; }
        public string FactoryConfirmedRemark { get; set; }
        public string ConcurrencyFlag { get; set; }
        public string ClientUD { get; set; }
        public string FactoryUD { get; set; }
        public string FileLocation { get; set; }
        public string FriendlyName { get; set; }
        public string UpdatorName { get; set; }
        public string FurnindoConfirmerName { get; set; }
        public string FactoryConfirmerName { get; set; }
        public int PaymentTermID { get; set; }
        public int DeliveryTermID { get; set; }

        public List<DTO.FactoryProformaInvoiceDetail> FactoryProformaInvoiceDetails { get; set; }
        public string AttachedFile_NewFile { get; set; }
        public bool AttachedFile_HasChanged { get; set; }
    }
}

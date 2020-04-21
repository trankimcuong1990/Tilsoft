using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactorySaleInvoice.DTO
{
    public class FactorySaleInvoiceDTO
    {
        public int FactorySaleInvoiceID { get; set; }
        public string DocCode { get; set; }
        public int? FactorySaleInvoiceStatusID { get; set; }
        public int? FactoryRawMaterialID { get; set; }
        public string FactoryRawMaterialOfficialNM { get; set; }
        public string FactoryRawMaterialUD { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceNo { get; set; }
        public string PostingDate { get; set; }
        public string Currency { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatorNM { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatorNM { get; set; }
        public string UpdatedDate { get; set; }
        public int? VAT { get; set; }
        public string Remark { get; set; }
        public string FactorySaleInvoiceStatusNM { get; set; }
        public int? SupplierPaymentTerm { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string FriendlyName { get; set; }
        public bool? File_HasChange { get; set; }
        public string File_NewFile { get; set; }
        public string AttachedFile { get; set; }
        public string Season { get; set; }

        public List<FactorySaleInvoiceDetailDTO> FactorySaleInvoiceDetailDTOs { get; set; }
        public FactorySaleInvoiceDTO()
        {
            FactorySaleInvoiceDetailDTOs = new List<FactorySaleInvoiceDetailDTO>();
        }
    }
}

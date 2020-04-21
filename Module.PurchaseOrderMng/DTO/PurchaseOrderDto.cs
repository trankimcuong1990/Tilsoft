using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseOrderMng.DTO
{
    public class PurchaseOrderDto
    {
        public int? PurchaseOrderID { get; set; }
        public string PurchaseOrderUD { get; set; }
        public string PurchaseOrderDate { get; set; }
        public int? CompanyID { get; set; }
        public int? FactoryRawMaterialID { get; set; }
        public string FactoryRawMaterialUD { get; set; }
        public string FactoryRawMaterialShortNM { get; set; }
        public string FactoryRawMaterialOfficialNM { get; set; }
        public string Address { get; set; }
        public string Currency { get; set; }
        public int? PurchaseRequestID { get; set; }
        public string PurchaseRequestUD { get; set; }
        public string ETA { get; set; }
        public string AttachedFile { get; set; }
        public string Remark { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public int? ApprovedBy { get; set; }
        public string ApprovedDate { get; set; }

        public string UpdatorName { get; set; }
        public string ApproverName { get; set; }

        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string FriendlyName { get; set; }
        public bool? File_HasChange { get; set; }
        public string File_NewFile { get; set; }
        public List<DTO.PurchaseOrderDetailDto> PurchaseOrderDetailDTOs { get; set; }

        public string RequiredDocuments { get; set; }
        public string PaymentDocuments { get; set; }
        public int? SupplierPaymentTermID { get; set; }
        public string SupplierPaymentTermNM { get; set; }
        public decimal? VAT { get; set; }
        public string Season { get; set; }
        public int? PurchaseOrderStatusID { get; set; }
        public int? WorkOrderID { get; set; }
       
    }
}

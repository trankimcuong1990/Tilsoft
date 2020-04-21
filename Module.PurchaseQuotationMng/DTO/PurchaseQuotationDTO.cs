using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseQuotationMng.DTO
{
   public class PurchaseQuotationDTO
    {
        public int PurchaseQuotationID { get; set; }

        public string PurchaseQuotationUD { get; set; }

        public int? CompanyID { get; set; }

        public int? FactoryRawMaterialID { get; set; }

        public string FactoryRawMaterialUD { get; set; }

        public string FactoryRawMaterialShortNM { get; set; }

        public string FactoryRawMaterialOfficialNM { get; set; }

        public string Currency { get; set; }

        public string ValidFrom { get; set; }

        public string ValidTo { get; set; }

        public int? PurchaseDeliveryTermID { get; set; }

        public string PurchaseDeliveryTermNM { get; set; }

        public int? PurchasePaymentTermID { get; set; }

        public string PurchasePaymentTermNM { get; set; }

        public string AttachedFile { get; set; }

        public string FriendlyName { get; set; }

        public string FileLocation { get; set; }

        public string ThumbnailLocation { get; set; }

        public bool? File_HasChange { get; set; }

        public string File_NewFile { get; set; }

        public string Remark { get; set; }

        public bool? IsApproved { get; set; }

        public int? CreatedBy { get; set; }

        public string CreatorName { get; set; }

        public string CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public string UpdatorName { get; set; }

        public string UpdatedDate { get; set; }

        public int? ApprovedBy { get; set; }

        public string ApproverName { get; set; }

        public string ApprovedDate { get; set; }

        public bool? IsCancelled { get; set; }

        public int? CanceledBy { get; set; }

        public string CancellerName { get; set; }

        public string CancelelDate { get; set; }

        public List<DTO.PurchaseQuotationDetailDTO> PurchaseQuotationDetailDTOs { get; set; }

        #region ** Code developer Truong Son **

        public List<DTO.SupplierDTO> Suppliers { get; set; }

        public int ProductionItemID { get; set; }

        public string ProductionItemUD { get; set; }

        public string ProductionItemNM { get; set; }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactorySalesOrderMng.DTO
{
    public class FactorySaleQuotationDTO
    {
        public int FactorySaleQuotationID { get; set; }
        public string FactorySaleQuotationUD { get; set; }
        public int? FactorySaleQuotationStatusID { get; set; }
        public int? CompanyID { get; set; }
        public int? FactoryRawMaterialID { get; set; }
        public string FactoryRawMaterialContactPersonNM { get; set; }
        public string FactoryRawMaterialDocumentRefNo { get; set; }
        public string FactoryRawMaterialUD { get; set; }
        public string Currency { get; set; }
        public string DocumentDate { get; set; }
        public string ValidUntil { get; set; }
        public decimal? DiscountPercent { get; set; }
        public int? FactorySaleUserID { get; set; }
        public string Remark { get; set; }
        public string ShippingAddress { get; set; }
        public int? FactoryShippingMethodID { get; set; }
        public string BillingAddress { get; set; }
        public int? FactoryPaymentTermID { get; set; }
        public string ExpectedPaidDate { get; set; }
        public string EmployeeNM { get; set; }
        public string FactoryRawMaterialOfficialNM { get; set; }
        public int? Id { get; set; }
        public string Value { get; set; }
        public string Label { get; set; }
        public string CreateDate { get; set; }
        public string FactoryRawMaterialShortNM { get; set; }
        public int? SupplierContactQuickInfoId { get; set; }
        public string FullName { get; set; }
        public List<DTO.FactorySaleQuotationDetailDTO> FactorySaleQuotationDetailDTOs { get; set; }
        public List<DTO.FactorySaleQuotationAttachedFileDTO> FactorySaleQuotationAttachedFileDTOs { get; set; }
        public FactorySaleQuotationDTO()
        {
            FactorySaleQuotationDetailDTOs = new List<FactorySaleQuotationDetailDTO>();
            FactorySaleQuotationAttachedFileDTOs = new List<FactorySaleQuotationAttachedFileDTO>();
        }
    }

    public class FactorySaleQuotationDetailDTO
    {
        public int FactorySaleQuotationDetailID { get; set; }
        public int? FactorySaleQuotationID { get; set; }
        public int? ProductionItemID { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? DiscountPercent { get; set; }
        public decimal? VATPercent { get; set; }
        public int? FactoryWarehouseID { get; set; }
        public string FactoryWarehouseNM { get; set; }
        public string ProductionItemNM { get; set; }
        public string Remark { get; set; }
        public string ProductionItemUD { get; set; }
        public int? UnitID { get; set; }
        public string UnitNM { get; set; }
    }

    public class FactorySaleQuotationAttachedFileDTO
    {
        public int FactorySaleQuotationAttachedFileID { get; set; }
        public int? FactorySaleQuotationID { get; set; }
        public string FileUD { get; set; }
        public string Remark { get; set; }
        public string OtherFileUrl { get; set; }
        public string OtherFileFriendlyName { get; set; }
    }
}
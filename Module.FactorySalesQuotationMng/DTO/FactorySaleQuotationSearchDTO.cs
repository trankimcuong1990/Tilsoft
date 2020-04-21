using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactorySalesQuotationMng.DTO
{
    public class FactorySaleQuotationSearchDTO
    {
        public int FactorySaleQuotationID { get; set; }
        public string FactorySaleQuotationUD { get; set; }
        public int? FactorySaleQuotationStatusID { get; set; }
        public string FactorySaleQuotationStatusNM { get; set; }
        public int? CompanyID { get; set; }
        public int? FactoryRawMaterialID { get; set; }
        public string FactoryRawMaterialOfficialNM { get; set; }
        public string FactoryRawMaterialContactPersonNM { get; set; }
        public string FactoryRawMaterialDocumentRefNo { get; set; }
        public string Currency { get; set; }
        public string DocumentDate { get; set; }
        public string ValidUntil { get; set; }
        public decimal DiscountPercent { get; set; }
        public int? FactorySaleUserID { get; set; }
        public string Remark { get; set; }
        public string ShippingAddress { get; set; }
        public int? FactoryShippingMethodID { get; set; }
        public string BillingAddress { get; set; }
        public int? FactoryPaymentTermID { get; set; }
        public string ExpectedPaidDate { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public bool IsConfirmed { get; set; }
        public int? ConfirmedBy { get; set; }
        public string ConfirmedDate { get; set; }
        //public bool IsDeleted { get; set; }
        //public int? DeletedBy { get; set; }
        //public string DeletedDate { get; set; }
        public string Confirmer { get; set; }
        public string UpdaterName { get; set; }
        public bool? IsDeleted { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AccountPayableRpt.DTO
{
    public class DetailOfLiabilitiesDto
    {
        public int PurchaseInvoiceID { get; set; }
        public int FactoryRawMaterialID { get; set; }
        public string FactoryRawMaterialUD { get; set; }
        public string FactoryRawMaterialOfficialNM { get; set; }
        public string InvoiceNo { get; set; }
        public string Remark { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? TotalRealDeposit { get; set; }
        public string LoadingDate { get; set; }
        public decimal BeginningBalance { get; set; }
        public List<DetailOfLiabilitiesByInvoiceNoDto> DetailOfLiabilitiesByInvoiceNoDto { get; set; }
        public bool OpenDetailOfLiabilities { get; set; }
        public int NumberRowDetail { get; set; }
        public decimal? ExchangeRate { get; set; }
        public string Currency { get; set; }
        public string KeyRawMaterialNM { get; set; }
        public string PurchaseInvoiceDate { get; set; }
        public string DueDate { get; set; }
        public int? DueDay { get; set; }
        public Nullable<int> DueColorID { get; set; }
        public string DueColorDate { get; set; }
        public string DueColorNM { get; set; }
        public decimal? ToTalInvoiceAmount { get; set; }

        //field to get code of color picker
        public string DueColorUD { get; set; }
        public int? ToDue { get; set; }
        public string SupplierPaymentTermNM { get; set; }
    }
}

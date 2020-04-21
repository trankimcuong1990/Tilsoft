using System.Collections.Generic;

namespace Module.CashBookRpt.DTO
{
    public class SupportFormDto
    {
        public SupportFormDto()
        {
            CashBookRptPaymentTypeDtos = new List<CashBookRptPaymentTypeDto>();
            CashBookRptBankAccountDtos = new List<CashBookRptBankAccountDto>();
        }
        public List<CashBookRptPaymentTypeDto> CashBookRptPaymentTypeDtos { get; set; }
        public List<CashBookRptBankAccountDto> CashBookRptBankAccountDtos { get; set; }
    }
    public class CashBookRptPaymentTypeDto
    {
        public object KeyID { get; set; }
        public int? PaymentTypeID { get; set; }
        public string PaymentTypeNM { get; set; }
    }
    public class CashBookRptBankAccountDto
    {
        public int SupplierBankID { get; set; }
        public string BankCode { get; set; }
        public string BankNM { get; set; }
        public string AccountNM { get; set; }
        public int? SupplierID { get; set; }
        public string Infor { get; set; }
    }
}

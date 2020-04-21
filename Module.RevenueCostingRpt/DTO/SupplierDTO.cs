using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.RevenueCostingRpt.DTO
{
    public class SupplierDTO
    {
        public int SupplierID { get; set; }
        public string SupplierUD { get; set; }
        public string SupplierNM { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Website { get; set; }
        public string PIC { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public int? ManufacturerCountryID { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
        public int? BookKeepingCode { get; set; }
        public int? PaymentTermID { get; set; }
        public int? DeliveryTermID { get; set; }
        public string BankBeneficiary { get; set; }
        public string BankName { get; set; }
        public string BankAddress { get; set; }
        public string BankAccountNo { get; set; }
        public string BankSwiftCode { get; set; }
        public string ShortAddress { get; set; }
        public string SupplierNameOnExactOnline { get; set; }
        public int? CompanyID { get; set; }
        public string FSCCode { get; set; }
    }
}

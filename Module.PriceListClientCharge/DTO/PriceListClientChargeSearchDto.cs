namespace Module.PriceListClientCharge.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PriceListClientChargeSearchDto
    {
        public int PriceListClientChargeID { get; set; }
        public int? ClientID { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int? ChargeTypeID { get; set; }
        public string ChargeTypeNM { get; set; }
        public int? CurrencyTypeID { get; set; }
        public string CurrencyTypeNM { get; set; }
        public int? CreatedBy { get; set; }
        public string CreateFullName { get; set; }
        public string CreateEmployeeNM { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdateFullName { get; set; }
        public string UpdateEmployeeNM { get; set; }
        public string UpdatedDate { get; set; }
    }
}

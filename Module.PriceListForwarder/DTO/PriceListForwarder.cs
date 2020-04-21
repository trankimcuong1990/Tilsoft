namespace Module.PriceListForwarder.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PriceListForwarder
    {
        public int PriceListForwarderID { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int ForwarderID { get; set; }
        public string ForwarderNM { get; set; }
        public int TypeOfCostID { get; set; }
        public string CostNM { get; set; }
        public int TypeOfCurrencyID { get; set; }
        public string CurrencyNM { get; set; }
        public int? CreatedBy { get; set; }
        public string CreateProfileNM { get; set; }
        public string CreatedEmployeeNM { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdateProfileNM { get; set; }
        public string UpdateEmployeeNM { get; set; }
        public string UpdatedDate { get; set; }

        public List<PriceListForwarderDetail> PriceListForwarderDetails { get; set; }
    }
}

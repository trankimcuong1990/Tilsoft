namespace Module.PriceListForwarder.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PriceListForwarderSearchResult
    {
        public int PriceListForwarderID { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string ForwarderUD { get; set; }
        public string ForwarderNM { get; set; }
        public string DisplayCost { get; set; }
        public string DisplayCurrency { get; set; }
        public int? CreatedBy { get; set; }
        public string CreateProfileNM { get; set; }
        public string CreateEmployeeNM { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdateProfileNM { get; set; }
        public string UpdateEmployeeNM { get; set; }
        public string UpdatedDate { get; set; }
    }
}

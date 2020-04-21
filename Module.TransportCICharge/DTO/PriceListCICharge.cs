namespace Module.TransportCICharge.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PriceListCICharge
    {
        public int PriceListClientChargeDetailID { get; set; }
        public int? PoLID { get; set; }
        public int? ContainerTypeID { get; set; }
        public int? ClientID { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public decimal? PricePerUnit { get; set; }
        public int? CurrencyTypeID { get; set; }
        public int? ChargeTypeID { get; set; }
    }
}

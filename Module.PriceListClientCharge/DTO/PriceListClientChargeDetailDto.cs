namespace Module.PriceListClientCharge.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PriceListClientChargeDetailDto
    {
        public int PriceListClientChargeDetailID { get; set; }
        public int? ClientID { get; set; }
        public int? PoLID { get; set; }
        public int? PoDID { get; set; }
        public int? ContainerTypeID { get; set; }
        public decimal? PricePerUnit { get; set; }
        public int? PriceListClientChargeID { get; set; }
    }
}

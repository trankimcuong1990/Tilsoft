namespace Module.TransportCICharge.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TransportCIChargeDetailDto
    {
        public int TransportCIChargeDetailID { get; set; }
        public int ChargeTypeID { get; set; }
        public string ContainerNo { get; set; }
        public int ContainerTypeID { get; set; }
        public string Description { get; set; }
        public decimal? PricePerUnit { get; set; }
        public decimal? NumberOfUnits { get; set; }
        public int TransportCIChargeID { get; set; }
        public decimal Amount { get; set; }
        public int? CurrencyTypeID { get; set; }
    }
}

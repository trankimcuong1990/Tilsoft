namespace Module.PriceListForwarder.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PriceListForwarderDetail
    {
        public int PriceListForwarderDetailID { get; set; }
        public int? PoLID { get; set; }
        public int? PoDID { get; set; }
        public int TypeOfContainerID { get; set; }
        public decimal? PricePerUnit { get; set; }
        public int? PriceListForwarderID { get; set; }
    }
}

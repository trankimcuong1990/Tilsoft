namespace Module.TransportCICharge.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SearchData
    {
        public List<DTO.TransportCIChargeSearchResultDto> Data { get; set; }

        /**
         * Support data: Payment Term, Currency Type
         */
        public List<Module.Support.DTO.PaymentTerm> PaymentTerms { get; set; }
        public List<Module.Support.DTO.TypeOfCurrency> CurrencyTypes { get; set; }
    }
}

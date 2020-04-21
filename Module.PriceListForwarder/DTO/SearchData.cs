namespace Module.PriceListForwarder.DTO
{
    using Support.DTO;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SearchData
    {
        public List<PriceListForwarderSearchResult> Data { get; set; }
        public List<TypeOfCost> TypeCosts { get; set; }
        public List<TypeOfCurrency> TypeCurrencys { get; set; }
    }
}

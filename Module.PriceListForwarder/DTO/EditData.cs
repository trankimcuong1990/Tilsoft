namespace Module.PriceListForwarder.DTO
{
    using Support.DTO;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class EditData
    {
        public PriceListForwarder Data { get; set; }
        public List<TypeOfCost> TypeCosts { get; set; }
        public List<TypeOfCurrency> TypeCurrencys { get; set; }
        public List<ContainerType> TypeContainers { get; set; }
        public List<POD> PODs { get; set; }
        public List<POL> POLs { get; set; }
    }
}

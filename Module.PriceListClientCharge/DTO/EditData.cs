namespace Module.PriceListClientCharge.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class EditData
    {
        public DTO.PriceListClientChargeDto Data { get; set; }

        /**
         *  Support: CurrencyType, ChargeType, POL, POD, ContainerType
         */
        public List<Module.Support.DTO.TypeOfCurrency> SupportCurrency { get; set; }
        public List<Module.Support.DTO.TypeOfCharge> SupportCharge { get; set; }
        public List<Module.Support.DTO.POL> SupportPOL { get; set; }
        public List<Module.Support.DTO.POD> SupportPOD { get; set; }
        public List<Module.Support.DTO.ContainerType> SupportContainerType { get; set; }
    }
}

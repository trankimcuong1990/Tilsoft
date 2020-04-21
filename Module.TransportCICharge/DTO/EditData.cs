namespace Module.TransportCICharge.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class EditData
    {
        public TransportCIChargeDto Data { get; set; }
        //public List<Support.DTO.TypeOfCost> TypeOfCosts { get; set; }
        //public List<Support.DTO.ContainerType> ContainerTypes { get; set; }
        //public List<BookingContainer> Containers { get; set; }
        //public List<Support.DTO.TypeOfCurrency> DropDownCurrency { get; set; }
        public List<Support.DTO.PaymentTerm> PaymentTerms { get; set; }
        public List<Support.DTO.ContainerType> ContainerTypes { get; set; }
        public List<Support.DTO.TypeOfCharge> ChargeTypes { get; set; }
        public List<Support.DTO.TypeOfCurrency> CurrencyTypes { get; set; }
        public List<DTO.ContainerNr> ContainerNrs { get; set; }
    }
}

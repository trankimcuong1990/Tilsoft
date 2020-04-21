using System.Collections.Generic;

namespace Module.TransportCostForwarder.DTO
{
    public class EditFormData
    {
        public TransportCostForwarder Data { get; set; }
        public List<Support.DTO.TypeOfCost> TypeOfCosts { get; set; }
        public List<Support.DTO.ContainerType> ContainerTypes { get; set; }
        public List<BookingContainer> Containers { get; set; }
        public List<Support.DTO.TypeOfCurrency> DropDownCurrency { get; set; }
    }
}

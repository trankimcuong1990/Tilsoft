namespace Module.TransportCostForwarder.DTO
{
    public class TransportCostForwarderItem
    {
        public int TransportCostForwarderItemID { get; set; }
        public int TransportCostForwarderID { get; set; }
        public int? TypeOfCost { get; set; }
        public string ContainerNumber { get; set; }
        public int? ContainerType { get; set; }
        public string ContainerNm { get; set; }
        public string Description { get; set; }
        public double? PricePerUnit { get; set; }
        public int? NumberOfUnits { get; set; }
        public int? CurrencyValue { get; set; }
        public string CurrencyName { get; set; }
        public double? Amount { get; set; }
    }
}

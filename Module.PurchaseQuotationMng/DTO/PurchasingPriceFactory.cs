namespace Module.PurchaseQuotationMng.DTO
{
    public class PurchasingPriceFactory
    {
        public int PrimaryID { get; set; }
        public int? KeyID { get; set; }
        public int? PurchaseQuotationID { get; set; }
        public int? ProductionItemID { get; set; }
        public int? FactoryRawMaterialID { get; set; }
        public int? ProductionItemGroupID { get; set; }
        public int? ApprovedBy { get; set; }

        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string ProductionItemGroupNM { get; set; }
        public string UnitNM { get; set; }
        public decimal? UnitPrice { get; set; }
        public string FactoryRawMaterialUD { get; set; }
        public bool? IsDefault { get; set; }
        public string EmployeeNM { get; set; }
        public string ValidFrom { get; set; }
        public string ValidTo { get; set; }
        public string PurchaseQuotationUD { get; set; }
        public string FileLocation { get; set; }
        public string FriendlyName { get; set; }
        public int? StatusID { get; set; }
    }
}

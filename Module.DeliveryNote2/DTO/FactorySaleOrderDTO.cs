using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DeliveryNote2.DTO
{
    public class FactorySaleOrderDTO
    {
        public int FactorySaleOrderID { get; set; }
        public string FactorySaleOrderUD { get; set; }
        public int Id { get; set; }
        public string Value { get; set; }
        public string Label { get; set; }
        public int? FactorySaleOrderStatusID { get; set; }
        public int? FactoryRawMaterialID { get; set; }
        public string FactoryRawMaterialContactPersonNM { get; set; }
        public string FactoryRawMaterialDocumentRefNo { get; set; }
        public string FactoryRawMaterialUD { get; set; }
        public string Currency { get; set; }
        public string ShippingAddress { get; set; }
        public int? FactoryShippingMethodID { get; set; }
        public string BillingAddress { get; set; }
        public int? FactoryPaymentTermID { get; set; }
        public string FactoryRawMaterialOfficialNM { get; set; }
        public string FactoryRawMaterialShortNM { get; set; }
        public string FactoryShippingMethodNM { get; set; }
        public string FactoryPaymentTermNM { get; set; }
        public int? CompanyID { get; set; }
        public bool? IsConfirmed { get; set; }
        public List<DTO.FactorySaleOrderDetailDTO> FactorySaleOrderDetailDTOs { get; set; }       
        public FactorySaleOrderDTO()
        {
            FactorySaleOrderDetailDTOs = new List<FactorySaleOrderDetailDTO>();         
        }
    }

    public class FactorySaleOrderDetailDTO
    {
        public int FactorySaleOrderDetailID { get; set; }
        public int? FactorySaleOrderID { get; set; }
        public int? ProductionItemID { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? DiscountPercent { get; set; }
        public decimal? VATPercent { get; set; }
        public int? FactoryWarehouseID { get; set; }
        public string FactoryWarehouseNM { get; set; }
        public string ProductionItemNM { get; set; }
        public string ProductionItemUD { get; set; }
        public int? UnitID { get; set; }
        public string UnitNM { get; set; }
        public List<DTO.ProductionItemUnit> ProductionItemUnits { get; set; }
        public FactorySaleOrderDetailDTO()
        {
            ProductionItemUnits = new List<ProductionItemUnit>();
        }

        public int? BranchID { get; set; }

        public decimal? StockQnt { get; set; }
        public decimal? TotalDelivery { get; set; }
    }
    public class ListProductionItemUnit
    {
        public int? ProductionItemID { get; set; }
        public int? UnitID { get; set; }
        public decimal? ConversionFactor { get; set; }
        public string UnitNM { get; set; }
    }
}

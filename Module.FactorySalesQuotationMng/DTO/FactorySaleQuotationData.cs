using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactorySalesQuotationMng.DTO
{
    public class FactorySaleQuotationData
    {
        public FactorySaleQuotationDTO Data { get; set; }

        //use to choose when create/edit
        public List<RawMaterial> lstListRawMaterial { get; set; }
        public List<ClientContact> lstClientContact { get; set; }
        public List<Status> lstStatus { get; set; }
        public List<PaymentTerm> lstPaymentTerm { get; set; }
        public List<ShipmentTypeOption> lstShipmentTypeOption { get; set; }
        public List<ShipmentToOption> lstShipmentToOption { get; set; }
        public List<ProductionItem> lstProductionItem { get; set; }
        public List<Warehouse> lstWarehouse { get; set; }
        public List<SaleEmployee> LstSaleEmployee { get; set; }
        public List<SupplierContactQuickInfo> SupplierContactQuickInfo { get; set; }

    }
    public class RawMaterial
    {
        public int FactoryRawMaterialID { get; set; }
        public string FactoryRawMaterialUD { get; set; }
        public string FactoryRawMaterialOfficialNM { get; set; }
        public string FactoryRawMaterialShortNM { get; set; }
        public string Address { get; set; }
        public int? CompanyID { get; set; }

        public int Id { get; set; }

        public string Value { get; set; }

        public string Label { get; set; }
    }
    public class ClientContact
    {
        public int ClientContactID { get; set; }
        public string FullName { get; set; }
        public int Id { get; set; }

        public string Value { get; set; }

        public string Label { get; set; }

    }
    public class Status
    {
        public int ConstantEntryID { get; set; }
        public int? FactorySaleQuotationStatusID { get; set; }
        public string FactorySaleQuotationStatusNM { get; set; }
    }
    public class PaymentTerm
    {
        public int PaymentTermID { get; set; }
        public string PaymentTermNM { get; set; }
    }
    public class ShipmentTypeOption
    {
        public int ShipmentTypeOptionID { get; set; }
        public string ShipmentTypeOptionNM { get; set; }
    }
    public class ShipmentToOption
    {
        public int ShipmentToOptionID { get; set; }
        public string ShipmentToOptionNM { get; set; }
    }
    public class ProductionItem
    {
        public int ProductionItemID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string FactoryWarehouseNM { get; set; }
        public int? CompanyID { get; set; }
        public int? FactoryWarehouseID { get; set; }
        public int Id { get; set; }

        public string Value { get; set; }

        public string Label { get; set; }
        public int? UnitID { get; set; }
        public string UnitNM { get; set; }
    }
    public class Warehouse
    {
        public int FactoryWarehouseID { get; set; }
        public string FactoryWarehouseNM { get; set; }
        public int? CompanyID { get; set; }
    }
    public class SaleEmployee
    {
        public int EmployeeID { get; set; }
        public string EmployeeNM { get; set; }
        public int? CompanyID { get; set; }
        public int Id { get; set; }

        public string Value { get; set; }

        public string Label { get; set; }
    }
}

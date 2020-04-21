using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReportStockList
{
    public class StockListDetail
    {
        public List<SaleOrderDetail> SaleOrderDetails { get; set; }
        public List<WarehouseImportDetail> WarehouseImportDetails { get; set; }
        public List<WarehousePickingListDetail> WarehousePickingListDetails { get; set; }
        public List<LoadingPlanDetail> LoadingPlanDetails { get; set; }
    }


    public class SaleOrderDetail
    {
        public object RowIndex { get; set; }

        public string KeyID { get; set; }

        public int? GoodsID { get; set; }

        public int? ProductStatusID { get; set; }

        public string ProductType { get; set; }

        public string ProformaInvoiceNo { get; set; }

        public string OrderType { get; set; }

        public string ClientUD { get; set; }

        public string ClientNM { get; set; }

        public int? FobWarehouse_ReservationQnt { get; set; }

        public int? Warehouse_ReservationQnt { get; set; }

        public int? ReservationQnt { get; set; }

        public int? RemainReservationQnt { get; set; }

    }

    public class WarehouseImportDetail
    {
        public object RowIndex { get; set; }
        
        public string KeyID { get; set; }

        public int? GoodsID { get; set; }

        public int? ProductStatusID { get; set; }

        public string ProductType { get; set; }

        public string ReceiptNo { get; set; }

        public string ImportTypeNM { get; set; }

        public int? ImportQnt { get; set; }

        public int? FobWarehouse_ImportedQnt { get; set; }

    }

    public class WarehousePickingListDetail
    {
        public object RowIndex { get; set; }
        
        public string KeyID { get; set; }

        public int? GoodsID { get; set; }

        public int? ProductStatusID { get; set; }

        public string ProductType { get; set; }

        public string ReceiptNo { get; set; }

        public string ProformaInvoiceNo { get; set; }

        public string OrderType { get; set; }

        public string ClientUD { get; set; }

        public string ClientNM { get; set; }

        public int? PickedQnt { get; set; }

    }

    public class LoadingPlanDetail
    {
        public object RowIndex { get; set; }
        public string KeyID { get; set; }

        public int? GoodsID { get; set; }

        public int? ProductStatusID { get; set; }

        public string ProductType { get; set; }

        public string ProformaInvoiceNo { get; set; }

        public string OrderType { get; set; }

        public string ClientUD { get; set; }

        public string ClientNM { get; set; }

        public string LoadingPlanUD { get; set; }

        public string ContainerNo { get; set; }

        public string BLNo { get; set; }

        public int? FobWarehouse_ShippedQnt { get; set; }

    }
}

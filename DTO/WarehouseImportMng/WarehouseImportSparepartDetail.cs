using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.WarehouseImportMng
{
    public class WarehouseImportSparepartDetail
    {
        public int? WarehouseImportSparepartDetailID { get; set; }

        public int? WarehouseImportID { get; set; }

        public int? SparepartID { get; set; }

        public int? ProductStatusID { get; set; }

        public int? Quantity { get; set; }

        public decimal? UnitPrice { get; set; }

        public int? ECommercialInvoiceSparepartDetailID { get; set; }

        public int? LoadingPlanSparepartDetailID { get; set; }

        public string ArticleCode { get; set; }

        public string Description { get; set; }

        public string ProductStatusNM { get; set; }

        public string ClientUD { get; set; }

        public string ClientNM { get; set; }

        public string ProformaInvoiceNo { get; set; }

        public string FactoryUD { get; set; }

        public string BLNo { get; set; }

        public string ContainerNo { get; set; }

        public string ContainerTypeNM { get; set; }

        public string SealNo { get; set; }

        public int? ECommercialInvoiceID { get; set; }

        public int? SaleOrderID { get; set; }
        public int? LoadingPlanID { get; set; }
        public int? ClientID { get; set; }
        public int? RefQnt { get; set; }
        public int? WexQnt { get; set; }

        public List<WarehouseImportAreaDetail> WarehouseImportAreaDetails { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.SaleOrderMng
{
    public class LoadingPlanSparepartDetail
    {
        public int? LoadingPlanSparepartDetailID { get; set; }

        public int? LoadingPlanID { get; set; }

        public int? SaleOrderID { get; set; }

        public int? SaleOrderDetailSparepartID { get; set; }

        public string LoadingPlanUD { get; set; }

        public string BLNo { get; set; }

        public string ContainerNo { get; set; }

        public string FactoryOrderUD { get; set; }

        public string FactoryUD { get; set; }

        public string ProformaInvoiceNo { get; set; }

        public string ClientUD { get; set; }

        public string ClientNM { get; set; }

        public string ArticleCode { get; set; }

        public string Description { get; set; }

        public int? SaleOrderQnt { get; set; }

        public int? ShippedQnt { get; set; }

        public int? TotalReturnQnt { get; set; }

        public int? RemaingReturnQnt { get; set; }

        public int? SparepartID { get; set; }
        public int? OfferLineSparepartID { get; set; }
        public int? ModelID { get; set; }
        public int? PartID { get; set; }

    }
}
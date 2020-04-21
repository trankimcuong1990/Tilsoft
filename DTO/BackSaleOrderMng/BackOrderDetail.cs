using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.BackSaleOrderMng
{
    public class BackOrderDetail
    {
        public int? BackOrderDetailID { get; set; }

        public int? BackOrderID { get; set; }

        public int? OriginSaleOrderDetailID { get; set; }

        public int? BackSaleOrderDetailID { get; set; }

        public int? OriginSaleOrderDetailSparepartID { get; set; }

        public int? BackSaleOrderDetailSparepartID { get; set; }

        public int? OriginQnt { get; set; }

        public int? BackQnt { get; set; }

        public int? tempBackQnt { get; set; }

        public string ProformaInvoiceNo { get; set; }

        public string ClientUD { get; set; }

        public string ClientNM { get; set; }

        public string SaleNM { get; set; }

        public string ArticleCode { get; set; }

        public string Description { get; set; }

        public int? OrderQnt { get; set; }

        public int? TolalLoadingPlanQnt { get; set; }

        public int? RemainQnt { get; set; }

        public int? ModelID { get; set; }

        public int? FrameMaterialID { get; set; }

        public int? FrameMaterialColorID { get; set; }

        public int? MaterialID { get; set; }

        public int? MaterialTypeID { get; set; }

        public int? MaterialColorID { get; set; }

        public int? SubMaterialID { get; set; }

        public int? SubMaterialColorID { get; set; }

        public int? SeatCushionID { get; set; }

        public int? BackCushionID { get; set; }

        public int? CushionColorID { get; set; }

        public int? FSCTypeID { get; set; }

        public int? FSCPercentID { get; set; }

        public int? PartID { get; set; }

    }
}
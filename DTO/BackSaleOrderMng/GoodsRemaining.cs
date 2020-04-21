using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.BackSaleOrderMng
{
    public class GoodsRemaining
    {
        public int? DetailID { get; set; }

        public string ProformaInvoiceNo { get; set; }

        public string Season { get; set; }

        public string ClientUD { get; set; }

        public string ClientNM { get; set; }

        public int? SaleID { get; set; }

        public string SaleNM { get; set; }

        public string Sale2NM { get; set; }

        public string PaymentTermNM { get; set; }

        public string DeliveryTermNM { get; set; }

        public string ArticleCode { get; set; }

        public string Description { get; set; }

        public string ClientArticleCode { get; set; }

        public string ClientArticleName { get; set; }

        public string ClientOrderNumber { get; set; }

        public string ClientEANCode { get; set; }

        public int? OrderQnt { get; set; }

        public int? TolalLoadingPlanQnt { get; set; }

        public int? RemainQnt { get; set; }

        public int? BackQnt { get; set; }

        public string GoodsType { get; set; }

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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.OfferMng
{
    public class OfferLineSparepart
    {
        public int? OfferLineSparePartID { get; set; }

        public int? OfferID { get; set; }

        public string ArticleCode { get; set; }

        public string Description { get; set; }

        public decimal? UnitPrice { get; set; }

        public int? Quantity { get; set; }

        public int? ModelID { get; set; }

        public int? PartID { get; set; }

        public int? MaterialID { get; set; }

        public int? MaterialTypeID { get; set; }

        public int? MaterialColorID { get; set; }

        public int? CushionID { get; set; }

        public int? CushionColorID { get; set; }

        public int? CountryID { get; set; }

        public string Remark { get; set; }

        public int? RowIndex { get; set; }

        public string ModelUD { get; set; }

        public string ModelNM { get; set; }

        public string PartUD { get; set; }

        public string PartNM { get; set; }

        //SUPPORT INFO
        public bool IsEditing { get; set; }

        public string MaterialUD { get; set; }

        public string MaterialNM { get; set; }

        public string MaterialTypeUD { get; set; }

        public string MaterialTypeNM { get; set; }

        public string MaterialColorUD { get; set; }

        public string MaterialColorNM { get; set; }

        public string CushionUD { get; set; }

        public string CushionNM { get; set; }

        public string CushionColorUD { get; set; }

        public string CushionColorNM { get; set; }

        public string ManufacturerCountryUD { get; set; }

        public string ManufacturerCountryNM { get; set; }

        public string MaterialThumbnailLocation { get; set; }

        public string CushionColorThumbnailLocation { get; set; }

        public bool? IsCreatedOrder { get; set; }

        public string ClientArticleCode { get; set; }
        public string ClientArticleName { get; set; }
        public string ClientEANCode { get; set; }

    }
}
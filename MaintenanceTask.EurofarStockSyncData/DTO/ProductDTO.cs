﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceTask.EurofarStockSyncData.DTO
{
    public class ProductDTO
    {
        public string ArticleCode { get; set; }
        public string SubEANCode { get; set; }
        public string ModelNM { get; set; }
        public string Description { get; set; }
        public string ProductTypeNM { get; set; }
        public string EANCode { get; set; }
        public string ProductGroupNM { get; set; }
        public string ProductCategoryNM { get; set; }
        public string NetWeight { get; set; }
        public string OverallDimL { get; set; }
        public string OverallDimW { get; set; }
        public string OverallDimH { get; set; }
        public string SeatDimL { get; set; }
        public string SeatDimW { get; set; }
        public string SeatDimH { get; set; }
        public string ArmDimL { get; set; }
        public string ArmDimW { get; set; }
        public string ArmDimH { get; set; }
        public string OtherDimL { get; set; }
        public string OtherDimW { get; set; }
        public string OtherDimH { get; set; }
        public string PackagingMethodNM { get; set; }
        public string CartonBoxDimL { get; set; }
        public string CartonBoxDimW { get; set; }
        public string CartonBoxDimH { get; set; }
        public int? Qnt20DC { get; set; }
        public int? Qnt40DC { get; set; }
        public int? Qnt40HC { get; set; }
        public string CBM { get; set; }
        public string QntInBox { get; set; }
        public string GrossWeight { get; set; }
        public string BackCushionStuffing { get; set; }
        public string BackCushionParts { get; set; }
        public string BackCushionWeight { get; set; }
        public string BackCushionDimL { get; set; }
        public string BackCushionDimW { get; set; }
        public string BackCushionDimH { get; set; }
        public string SeatCushionStuffing { get; set; }
        public string SeatCushionParts { get; set; }
        public string SeatCushionWeight { get; set; }
        public string SeatCushionDimL { get; set; }
        public string SeatCushionDimW { get; set; }
        public string SeatCushionDimH { get; set; }
        public string MaterialNM { get; set; }
        public string MaterialTypeNM { get; set; }
        public string MaterialColorNM { get; set; }
        public string FrameMaterialNM { get; set; }
        public string FrameMaterialColorNM { get; set; }
        public string SubMaterialNM { get; set; }
        public string SubMaterialColorNM { get; set; }
        public string CushionNM { get; set; }
        public string CushionColorNM { get; set; }
        public string BackCushionNM { get; set; }
        public string SeatCushionNM { get; set; }
        public string FSCTypeNM { get; set; }
        public string FSCPercentNM { get; set; }
        public int StockQnt { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? VVPPrice { get; set; }

        public int ModelID { get; set; }
        public bool IsEnabled { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BOMStandardMng.DTO.ImportTemplate
{
    public class ImportTemplateData
    {
        public ImportTemplateData()
        {
            ProductionProcess = new ProductionProcessDTO();
            Pieces = new List<PieceDTO>();
            WorkCenters = new List<WorkCenterDTO>();
            ProductionItems = new List<ProductionItemDTO>();
            BOMStandardSources = new List<BOMStandardSourceDTO>();
        }

        public ProductionProcessDTO ProductionProcess { get; set; }
        public List<PieceDTO> Pieces { get; set; }
        public List<WorkCenterDTO> WorkCenters { get; set; }
        public List<ProductionItemDTO> ProductionItems { get; set; }
        public List<BOMStandardSourceDTO> BOMStandardSources { get; set; }


    }

    public class ProductionProcessDTO
    {
        public int? ProductionProcessID { get; set; }
        public int? ProductID { get; set; }
        public int? OPSequenceID { get; set; }
        public int? ModelID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        
    }

    public class PieceDTO
    {
        public int? ModelID { get; set; }
        public int? PieceID { get; set; }
        public string PieceUD { get; set; }
        public string PieceNM { get; set; }
        public int? Quantity { get; set; }
        public string PieceArticleCode { get; set; }
        public string PieceDescription { get; set; }
        public int? PieceProductID { get; set; }
    }

    public class WorkCenterDTO
    {
        public int? OPSequenceDetailID { get; set; }
        public int? OPSequenceID { get; set; }
        public string WorkCenterNM { get; set; }
        public int? SequenceIndexNumber { get; set; }
    }

    public class BOMStandardSourceDTO
    {
        public int? BOMStandardID { get; set; }
        public int? ParentBOMStandardID { get; set; }
        public int? ProductionProcessID { get; set; }
        public int? ProductionItemID { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? QtyByUnit { get; set; }
        public int? ProductionItemTypeID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string Unit { get; set; }
        public string ProductionItemTypeNM { get; set; }
        public int? OPSequenceDetailID { get; set; }
        public string WorkCenterNM { get; set; }
        public int? UnitID { get; set; }
        public string UnitNM { get; set; }
        public decimal? ConversionFactor { get; set; }

        public List<ProductionItemUnitDTO> ProductionItemUnitDTOs { get; set; }
    }

    public class ProductionItemUnitDTO
    {
        public int? ProductionItemID { get; set; }
        public int? UnitID { get; set; }
        public decimal? ConversionFactor { get; set; }
        public string UnitNM { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.SaleOrderMng
{
    public class SaleOrderSparepartReturn
    {
        public int SaleOrderSparepartReturnID { get; set; }

        public int? LoadingPlanSparepartDetailID { get; set; }

        public int? SaleOrderDetailSparepartID { get; set; }

        public int? OrderQnt { get; set; }

        public int? ReturnIndex { get; set; }

        public string CreatedDate { get; set; }

        public int? SaleOrderID { get; set; }

        public string LoadingPlanUD { get; set; }

        public string BLNo { get; set; }

        public string ContainerNo { get; set; }

        public string FactoryOrderUD { get; set; }

        public string FactoryUD { get; set; }

        public string ArticleCode { get; set; }

        public string Description { get; set; }

        public string NewPINo { get; set; }

        public int? NewSaleOrderID { get; set; }

        public int? NewOfferID { get; set; }

        
        //these field below is used for dto, do not need get from database
        public int? LoadingPlanID { get; set; }
        public int? SparepartID { get; set; }
        public int? OfferLineSparepartID { get; set; }
        public int? ModelID { get; set; }
        public int? PartID { get; set; }

        public int? RemaingReturnQnt { get; set; }
        
    }
}
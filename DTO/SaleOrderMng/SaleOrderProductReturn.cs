using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.SaleOrderMng
{
    public class SaleOrderProductReturn
    {
        public int SaleOrderProductReturnID { get; set; }

        public int? LoadingPlanDetailID { get; set; }

        public int? SaleOrderDetailID { get; set; }

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
        public int? ProductID { get; set; }
        public int? OfferLineID { get; set; }
        public int? ModelID { get; set; }
        public int? MaterialID { get; set; }
        public int? MaterialTypeID { get; set; }
        public int? MaterialColorID { get; set; }
        public int? FrameMaterialID { get; set; }
        public int? FrameMaterialColorID { get; set; }
        public int? SubMaterialID { get; set; }
        public int? SubMaterialColorID { get; set; }
        public int? SeatCushionID { get; set; }
        public int? BackCushionID { get; set; }
        public int? CushionColorID { get; set; }
        public int? FSCTypeID { get; set; }
        public int? FSCPercentID { get; set; }

        public int? RemaingReturnQnt { get; set; }

        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ReportStockOverview
{
    public class PhysicalStockByArea
    {
        public string FullKeyID { get; set; }
        public string KeyID { get; set; }
        public int? GoodsID { get; set; }
        public int? ProductStatusID { get; set; }
        public string ProductType { get; set; }
        public int? WarehouseAreaID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string ProductStatusNM { get; set; }
        public string WarehouseAreaUD { get; set; }
        public int? PhysicalQnt { get; set; }
    }   
}
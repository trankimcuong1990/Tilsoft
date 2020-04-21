using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.WarehouseTransportMng
{
    public class WarehouseTransportSparepartDetail
    {
        public int? WarehouseTransportSparepartDetailID { get; set; }

        public int? WarehouseTransportID { get; set; }

        public int? FromWarehouseAreaID { get; set; }

        public int? ToWarehouseAreaID { get; set; }

        public int? SparepartID { get; set; }

        public int? ProductStatusID { get; set; }

        public int? Quantity { get; set; }

        public string ArticleCode { get; set; }

        public string Description { get; set; }

        public string ProductStatusNM { get; set; }

        public string FromWarehouseAreaUD { get; set; }

    }
}
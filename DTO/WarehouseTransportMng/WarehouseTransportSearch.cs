using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.WarehouseTransportMng
{
    public class WarehouseTransportSearch
    {
        public int? WarehouseTransportID { get; set; }

        public string ReceiptNo { get; set; }

        public string TransportDate { get; set; }

        public int? TransportBy { get; set; }

        public int? CreatedBy { get; set; }

        public string CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public string UpdatedDate { get; set; }

        public object ConcurrencyFlag { get; set; }

        public string CreatorName { get; set; }

        public string UpdatorName { get; set; }

        public string TransportName { get; set; }

        public string Remark { get; set; }

    }
}
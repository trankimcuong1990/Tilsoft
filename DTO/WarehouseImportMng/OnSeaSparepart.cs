using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.WarehouseImportMng
{
    public class OnSeaSparepart
    {
        public int? LoadingPlanSparepartDetailID { get; set; }

        public int? SparepartID { get; set; }

        public int? ProductStatusID { get; set; }

        public int? Quantity { get; set; }

        public string ArticleCode { get; set; }

        public string Description { get; set; }

        public string ProductStatusNM { get; set; }

        public string ClientUD { get; set; }

        public string ClientNM { get; set; }

        public string ProformaInvoiceNo { get; set; }

        public string FactoryUD { get; set; }

        public string BLNo { get; set; }

        public string ContainerNo { get; set; }

        public string ContainerTypeNM { get; set; }

        public string SealNo { get; set; }

    }
}
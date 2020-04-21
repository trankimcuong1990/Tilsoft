using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.PLCMng
{
    public class ItemForCreatePLC
    {
        public int? OfferLineID { get; set; }
        public int? OfferLineSparepartID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string BookingUD { get; set; }
        public string BLNo { get; set; }
        public string Season { get; set; }
        public int? FactoryID { get; set; }
        public int? BookingID { get; set; }
        public string ClientUD { get; set; }
        public string ContainerNo { get; set; }
        public string LoadingPlanUD { get; set; }
        public string ProfomaInvoiceNo { get; set; }
    }
}
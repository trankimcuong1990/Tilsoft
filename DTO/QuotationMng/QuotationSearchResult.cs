using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.QuotationMng
{
    public class QuotationSearchResult
    {
        public int QuotationID { get; set; }
        public string Season { get; set; }
        public string QuotationUD { get; set; }
        public string FactoryUD { get; set; }
        public string FactoryName { get; set; }
        public string DeliveryTermNM { get; set; }
        public string PaymentTermNM { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }
        public int? FactoryID { get; set; }
        public int UpdatedBy { get; set; }
        public string EmployeeName { get; set; }
    }
}
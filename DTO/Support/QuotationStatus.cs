using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class QuotationStatus
    {
        public int QuotationStatusID { get; set; }
        public string QuotationStatusNM { get; set; }
        public int? DisplayOrder { get; set; }
    }
}
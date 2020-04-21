using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class OfferDirection
    {
        public int QuotationOfferDirectionID { get; set; }

        public string QuotationOfferDirectionNM { get; set; }

        public int? DisplayOrder { get; set; }
    }
}
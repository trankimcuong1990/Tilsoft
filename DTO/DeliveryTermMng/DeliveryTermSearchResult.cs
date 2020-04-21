using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DTO.DeliveryTermMng
{
    public class DeliveryTermSearchResult
    {
        public int DeliveryTermID { get; set; }

        public string DeliveryTermUD { get; set; }

        public string DeliveryTypeNM { get; set; }

        public string DeliveryTermNM { get; set; }

        public bool IsActive { get; set; }

    }
}
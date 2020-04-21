﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ECommercialInvoiceMng
{
    public class ClientOfferCostItem
    {
        public int KeyID { get; set; }
        public int? ClientCostItemID { get; set; }
        public string ClientCostItemNM { get; set; }
        public int? ClientCostTypeID { get; set; }
        public string Currency { get; set; }
        public decimal? Cost20DC { get; set; }
        public decimal? Cost40DC { get; set; }
        public decimal? Cost40HC { get; set; }
    }
}

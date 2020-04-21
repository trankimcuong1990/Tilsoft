﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DashboardMng.DTO
{
    public class OfferApprovedAccountManagerDTO
    {
        public int OfferSeasonID { get; set; }
        public string OfferSeasonUD { get; set; }
        public Nullable<int> TotalOfferItem { get; set; }
        public Nullable<int> TotalReadyToApproved { get; set; }
        public Nullable<int> TotalApproved { get; set; }
        public int IsOfferNotApproved { get; set; }
        public int IsOfferApproved { get; set; }
        public string Season { get; set; }
        public Nullable<int> ClientID { get; set; }
        public string ClientNM { get; set; }
        public string SaleNM { get; set; }
        public Nullable<int> SaleID { get; set; }
        public bool IsAccountManager { get; set; }
        public string Currency { get; set; }
        public Nullable<decimal> DeltaPercent { get; set; }
        public Nullable<decimal> SaleAmount { get; set; }
        public string ItemStatusText { get; set; }
        public string OfferSeasonTypeNM { get; set; }
        public string ApprovedDate { get; set; }
        public string ApprovedUser { get; set; }
    }
}

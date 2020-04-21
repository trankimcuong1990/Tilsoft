﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryPayment2BalanceMng.DTO
{
    public class FactoryPayment2BalanceDTO
    {
        public int? FactoryPayment2BalanceID { get; set; }
        public string Season { get; set; }
        public int? SupplierID { get; set; }
        public decimal? BeginBalance { get; set; }
        public decimal? EndBalance { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public bool? isClosed { get; set; }
        public string UpdatorName { get; set; }
        public string SupplierNM { get; set; }
    }
}
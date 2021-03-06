﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ShowroomRpt.DTO
{
    public class FactoryWarehousePallet
    {
        public int FactoryWarehousePalletID { get; set; }
        public int FactoryWarehouseID { get; set; }
        public string FactoryWarehousePalletUD { get; set; }
        public string FactoryWarehousePalletNM { get; set; }
        public int? CompanyID { get; set; }
        public int ProductionItemID { get; set; }
        public long KeyID { get; set; }
    }
}

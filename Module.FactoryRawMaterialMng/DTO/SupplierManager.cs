﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryRawMaterialMng.DTO
{
     public class SupplierManager
    {
        public int SupplierManagerID { get; set; }
        public Nullable<int> FactoryRawMaterialID { get; set; }
        public int? EmployeeID { get; set; }
        public string EmployeeNM { get; set; }
        public bool PIC { get; set; }
        public bool IsReceivePriceRequest { get; set; }
    }
}

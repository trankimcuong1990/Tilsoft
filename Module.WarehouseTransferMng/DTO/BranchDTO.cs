﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WarehouseTransferMng.DTO
{
    public class BranchDTO
    {
        public int BranchID { get; set; }

        public string BranchUD { get; set; }

        public string BranchNM { get; set; }

        public int? CompanyID { get; set; }
    }
}

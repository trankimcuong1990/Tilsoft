﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionItemGroup.DTO
{
  public class ProductionItemGroupNotificationDTO
    {
        public int ProductionItemGroupNotificationID { get; set; }
        public int? ProductionItemGroupID { get; set; }
        public int? UserID { get; set; }
        public string EmployeeNM { get; set; }
        public string Remark { get; set; }
    }
}

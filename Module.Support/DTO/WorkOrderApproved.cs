﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Support.DTO
{
    public class WorkOrderApproved
    {
        public int Id { get; set; }

        public string Value { get; set; }

        public string Label { get; set; }

        public int? CompanyID { get; set; }

        public List<WorkOrderApprovedProductionItem> WorkOrderApprovedProductionItems { get; set; }
    }
}

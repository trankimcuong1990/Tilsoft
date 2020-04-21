using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WorkOrderRpt.DTO
{
    public class ReportFormData
    {
        public DTO.WorkOrder Data { get; set; }
        public List<DTO.OPSequenceDetail> OPSequenceDetails { get; set; }
        public List<DTO.ProductionTeam> ProductionTeams { get; set; }
        public List<DTO.FactoryWarehouse> FactoryWarehouses { get; set; }
        public List<DTO.Detail> Details { get; set; }
        public List<DTO.Receipt> Receipts { get; set; }

        public List<DTO.ReceiptOverview> ReceiptOverviews { get; set; }
        public List<DTO.ReceiptOverviewDetail> ReceiptOverviewDetails { get; set; }
    }
}

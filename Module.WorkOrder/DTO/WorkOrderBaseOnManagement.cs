namespace Module.WorkOrder.DTO
{
    using System.Collections.Generic;

    public class WorkOrderBaseOnManagement
    {
        public WorkOrderBaseOnManagement()
        {
            PreOrderWorkOrderBaseOnManagements = new List<PreOrderWorkOrderBaseOnManagement>();
        }

        public int WorkOrderID { get; set; }
        public int PreOrderWorkOrderID { get; set; }

        public string ProformaInvoiceNo { get; set; }
        public string LDS { get; set; }

        public decimal? ProductionQuantity { get; set; }

        public List<PreOrderWorkOrderBaseOnManagement> PreOrderWorkOrderBaseOnManagements { get; set; }

        public string WorkOrderUD { get; set; }
    }
}

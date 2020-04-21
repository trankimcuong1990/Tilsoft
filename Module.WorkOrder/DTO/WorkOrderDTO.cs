using System.Collections.Generic;

namespace Module.WorkOrder.DTO
{
    public class WorkOrderDTO
    {
        public int? WorkOrderID { get; set; }
        public string WorkOrderUD { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }
        public int? OPSequenceID { get; set; }
        public int? ClientID { get; set; }
        public int? ModelID { get; set; }
        public int? FactoryOrderDetailID { get; set; }
        public int? SampleProductID { get; set; }
        public int? CompanyID { get; set; }
        public bool? IsConfirmed { get; set; }
        public int? ConfirmedBy { get; set; }
        public string ConfirmedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public string ConfirmerName { get; set; }
        public string UpdatorName { get; set; }
        public string ClientUD { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public int BOMID { get; set; }
        public int BOMStandardID { get; set; }
        public string OPSequenceNM { get; set; }


        public int? WorkOrderTypeID { get; set; }
        public string StartDate { get; set; }
        public string FinishDate { get; set; }
        public int? WorkOrderStatusID { get; set; }

        public List<WorkOrderDetailDTO> WorkOrderDetailDTOs { get; set; }
        public int? SaleOrderID { get; set; }
        public int? ProductID { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ProductArticleCode { get; set; }
        public string ProductDescription { get; set; }
        public string WorkOrderTypeNM { get; set; }
        public string WorkOrderStatusNM { get; set; }
        public string CreatedDate { get; set; }
        public decimal? WastagePercent { get; set; }
        public int? SetStatusBy { get; set; }
        public string SetStatusDate { get; set; }
        public string SetStatustorName { get; set; }
        public int? SampleOrderID { get; set; }
        public string SampleOrderInfo { get; set; }
        public string PreOrderDescription { get; set; }
        public int? PreOrderWorkOrderID { get; set; }

        public int BranchID { get; set; }

        public List<WorkOrderOPSequenceDTO> WorkOrderOPSequences { get; set; }

        public WorkOrderDTO()
        {
            WorkOrderOPSequences = new List<WorkOrderOPSequenceDTO>();
        }
    }
}

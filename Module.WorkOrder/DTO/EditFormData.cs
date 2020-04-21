using System.Collections.Generic;

namespace Module.WorkOrder.DTO
{
    public class EditFormData
    {
        public WorkOrderDTO Data { get; set; }
        public List<Support.DTO.Client> SupportClient { get; set; }
        public List<Support.DTO.OPSequence> SupportOPSequence { get; set; }
        public List<Module.Support.DTO.WorkOrderType> WorkOrderTypes { get; set; }
        public List<Module.Support.DTO.WorkOrderStatus> WorkOrderStatuses { get; set; }
        public List<DTO.SampleProductDTO> SampleProductDTOs { get; set; }
        public BOMDTO BOMData { get; set; }
        public List<DTO.WorkOrderListPreOrderDTO> WorkOrderListPreOrderDTOs { get; set; }

        public List<OPSequenceDetailDTO> OPSequenceDetails { get; set; }
        public List<WorkOrderReportDTO> WorkOrderReportDTOs { get; set; }
        public List<WorkOrderReportHeaderDTO> WorkOrderReportHeaderDTOs { get; set; }
        public List<WorkOrderReportContentDTO> WorkOrderReportContentDTOs { get; set; }
        public List<ItemNotBOMDTO> ItemNotBOMDTOs { get; set; }

        public EditFormData()
        {
            OPSequenceDetails = new List<OPSequenceDetailDTO>();
        }
    }
}

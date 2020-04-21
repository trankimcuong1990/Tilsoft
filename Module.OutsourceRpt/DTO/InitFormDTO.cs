namespace Module.OutsourceRpt.DTO
{
    using System.Collections.Generic;

    public class InitFormDTO
    {
        public List<OutsourceProductionTeamDTO> ProductionTeam { get; set; }
        public List<OutsourceWorkOrderStatusDTO> WorkOrderStatus { get; set; }

        public InitFormDTO()
        {
            ProductionTeam = new List<OutsourceProductionTeamDTO>();

            WorkOrderStatus = new List<OutsourceWorkOrderStatusDTO>();
            WorkOrderStatus.Add(new OutsourceWorkOrderStatusDTO() { WorkOrderStatusID = 1, WorkOrderStatusNM = "NOT FINISHED" });
            WorkOrderStatus.Add(new OutsourceWorkOrderStatusDTO() { WorkOrderStatusID = 2, WorkOrderStatusNM = "FINISHED" });
        }
    }
}

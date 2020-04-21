using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionStatisticsMng.DAL
{
    internal partial class DataFactory
    {
        public List<DTO.PlanningProductionTeamDTO> GetPlanningProductionTeam(int userId, int workCenterID, string producedDate, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.PlanningProductionTeamDTO> data = new List<DTO.PlanningProductionTeamDTO>();
            try
            {
                using (ProductionStatisticsMngEntities context = CreateContext())
                {
                    int? companyID = fw_factory.GetCompanyID(userId);
                    var planningProductionTeam = context.ProductionStatisticsMng_function_GetPlanningProductionTeam(companyID, workCenterID, producedDate).ToList();
                    data = AutoMapper.Mapper.Map<List<ProductionStatisticsMng_PlanningProductionTeam_View>, List<DTO.PlanningProductionTeamDTO>>(planningProductionTeam);
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return data;
        }

    }
}

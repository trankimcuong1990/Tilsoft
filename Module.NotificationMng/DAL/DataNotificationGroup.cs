using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.NotificationMng.DAL
{
    class DataNotificationGroup
    {

        private DataConverter converter = new DataConverter();

        public override DTO.SearchFormDataDTO GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.SearchFormDataDTO data = new DTO.SearchFormDataDTO();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            Module.Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();

            string FactoryTeamUD = string.Empty;
            string FactoryTeamNM = string.Empty;
            int? factoryStepID = null;

            if (filters.ContainsKey("factoryTeamUD")) FactoryTeamUD = filters["factoryTeamUD"].ToString();
            if (filters.ContainsKey("factoryTeamNM")) FactoryTeamNM = filters["factoryTeamNM"].ToString();
            if (filters.ContainsKey("factoryStepID") && filters["factoryStepID"] != null) factoryStepID = Convert.ToInt32(filters["factoryStepID"].ToString());

            try
            {
                using (FactoryTeamEntities context = CreateContext())
                {
                    totalRows = context.FactoryTeamMng_function_SearchFactoryTeam(orderBy, orderDirection, FactoryTeamUD, FactoryTeamNM, factoryStepID).Count();
                    var result = context.FactoryTeamMng_function_SearchFactoryTeam(orderBy, orderDirection, FactoryTeamUD, FactoryTeamNM, factoryStepID);
                    data.Data = converter.DB2DTO_FactoryTeamSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
                data.FactorySteps = supportFactory.GetFactoryStep();
                return data;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
                return new DTO.SearchFormDataDTO();
            }
        }
    }
}

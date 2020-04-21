using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PreOrderPlanningMng.DAL
{
    public class DataFactory
    {
        private DataConverter converter = new DataConverter();

        public object GetInitData(int userID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            DTO.InitFormData data = new DTO.InitFormData();

            try
            {
                Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();

                data.SupportSeason = supportFactory.GetSeason();
                data.SupportFactory = supportFactory.GetFactory(userID);

                using (var context = CreateContext())
                {
                    context.FactoryPlanningMng_function_AddFactoryPlanningData();
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public object GetDataWithFilter(int userID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            totalRows = 0;

            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            List<DTO.FactoryPlanningData> data = new List<DTO.FactoryPlanningData>();

            try
            {
                string paramSeason = filters.ContainsKey("season") && filters["season"] != null && !string.IsNullOrEmpty(filters["season"].ToString().Trim()) ? filters["season"].ToString() : null;
                int? paramFactoryID = filters.ContainsKey("factoryID") && filters["factoryID"] != null && !string.IsNullOrEmpty(filters["factoryID"].ToString().Trim()) ? (int?)Convert.ToInt32(filters["factoryID"].ToString()) : null;

                using (var context = CreateContext())
                {
                    totalRows = context.PreOrderPlanningMng_function_FactoryPlanningSearchResult(paramSeason, paramFactoryID, orderBy, orderDirection).Count();
                    data = converter.DB2DTO_FactoryPlanningSearchResult(context.PreOrderPlanningMng_function_FactoryPlanningSearchResult(paramSeason, paramFactoryID, orderBy, orderDirection).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public object UpdateData(int userID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            try
            {
                string paramSeason = filters.ContainsKey("season") && filters["season"] != null && !string.IsNullOrEmpty(filters["season"].ToString().Trim()) ? filters["season"].ToString() : null;
                int? paramFactoryID = filters.ContainsKey("factoryID") && filters["factoryID"] != null && !string.IsNullOrEmpty(filters["factoryID"].ToString().Trim()) ? (int?)Convert.ToInt32(filters["factoryID"].ToString()) : null;

                List<DTO.FactoryPlanningData> dataView = filters.ContainsKey("dataView") && filters["dataView"] != null && !string.IsNullOrEmpty(filters["dataView"].ToString().Trim()) ? ((Newtonsoft.Json.Linq.JArray)filters["dataView"]).ToObject<List<DTO.FactoryPlanningData>>() : null;

                using (var context = CreateContext())
                {
                    foreach (var dtoItem in dataView)
                    {
                        FactoryPlanning dbItem;

                        if (dtoItem.FactoryPlanningID == 0)
                        {
                            dbItem = new FactoryPlanning();
                            context.FactoryPlanning.Add(dbItem);
                        }
                        else
                        {
                            dbItem = context.FactoryPlanning.FirstOrDefault(o => o.FactoryPlanningID == dtoItem.FactoryPlanningID);
                        }

                        if (dbItem == null)
                        {
                            continue;
                        }

                        converter.DTO2DB_FactoryPlanning(dtoItem, ref dbItem);

                        dbItem.UpdatedBy = userID;
                        dbItem.UpdatedDate = DateTime.Now;
                    }

                    context.SaveChanges();
                }

                return GetDataWithFilter(userID, filters, 20, 1, "PrimaryID", "ASC", out int totalRows, out notification);
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;

                return null;
            }
        }

        private PreOrderPlanningMngEntities CreateContext()
        {
            return new PreOrderPlanningMngEntities(Library.Helper.CreateEntityConnectionString("DAL.PreOrderPlanningMngModel"));
        }
    }
}

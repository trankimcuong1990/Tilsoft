using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionSummaryRpt.DAL
{
    internal class DataFactory
    {
        private readonly DataConverter converter = new DataConverter();

        public object GetInitForm(int userID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            DTO.InitFormDTO data = new DTO.InitFormDTO();

            try
            {
                Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();

                data.Seasons = supportFactory.GetSeason().ToList();
                data.Factories = supportFactory.GetFactory().Where(s => s.FactoryID == 374043 || s.FactoryID == 374072).ToList();
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

            DTO.SearchFormDTO data = new DTO.SearchFormDTO();

            try
            {
                string season = (filters.ContainsKey("Season") && filters["Season"] != null && !string.IsNullOrEmpty(filters["Season"].ToString())) ? filters["Season"].ToString() : null;
                int? factoryID = (filters.ContainsKey("FactoryID") && filters["FactoryID"] != null && !string.IsNullOrEmpty(filters["FactoryID"].ToString())) ? (int?)Convert.ToInt32(filters["FactoryID"].ToString()) : null;
                string clientUD = (filters.ContainsKey("ClientUD") && filters["ClientUD"] != null && !string.IsNullOrEmpty(filters["ClientUD"].ToString())) ? filters["ClientUD"].ToString() : null;
                string proformaInvoiceNo = (filters.ContainsKey("ProformaInvoice") && filters["ProformaInvoice"] != null && !string.IsNullOrEmpty(filters["ProformaInvoice"].ToString())) ? filters["ProformaInvoice"].ToString() : null;

                using (var context = CreateContext())
                {
                    var dbItem = context.ProductionSummaryRpt_function_GetDataProductionSummary(factoryID, season, clientUD, proformaInvoiceNo);
                    data.ProductionSummaries = converter.DB2DTO_ProductionSummary(dbItem.ToList());

                    var dbItemDetail = context.ProductionSummaryRpt_function_GetDataProductionSummaryDetail(factoryID, season, clientUD, proformaInvoiceNo);
                    data.ProductionSummaryDetails = converter.DB2DTO_ProductionSummaryDetail(dbItemDetail.ToList());

                    var dbWorkCenter = context.ProductionSummaryRpt_WorkCenter_View.Take(4);
                    data.WorkCenters = converter.DB2DTO_WorkCenter(dbWorkCenter.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public ProductionSummaryRptEntities CreateContext()
        {
            return new ProductionSummaryRptEntities(Library.Helper.CreateEntityConnectionString("DAL.ProductionSummaryRptModel"));
        }
    }
}

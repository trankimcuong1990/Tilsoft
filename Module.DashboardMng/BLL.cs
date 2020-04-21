using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Module.DashboardMng
{
    public class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public DTO.DashboardDetalResultDTO GetDashboardDetal(int userID, string season, out Library.DTO.Notification notification)
        {
            return factory.GetDashboardDetal(userID, season, out notification);
        }
        public DTO.DashboardReportData GetReportData(int iRequesterID, string season, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get dashboard report data");

            // query data
            return factory.GetReportData(iRequesterID, season, out notification);
        }

        public DTO.DashboardUserData GetDataForUserDashBoard(int iRequesterId, string season, out Library.DTO.Notification notification)
        {
            return factory.GetDataForUserDashBoard(iRequesterId, season, out notification);
        }

        public DTO.DashboardProductionData GetProductionData(int iRequesterId, string season, out Library.DTO.Notification notification)
        {
            return factory.GetProductionData(iRequesterId, season, out notification);
        }

        public DTO.DashboardPendingPriceDTO GetPendingPriceData(int userID, out Library.DTO.Notification notification)
        {
            return factory.GetPendingPriceData(userID, out notification);
        }
        public DTO.DashboardProductionRptData GetWeeklyProductionData(int iRequesterId, string season, int factoryId, out Library.DTO.Notification notification)
        {
            return factory.GetWeeklyProductionData(iRequesterId, season, factoryId, out notification);
        }

        public List<DTO.DashboardProductionOverviewDetail> GetDataForProductionOverviewDetail(int factoryOrderID, out Library.DTO.Notification notification)
        {
            return factory.GetDataForProductionOverviewDetail(factoryOrderID, out notification);
        }

        public object GetFinanceOverviewByFactory(int iRequesterID, string season, int? factoryID, out Library.DTO.Notification notification)
        {
            return factory.GetFinanceOverviewByFactory(iRequesterID, season, factoryID, out notification);
        }

        public DTO.OfferStatusDTO GetAdminDashboardOfferNotApprovedYetDTOs(int userId, string season, out Library.DTO.Notification notification)
        {
            return factory.GetAdminDashboardOfferNotApprovedYetDTOs(userId, season, out notification);
        }
        //public List<DTO.UserDashboardOfferSeasonNotApprovedYetDTO> GetUserDashboardOfferNotApprovedYetDTOs(out Library.DTO.Notification notification)
        //{
        //    return factory.GetUserDashboardOfferNotApprovedYetDTOs(out notification);
        //}
        public List<DTO.OfferAndPIDeltaComparisonDTO> GetOfferAndPIDeltaComparison(int userID, out Library.DTO.Notification notification)
        {
            return factory.GetOfferAndPIDeltaComparison(userID, out notification);
        }

        public List<DTO.DeltaByClientCompare> GetDeltaByClientCompare(int userID, string season, out Library.DTO.Notification notification)
        {
            return factory.GetDeltaByClientCompare(userID, season, out notification);
        }
        public List<DTO.PendingOfferItemPrice> GetPendingOfferItemPrice(int userID, string season, out Library.DTO.Notification notification)
        {
            return factory.GetPendingOfferItemPrice(userID, season, out notification);
        }

        public DTO.HtmlReportData GetHTMLReportData(int userID, string season, out Library.DTO.Notification notification)
        {
            return factory.GetHTMLReportData(userID, season, out notification);
        }
    }
}

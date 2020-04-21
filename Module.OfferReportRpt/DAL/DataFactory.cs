using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Data.Entity.Core.Objects;

namespace Module.OfferReportRpt.DAL
{
    public class DataFactory
    {
        private DataConverter converter = new DataConverter();
        private Framework.DAL.DataFactory dalFramework = new Framework.DAL.DataFactory();

        private OfferReportRptEntities CreateContext()
        {
            return new OfferReportRptEntities(Library.Helper.CreateEntityConnectionString("DAL.OfferReportRptModel"));
        }

        // User Offer
        public DTO.FOBItemOnlyData GetUserFOBItemOnly(int userID, string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.FOBItemOnlyData data = new DTO.FOBItemOnlyData
            {
                fOBItemOnlyDTOs = new List<DTO.FOBItemOnlyDTO>(),
                seasons = new List<Support.DTO.Season>()
            };
            try
            {
                using (OfferReportRptEntities context = CreateContext())
                {
                    //var dbUserID = context.DashboardMng_Employee_ReportView.Where(o => o.UserID == userID).FirstOrDefault();
                    //if(dbUserID != null)
                    //{
                    //    data.UserID = dbUserID.UserID;
                    //}
                    //else
                    //{
                    //    data.UserID = null;
                    //}
                   
                    var dbItem = context.OfferReportRpt_FOBItemOnly_ReportView.Where(o => o.Season == season).ToList();
                    data.fOBItemOnlyDTOs = AutoMapper.Mapper.Map<List<OfferReportRpt_FOBItemOnly_ReportView>, List<DTO.FOBItemOnlyDTO>>(dbItem);
                    Support.DAL.DataFactory support_Factory = new Support.DAL.DataFactory();
                    data.seasons = support_Factory.GetSeason();
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public string GetExcelFOBItemOnlyReportData(string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            FOBItemOnlyDataObject ds = new FOBItemOnlyDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("DashboardMng_function_FOBItemOnlyReportView", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Season", season);
                adap.TableMappings.Add("Table", "FOBItemOnly");
                adap.Fill(ds);
                return Library.Helper.CreateReportFileWithEPPlus2(ds, "Offer_FOBItemOnly");
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return string.Empty;
            }
        }
    }
}


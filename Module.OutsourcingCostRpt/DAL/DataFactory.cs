using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;
using Module.OutsourcingCostRpt.DTO;
using Library;
using System.Data.SqlClient;

namespace Module.OutsourcingCostRpt.DAL
{
    class DataFactory : Library.Base.DataFactory2<DTO.SearchDTO, DTO.EditDTO>
    {
        Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();
        private OutsourcingCostRptEntities CreateContext()
        {
            return new OutsourcingCostRptEntities(Library.Helper.CreateEntityConnectionString("DAL.OutsourcingCostRptModel"));
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int userId, int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override EditDTO GetData(int userId, int id, Hashtable param, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override SearchDTO GetDataWithFilter(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override object GetInitData(int userId, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override object GetSearchFilter(int userId, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public DTO.Report GetReportData(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.Report report = new Report();
            try
            {
                string fromDate = null;
                string toDate = null;
                int? productionTeamID = null;
                int? clientID = null;
                bool? isOutSource = null;
                int? workCenterID = null;

                if (filters.ContainsKey("fromDate") && !string.IsNullOrEmpty(filters["fromDate"].ToString()))
                {
                    fromDate = filters["fromDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("toDate") && !string.IsNullOrEmpty(filters["toDate"].ToString()))
                {
                    toDate = filters["toDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("productionTeamID") && filters["productionTeamID"] != null)
                {
                    productionTeamID = Convert.ToInt32(filters["productionTeamID"]);
                }
                if (filters.ContainsKey("clientID") && filters["clientID"] != null)
                {
                    clientID = Convert.ToInt32(filters["clientID"]);
                }
                if (filters.ContainsKey("workCenterID") && filters["workCenterID"] != null)
                {
                    workCenterID = Convert.ToInt32(filters["workCenterID"]);
                }
                if (filters.ContainsKey("isOutSource") && filters["isOutSource"] != null && !string.IsNullOrEmpty(filters["isOutSource"].ToString()))
                {
                    isOutSource = (filters["isOutSource"].ToString() == "True") ? true : false;
                }

                //int pageSize = Convert.ToInt32(filters["pageSize"]);
                DateTime? frDate = fromDate.ConvertStringToDateTime();
                DateTime? tDate = toDate.ConvertStringToDateTime();

                using (var context = CreateContext())
                {
                    report.TotalRows = context.OutsourcingCostRpt_Function_GetReport(frDate, tDate, workCenterID, clientID, productionTeamID, isOutSource).Count();
                    var result = context.OutsourcingCostRpt_Function_GetReport(frDate, tDate, workCenterID, clientID, productionTeamID, isOutSource);
                    report.ReportDataDTOs = converter.DB2DTO_ReportView(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());                    
                }
                return report;
            }
            catch (Exception ex)
            {

                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return report;
            }
        }

        public List<DTO.ReportDataDetailDTO> GetReportDataDetail(System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.ReportDataDetailDTO> data = new List<ReportDataDetailDTO>();
            try
            {
                string fromDate = null;
                string toDate = null;
                int? productionTeamID = null;
                int? clientID = null;
                bool? isOutSource = null;
                int? workCenterID = null;

                int? selectedModelID = null;
                int? selectedProductionTeamID = null;
                int? selectedClientID = null;
                int? selectedWorkCenterID = null;

                if (filters.ContainsKey("fromDate") && !string.IsNullOrEmpty(filters["fromDate"].ToString()))
                {
                    fromDate = filters["fromDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("toDate") && !string.IsNullOrEmpty(filters["toDate"].ToString()))
                {
                    toDate = filters["toDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("productionTeamID") && filters["productionTeamID"] != null)
                {
                    productionTeamID = Convert.ToInt32(filters["productionTeamID"]);
                }
                if (filters.ContainsKey("clientID") && filters["clientID"] != null)
                {
                    clientID = Convert.ToInt32(filters["clientID"]);
                }
                if (filters.ContainsKey("workCenterID") && filters["workCenterID"] != null)
                {
                    workCenterID = Convert.ToInt32(filters["workCenterID"]);
                }
                if (filters.ContainsKey("isOutSource") && filters["isOutSource"] != null && !string.IsNullOrEmpty(filters["isOutSource"].ToString()))
                {
                    isOutSource = (filters["isOutSource"].ToString() == "True") ? true : false;
                }
                selectedModelID = Convert.ToInt32(filters["selectedModelID"]);
                selectedProductionTeamID = Convert.ToInt32(filters["selectedProductionTeamID"]);
                selectedClientID = Convert.ToInt32(filters["selectedClientID"]);
                selectedWorkCenterID = Convert.ToInt32(filters["selectedWorkCenterID"]);

                DateTime? frDate = fromDate.ConvertStringToDateTime();
                DateTime? tDate = toDate.ConvertStringToDateTime();

                using (var context = CreateContext())
                {
                    var detail = context.OutsourcingCostRpt_Function_GetReportDetail(frDate, tDate, workCenterID, clientID, productionTeamID, isOutSource)
                                        .Where(o => o.ProductionTeamID == selectedProductionTeamID && o.ModelID == selectedModelID && o.ClientID == selectedClientID && o.WorkCenterID == selectedWorkCenterID && o.ReceiptType == "N").ToList();
                    data = AutoMapper.Mapper.Map<List<OutsourcingCostRpt_Function_GetReportDetail_Result>, List<DTO.ReportDataDetailDTO>>(detail);
                }
                return data;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return data;
            }
        }

        public DTO.InitDTO GetInitDataReport(int userId, out Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            Support.DAL.DataFactory spF = new Support.DAL.DataFactory();
            DTO.InitDTO data = new InitDTO();
            int? companyID = fw_factory.GetCompanyID(userId);
            try
            {
                using(var context = CreateContext())
                {
                    data.productionTeams = AutoMapper.Mapper.Map<List<SupportMng_ProductionTeam_View>, List<Support.DTO.ProductionTeam>>(context.SupportMng_ProductionTeam_View.ToList()).ToList();
                }
                data.clients = spF.GetClient().ToList();
                
                return data;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return data;
            }
        }

        public string GetExcelReportData(System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();
            try
            {                
                string fromDate = string.Empty;
                string toDate = string.Empty;
                int? workCenterID = null;
                int? clientID = null;
                int? productionTeamID = null;
                bool? isOutSource = null;

                if (filters.ContainsKey("productionTeamID") && filters["productionTeamID"] != null)
                {
                    productionTeamID = Convert.ToInt32(filters["productionTeamID"]);
                }
                if (filters.ContainsKey("clientID") && filters["clientID"] != null)
                {
                    clientID = Convert.ToInt32(filters["clientID"]);
                }
                if (filters.ContainsKey("workCenterID") && filters["workCenterID"] != null)
                {
                    workCenterID = Convert.ToInt32(filters["workCenterID"]);
                }
                if (filters.ContainsKey("isOutSource") && filters["isOutSource"] != null && !string.IsNullOrEmpty(filters["isOutSource"].ToString()))
                {
                    isOutSource = (filters["isOutSource"].ToString() == "True") ? true : false;
                }
                if (filters.ContainsKey("fromDate")) fromDate = filters["fromDate"].ToString();
                if (filters.ContainsKey("toDate")) toDate = filters["toDate"].ToString();

                //data
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("OutsourcingCostRpt_Function_GetReport", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate.ConvertStringToDateTime());
                adap.SelectCommand.Parameters.AddWithValue("@ToDate", toDate.ConvertStringToDateTime());
                adap.SelectCommand.Parameters.AddWithValue("@WorkCenterID", workCenterID);
                adap.SelectCommand.Parameters.AddWithValue("@ClientID", clientID);
                adap.SelectCommand.Parameters.AddWithValue("@ProductionTeamID", productionTeamID);
                adap.SelectCommand.Parameters.AddWithValue("@IsOutsource", isOutSource);
                adap.Fill(ds.Function_GetReport);

                //data detail
                SqlDataAdapter adap2 = new SqlDataAdapter();
                adap2.SelectCommand = new SqlCommand("OutsourcingCostRpt_Function_GetReportDetail", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap2.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap2.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate.ConvertStringToDateTime());
                adap2.SelectCommand.Parameters.AddWithValue("@ToDate", toDate.ConvertStringToDateTime());
                adap2.SelectCommand.Parameters.AddWithValue("@WorkCenterID", workCenterID);
                adap2.SelectCommand.Parameters.AddWithValue("@ClientID", clientID);
                adap2.SelectCommand.Parameters.AddWithValue("@ProductionTeamID", productionTeamID);
                adap2.SelectCommand.Parameters.AddWithValue("@IsOutsource", isOutSource);
                adap2.Fill(ds.Function_GetReportDetail);

                return Library.Helper.CreateReportFileWithEPPlus2(ds, "OutsourcingCostRpt");
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

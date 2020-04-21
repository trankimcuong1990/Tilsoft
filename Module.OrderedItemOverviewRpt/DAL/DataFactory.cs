using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Module.OrderedItemOverviewRpt.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {

        private DataConverter converter = new DataConverter();

        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();

        public DTO.SearchFormData GetDataWithFilters(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.OrderedItemOverviewReportViewDTO>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            try
            {
                string season = "";
              
                if (filters.ContainsKey("season") && !string.IsNullOrEmpty(filters["season"].ToString()))
                {
                    season = filters["season"].ToString().Replace("'", "''");
                }

                if (filters.ContainsKey("pageSize") && !string.IsNullOrEmpty(filters["pageSize"].ToString()))
                {
                    pageSize =Int32.Parse( filters["pageSize"].ToString().Replace("'", "''"));
                }

                if (filters.ContainsKey("pageIndex") && !string.IsNullOrEmpty(filters["pageIndex"].ToString()))
                {
                    pageIndex =Int32.Parse(filters["pageIndex"].ToString().Replace("'", "''"));
                }

                Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();

                using (var context = CreateContext())
                {
                   
                    var clientCodes = context.OrderedItemOverviewRpt_Function_getClienUDs(season).ToList();
                    var results = context.OrderItemOverviewRpt_Function_getReportData(season).ToList();

                    foreach (var item in results)
                    {
                        string clientUDs = null;

                        foreach (var item2 in clientCodes)
                        {
                           
                            if(item.ModelID == item2.ModelID)
                            {
                                clientUDs += item2.ClientUD + ",";
                            }
                        }
                        item.ClientUDs = clientUDs.TrimEnd(',');
                    }
                    data.Data = converter.DB2DTO_OrderedItemOverviewReportView(results);
                    totalRows = results.Count();
                }
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
                return data;
            }
        }
        private OrderedItemOverviewRptEntities CreateContext()
        {
            return new OrderedItemOverviewRptEntities(Library.Helper.CreateEntityConnectionString("DAL.OrderedItemOverviewRptModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        //
        // CUSTOM FUNCTION
        //
        //
        // CUSTOM FUNCTION
        //
        public DTO.InitFormData GetInitData(int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.InitFormData data = new DTO.InitFormData();
            data.Seasons = new List<Support.DTO.Season>();

            //try to get data
            try
            {
                data.Seasons = supportFactory.GetSeason().ToList();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public string GetExcelReportData(string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("OrderItemOverviewRpt_Function_getReportData", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Season", season);

                //adap.TableMappings.Add("Table", "OrderedItemOverview_ReportView");
                adap.Fill(ds.OrderedItemOverview_ReportView);

                var result1 = ds.OrderedItemOverview_ReportView;

                SqlDataAdapter adap2 = new SqlDataAdapter();
                adap2.SelectCommand = new SqlCommand("OrderedItemOverviewRpt_Function_getClienUDs", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap2.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap2.SelectCommand.Parameters.AddWithValue("@Season", season);
                ds.EnforceConstraints = false;
                adap2.Fill(ds.OrderedOverviewRpt_Client_View);
                var result = ds.OrderedOverviewRpt_Client_View;

                foreach (var item in result1)
                {
                    string ClientUDs = null;
                    foreach (var item2 in result)
                    {
                        if (item.ModelID == item2.ModelID)
                        {
                            ClientUDs += item2.ClientUD + ",";
                        }
                    }
                    item.ClientUDs = ClientUDs.TrimEnd(',');
                }
                
                return Library.Helper.CreateReportFileWithEPPlus2(ds, "OrderedItemOverview");
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

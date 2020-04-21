using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;
using Module.ShippingPerformanceRpt.DTO;

namespace Module.ShippingPerformanceRpt.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Module.Support.DAL.DataFactory supportFactory = new Module.Support.DAL.DataFactory();
        private Module.Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private ShippingPerformanceRptEntities CreateContext()
        {
            return new ShippingPerformanceRptEntities(Library.Helper.CreateEntityConnectionString("DAL.ShippingPerformanceRptModel"));
        }
        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override DTO.EditFormData GetData(int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override SearchFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            totalRows = 0;

            string Season = null;
            if (filters.ContainsKey("Season") && filters["Season"] != null && !string.IsNullOrEmpty(filters["Season"].ToString()))
            {
                Season = filters["Season"].ToString().Replace("'", "''");
            }

            //try to get data
            try
            {
                using (ShippingPerformanceRptEntities context = CreateContext())
                {
                    var result = context.ShippingPerformanceRpt_function_GetReportData(Season, orderBy, orderDirection).ToList();
                   
                    data.Data = converter.DB2DTO_ShippingPerformanceRptDTO(result.ToList());
                    totalRows = result.Count();
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }


            return data;
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public string ExportExcel(int userId, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("ShippingPerformanceRpt_function_GetReportData", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                if (filters.ContainsKey("Season") && !string.IsNullOrEmpty(filters["Season"].ToString()))
                {
                    adap.SelectCommand.Parameters.AddWithValue("@Season", filters["Season"].ToString().Replace("'", "''"));
                }
                adap.Fill(ds.ShippingPerformanceRpt);
                ds.AcceptChanges();
                return Library.Helper.CreateReportFileWithEPPlus(ds, "ShippingPerformanceRpt", new List<string>() { "ShippingPerformanceRpt" });
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

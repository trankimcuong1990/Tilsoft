using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MissingShippingInfoRpt.DAL
{
    internal class DataFactory
    {
        private DataConverter converter = new DataConverter();

        private MissingShippingInfoRptEntities CreateContext()
        {
            return new MissingShippingInfoRptEntities(Library.Helper.CreateEntityConnectionString("DAL.MissingShippingInfoRptModel"));
        }

        public DTO.ReportData GetReportData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ReportData data = new DTO.ReportData();
            data.MissingShippingInfos = new List<DTO.MissingShippingInfo>();

            //try to get data
            try
            {
                using (MissingShippingInfoRptEntities context = CreateContext())
                {
                    data.MissingShippingInfos = converter.DB2DTO_MissingShippingInfoList(context.MissingShippingInfoRpt_MissingShippingInfo_View.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public string GetExcelReportData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("MissingShippingInfoRpt_function_getReport", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.Fill(ds.MissingShippingInfoRpt_MissingShippingInfo_View);

                // dev
                //Library.Helper.DevCreateReportXMLSource(ds, "MissingShippingInfo");

                // generate xml file
                return Library.Helper.CreateReportFiles(ds, "MissingShippingInfo");
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

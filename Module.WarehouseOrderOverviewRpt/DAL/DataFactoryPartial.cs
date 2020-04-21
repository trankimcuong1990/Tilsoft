using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Module.WarehouseOrderOverviewRpt.DAL
{
    internal partial class DataFactory
    {
        public string GetWarehouseSoldItem(string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();
            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("WarehouseOrderOverviewRpt_function_GetReport", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Season", season);

                adap.TableMappings.Add("Table", "Param");
                adap.TableMappings.Add("Table1", "WarehouseSoldItem");
                adap.Fill(ds);
                
                foreach (var item in ds.WarehouseSoldItem)
                {
                    item.FileLocation = FrameworkSetting.Setting.MediaFullSizeUrl + item.FileLocation;
                    item.ThumbnailLocation = FrameworkSetting.Setting.MediaThumbnailUrl + item.ThumbnailLocation;
                }
                return Library.Helper.CreateReportFileWithEPPlus2(ds, "WarehouseOrderOverviewRpt");
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return "";
            }
        }
    }
}

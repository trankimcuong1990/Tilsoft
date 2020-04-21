using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace DAL.ReportDeliverySheduleMng
{
    public class DataFactory
    {
        public string GetReportDeliveryShedule(string Season, string ClientNM, string ETA, string ContainerNo, int SaleID,  out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObjects ds = new ReportDataObjects();
          
            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("Report_function_getDeliveryScheduleList", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

                adap.SelectCommand.Parameters.AddWithValue("@Season", Season);
                adap.SelectCommand.Parameters.AddWithValue("@ClientNM", ClientNM);
                adap.SelectCommand.Parameters.AddWithValue("@ETA", ETA);
                adap.SelectCommand.Parameters.AddWithValue("@ContainerNo", ContainerNo);
                
                adap.SelectCommand.Parameters.AddWithValue("@SaleID", SaleID);
                //adap.SelectCommand.Parameters.AddWithValue("@Sale2ID", Sale2ID);
                adap.TableMappings.Add("Table", "Header");
                adap.TableMappings.Add("Table1", "Report_DeliveryShedule_View");
           

                adap.Fill(ds);


                // dev : this is  code line to render  xml schema to mapping to excel file.
                //DALBase.Helper.DevCreateReportXMLSource(ds, "ReportDeliverySchedule");

            
                 //if (System.IO.File.Exists(FrameworkSetting.Setting.AbsoluteThumbnailFolder + item.ThumbnailLocation))
                    // DALBase.Helper.CreateReportFiles(ds, "PLC");

                

                string result = DALBase.Helper.CreateReportFiles(ds, "ReportDeliverySchedule");
             if (string.IsNullOrEmpty(result))
             {
                 notification.Type = Library.DTO.NotificationType.Error;
                 result = string.Empty;
             }
             return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return string.Empty;
            }
         
            }
        //    catch (Exception ex)
        //    {
        //        notification.Type = Library.DTO.NotificationType.Error;
        //        notification.Message = ex.Message;
        //        if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
        //        {
        //            notification.DetailMessage.Add(ex.InnerException.Message);
        //        }
        //        return string.Empty;
        //    }
        //}



        public DTO.ReportDeliveryShedule.SupportDataContainer GetFilterData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DAL.Support.DataFactory factory = new Support.DataFactory();

            //try to get data
            try
            {
                DTO.ReportDeliveryShedule.SupportDataContainer dtoItem = new DTO.ReportDeliveryShedule.SupportDataContainer();

                dtoItem.Salers  =  factory.GetSaler().ToList();
                dtoItem.Seasons = factory.GetSeason().ToList();

                return dtoItem;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return new DTO.ReportDeliveryShedule.SupportDataContainer();
            }
        }

       
    }
}

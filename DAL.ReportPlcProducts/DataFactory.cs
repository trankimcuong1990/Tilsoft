using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace DAL.ReportPlcProducts
{
    public class DataFactory
    {
        public string GetReportPlcProducts(int PLCID, int UserID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObjects ds = new ReportDataObjects();

          //  int demoPLCID = 1690;

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("PLCMng_function_GetPrintData", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

                adap.SelectCommand.Parameters.AddWithValue("@PLCID", PLCID );
                adap.TableMappings.Add("Table", "PLCMng_PLC_ReportView");
                adap.TableMappings.Add("Table1", "PLCMng_PLC_ImnageReportView");

                adap.Fill(ds);


                // dev : this is  code line to render  xml schema to mapping to excel file.
               //  DALBase.Helper.DevCreateReportXMLSource(ds, "PLC_Test");

                foreach (var item in ds.PLCMng_PLC_ImnageReportView)
                {
                    //if (System.IO.File.Exists(FrameworkSetting.Setting.AbsoluteFileFolder + item.FileLocation))
                    //{
                    //    item.FileLocation = FrameworkSetting.Setting.AbsoluteFileFolder + item.FileLocation;
                    //}
                    //else
                    //{
                    //    item.FileLocation = "NONE";
                    //}

                   
                    if (item.IsThumbnailLocationNull())
                    {
                        item.ThumbnailLocation = "NONE";
                        if (System.IO.File.Exists(FrameworkSetting.Setting.AbsoluteThumbnailFolder + item.ThumbnailLocation))
                        {

                            item.ThumbnailLocation = FrameworkSetting.Setting.AbsoluteThumbnailFolder + item.ThumbnailLocation;
                        }
                        else
                        {
                            item.ThumbnailLocation = "NONE";
                        }
                    }
                       
                    
                 
                }
                 //if (System.IO.File.Exists(FrameworkSetting.Setting.AbsoluteThumbnailFolder + item.ThumbnailLocation))
                    // DALBase.Helper.CreateReportFiles(ds, "PLC");

                DALBase.Helper.DevCreateReportXMLSource(ds, "PLC");

             string result=  DALBase.Helper.CreateReportFiles(ds, "PLC") ;
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



        public DTO.ReportPlcProducts.SupportDataContainer GetSupportData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DAL.Support.DataFactory factory = new Support.DataFactory();

            //try to get data
            try
            {
                DTO.ReportPlcProducts.SupportDataContainer dtoItem = new DTO.ReportPlcProducts.SupportDataContainer();

                dtoItem.Factories = factory.GetFactory().ToList();
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
                return new DTO.ReportPlcProducts.SupportDataContainer();
            }
        }
    }
}

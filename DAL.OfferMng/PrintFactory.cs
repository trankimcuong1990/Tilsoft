using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DAL.OfferMng
{
    public class PrintFactory
    {
        public string GetPrintDataOffer5(int OfferID, bool IsSendEmail, bool IsGetFullSizeImage, int imageOption, int userID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            PrintDataObject ds = new PrintDataObject();
            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("OfferMng_function_GetPrintData", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@OfferID", OfferID);
                adap.SelectCommand.Parameters.AddWithValue("@IsSendEmail", IsSendEmail);
                adap.SelectCommand.Parameters.AddWithValue("@ImageOption", imageOption);

                adap.TableMappings.Add("Table", "ParamTable");
                adap.TableMappings.Add("Table1", "PrintHeader");
                adap.TableMappings.Add("Table2", "PrintDetail");
                adap.TableMappings.Add("Table3", "ModelPieceReport");
                adap.Fill(ds);

                //logo image
                ds.PrintHeader.FirstOrDefault().LogoImage = FrameworkSetting.Setting.MediaThumbnailUrl + ds.PrintHeader.FirstOrDefault().LogoImage;

                //set image url
                foreach (var item in ds.PrintDetail)
                {
                    if (IsGetFullSizeImage)
                    {
                        item.FileLocation = FrameworkSetting.Setting.MediaFullSizeUrl + item.FileLocation_FullSize;
                        item.FileGardenLocation = FrameworkSetting.Setting.MediaFullSizeUrl + item.FileGardenLocation_FullSize;
                    }
                    else
                    {
                        item.FileLocation = FrameworkSetting.Setting.MediaThumbnailUrl + item.FileLocation;
                        item.FileGardenLocation = FrameworkSetting.Setting.MediaThumbnailUrl + item.FileGardenLocation;
                    }
                }

                //DALBase.Helper.DevCreateReportXMLSource(ds, "Offer5");
                // generate xml file
                string reportFile = "";
                Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
                int? companyID = fwFactory.GetCompanyID(userID);
                switch (companyID)
                {
                    case 13:
                        reportFile = "Offer5_OrangePine";
                        break;
                    default:
                        reportFile = "Offer5";
                        break;
                }

                // remove table from dataset
                ds.Tables.Remove("OfferMng_OfferPrintoutPP_View");
                ds.Tables.Remove("OfferMng_OfferPrintoutPP_Detail_View");
                ds.Tables.Remove("OfferMng_OfferPrintoutPP_PieceDetail_View");
                ds.AcceptChanges();

                return Library.Helper.CreateReportFileWithEPPlus2(ds, reportFile);
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                Exception iEx = Library.Helper.GetInnerException(ex);
                notification.Message = iEx.Message;                
                return string.Empty;
            }
        }

        public string GetPrintDataOfferPP2013(int OfferID, int userID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            PrintDataObject ds = new PrintDataObject();
            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("OfferMng_function_GetPPPrintData", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@OfferID", OfferID);

                adap.TableMappings.Add("Table", "OfferMng_OfferPrintoutPP_View");
                adap.TableMappings.Add("Table1", "OfferMng_OfferPrintoutPP_Detail_View");
                adap.TableMappings.Add("Table2", "OfferMng_OfferPrintoutPP_PieceDetail_View");
                adap.Fill(ds);

                //
                // TO DO LIST
                //
                return (new Office2013Helper.PowerpointController()).ExportOffer(ds);                
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return string.Empty;
            }
        }
    }
}

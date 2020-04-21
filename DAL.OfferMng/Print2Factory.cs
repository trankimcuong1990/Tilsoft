using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DAL.OfferMng
{
    public class Print2Factory
    {
        public string GetPrintDataOffer2(int OfferID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            Print2DataObject ds = new Print2DataObject();
            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("OfferMng_function_GetPrint2Data", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@OfferID", OfferID);

                adap.TableMappings.Add("Table", "OfferMng_PrintHeader_View");
                adap.TableMappings.Add("Table1", "OfferMng_PrintDetail_View");
                adap.TableMappings.Add("Table2", "OfferMng_PrintHeaderReport_View");
                adap.TableMappings.Add("Table3", "OfferMng_PrintDetailReport_View");
                adap.Fill(ds);

                if (!string.IsNullOrEmpty(ds.OfferMng_PrintHeader_View.FirstOrDefault().LogoImage))
                {
                    if (System.IO.File.Exists(FrameworkSetting.Setting.AbsoluteThumbnailFolder + ds.OfferMng_PrintHeader_View.FirstOrDefault().LogoImage))
                    {
                        ds.OfferMng_PrintHeader_View.FirstOrDefault().LogoImage = FrameworkSetting.Setting.ThumbnailUrl + ds.OfferMng_PrintHeader_View.FirstOrDefault().LogoImage;
                    }
                    else
                    {
                        ds.OfferMng_PrintHeader_View.FirstOrDefault().LogoImage = "NONE";
                    }
                }

                //set image url
                foreach (var item in ds.OfferMng_PrintDetail_View)
                {
                    if (System.IO.File.Exists(FrameworkSetting.Setting.AbsoluteThumbnailFolder + item.FileLocation))
                    {
                        item.FileLocation = FrameworkSetting.Setting.ThumbnailUrl + item.FileLocation;
                    }
                    else
                    {
                        item.FileLocation = "NONE";
                    }
                }


                if (!string.IsNullOrEmpty(ds.OfferMng_PrintHeaderReport_View.FirstOrDefault().LogoImage))
                {
                    if (System.IO.File.Exists(FrameworkSetting.Setting.AbsoluteThumbnailFolder + ds.OfferMng_PrintHeaderReport_View.FirstOrDefault().LogoImage))
                    {
                        ds.OfferMng_PrintHeaderReport_View.FirstOrDefault().LogoImage = FrameworkSetting.Setting.ThumbnailUrl + ds.OfferMng_PrintHeaderReport_View.FirstOrDefault().LogoImage;
                    }
                    else
                    {
                        ds.OfferMng_PrintHeaderReport_View.FirstOrDefault().LogoImage = "NONE";
                    }
                }

                //set image url
                foreach (var item in ds.OfferMng_PrintDetailReport_View)
                {
                    if (System.IO.File.Exists(FrameworkSetting.Setting.AbsoluteThumbnailFolder + item.FileLocation))
                    {
                        item.FileLocation = FrameworkSetting.Setting.ThumbnailUrl + item.FileLocation;
                    }
                    else
                    {
                        item.FileLocation = "NONE";
                    }
                }

                // dev
                //DALBase.Helper.DevCreateReportXMLSource(ds, "Offer2");
                //return string.Empty;

                //generate xml file
                return DALBase.Helper.CreateReportFiles(ds, "Offer2");
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message };
                return string.Empty;
            }
        }

        public string GetExportNewVersion(int offerID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            Print1448DataObject ds = new Print1448DataObject();

            string pathFile = string.Empty;

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("OfferMng_function_GetPrint1448Data", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@offerID", offerID);

                adap.TableMappings.Add("Table", "OfferData");
                adap.TableMappings.Add("Table1", "OfferDetailData");
                adap.TableMappings.Add("Table2", "OfferSubDetailData");
                adap.Fill(ds);

                if (ds.OfferData != null)
                {
                    if (!string.IsNullOrEmpty(ds.OfferData.FirstOrDefault().ThumbnailLocation))
                    {
                        if (System.IO.File.Exists(FrameworkSetting.Setting.AbsoluteThumbnailFolder + ds.OfferData.FirstOrDefault().ThumbnailLocation))
                        {
                            ds.OfferData.FirstOrDefault().ThumbnailLocation = FrameworkSetting.Setting.MediaThumbnailUrl + ds.OfferData.FirstOrDefault().ThumbnailLocation;
                        }
                        else
                        {
                            ds.OfferData.FirstOrDefault().ThumbnailLocation = "NONE";
                        }
                    }
                }

                if (ds.OfferDetailData != null)
                {
                    foreach (var item in ds.OfferDetailData)
                    {
                        if (item != null)
                        {
                            if (!string.IsNullOrEmpty(item.ThumbnailLocation))
                            {
                                if (System.IO.File.Exists(FrameworkSetting.Setting.AbsoluteThumbnailFolder + item.ThumbnailLocation))
                                {
                                    item.ThumbnailLocation = FrameworkSetting.Setting.MediaThumbnailUrl + item.ThumbnailLocation;
                                }
                                else
                                {
                                    item.ThumbnailLocation = "NONE";
                                }
                            }
                        }
                    }
                }

                pathFile = Library.Helper.CreateReportFileWithEPPlus2(ds, "OfferReport_1448");

            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return pathFile;
        }
    }
}

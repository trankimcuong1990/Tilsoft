using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DAL.LoadingPlanMng
{
    public class ReportDataFactory
    {
        private LoadingPlanMngEntities CreateContext()
        {
            return new LoadingPlanMngEntities(DALBase.Helper.CreateEntityConnectionString("LoadingPlanMngModel"));
        }

        public string GetReportData(List<int> loadingPlanIDs, int print_type, int UserId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Print Success" };
            ReportDataObject ds = new ReportDataObject();

            string OrangePineLogo = "o/25092017/ea3df196aba243c698e45922ddca9afd.png";
            string EurofarLogo = "l/25092017/ea80f37500ec462990588ea9a858d0a3.png";

            try
            {
                //Get User data
                Employee dbEmployee = null;
                dbEmployee = new Employee();
                using (LoadingPlanMngEntities context = CreateContext())
                {
                    dbEmployee = context.Employee.FirstOrDefault(o => o.UserID == UserId);
                }

                if (print_type == 4 || print_type == 2)
                {
                    int? userGroupId = null;
                    using (LoadingPlanMngEntities context = CreateContext())
                    {
                        userGroupId = context.LoadingPlanMng_Function_GetUserGroupIdByUserId(UserId).FirstOrDefault();
                    }
                    if(userGroupId != 1)
                    {
                        throw new Exception("Sorry " + dbEmployee.EmployeeNM + ". You are not authorized to perform this function.");
                    }
                }
                foreach (int id in loadingPlanIDs)
                {
                    SqlDataAdapter adap = new SqlDataAdapter();
                    adap.SelectCommand = new SqlCommand("LoadingPlanMng_function_GetReportData", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                    adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    adap.SelectCommand.Parameters.AddWithValue("@LoadingPlanID", id);

                    adap.TableMappings.Add("Table", "LoadingPlanMng_LoadingPlan_ReportView");
                    adap.TableMappings.Add("Table1", "LoadingPlanMng_LoadingPlanDetail_ReportView");
                    adap.Fill(ds);
                }

               

                //ReportDataObject.LoadingPlanMng_LoadingPlan_ReportViewRow drLoadingPlan = ds.LoadingPlanMng_LoadingPlan_ReportView.FirstOrDefault();
                foreach (var drLoadingPlan in ds.LoadingPlanMng_LoadingPlan_ReportView)
                {
                    // Check Company to display logo
                    if (dbEmployee != null)
                    {
                        if (dbEmployee.CompanyID == 13)
                        {
                            drLoadingPlan.ReportLogo = FrameworkSetting.Setting.MediaFullSizeUrl + OrangePineLogo;
                        }
                        else
                        {
                            drLoadingPlan.ReportLogo = FrameworkSetting.Setting.MediaFullSizeUrl + EurofarLogo;
                        }
                    }

                   
                    if (!drLoadingPlan.IsSignatureImageNull() && !string.IsNullOrEmpty(drLoadingPlan.SignatureImage))
                    {
                        drLoadingPlan.SignatureImage = FrameworkSetting.Setting.MediaThumbnailUrl + drLoadingPlan.SignatureImage;
                    }
                    else
                    {
                        drLoadingPlan.SignatureImage = "NONE";
                    }

                    if (!drLoadingPlan.IsProductPicture1Null() && !string.IsNullOrEmpty(drLoadingPlan.ProductPicture1))
                    {
                        drLoadingPlan.ProductPicture1 = FrameworkSetting.Setting.MediaThumbnailUrl + drLoadingPlan.ProductPicture1;
                    }
                    else
                    {
                        drLoadingPlan.ProductPicture1 = "NONE";
                    }

                    if (!drLoadingPlan.IsProductPicture2Null() && !string.IsNullOrEmpty(drLoadingPlan.ProductPicture2))
                    {
                        drLoadingPlan.ProductPicture2 = FrameworkSetting.Setting.MediaThumbnailUrl + drLoadingPlan.ProductPicture2;
                    }
                    else
                    {
                        drLoadingPlan.ProductPicture2 = "NONE";
                    }

                    if (!drLoadingPlan.IsContainerPicture1Null() && !string.IsNullOrEmpty(drLoadingPlan.ContainerPicture1))
                    {
                        drLoadingPlan.ContainerPicture1 = FrameworkSetting.Setting.MediaThumbnailUrl + drLoadingPlan.ContainerPicture1;
                    }
                    else
                    {
                        drLoadingPlan.ContainerPicture1 = "NONE";
                    }

                    if (!drLoadingPlan.IsContainerPicture2Null() && !string.IsNullOrEmpty(drLoadingPlan.ContainerPicture2))
                    {
                        drLoadingPlan.ContainerPicture2 = FrameworkSetting.Setting.MediaThumbnailUrl + drLoadingPlan.ContainerPicture2;
                    }
                    else
                    {
                        drLoadingPlan.ContainerPicture2 = "NONE";
                    }


                    if (!drLoadingPlan.IsContainerPicture3Null() && !string.IsNullOrEmpty(drLoadingPlan.ContainerPicture3))
                    {
                        drLoadingPlan.ContainerPicture3 = FrameworkSetting.Setting.MediaThumbnailUrl + drLoadingPlan.ContainerPicture3;
                    }
                    else
                    {
                        drLoadingPlan.ContainerPicture3 = "NONE";
                    }

                    if (!drLoadingPlan.IsContainerPicture4Null() && !string.IsNullOrEmpty(drLoadingPlan.ContainerPicture4))
                    {
                        drLoadingPlan.ContainerPicture4 = FrameworkSetting.Setting.MediaThumbnailUrl + drLoadingPlan.ContainerPicture4;
                    }
                    else
                    {
                        drLoadingPlan.ContainerPicture4 = "NONE";
                    }

                    if (!drLoadingPlan.IsContainerPicture5Null() && !string.IsNullOrEmpty(drLoadingPlan.ContainerPicture5))
                    {
                        drLoadingPlan.ContainerPicture5 = FrameworkSetting.Setting.MediaThumbnailUrl + drLoadingPlan.ContainerPicture5;
                    }
                    else
                    {
                        drLoadingPlan.ContainerPicture5 = "NONE";
                    }

                    if (!drLoadingPlan.IsContainerPicture6Null() && !string.IsNullOrEmpty(drLoadingPlan.ContainerPicture6))
                    {
                        drLoadingPlan.ContainerPicture6 = FrameworkSetting.Setting.MediaThumbnailUrl + drLoadingPlan.ContainerPicture6;
                    }
                    else
                    {
                        drLoadingPlan.ContainerPicture6 = "NONE";
                    }

                    if (!drLoadingPlan.IsMerchandisesInside1Per3ContImageNull() && !string.IsNullOrEmpty(drLoadingPlan.MerchandisesInside1Per3ContImage))
                    {
                        drLoadingPlan.MerchandisesInside1Per3ContImage = FrameworkSetting.Setting.MediaThumbnailUrl + drLoadingPlan.MerchandisesInside1Per3ContImage;
                    }
                    else
                    {
                        drLoadingPlan.MerchandisesInside1Per3ContImage = "NONE";
                    }

                    if (!drLoadingPlan.IsMerchandisesInside1Per2ContImageNull() && !string.IsNullOrEmpty(drLoadingPlan.MerchandisesInside1Per2ContImage))
                    {
                        drLoadingPlan.MerchandisesInside1Per2ContImage = FrameworkSetting.Setting.MediaThumbnailUrl + drLoadingPlan.MerchandisesInside1Per2ContImage;
                    }
                    else
                    {
                        drLoadingPlan.MerchandisesInside1Per2ContImage = "NONE";
                    }

                    if (!drLoadingPlan.IsMerchandisesInsideFullContImageNull() && !string.IsNullOrEmpty(drLoadingPlan.MerchandisesInsideFullContImage))
                    {
                        drLoadingPlan.MerchandisesInsideFullContImage = FrameworkSetting.Setting.MediaThumbnailUrl + drLoadingPlan.MerchandisesInsideFullContImage;
                    }
                    else
                    {
                        drLoadingPlan.MerchandisesInsideFullContImage = "NONE";
                    }

                    if (!drLoadingPlan.IsContainerDoorAndLockImageNull() && !string.IsNullOrEmpty(drLoadingPlan.ContainerDoorAndLockImage))
                    {
                        drLoadingPlan.ContainerDoorAndLockImage = FrameworkSetting.Setting.MediaThumbnailUrl + drLoadingPlan.ContainerDoorAndLockImage;
                    }
                    else
                    {
                        drLoadingPlan.ContainerDoorAndLockImage = "NONE";
                    }

                    if (!drLoadingPlan.IsContainerSealImageNull() && !string.IsNullOrEmpty(drLoadingPlan.ContainerSealImage))
                    {
                        drLoadingPlan.ContainerSealImage = FrameworkSetting.Setting.MediaThumbnailUrl + drLoadingPlan.ContainerSealImage;
                    }
                    else
                    {
                        drLoadingPlan.ContainerSealImage = "NONE";
                    }

                    if (!drLoadingPlan.IsDryPackImageNull() && !string.IsNullOrEmpty(drLoadingPlan.DryPackImage))
                    {
                        drLoadingPlan.DryPackImage = FrameworkSetting.Setting.MediaThumbnailUrl + drLoadingPlan.DryPackImage;
                    }
                    else
                    {
                        drLoadingPlan.DryPackImage = "NONE";
                    }

                    if (!drLoadingPlan.IsApprovedImageNull() && !string.IsNullOrEmpty(drLoadingPlan.ApprovedImage))
                    {
                        drLoadingPlan.ApprovedImage = FrameworkSetting.Setting.MediaThumbnailUrl + drLoadingPlan.ApprovedImage;
                    }
                    else
                    {
                        drLoadingPlan.ApprovedImage = "NONE";
                    }
                }
                //Get image detail
                foreach (var drLoadingPlanDetail in ds.LoadingPlanMng_LoadingPlanDetail_ReportView)
                {
                    if (!drLoadingPlanDetail.IsOverralImageFileNull() && !string.IsNullOrEmpty(drLoadingPlanDetail.OverralImageFile))
                    {
                        drLoadingPlanDetail.OverralImageFile = FrameworkSetting.Setting.MediaThumbnailUrl + drLoadingPlanDetail.OverralImageFile;
                    }
                    else
                    {
                        drLoadingPlanDetail.OverralImageFile = "NONE";
                    }

                    if (!drLoadingPlanDetail.IsPackagingImageFileNull() && !string.IsNullOrEmpty(drLoadingPlanDetail.PackagingImageFile))
                    {
                        drLoadingPlanDetail.PackagingImageFile = FrameworkSetting.Setting.MediaThumbnailUrl + drLoadingPlanDetail.PackagingImageFile;
                    }
                    else
                    {
                        drLoadingPlanDetail.PackagingImageFile = "NONE";
                    }
                }

                    //generate schema
                    //DALBase.Helper.DevCreateReportXMLSource(ds, "LoadingPlan");

                    //generate xml file
                if (print_type == 1 && dbEmployee.CompanyID != 13)
                {
                    return DALBase.Helper.CreateReportFiles(ds, "LoadingPlan");
                }
                else if(print_type == 1 && dbEmployee.CompanyID == 13)
                {
                    return DALBase.Helper.CreateReportFiles(ds, "LoadingPlanOP");
                }
                else if (print_type == 2)
                {
                    return DALBase.Helper.CreateReportFiles(ds, "LoadingPlanOP_Client");
                }
                else if (print_type == 4)
                {
                    ds.Tables["LoadingPlanMng_LoadingPlan_ReportView"].AcceptChanges();
                    ds.Tables["LoadingPlanMng_LoadingPlanDetail_ReportView"].AcceptChanges();
                    return Library.Helper.CreateReceiptPrintout(ds.Tables["LoadingPlanMng_LoadingPlan_ReportView"], ds.Tables["LoadingPlanMng_LoadingPlanDetail_ReportView"], "LoadingPlanOP_Client_pdf.rdlc");
                }
                else
                {
                    return DALBase.Helper.CreateReportFiles(ds, "LoadingPlan2");
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return "";
            }
        }

        public DTO.LoadingPlanMng.LoadingPlanReportDTO GetPrinOutHTML(int loadingPlanID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Print HTML Success" };
            DTO.LoadingPlanMng.LoadingPlanReportDTO LoadingPlanPrintOut = new DTO.LoadingPlanMng.LoadingPlanReportDTO();
            LoadingPlanPrintOut.loadingPlanDetailReportDTOs = new List<DTO.LoadingPlanMng.LoadingPlanDetailReportDTO>();
            ReportDataObject ds = new ReportDataObject();
            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("LoadingPlanMng_function_GetHTMLReportData", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@LoadingPlanID", loadingPlanID);
                adap.TableMappings.Add("Table", "LoadingPlan");
                adap.TableMappings.Add("Table1", "LoadingPlanDetail");
                adap.Fill(ds);

                var loadingPlan = ds.LoadingPlan.FirstOrDefault();
                if(loadingPlan.Cont20DC != "" && loadingPlan.Cont20DC != null)
                {
                    LoadingPlanPrintOut.Cont20DC = 1;
                }
                else
                {
                    LoadingPlanPrintOut.Cont20DC = 0;
                }
                if (loadingPlan.Cont40DC != null && loadingPlan.Cont40DC != "")
                {
                    LoadingPlanPrintOut.Cont40DC = 1;
                }
                else
                {
                    LoadingPlanPrintOut.Cont40DC = 0;
                }
                if (loadingPlan.Cont40HC !=null && loadingPlan.Cont40HC != "")
                {
                    LoadingPlanPrintOut.Cont40HC = 1;
                }
                else
                {
                    LoadingPlanPrintOut.Cont40HC = 0;
                }
                if (loadingPlan.ContainerNo !=null)
                {
                    LoadingPlanPrintOut.ContainerNo = loadingPlan.ContainerNo;
                }
                else
                {
                    LoadingPlanPrintOut.ContainerNo = "";
                }
                if (loadingPlan.ContainerRefNo !=null)
                {
                    LoadingPlanPrintOut.ContainerRefNo = loadingPlan.ContainerRefNo;
                }
                else
                {
                    LoadingPlanPrintOut.ContainerRefNo = "";
                }
                if (loadingPlan.ControllerName !=null)
                {
                    LoadingPlanPrintOut.ControllerName = loadingPlan.ControllerName;
                }
                else
                {
                    LoadingPlanPrintOut.ControllerName = "";
                }
                if (loadingPlan.ETA != null)
                {
                    LoadingPlanPrintOut.ETA = loadingPlan.ETA;
                }
                else
                {
                    LoadingPlanPrintOut.ETA = "";
                }
                if (loadingPlan.LoadingPlanUD != null)
                {
                    LoadingPlanPrintOut.LoadingPlanUD = loadingPlan.LoadingPlanUD;
                }
                else
                {
                    LoadingPlanPrintOut.ETA = "";
                }
                if (loadingPlan.ETD != null)
                {
                    LoadingPlanPrintOut.ETD = loadingPlan.ETD;
                }
                else
                {
                    LoadingPlanPrintOut.ETD = "";
                }
                if (loadingPlan.FactoryUD != null)
                {
                    LoadingPlanPrintOut.FactoryUD = loadingPlan.FactoryUD;
                }
                else
                {
                    LoadingPlanPrintOut.FactoryUD = "";
                }
                LoadingPlanPrintOut.IsConfirmed = loadingPlan.IsConfirmed;
                if (loadingPlan.LoadingDate != null)
                {
                    LoadingPlanPrintOut.LoadingDate = loadingPlan.LoadingDate;
                }
                else
                {
                    LoadingPlanPrintOut.LoadingDate = "";
                }
                if (loadingPlan.PODName != null)
                {
                    LoadingPlanPrintOut.PODName = loadingPlan.PODName;
                }
                else
                {
                    LoadingPlanPrintOut.PODName = "";
                }
                if (loadingPlan.POLName != null)
                {
                    LoadingPlanPrintOut.POLName = loadingPlan.POLName;
                }
                else
                {
                    LoadingPlanPrintOut.POLName = "";
                }
                if (loadingPlan.ReportDate != null)
                {
                    LoadingPlanPrintOut.ReportDate = loadingPlan.ReportDate;
                }
                else
                {
                    LoadingPlanPrintOut.ReportDate = "";
                }
                if (loadingPlan.SealNo != null)
                {
                    LoadingPlanPrintOut.SealNo = loadingPlan.SealNo;
                }
                else
                {
                    LoadingPlanPrintOut.SealNo = "";
                }
                
                if(!loadingPlan.IsApprovedImageNull() && !string.IsNullOrEmpty(loadingPlan.ApprovedImage))
                {
                        LoadingPlanPrintOut.ApprovedImage = FrameworkSetting.Setting.MediaThumbnailUrl + loadingPlan.ApprovedImage;
                }
                else
                {
                    LoadingPlanPrintOut.ApprovedImage = "";
                }

                if(!loadingPlan.IsContainerDoorAndLockImageNull() && !string.IsNullOrEmpty(loadingPlan.ContainerDoorAndLockImage))
                {
                        LoadingPlanPrintOut.ContainerDoorAndLockImage = FrameworkSetting.Setting.MediaThumbnailUrl + loadingPlan.ContainerDoorAndLockImage;
                }
                else
                {
                    LoadingPlanPrintOut.ContainerDoorAndLockImage = "";
                }

                if(!loadingPlan.IsContainerPicture1Null() && !string.IsNullOrEmpty(loadingPlan.ContainerPicture1))
                {
                        LoadingPlanPrintOut.ContainerPicture1 = FrameworkSetting.Setting.MediaThumbnailUrl + loadingPlan.ContainerPicture1;
                }
                else
                {
                    LoadingPlanPrintOut.ContainerPicture1 = "";
                }
                if (!loadingPlan.IsContainerPicture2Null() && !string.IsNullOrEmpty(loadingPlan.ContainerPicture2))
                {
                        LoadingPlanPrintOut.ContainerPicture2 = FrameworkSetting.Setting.MediaThumbnailUrl + loadingPlan.ContainerPicture2;
                }
                else
                {
                    LoadingPlanPrintOut.ContainerPicture2 = "";
                }
                if (!loadingPlan.IsContainerPicture3Null() && !string.IsNullOrEmpty(loadingPlan.ContainerPicture3))
                {
                        LoadingPlanPrintOut.ContainerPicture3 = FrameworkSetting.Setting.MediaThumbnailUrl + loadingPlan.ContainerPicture3;
                }
                else
                {
                    LoadingPlanPrintOut.ContainerPicture3 = "";
                }
                if (!loadingPlan.IsContainerPicture4Null() && !string.IsNullOrEmpty(loadingPlan.ContainerPicture4))
                {
                        LoadingPlanPrintOut.ContainerPicture4 = FrameworkSetting.Setting.MediaThumbnailUrl + loadingPlan.ContainerPicture4;
                }
                else
                {
                    LoadingPlanPrintOut.ContainerPicture4 = "";
                }
                if (!loadingPlan.IsContainerPicture5Null() && !string.IsNullOrEmpty(loadingPlan.ContainerPicture5))
                {
                        LoadingPlanPrintOut.ContainerPicture5 = FrameworkSetting.Setting.MediaThumbnailUrl + loadingPlan.ContainerPicture5;
                }
                else
                {
                    LoadingPlanPrintOut.ContainerPicture5 = "";
                }
                if (!loadingPlan.IsContainerPicture6Null() && !string.IsNullOrEmpty(loadingPlan.ContainerPicture6))
                {
                        LoadingPlanPrintOut.ContainerPicture6 = FrameworkSetting.Setting.MediaThumbnailUrl + loadingPlan.ContainerPicture6;
                }
                else
                {
                    LoadingPlanPrintOut.ContainerPicture6 = "";
                }
                if (!loadingPlan.IsContainerSealImageNull() && !string.IsNullOrEmpty(loadingPlan.ContainerSealImage))
                {
                        LoadingPlanPrintOut.ContainerSealImage = FrameworkSetting.Setting.MediaThumbnailUrl + loadingPlan.ContainerSealImage;
                }
                else
                {
                    LoadingPlanPrintOut.ContainerSealImage = "";
                }
                if (!loadingPlan.IsDryPackImageNull() && !string.IsNullOrEmpty(loadingPlan.DryPackImage))
                {
                        LoadingPlanPrintOut.DryPackImage = FrameworkSetting.Setting.MediaThumbnailUrl + loadingPlan.DryPackImage;
                }
                else
                {
                    LoadingPlanPrintOut.DryPackImage = "";
                }
                if (!loadingPlan.IsMerchandisesInside1Per2ContImageNull() && !string.IsNullOrEmpty(loadingPlan.MerchandisesInside1Per2ContImage))
                {
                        LoadingPlanPrintOut.MerchandisesInside1Per2ContImage = FrameworkSetting.Setting.MediaThumbnailUrl + loadingPlan.MerchandisesInside1Per2ContImage;
                }
                else
                {
                    LoadingPlanPrintOut.MerchandisesInside1Per2ContImage = "";
                }
                if (!loadingPlan.IsMerchandisesInside1Per3ContImageNull() && !string.IsNullOrEmpty(loadingPlan.MerchandisesInside1Per3ContImage))
                {
                        LoadingPlanPrintOut.MerchandisesInside1Per3ContImage = FrameworkSetting.Setting.MediaThumbnailUrl + loadingPlan.MerchandisesInside1Per3ContImage;
                }
                else
                {
                    LoadingPlanPrintOut.MerchandisesInside1Per3ContImage = "";
                }
                if (!loadingPlan.IsMerchandisesInsideFullContImageNull() && !string.IsNullOrEmpty(loadingPlan.MerchandisesInsideFullContImage))
                {
                        LoadingPlanPrintOut.MerchandisesInsideFullContImage = FrameworkSetting.Setting.MediaThumbnailUrl + loadingPlan.MerchandisesInsideFullContImage;
                }
                else
                {
                    LoadingPlanPrintOut.MerchandisesInsideFullContImage = "";
                }
                if (!loadingPlan.IsProductPicture1Null() && !string.IsNullOrEmpty(loadingPlan.ProductPicture1))
                {
                        LoadingPlanPrintOut.ProductPicture1 = FrameworkSetting.Setting.MediaThumbnailUrl + loadingPlan.ProductPicture1;
                }
                else
                {
                    LoadingPlanPrintOut.ProductPicture1 = "";
                }
                if (!loadingPlan.IsProductPicture2Null() && !string.IsNullOrEmpty(loadingPlan.ProductPicture2))
                {
                        LoadingPlanPrintOut.ProductPicture2 = FrameworkSetting.Setting.MediaThumbnailUrl + loadingPlan.ProductPicture2;
                }
                else
                {
                    LoadingPlanPrintOut.ProductPicture2 = "";
                }
                if (!loadingPlan.IsSignatureImageNull() && !string.IsNullOrEmpty(loadingPlan.SignatureImage))
                {
                        LoadingPlanPrintOut.SignatureImage = FrameworkSetting.Setting.MediaThumbnailUrl + loadingPlan.SignatureImage;
                }
                else
                {
                    LoadingPlanPrintOut.SignatureImage = "";
                }

                //Fill Detail
                DTO.LoadingPlanMng.LoadingPlanDetailReportDTO LoadingPlanDetail;
                
                foreach(var item in ds.LoadingPlanDetail)
                {
                    LoadingPlanDetail = new DTO.LoadingPlanMng.LoadingPlanDetailReportDTO();
                    LoadingPlanPrintOut.loadingPlanDetailReportDTOs.Add(LoadingPlanDetail);

                    if (item.ArticleCode != null)
                    {
                        LoadingPlanDetail.ArticleCode = item.ArticleCode;
                    }
                    else
                    {
                        LoadingPlanDetail.ArticleCode = "";
                    }
                    if (item.ClientUD != null)
                    {
                        LoadingPlanDetail.ClientUD = item.ClientUD;
                    }
                    else
                    {
                        LoadingPlanDetail.ClientUD = "";
                    }
                    if (item.Description != null)
                    {
                        LoadingPlanDetail.Description = item.Description;
                    }
                    else
                    {
                        LoadingPlanDetail.Description = "";
                    }
                    if (item.FactoryUD != null)
                    {
                        LoadingPlanDetail.FactoryUD = item.FactoryUD;
                    }
                    else
                    {
                        LoadingPlanDetail.FactoryUD = "";
                    }
                    if (item.ProformaInvoiceNo != null)
                    {
                        LoadingPlanDetail.ProformaInvoiceNo = item.ProformaInvoiceNo;
                    }
                    else
                    {
                        LoadingPlanDetail.ProformaInvoiceNo = "";
                    }
                    if(item.QntIn40HC != 0)
                    {
                        LoadingPlanDetail.QntIn40HC = item.QntIn40HC;
                    }
                    else
                    {
                        LoadingPlanDetail.QntIn40HC = null;
                    }
                    if (item.Quantity != 0)
                    {
                        LoadingPlanDetail.Quantity = item.Quantity;
                    }
                    else
                    {
                        LoadingPlanDetail.Quantity = null;
                    }
                }
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
            }
            return LoadingPlanPrintOut;
        }
    }
}

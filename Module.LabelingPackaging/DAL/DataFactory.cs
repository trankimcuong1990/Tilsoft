using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Collections;
using Library.DTO;

namespace Module.LabelingPackaging.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private DataConverter converter = new DataConverter();
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
        private string frontendURL = "http://app.tilsoft.bg/";
        private LabelingPackagingEntities CreateContext()
        {
            return new LabelingPackagingEntities(Library.Helper.CreateEntityConnectionString("DAL.LabelingPackagingModel"));
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (LabelingPackagingEntities context = CreateContext())
                {
                    var dbItem = context.LabelingPackaging.Where(o => o.LabelingPackagingID == id).FirstOrDefault();
                    context.LabelingPackaging.Remove(dbItem);
                    context.SaveChanges();
                }
                return true;
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
                return false;
            }

        }

        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override DTO.SearchFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.LabelingPackaging dtoLabelingPackaging = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.LabelingPackaging>();
            dtoLabelingPackaging.UpdatedBy = userId;
            Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

            try
            {
                using (LabelingPackagingEntities context = CreateContext())
                {
                    LabelingPackaging dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new LabelingPackaging();
                        context.LabelingPackaging.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.LabelingPackaging.Where(o => o.LabelingPackagingID == id).FirstOrDefault();
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "data not found!";
                        return false;
                    }
                    else
                    {
                        // processing other file pdf
                        if (dtoLabelingPackaging.OtherFileHasChange)
                        {
                            dtoLabelingPackaging.OtherFile = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoLabelingPackaging.NewOtherFile, dtoLabelingPackaging.OtherFile, dtoLabelingPackaging.OtherFileFriendlyName);
                        }

                        if (dtoLabelingPackaging.RejectCommentFileHasChange)
                        {
                            dtoLabelingPackaging.RejectCommentFile = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoLabelingPackaging.NewRejectCommentFile, dtoLabelingPackaging.RejectCommentFile, dtoLabelingPackaging.RejectCommentFile_FriendlyName);
                        }

                        int i = 0;
                        // process Detail
                        foreach (DTO.LabelingPackagingDetail dtoDetail in dtoLabelingPackaging.LabelingPackagingDetails)
                        {
                            // Update client EAN code for Sale Order
                            SaleOrderDetail dbSaleOrderDetail = context.SaleOrderDetail.FirstOrDefault(o => o.SaleOrderDetailID == dtoDetail.SaleOrderDetailID);
                            if (dbSaleOrderDetail != null)
                            {
                                if (!string.IsNullOrEmpty(dtoDetail.ClientEANCode))
                                {
                                    dbSaleOrderDetail.ClientEANCode = dtoDetail.ClientEANCode;
                                }
                            }

                            // Hangtag
                            if (dtoDetail.HangTagFileHasChange)
                            {
                                if (string.IsNullOrEmpty(dtoDetail.NewHangTagFile))
                                {
                                    if (dtoDetail.HangTagFile == dtoLabelingPackaging.AIOHangTagFile)
                                    {
                                        dtoDetail.HangTagFile = null;
                                    }
                                    else
                                    {
                                        fwFactory.RemoveFile(dtoDetail.HangTagFile);
                                    }
                                    if (dtoLabelingPackaging.LabelingPackagingFileMonitors.Count >= (i + 1))
                                    {
                                        dtoLabelingPackaging.LabelingPackagingFileMonitors[i].HangTagFileStatus = false;
                                    }
                                }
                                else
                                {
                                    if (dtoDetail.HangTagFile == dtoLabelingPackaging.AIOHangTagFile)
                                    {
                                        dtoDetail.HangTagFile = null;
                                    }
                                    dtoDetail.HangTagFile = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoDetail.NewHangTagFile, dtoDetail.HangTagFile, dtoDetail.HangTagFriendlyName);
                                    if (dtoLabelingPackaging.LabelingPackagingFileMonitors.Count >= (i + 1))
                                    {
                                        dtoLabelingPackaging.LabelingPackagingFileMonitors[i].HangTagFileStatus = true;
                                    }
                                }

                            }
                            // BoxShippingMark
                            if (dtoDetail.BoxShippingMarkFileHasChange)
                            {
                                if (string.IsNullOrEmpty(dtoDetail.NewBoxShippingMarkFile))
                                {
                                    if (dtoDetail.BoxShippingMarkFile == dtoLabelingPackaging.AIOBoxShippingMarkFile)
                                    {
                                        dtoDetail.BoxShippingMarkFile = null;
                                    }
                                    else
                                    {
                                        fwFactory.RemoveFile(dtoDetail.BoxShippingMarkFile);
                                    }
                                    if (dtoLabelingPackaging.LabelingPackagingFileMonitors.Count >= (i + 1))
                                    {
                                        dtoLabelingPackaging.LabelingPackagingFileMonitors[i].BoxShippingMarkFileStatus = false;
                                    }
                                }
                                else
                                {
                                    if (dtoDetail.BoxShippingMarkFile == dtoLabelingPackaging.AIOBoxShippingMarkFile)
                                    {
                                        dtoDetail.BoxShippingMarkFile = null;
                                    }
                                    dtoDetail.BoxShippingMarkFile = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoDetail.NewBoxShippingMarkFile, dtoDetail.BoxShippingMarkFile, dtoDetail.BoxShippingMarkFriendlyName);
                                    if (dtoLabelingPackaging.LabelingPackagingFileMonitors.Count >= (i + 1))
                                    {
                                        dtoLabelingPackaging.LabelingPackagingFileMonitors[i].BoxShippingMarkFileStatus = true;
                                    }
                                }
                            }
                            // BrassLabel
                            if (dtoDetail.BrassLabelFileHasChange)
                            {
                                if (string.IsNullOrEmpty(dtoDetail.NewBrassLabelFile))
                                {
                                    if (dtoDetail.BrassLabelFile == dtoLabelingPackaging.AIOBrassLabelFile)
                                    {
                                        dtoDetail.BrassLabelFile = null;
                                    }
                                    else
                                    {
                                        fwFactory.RemoveFile(dtoDetail.BrassLabelFile);
                                    }
                                    if (dtoLabelingPackaging.LabelingPackagingFileMonitors.Count >= (i + 1))
                                    {
                                        dtoLabelingPackaging.LabelingPackagingFileMonitors[i].BrassLabelFileStatus = false;
                                    }
                                }
                                else
                                {
                                    if (dtoDetail.BrassLabelFile == dtoLabelingPackaging.AIOBrassLabelFile)
                                    {
                                        dtoDetail.BrassLabelFile = null;
                                    }
                                    dtoDetail.BrassLabelFile = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoDetail.NewBrassLabelFile, dtoDetail.BrassLabelFile, dtoDetail.BrassLabelFriendlyName);
                                    if (dtoLabelingPackaging.LabelingPackagingFileMonitors.Count >= (i + 1))
                                    {
                                        dtoLabelingPackaging.LabelingPackagingFileMonitors[i].BrassLabelFileStatus = true;
                                    }
                                }
                            }
                            // AI
                            if (dtoDetail.AIFileHasChange)
                            {
                                if (string.IsNullOrEmpty(dtoDetail.NewAIFile))
                                {
                                    if (dtoDetail.AIFile != dtoLabelingPackaging.AIOAIFile)
                                    {
                                        dtoDetail.AIFile = null;
                                    }
                                    else
                                    {
                                        fwFactory.RemoveFile(dtoDetail.AIFile);
                                    }
                                    if (dtoLabelingPackaging.LabelingPackagingFileMonitors.Count >= (i + 1))
                                    {
                                        dtoLabelingPackaging.LabelingPackagingFileMonitors[i].AIFileStatus = false;
                                    }
                                }
                                else
                                {
                                    if (dtoDetail.AIFile != dtoLabelingPackaging.AIOAIFile)
                                    {
                                        dtoDetail.AIFile = null;
                                    }
                                    dtoDetail.AIFile = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoDetail.NewAIFile, dtoDetail.AIFile, dtoDetail.AIFriendlyName);
                                    if (dtoLabelingPackaging.LabelingPackagingFileMonitors.Count >= (i + 1))
                                    {
                                        dtoLabelingPackaging.LabelingPackagingFileMonitors[i].AIFileStatus = true;
                                    }
                                }
                            }
                            // WashCushionLabel
                            if (dtoDetail.WashCushionLabelFileHasChange)
                            {
                                if (string.IsNullOrEmpty(dtoDetail.NewWashCushionLabelFile))
                                {
                                    if (dtoDetail.NewWashCushionLabelFile != dtoLabelingPackaging.AIOWashCushionLabelFile)
                                    {
                                        dtoDetail.NewWashCushionLabelFile = null;
                                    }
                                    else
                                    {
                                        fwFactory.RemoveFile(dtoDetail.WashCushionLabelFile);
                                    }
                                    if (dtoLabelingPackaging.LabelingPackagingFileMonitors.Count >= (i + 1))
                                    {
                                        dtoLabelingPackaging.LabelingPackagingFileMonitors[i].WashCushionLabelFileStatus = false;
                                    }
                                }
                                else
                                {
                                    if (dtoDetail.WashCushionLabelFile != dtoLabelingPackaging.AIOWashCushionLabelFile)
                                    {
                                        dtoDetail.WashCushionLabelFile = null;
                                    }
                                    dtoDetail.WashCushionLabelFile = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoDetail.NewWashCushionLabelFile, dtoDetail.WashCushionLabelFile, dtoDetail.WashCushionLabelFriendlyName);
                                    if (dtoLabelingPackaging.LabelingPackagingFileMonitors.Count >= (i + 1))
                                    {
                                        dtoLabelingPackaging.LabelingPackagingFileMonitors[i].WashCushionLabelFileStatus = true;
                                    }
                                }
                            }
                            // FSC Label
                            if (dtoDetail.FSCLabelFileHasChange)
                            {
                                if (string.IsNullOrEmpty(dtoDetail.NewFSCLabelFile))
                                {
                                    if (dtoDetail.NewFSCLabelFile != dtoLabelingPackaging.AIOFSCLabelFile)
                                    {
                                        dtoDetail.NewFSCLabelFile = null;
                                    }
                                    else
                                    {
                                        fwFactory.RemoveFile(dtoDetail.FSCLabelFile);
                                    }
                                }
                                else
                                {
                                    if (dtoDetail.FSCLabelFile != dtoLabelingPackaging.AIOFSCLabelFile)
                                    {
                                        dtoDetail.FSCLabelFile = null;
                                    }
                                    dtoDetail.FSCLabelFile = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoDetail.NewFSCLabelFile, dtoDetail.FSCLabelFile, dtoDetail.FSCLabelFriendlyName);
                                }
                            }

                            // Option 1
                            if (dtoDetail.Option1FileHasChange)
                            {
                                if (string.IsNullOrEmpty(dtoDetail.NewOption1File))
                                {
                                    if (dtoDetail.Option1File != dtoLabelingPackaging.AIOOption1File)
                                    {
                                        dtoDetail.Option1File = null;
                                    }
                                    else
                                    {
                                        fwFactory.RemoveFile(dtoDetail.Option1File);
                                    }
                                }
                                else
                                {
                                    if (dtoDetail.Option1File != dtoLabelingPackaging.AIOOption1File)
                                    {
                                        dtoDetail.Option1File = null;
                                    }
                                    dtoDetail.Option1File = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoDetail.NewOption1File, dtoDetail.Option1File, dtoDetail.Option1FriendlyName);
                                }
                            }

                            // Option 2
                            if (dtoDetail.Option2FileHasChange)
                            {
                                if (string.IsNullOrEmpty(dtoDetail.NewOption2File))
                                {
                                    if (dtoDetail.Option2File != dtoLabelingPackaging.AIOOption2File)
                                    {
                                        dtoDetail.Option2File = null;
                                    }
                                    else
                                    {
                                        fwFactory.RemoveFile(dtoDetail.Option2File);
                                    }
                                }
                                else
                                {
                                    if (dtoDetail.Option2File != dtoLabelingPackaging.AIOOption2File)
                                    {
                                        dtoDetail.Option2File = null;
                                    }
                                    dtoDetail.Option2File = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoDetail.NewOption2File, dtoDetail.Option2File, dtoDetail.Option2FriendlyName);
                                }
                            }

                            // Option 3
                            if (dtoDetail.Option3FileHasChange)
                            {
                                if (string.IsNullOrEmpty(dtoDetail.NewOption3File))
                                {
                                    if (dtoDetail.Option3File != dtoLabelingPackaging.AIOOption3File)
                                    {
                                        dtoDetail.Option3File = null;
                                    }
                                    else
                                    {
                                        fwFactory.RemoveFile(dtoDetail.Option3File);
                                    }
                                }
                                else
                                {
                                    if (dtoDetail.Option3File != dtoLabelingPackaging.AIOOption3File)
                                    {
                                        dtoDetail.Option3File = null;
                                    }
                                    dtoDetail.Option3File = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoDetail.NewOption3File, dtoDetail.Option3File, dtoDetail.Option3FriendlyName);
                                }
                            }

                            //Option 4
                            if (dtoDetail.Option4FileHasChange)
                            {
                                dtoDetail.Option4File = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoDetail.NewOption4File, dtoDetail.Option4File, dtoDetail.Option4FriendlyName);
                            }
                            //Option 5
                            if (dtoDetail.Option5FileHasChange)
                            {
                                dtoDetail.Option5File = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoDetail.NewOption5File, dtoDetail.Option5File, dtoDetail.Option5FriendlyName);
                            }
                            //Option 6
                            if (dtoDetail.Option6FileHasChange)
                            {
                                dtoDetail.Option6File = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoDetail.NewOption6File, dtoDetail.Option6File, dtoDetail.Option6FriendlyName);
                            }
                            //Option 7
                            if (dtoDetail.Option7FileHasChange)
                            {
                                dtoDetail.Option7File = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoDetail.NewOption7File, dtoDetail.Option7File, dtoDetail.Option7FriendlyName);
                            }
                            //Option 8
                            if (dtoDetail.Option8FileHasChange)
                            {
                                dtoDetail.Option8File = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoDetail.NewOption8File, dtoDetail.Option8File, dtoDetail.Option8FriendlyName);
                            }
                            //Option 9
                            if (dtoDetail.Option9FileHasChange)
                            {
                                dtoDetail.Option9File = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoDetail.NewOption9File, dtoDetail.Option9File, dtoDetail.Option9FriendlyName);
                            }
                            //Option 10
                            if (dtoDetail.Option10FileHasChange)
                            {
                                dtoDetail.Option10File = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoDetail.NewOption10File, dtoDetail.Option10File, dtoDetail.Option10FriendlyName);
                            }
                            //Option 11
                            if (dtoDetail.Option11FileHasChange)
                            {
                                dtoDetail.Option11File = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoDetail.NewOption11File, dtoDetail.Option11File, dtoDetail.Option11FriendlyName);
                            }
                            //Option 12
                            if (dtoDetail.Option12FileHasChange)
                            {
                                dtoDetail.Option12File = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoDetail.NewOption12File, dtoDetail.Option12File, dtoDetail.Option12FriendlyName);
                            }
                            //Option 10
                            if (dtoDetail.Option13FileHasChange)
                            {
                                dtoDetail.Option13File = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoDetail.NewOption13File, dtoDetail.Option13File, dtoDetail.Option13FriendlyName);
                            }
                            //Option 14
                            if (dtoDetail.Option14FileHasChange)
                            {
                                dtoDetail.Option14File = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoDetail.NewOption14File, dtoDetail.Option14File, dtoDetail.Option14FriendlyName);
                            }
                            //Option 15
                            if (dtoDetail.Option15FileHasChange)
                            {
                                dtoDetail.Option15File = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoDetail.NewOption15File, dtoDetail.Option15File, dtoDetail.Option15FriendlyName);
                            }

                            i++;
                        }

                        // process sparepart detail
                        foreach (DTO.LabelingPackagingSparepartDetail dtoSparepartDetail in dtoLabelingPackaging.LabelingPackagingSparepartDetails)
                        {
                            // Hangtag
                            if (dtoSparepartDetail.HangTagFileHasChange)
                            {
                                if (string.IsNullOrEmpty(dtoSparepartDetail.NewHangTagFile))
                                {
                                    if (dtoSparepartDetail.HangTagFile == dtoLabelingPackaging.AIOHangTagFile)
                                    {
                                        dtoSparepartDetail.HangTagFile = null;
                                    }
                                    else
                                    {
                                        fwFactory.RemoveImageFile(dtoSparepartDetail.HangTagFile);
                                    }
                                }
                                else
                                {
                                    if (dtoSparepartDetail.HangTagFile == dtoLabelingPackaging.AIOHangTagFile)
                                    {
                                        dtoSparepartDetail.HangTagFile = null;
                                    }
                                    dtoSparepartDetail.HangTagFile = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoSparepartDetail.NewHangTagFile, dtoSparepartDetail.HangTagFile, dtoSparepartDetail.HangTagFriendlyName);
                                }

                            }
                            // BoxShippingMark
                            if (dtoSparepartDetail.BoxShippingMarkFileHasChange)
                            {
                                if (string.IsNullOrEmpty(dtoSparepartDetail.NewBoxShippingMarkFile))
                                {
                                    if (dtoSparepartDetail.BoxShippingMarkFile == dtoLabelingPackaging.AIOBoxShippingMarkFile)
                                    {
                                        dtoSparepartDetail.BoxShippingMarkFile = null;
                                    }
                                    else
                                    {
                                        fwFactory.RemoveImageFile(dtoSparepartDetail.BoxShippingMarkFile);
                                    }
                                }
                                else
                                {
                                    if (dtoSparepartDetail.BoxShippingMarkFile == dtoLabelingPackaging.AIOBoxShippingMarkFile)
                                    {
                                        dtoSparepartDetail.BoxShippingMarkFile = null;
                                    }
                                    dtoSparepartDetail.BoxShippingMarkFile = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoSparepartDetail.NewBoxShippingMarkFile, dtoSparepartDetail.BoxShippingMarkFile, dtoSparepartDetail.BoxShippingMarkFriendlyName);
                                }
                            }
                            // BrassLabel
                            if (dtoSparepartDetail.BrassLabelFileHasChange)
                            {
                                if (string.IsNullOrEmpty(dtoSparepartDetail.NewBrassLabelFile))
                                {
                                    if (dtoSparepartDetail.BrassLabelFile == dtoLabelingPackaging.AIOBrassLabelFile)
                                    {
                                        dtoSparepartDetail.BrassLabelFile = null;
                                    }
                                    else
                                    {
                                        fwFactory.RemoveImageFile(dtoSparepartDetail.BrassLabelFile);
                                    }
                                }
                                else
                                {
                                    if (dtoSparepartDetail.BrassLabelFile == dtoLabelingPackaging.AIOBrassLabelFile)
                                    {
                                        dtoSparepartDetail.BrassLabelFile = null;
                                    }
                                    dtoSparepartDetail.BrassLabelFile = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoSparepartDetail.NewBrassLabelFile, dtoSparepartDetail.BrassLabelFile, dtoSparepartDetail.BrassLabelFriendlyName);
                                }
                            }
                            // AI
                            if (dtoSparepartDetail.AIFileHasChange)
                            {
                                if (string.IsNullOrEmpty(dtoSparepartDetail.NewAIFile))
                                {
                                    if (dtoSparepartDetail.AIFile != dtoLabelingPackaging.AIOAIFile)
                                    {
                                        dtoSparepartDetail.AIFile = null;
                                    }
                                    else
                                    {
                                        fwFactory.RemoveImageFile(dtoSparepartDetail.AIFile);
                                    }
                                }
                                else
                                {
                                    if (dtoSparepartDetail.AIFile != dtoLabelingPackaging.AIOAIFile)
                                    {
                                        dtoSparepartDetail.AIFile = null;
                                    }
                                    dtoSparepartDetail.AIFile = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoSparepartDetail.NewAIFile, dtoSparepartDetail.AIFile, dtoSparepartDetail.AIFriendlyName);
                                }
                            }
                            // WashCushionLabel
                            if (dtoSparepartDetail.WashCushionLabelFileHasChange)
                            {
                                if (string.IsNullOrEmpty(dtoSparepartDetail.NewWashCushionLabelFile))
                                {
                                    if (dtoSparepartDetail.NewWashCushionLabelFile != dtoLabelingPackaging.AIOWashCushionLabelFile)
                                    {
                                        dtoSparepartDetail.NewWashCushionLabelFile = null;
                                    }
                                    else
                                    {
                                        fwFactory.RemoveFile(dtoSparepartDetail.WashCushionLabelFile);
                                    }
                                }
                                else
                                {
                                    if (dtoSparepartDetail.WashCushionLabelFile != dtoLabelingPackaging.AIOWashCushionLabelFile)
                                    {
                                        dtoSparepartDetail.WashCushionLabelFile = null;
                                    }
                                    dtoSparepartDetail.WashCushionLabelFile = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoSparepartDetail.NewWashCushionLabelFile, dtoSparepartDetail.WashCushionLabelFile, dtoSparepartDetail.WashCushionLabelFriendlyName);
                                }
                            }

                            // Option 1
                            if (dtoSparepartDetail.Option1FileHasChange)
                            {
                                if (string.IsNullOrEmpty(dtoSparepartDetail.NewOption1File))
                                {
                                    if (dtoSparepartDetail.Option1File != dtoLabelingPackaging.AIOOption1File)
                                    {
                                        dtoSparepartDetail.Option1File = null;
                                    }
                                    else
                                    {
                                        fwFactory.RemoveFile(dtoSparepartDetail.Option1File);
                                    }
                                }
                                else
                                {
                                    if (dtoSparepartDetail.Option1File != dtoLabelingPackaging.AIOOption1File)
                                    {
                                        dtoSparepartDetail.Option1File = null;
                                    }
                                    dtoSparepartDetail.Option1File = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoSparepartDetail.NewOption1File, dtoSparepartDetail.Option1File, dtoSparepartDetail.Option1FriendlyName);
                                }
                            }

                            // Option 2
                            if (dtoSparepartDetail.Option2FileHasChange)
                            {
                                if (string.IsNullOrEmpty(dtoSparepartDetail.NewOption2File))
                                {
                                    if (dtoSparepartDetail.Option2File != dtoLabelingPackaging.AIOOption2File)
                                    {
                                        dtoSparepartDetail.Option2File = null;
                                    }
                                    else
                                    {
                                        fwFactory.RemoveFile(dtoSparepartDetail.Option2File);
                                    }
                                }
                                else
                                {
                                    if (dtoSparepartDetail.Option2File != dtoLabelingPackaging.AIOOption2File)
                                    {
                                        dtoSparepartDetail.Option2File = null;
                                    }
                                    dtoSparepartDetail.Option2File = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoSparepartDetail.NewOption2File, dtoSparepartDetail.Option2File, dtoSparepartDetail.Option2FriendlyName);
                                }
                            }

                            // Option 3
                            if (dtoSparepartDetail.Option3FileHasChange)
                            {
                                if (string.IsNullOrEmpty(dtoSparepartDetail.NewOption3File))
                                {
                                    if (dtoSparepartDetail.Option3File != dtoLabelingPackaging.AIOOption3File)
                                    {
                                        dtoSparepartDetail.Option3File = null;
                                    }
                                    else
                                    {
                                        fwFactory.RemoveFile(dtoSparepartDetail.Option3File);
                                    }
                                }
                                else
                                {
                                    if (dtoSparepartDetail.Option3File != dtoLabelingPackaging.AIOOption3File)
                                    {
                                        dtoSparepartDetail.Option3File = null;
                                    }
                                    dtoSparepartDetail.Option3File = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoSparepartDetail.NewOption3File, dtoSparepartDetail.Option3File, dtoSparepartDetail.Option3FriendlyName);
                                }
                            }

                            //Option 4
                            if (dtoSparepartDetail.Option4FileHasChange)
                            {
                                dtoSparepartDetail.Option4File = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoSparepartDetail.NewOption4File, dtoSparepartDetail.Option4File, dtoSparepartDetail.Option4FriendlyName);
                            }
                            //Option 5
                            if (dtoSparepartDetail.Option5FileHasChange)
                            {
                                dtoSparepartDetail.Option5File = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoSparepartDetail.NewOption5File, dtoSparepartDetail.Option5File, dtoSparepartDetail.Option5FriendlyName);
                            }
                            //Option 6
                            if (dtoSparepartDetail.Option6FileHasChange)
                            {
                                dtoSparepartDetail.Option6File = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoSparepartDetail.NewOption6File, dtoSparepartDetail.Option6File, dtoSparepartDetail.Option6FriendlyName);
                            }
                            //Option 7
                            if (dtoSparepartDetail.Option7FileHasChange)
                            {
                                dtoSparepartDetail.Option7File = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoSparepartDetail.NewOption7File, dtoSparepartDetail.Option7File, dtoSparepartDetail.Option7FriendlyName);
                            }
                            //Option 8
                            if (dtoSparepartDetail.Option8FileHasChange)
                            {
                                dtoSparepartDetail.Option8File = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoSparepartDetail.NewOption8File, dtoSparepartDetail.Option8File, dtoSparepartDetail.Option8FriendlyName);
                            }
                            //Option 9
                            if (dtoSparepartDetail.Option9FileHasChange)
                            {
                                dtoSparepartDetail.Option9File = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoSparepartDetail.NewOption9File, dtoSparepartDetail.Option9File, dtoSparepartDetail.Option9FriendlyName);
                            }
                            //Option 10
                            if (dtoSparepartDetail.Option10FileHasChange)
                            {
                                dtoSparepartDetail.Option10File = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoSparepartDetail.NewOption10File, dtoSparepartDetail.Option10File, dtoSparepartDetail.Option10FriendlyName);
                            }
                            //Option 11
                            if (dtoSparepartDetail.Option11FileHasChange)
                            {
                                dtoSparepartDetail.Option11File = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoSparepartDetail.NewOption11File, dtoSparepartDetail.Option11File, dtoSparepartDetail.Option11FriendlyName);
                            }
                            //Option 12
                            if (dtoSparepartDetail.Option12FileHasChange)
                            {
                                dtoSparepartDetail.Option12File = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoSparepartDetail.NewOption12File, dtoSparepartDetail.Option12File, dtoSparepartDetail.Option12FriendlyName);
                            }
                            //Option 10
                            if (dtoSparepartDetail.Option13FileHasChange)
                            {
                                dtoSparepartDetail.Option13File = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoSparepartDetail.NewOption13File, dtoSparepartDetail.Option13File, dtoSparepartDetail.Option13FriendlyName);
                            }
                            //Option 14
                            if (dtoSparepartDetail.Option14FileHasChange)
                            {
                                dtoSparepartDetail.Option14File = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoSparepartDetail.NewOption14File, dtoSparepartDetail.Option14File, dtoSparepartDetail.Option14FriendlyName);
                            }
                            //Option 15
                            if (dtoSparepartDetail.Option15FileHasChange)
                            {
                                dtoSparepartDetail.Option15File = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoSparepartDetail.NewOption15File, dtoSparepartDetail.Option15File, dtoSparepartDetail.Option15FriendlyName);
                            }

                        }

                        // process remark
                        foreach (DTO.LabelingPackagingRemark dtoRemark in dtoLabelingPackaging.LabelingPackagingRemarks)
                        {
                            if (dtoRemark.RemarkFileHasChange)
                            {
                                if (string.IsNullOrEmpty(dtoRemark.NewRemarkFile))
                                {
                                    fwFactory.RemoveFile(dtoRemark.RemarkFile);
                                }
                                else
                                {
                                    dtoRemark.RemarkFile = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoRemark.NewRemarkFile, dtoRemark.RemarkFile, dtoRemark.RemarkFriendlyName);
                                }
                            }
                        }

                        // process other file
                        foreach (DTO.LabelingPackagingOtherFile dtoOtherFile in dtoLabelingPackaging.LabelingPackagingOtherFiles)
                        {
                            if (dtoOtherFile.OtherFileHasChange)
                            {
                                if (string.IsNullOrEmpty(dtoOtherFile.NewOtherFile))
                                {
                                    fwFactory.RemoveFile(dtoOtherFile.OtherFile);
                                }
                                else
                                {
                                    dtoOtherFile.OtherFile = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoOtherFile.NewOtherFile, dtoOtherFile.OtherFile, dtoOtherFile.OtherFileFriendlyName);
                                }
                            }
                        }

                        // process rejection
                        foreach (var item in dtoLabelingPackaging.LabelingPackagingRejections)
                        {
                            if (item.LabelingPackagingRejectionFileHasChange)
                            {
                                if (string.IsNullOrEmpty(item.LabelingPackagingRejectionFileNew))
                                {
                                    fwFactory.RemoveFile(item.LabelingPackagingRejectionCommentFile);
                                }
                                else
                                {
                                    item.LabelingPackagingRejectionCommentFile = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", item.LabelingPackagingRejectionFileNew, item.LabelingPackagingRejectionCommentFile, item.LabelingPackagingRejectionFriendlyName);
                                }
                            }
                        }

                        //convert dto to db
                        converter.DTO2DB_LabelingPackaging(dtoLabelingPackaging, ref dbItem);

                        //remove orphan item
                        context.LabelingPackagingDetail.Local.Where(o => o.LabelingPackaging == null).ToList().ForEach(o => context.LabelingPackagingDetail.Remove(o));
                        context.LabelingPackagingRemark.Local.Where(o => o.LabelingPackaging == null).ToList().ForEach(o => context.LabelingPackagingRemark.Remove(o));
                        context.LabelingPackagingOtherFile.Local.Where(o => o.LabelingPackaging == null).ToList().ForEach(o => context.LabelingPackagingOtherFile.Remove(o));
                        context.LabelingPackagingRejection.Local.Where(o => o.LabelingPackaging == null).ToList().ForEach(o => context.LabelingPackagingRejection.Remove(o));

                        // Send email notification
                        //DTO.LabelingPackaging dtoResult = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.LabelingPackaging>();
                        string emailSubject = "";
                        string emailBody = "";
                        string url = "";
                        string url_approval = "";

                        url = this.frontendURL + "labelingpackaging/Edit/" + dtoLabelingPackaging.LabelingPackagingID.ToString();
                        url_approval = this.frontendURL + "LPOverview/Edit/" + dtoLabelingPackaging.LabelingPackagingID.ToString();

                        switch (dbItem.LPStatusID)
                        {
                            case 3: // Draft
                                emailSubject += "LP Client:" + "[" + dtoLabelingPackaging.ClientUD + "]" + "[" + dtoLabelingPackaging.ProformaInvoiceNo + "]" + "[" + dtoLabelingPackaging.FactoryUD + "]" + "[" + dtoLabelingPackaging.LPStatusNM + "]";
                                emailBody += "NL ADDED LP INFO FROM CLIENT; VN PLEASE MAKE DRAFT <br/>";
                                emailBody += Environment.NewLine + "Click <a href='" + url + "'>here</a> to check or copy the following url and paste to your browser: " + url;
                                SendNotification(dtoLabelingPackaging.FactoryID, emailSubject, emailBody);
                                break;

                            case 5: // Revision -> Rejection change with new issue 1218
                                emailSubject += "LP Client:" + "[" + dtoLabelingPackaging.ClientUD + "]" + "[" + dtoLabelingPackaging.ProformaInvoiceNo + "]" + "[" + dtoLabelingPackaging.FactoryUD + "]" + "[" + dtoLabelingPackaging.LPStatusNM + "]";
                                //emailBody += "DRAFT HAS BEEN REJECTED BECAUSE " + dtoLabelingPackaging.RejectComment + "<br/>";
                                emailBody += "NL REJECTED DRAFT; VN PLEASE NOTE REJECTION REASON AND MAKE NEW DRAFT <br/>";
                                emailBody += Environment.NewLine + "Click <a href='" + url + "'>here</a> to check or copy the following url and paste to your browser: " + url;
                                SendNotification(dtoLabelingPackaging.FactoryID, emailSubject, emailBody);
                                break;

                            case 6: // Approved
                                emailSubject += "LP Client:" + "[" + dtoLabelingPackaging.ClientUD + "]" + "[" + dtoLabelingPackaging.ProformaInvoiceNo + "]" + "[" + dtoLabelingPackaging.FactoryUD + "]" + "[" + dtoLabelingPackaging.LPStatusNM + "]";
                                emailBody += "DRAFT HAS BEEN APPROVED BY CLIENT; VN PLEASE PROCEED <br/>";
                                emailBody += Environment.NewLine + "Click <a href='" + url_approval + "'>here</a> to check or copy the following url and paste to your browser: " + url;
                                SendNotification(dtoLabelingPackaging.FactoryID, emailSubject, emailBody);
                                break;

                            case 4: // Confirmation
                                emailSubject += "LP Client:" + "[" + dtoLabelingPackaging.ClientUD + "]" + "[" + dtoLabelingPackaging.ProformaInvoiceNo + "]" + "[" + dtoLabelingPackaging.FactoryUD + "]" + "[" + dtoLabelingPackaging.LPStatusNM + "]";
                                emailBody += "VN ADDED DRAFT; NL PLEASE CHECK WITH CLIENT FOR CONFIRMATION <br/>";
                                emailBody += Environment.NewLine + "Click <a href='" + url + "'>here</a> to check or copy the following url and paste to your browser: " + url;
                                SendNotification(dtoLabelingPackaging.FactoryID, emailSubject, emailBody);
                                break;

                            case 2: // Awaiting for
                                emailSubject += "LP Client:" + "[" + dtoLabelingPackaging.ClientUD + "]" + "[" + dtoLabelingPackaging.ProformaInvoiceNo + "]" + "[" + dtoLabelingPackaging.FactoryUD + "]" + "[" + dtoLabelingPackaging.LPStatusNM + "]";
                                emailBody += "WAITING FOR LP INFO FROM CLIENT <br/>";
                                emailBody += Environment.NewLine + "Click <a href='" + url + "'>here</a> to check or copy the following url and paste to your browser: " + url;
                                SendNotification(dtoLabelingPackaging.FactoryID, emailSubject, emailBody);
                                break;

                            case 8: // Awaiting Approval
                                emailSubject += "LP Client:" + "[" + dtoLabelingPackaging.ClientUD + "]" + "[" + dtoLabelingPackaging.ProformaInvoiceNo + "]" + "[" + dtoLabelingPackaging.FactoryUD + "]" + "[" + dtoLabelingPackaging.LPStatusNM + "]";
                                emailBody += "NL SENT DRAFT TO CLIENT; NL WAITING FOR CLIENT CONFIRMATION <br/>";
                                emailBody += Environment.NewLine + "Click <a href='" + url + "'>here</a> to check or copy the following url and paste to your browser: " + url;
                                SendNotification(dtoLabelingPackaging.FactoryID, emailSubject, emailBody);
                                break;

                            default:
                                //Do the Default
                                break;
                        }

                        // Update Approved status
                        switch (dbItem.LPStatusID)
                        {
                            case 6:
                                dbItem.ApprovedBy = userId;
                                dbItem.ApprovedDate = DateTime.Now;
                                break;
                            // Update status revision -> change to status to Draft
                            case 5:
                                dbItem.LPStatusID = 3;
                                dbItem.RejectComment = null;
                                break;

                            default:
                                //Do the Default
                                break;
                        }

                        context.SaveChanges();

                        //auto add image qua các item khác
                        if (dtoLabelingPackaging.IsSaveConfig == true && dtoLabelingPackaging.LPStatusID == 6)
                        {
                            context.LabelingPackagingMng_function_AutoAddFileWhenUserConfig(dbItem.LabelingPackagingID);

                            context.SaveChanges();
                        }
                        //get return data
                        dtoItem = GetData(userId, dbItem.LabelingPackagingID, out notification).Data;

                        return true;
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
                return false;
            }
        }

        public DTO.EditFormData GetData(int userId, int id, out Library.DTO.Notification notification)
        {
            DTO.EditFormData editFormData = new DTO.EditFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            Module.Support.DAL.DataFactory support_factory = new Support.DAL.DataFactory();
            Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
            editFormData.LPStatuses = new List<Module.Support.DTO.LabelingPackagingStatus>();
            //Details
            editFormData.Data = new DTO.LabelingPackaging();
            editFormData.Data.LabelingPackagingDetails = new List<DTO.LabelingPackagingDetail>();
            editFormData.Data.LabelingPackagingSparepartDetails = new List<DTO.LabelingPackagingSparepartDetail>();
            editFormData.Data.LabelingPackagingRemarks = new List<DTO.LabelingPackagingRemark>();
            editFormData.Data.LabelingPackagingOtherFiles = new List<DTO.LabelingPackagingOtherFile>();
            editFormData.Data.LabelingPackagingRejections = new List<DTO.LabelingPackagingRejection>();
            editFormData.Data.LabelingPackagingFileMonitors = new List<DTO.LabelingPackagingFileMonitor>();

            editFormData.tmpDTODetails = new List<DTO.ApprovedDetail>();
            editFormData.tmpDTOSparepartDetails = new List<DTO.ApprovedSparepartDetail>();
            editFormData.options = getOptions();

            try
            {
                using (LabelingPackagingEntities context = CreateContext())
                {
                    // Insert order detail to labeling packaging detail
                    var dataItemByClient = context.LabelingPackagingMng_function_InsertFactoryOrderDetail();
                }
            }
            catch { }

            try
            {
                using (LabelingPackagingEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        LabelingPackagingMng_LabelingPackaging_View dbItem;
                        dbItem = context.LabelingPackagingMng_LabelingPackaging_View.FirstOrDefault(o => o.LabelingPackagingID == id);

                        //check permission on factory
                        int FactoryID = Convert.ToInt32(dbItem.FactoryID);
                        if (fwFactory.CheckFactoryPermission(userId, FactoryID) == 0)
                        {
                            throw new Exception("You do not have access permission on this factory");
                        }

                        editFormData.Data = converter.DB2DTO_LabelingPackaging(dbItem);

                        // Map fileUD to similar Item detail
                        if (editFormData.Data.LPStatusID == 1 && id <= 0)
                        {
                            // Get existing data by client and map it to new LP
                            //DTO.LabelingPackaging dbItemByClient = new DTO.LabelingPackaging();
                            var dbItemByClient = context.LabelingPackagingMng_LabelingPackaging_View.OrderByDescending(o => o.ApprovedDate).FirstOrDefault(o => o.ClientUD == editFormData.Data.ClientUD && o.LPStatusID == 6 && o.ApprovedDate.HasValue);
                            if (dbItemByClient != null)
                            {
                                editFormData.Data.HangTagStatusID = dbItemByClient.HangTagStatusID;
                                editFormData.Data.BoxShippingMarkStatusID = dbItemByClient.BoxShippingMarkStatusID;
                                editFormData.Data.BrassLabelStatusID = dbItemByClient.BrassLabelStatusID;
                                editFormData.Data.AIStatusID = dbItemByClient.AIStatusID;
                                editFormData.Data.WashCushionLabelID = dbItemByClient.WashCushionLabelID;
                            }

                            // Order detail
                            List<string> articleCodeApproval = editFormData.Data.LabelingPackagingDetails.Select(o => o.ArticleCode).ToList();
                            var dbArticleCodeApproval = context.LabelingPackagingMng_LabelingPackaging_ApprovedOrderDetail.Where(o => articleCodeApproval.Contains(o.ArticleCode)).ToList();
                            var dtoArticleCodeApproval = converter.DB2DTO_ApprovedDetailList(dbArticleCodeApproval);
                            foreach (DTO.LabelingPackagingDetail dtoDetail in editFormData.Data.LabelingPackagingDetails)
                            {
                                //DTO.ApprovedDetail tmpDbDetail = new DTO.ApprovedDetail();
                                var tmpDbDetail = dtoArticleCodeApproval.Where(o => o.ArticleCode == dtoDetail.ArticleCode).FirstOrDefault();
                                if (tmpDbDetail != null)
                                {
                                    //HangTagFile
                                    if (!string.IsNullOrEmpty(tmpDbDetail.HangTagFile))
                                    {
                                        dtoDetail.HangTagFile = tmpDbDetail.HangTagFile;
                                        dtoDetail.HangTagFileUrl = tmpDbDetail.HangTagFileUrl;
                                        dtoDetail.HangTagFriendlyName = tmpDbDetail.HangTagFriendlyName;
                                    }
                                    //BoxShippingMarkFile
                                    if (!string.IsNullOrEmpty(tmpDbDetail.BoxShippingMarkFile))
                                    {
                                        dtoDetail.BoxShippingMarkFile = tmpDbDetail.BoxShippingMarkFile;
                                        dtoDetail.BoxShippingMarkFileUrl = tmpDbDetail.BoxShippingMarkFileUrl;
                                        dtoDetail.BoxShippingMarkFriendlyName = tmpDbDetail.BoxShippingMarkFriendlyName;
                                    }
                                    //BrassLabelFile
                                    if (!string.IsNullOrEmpty(tmpDbDetail.BrassLabelFile))
                                    {
                                        dtoDetail.BrassLabelFile = tmpDbDetail.BrassLabelFile;
                                        dtoDetail.BrassLabelFileUrl = tmpDbDetail.BrassLabelFileUrl;
                                        dtoDetail.BrassLabelFriendlyName = tmpDbDetail.BrassLabelFriendlyName;
                                    }
                                    //AIFile
                                    if (!string.IsNullOrEmpty(tmpDbDetail.AIFile))
                                    {
                                        dtoDetail.AIFile = tmpDbDetail.AIFile;
                                        dtoDetail.AIFileUrl = tmpDbDetail.AIFileUrl;
                                        dtoDetail.AIFriendlyName = tmpDbDetail.AIFriendlyName;
                                    }
                                    //WashCushionLabel
                                    if (!string.IsNullOrEmpty(tmpDbDetail.WashCushionLabelFile))
                                    {
                                        dtoDetail.WashCushionLabelFile = tmpDbDetail.WashCushionLabelFile;
                                        dtoDetail.WashCushionLabelFileUrl = tmpDbDetail.WashCushionLabelFileUrl;
                                        dtoDetail.WashCushionLabelFriendlyName = tmpDbDetail.WashCushionLabelFriendlyName;
                                    }
                                }
                            }

                            // Sparepart detail
                            List<string> sparepartApproval = editFormData.Data.LabelingPackagingSparepartDetails.Select(o => o.ArticleCode).ToList();
                            var dbSparepartApproval = context.LabelingPackagingMng_LabelingPackaging_ApprovedOrderSparepartDetail.Where(o => sparepartApproval.Contains(o.ArticleCode)).ToList();
                            var dtoSparepartApproval = converter.DB2DTO_ApprovedSparepartDetailList(dbSparepartApproval);
                            foreach (DTO.LabelingPackagingSparepartDetail dtoSparepartDetail in editFormData.Data.LabelingPackagingSparepartDetails)
                            {
                                var tmpDbSparepartDetail = dtoSparepartApproval.Where(o => o.ArticleCode == dtoSparepartDetail.ArticleCode).FirstOrDefault();
                                if (tmpDbSparepartDetail != null)
                                {
                                    //HangTagFile
                                    if (!string.IsNullOrEmpty(tmpDbSparepartDetail.HangTagFile))
                                    {
                                        dtoSparepartDetail.HangTagFile = tmpDbSparepartDetail.HangTagFile;
                                        dtoSparepartDetail.HangTagFileUrl = tmpDbSparepartDetail.HangTagFileUrl;
                                        dtoSparepartDetail.HangTagFriendlyName = tmpDbSparepartDetail.HangTagFriendlyName;
                                    }
                                    //BoxShippingMarkFile
                                    if (!string.IsNullOrEmpty(tmpDbSparepartDetail.BoxShippingMarkFile))
                                    {
                                        dtoSparepartDetail.BoxShippingMarkFile = tmpDbSparepartDetail.BoxShippingMarkFile;
                                        dtoSparepartDetail.BoxShippingMarkFileUrl = tmpDbSparepartDetail.BoxShippingMarkFileUrl;
                                        dtoSparepartDetail.BoxShippingMarkFriendlyName = tmpDbSparepartDetail.BoxShippingMarkFriendlyName;
                                    }
                                    //BrassLabelFile
                                    if (!string.IsNullOrEmpty(tmpDbSparepartDetail.BrassLabelFile))
                                    {
                                        dtoSparepartDetail.BrassLabelFile = tmpDbSparepartDetail.BrassLabelFile;
                                        dtoSparepartDetail.BrassLabelFileUrl = tmpDbSparepartDetail.BrassLabelFileUrl;
                                        dtoSparepartDetail.BrassLabelFriendlyName = tmpDbSparepartDetail.BrassLabelFriendlyName;
                                    }
                                    //AIFile
                                    if (!string.IsNullOrEmpty(tmpDbSparepartDetail.AIFile))
                                    {
                                        dtoSparepartDetail.AIFile = tmpDbSparepartDetail.AIFile;
                                        dtoSparepartDetail.AIFileUrl = tmpDbSparepartDetail.AIFileUrl;
                                        dtoSparepartDetail.AIFriendlyName = tmpDbSparepartDetail.AIFriendlyName;
                                    }
                                    //WashCushionLabel
                                    if (!string.IsNullOrEmpty(tmpDbSparepartDetail.WashCushionLabelFile))
                                    {
                                        dtoSparepartDetail.WashCushionLabelFile = tmpDbSparepartDetail.WashCushionLabelFile;
                                        dtoSparepartDetail.WashCushionLabelFileUrl = tmpDbSparepartDetail.WashCushionLabelFileUrl;
                                        dtoSparepartDetail.WashCushionLabelFriendlyName = tmpDbSparepartDetail.WashCushionLabelFriendlyName;
                                    }
                                }
                            }
                        }
                        // Mark status new with new order
                        if (editFormData.Data.LPStatusID == null)
                        {
                            editFormData.Data.LPStatusID = 1;
                        }
                    }
                    else
                    {
                        editFormData.Data = new DTO.LabelingPackaging();
                        editFormData.LPStatuses = new List<Module.Support.DTO.LabelingPackagingStatus>();
                        editFormData.Data.LabelingPackagingDetails = new List<DTO.LabelingPackagingDetail>();
                        editFormData.Data.LabelingPackagingSparepartDetails = new List<DTO.LabelingPackagingSparepartDetail>();
                        editFormData.Data.LabelingPackagingRemarks = new List<DTO.LabelingPackagingRemark>();
                        editFormData.Data.LabelingPackagingOtherFiles = new List<DTO.LabelingPackagingOtherFile>();
                        editFormData.Data.LabelingPackagingFileMonitors = new List<DTO.LabelingPackagingFileMonitor>();
                    }
                    //editFormData.Factories = support_factory.GetFactory(userId);
                    editFormData.LPStatuses = support_factory.GetLabelingPackagingStatus().ToList();
                    return editFormData;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
            }

            return editFormData;
        }

        public DTO.SearchFormData GetDataWithFilter(int userID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.SearchFormData searchFormData = new DTO.SearchFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            int? UserID = userID;
            string Season = null;
            int? SaleID = null;
            string ProformaInvoiceNo = null;
            string ClientUD = null;
            string FactoryUD = null;
            int? LPStatusID = null;
            string productionStatus = null;

            if (filters.ContainsKey("Season") && !string.IsNullOrEmpty(filters["Season"].ToString()))
            {
                Season = filters["Season"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("SaleID") && !string.IsNullOrEmpty(filters["SaleID"].ToString()))
            {
                SaleID = Convert.ToInt32(filters["SaleID"].ToString());
            }
            if (filters.ContainsKey("ProformaInvoiceNo") && !string.IsNullOrEmpty(filters["ProformaInvoiceNo"].ToString()))
            {
                ProformaInvoiceNo = filters["ProformaInvoiceNo"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("ClientUD") && !string.IsNullOrEmpty(filters["ClientUD"].ToString()))
            {
                ClientUD = filters["ClientUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("FactoryUD") && !string.IsNullOrEmpty(filters["FactoryUD"].ToString()))
            {
                FactoryUD = filters["FactoryUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("LPStatusID") && !string.IsNullOrEmpty(filters["LPStatusID"].ToString()))
            {
                LPStatusID = Convert.ToInt32(filters["LPStatusID"].ToString());
            }

            if (filters.ContainsKey("productionStatus") && !string.IsNullOrEmpty(filters["productionStatus"].ToString()))
            {
                productionStatus = filters["productionStatus"].ToString().Replace("'", "''");
            }

            try
            {
                using (LabelingPackagingEntities context = CreateContext())
                {
                    totalRows = context.LabelingPackagingMng_function_SearchLabelingPackaging(orderBy, orderDirection, UserID, Season, SaleID, ProformaInvoiceNo, FactoryUD, ClientUD, LPStatusID, productionStatus).Count();
                    var result = context.LabelingPackagingMng_function_SearchLabelingPackaging(orderBy, orderDirection, UserID, Season, SaleID, ProformaInvoiceNo, FactoryUD, ClientUD, LPStatusID, productionStatus);
                    searchFormData.Data = converter.DB2DTO_LabelingPackagingSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }

                return searchFormData;
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
                return searchFormData;
            }
        }

        public DTO.SearchFilterData GetSearchFilter(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFilterData data = new DTO.SearchFilterData();
            data.Seasons = new List<Support.DTO.Season>();
            data.Statuses = new List<Support.DTO.LabelingPackagingStatus>();

            try
            {
                data.Seasons = supportFactory.GetSeason();
                data.Statuses = supportFactory.GetLabelingPackagingStatus();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public string GetExcelData(int labelingPackagingID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject2 ds = new ReportDataObject2();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("LabelingPackagingMng_function_GetExcelData", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@LabelingPackagingID", labelingPackagingID);

                adap.TableMappings.Add("Table", "LabelingPackaging");
                adap.TableMappings.Add("Table1", "LabelingPackagingDetail");
                adap.TableMappings.Add("Table2", "LabelingPackagingOtherFile");
                adap.Fill(ds);

                return Library.Helper.CreateReportFileWithEPPlus2(ds, "LabelingPackagingOverview");
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

        // Get overview excel report
        public string GetExcelReportData(int userID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();
            try
            {
                int? UserID = userID;
                string Season = null;
                string ProformaInvoiceNo = null;
                string ClientUD = null;
                string FactoryUD = null;
                int? LPStatusID = null;
                string SortedBy = null;
                string SortedDirection = null;


                if (filters.ContainsKey("Season") && !string.IsNullOrEmpty(filters["Season"].ToString()))
                {
                    Season = filters["Season"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("ProformaInvoiceNo") && !string.IsNullOrEmpty(filters["ProformaInvoiceNo"].ToString()))
                {
                    ProformaInvoiceNo = filters["ProformaInvoiceNo"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("ClientUD") && !string.IsNullOrEmpty(filters["ClientUD"].ToString()))
                {
                    ClientUD = filters["ClientUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("FactoryUD") && !string.IsNullOrEmpty(filters["FactoryUD"].ToString()))
                {
                    FactoryUD = filters["FactoryUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("LPStatusID") && !string.IsNullOrEmpty(filters["LPStatusID"].ToString()))
                {
                    LPStatusID = Convert.ToInt32(filters["LPStatusID"].ToString());
                }

                if (filters.ContainsKey("SortedBy") && !string.IsNullOrEmpty(filters["SortedBy"].ToString()))
                {
                    SortedBy = filters["SortedBy"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("SortedDirection") && !string.IsNullOrEmpty(filters["SortedDirection"].ToString()))
                {
                    SortedDirection = filters["SortedDirection"].ToString().Replace("'", "''");
                }

                if (SortedBy == "LDS")
                {
                    SortedBy = "LDS1";
                }

                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("LabelingPackagingMng_function_ReportExcel", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@UserID", UserID);
                adap.SelectCommand.Parameters.AddWithValue("@Season", Season);
                adap.SelectCommand.Parameters.AddWithValue("@ProformaInvoiceNo", ProformaInvoiceNo);
                adap.SelectCommand.Parameters.AddWithValue("@ClientUD", ClientUD);
                adap.SelectCommand.Parameters.AddWithValue("@FactoryUD", FactoryUD);
                adap.SelectCommand.Parameters.AddWithValue("@LPStatusID", LPStatusID);
                adap.SelectCommand.Parameters.AddWithValue("@SortedBy", SortedBy);
                adap.SelectCommand.Parameters.AddWithValue("@SortedDirection", SortedDirection);
                adap.Fill(ds.LabelingPackagingMng_function_ReportExcel);

                return Library.Helper.CreateReportFileWithEPPlus(ds, "LPRequestRpt");
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

        //Send Notification
        private void SendNotification(int? factoryID, string emailSubject, string emailBody)
        {
            try
            {
                List<string> emailAddress = new List<string>();

                using (LabelingPackagingEntities context = CreateContext())
                {
                    // Factory responsible email
                    var dbEmployeeList = context.LabelingPackagingMng_EmployeeEmail_View.Where(o => o.FactoryID == factoryID);
                    var dbEmployeeIDs = context.LabelingPackagingMng_EmployeeEmail_View.Where(o => o.FactoryID == factoryID).Select(o => o.UserID).ToList();
                    foreach (var dbEmployee in dbEmployeeList.ToArray())
                    {
                        if (!string.IsNullOrEmpty(dbEmployee.Email))
                        {
                            emailAddress.Add(dbEmployee.Email);
                        }
                        // add to NotificationMessage table
                        NotificationMessage notification1 = new NotificationMessage();
                        notification1.UserID = dbEmployee.UserID;
                        notification1.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_SALES;
                        notification1.NotificationMessageTitle = emailSubject;
                        notification1.NotificationMessageContent = emailBody;
                        context.NotificationMessage.Add(notification1);
                    }
                    // Custome email group
                    var dbNotificationEmails = context.SupportMng_NotificationGroup_View.Where(o => o.NotificationGroupUD == "LPStatus" /*&& dbEmployeeIDs.ToList().Contains(o.UserID)*/);
                    foreach (var dbNotificationEmail in dbNotificationEmails.ToArray())
                    {
                        if (!string.IsNullOrEmpty(dbNotificationEmail.Email1) && !emailAddress.Contains(dbNotificationEmail.Email1))
                        {
                            emailAddress.Add(dbNotificationEmail.Email1);
                        }
                        // add to NotificationMessage table
                        NotificationMessage notification1 = new NotificationMessage();
                        notification1.UserID = dbNotificationEmail.UserID;
                        notification1.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_SALES;
                        notification1.NotificationMessageTitle = emailSubject;
                        notification1.NotificationMessageContent = emailBody;
                        context.NotificationMessage.Add(notification1);
                    }

                    string sendToEmail = string.Empty;
                    foreach (string eAddress in emailAddress)
                    {
                        if (string.IsNullOrEmpty(sendToEmail))
                        {
                            sendToEmail += eAddress;
                        }
                        else
                        {
                            sendToEmail += ";" + eAddress;
                        }
                    }
                    EmailNotificationMessage dbEmail = new EmailNotificationMessage();
                    dbEmail.EmailBody = emailBody;
                    dbEmail.EmailSubject = emailSubject;
                    dbEmail.SendTo = sendToEmail;
                    context.EmailNotificationMessage.Add(dbEmail);
                    context.SaveChanges();
                }

            }
            catch { }
        }

        public bool ClearExistingFile(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.LabelingPackaging> dtoData = new List<DTO.LabelingPackaging>();
            try
            {
                using (LabelingPackagingEntities context = CreateContext())
                {
                    var result = context.LabelingPackagingMng_LabelingPackaging_View.ToList();
                    dtoData = converter.DB2DTO_LabelingPackagingList(result);
                    foreach (DTO.LabelingPackaging data in dtoData)
                    {
                        // Remove HangTagFile
                        if (!string.IsNullOrEmpty(data.OtherFile))
                        {
                            data.OtherFile = null;
                            fwFactory.RemoveImageFile(data.OtherFile);
                        }

                        // Detail
                        foreach (DTO.LabelingPackagingDetail item in data.LabelingPackagingDetails)
                        {
                            // Remove HangTagFile
                            if (!string.IsNullOrEmpty(item.HangTagFile))
                            {
                                item.HangTagFile = null;
                                fwFactory.RemoveImageFile(item.HangTagFile);
                            }
                            // Remove AIFile
                            if (!string.IsNullOrEmpty(item.AIFile))
                            {
                                item.AIFile = null;
                                fwFactory.RemoveImageFile(item.AIFile);
                            }
                            // Remove BoxShippingMarkFile
                            if (!string.IsNullOrEmpty(item.BoxShippingMarkFile))
                            {
                                item.BoxShippingMarkFile = null;
                                fwFactory.RemoveImageFile(item.BoxShippingMarkFile);
                            }
                            // Remove BrassLabelFile
                            if (!string.IsNullOrEmpty(item.BrassLabelFile))
                            {
                                item.BrassLabelFile = null;
                                fwFactory.RemoveImageFile(item.BrassLabelFile);
                            }
                            // Remove FSCLabelFile
                            if (!string.IsNullOrEmpty(item.FSCLabelFile))
                            {
                                item.FSCLabelFile = null;
                                fwFactory.RemoveImageFile(item.FSCLabelFile);
                            }
                            // Remove WashCushionLabelFile
                            if (!string.IsNullOrEmpty(item.WashCushionLabelFile))
                            {
                                item.WashCushionLabelFile = null;
                                fwFactory.RemoveImageFile(item.WashCushionLabelFile);
                            }
                        }

                        // Sparepart
                        foreach (DTO.LabelingPackagingSparepartDetail item in data.LabelingPackagingSparepartDetails)
                        {
                            // Remove HangTagFile
                            if (!string.IsNullOrEmpty(item.HangTagFile))
                            {
                                item.HangTagFile = null;
                                fwFactory.RemoveImageFile(item.HangTagFile);
                            }
                            // Remove AIFile
                            if (!string.IsNullOrEmpty(item.AIFile))
                            {
                                item.AIFile = null;
                                fwFactory.RemoveImageFile(item.AIFile);
                            }
                            // Remove BoxShippingMarkFile
                            if (!string.IsNullOrEmpty(item.BoxShippingMarkFile))
                            {
                                item.BoxShippingMarkFile = null;
                                fwFactory.RemoveImageFile(item.BoxShippingMarkFile);
                            }
                            // Remove BrassLabelFile
                            if (!string.IsNullOrEmpty(item.BrassLabelFile))
                            {
                                item.BrassLabelFile = null;
                                fwFactory.RemoveImageFile(item.BrassLabelFile);
                            }
                            // Remove WashCushionLabelFile
                            if (!string.IsNullOrEmpty(item.WashCushionLabelFile))
                            {
                                item.WashCushionLabelFile = null;
                                fwFactory.RemoveImageFile(item.WashCushionLabelFile);
                            }
                        }

                        LabelingPackaging dbItem = null;
                        dbItem = context.LabelingPackaging.Where(o => o.LabelingPackagingID == data.LabelingPackagingID).FirstOrDefault();
                        converter.DTO2DB_LabelingPackaging(data, ref dbItem);
                        context.SaveChanges();
                    }
                    return true;
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
                return false;
            }
        }

        // Send e-mail notification from NL to VN
        public bool SendNotificationNL2VN(int userID, Hashtable filters, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };

            try
            {
                Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
                int? companyID = fwFactory.GetCompanyID(userID);
                if (!companyID.HasValue || companyID.Value != 4)
                {
                    notification.Type = NotificationType.Error;
                    notification.Message = "You have not to work in company in Netherlands!";
                    return false;
                }

                int? factoryID = (filters.ContainsKey("FactoryID") && filters["FactoryID"] != null && !string.IsNullOrEmpty(filters["FactoryID"].ToString())) ? (int?)filters["FactoryID"] : null;
                if (!factoryID.HasValue)
                {
                    notification.Type = NotificationType.Error;
                    notification.Message = "Factory is not value";
                    return false;
                }

                string clientUD = (filters.ContainsKey("ClientUD") && filters["ClientUD"] != null && !string.IsNullOrEmpty(filters["ClientUD"].ToString())) ? filters["ClientUD"].ToString() : "";
                string piNo = (filters.ContainsKey("PINo") && filters["PINo"] != null && !string.IsNullOrEmpty(filters["PINo"].ToString())) ? filters["PINo"].ToString() : "";
                string factoryUD = (filters.ContainsKey("FactoryUD") && filters["FactoryUD"] != null && !string.IsNullOrEmpty(filters["FactoryUD"].ToString())) ? filters["FactoryUD"].ToString() : "";
                string lpStatusNM = (filters.ContainsKey("LPStatusNM") && filters["LPStatusNM"] != null && !string.IsNullOrEmpty(filters["LPStatusNM"].ToString())) ? filters["LPStatusNM"].ToString() : "";

                using (var context = CreateContext())
                {
                    string sendToEmail = "";
                    string emailBody = "NL ADDED LP INFO FROM CLIENT; VN PLEASE MAKE DRAFT";
                    string emailSubject = "LP Client:" + "[" + clientUD + "]" + "[" + piNo + "]" + "[" + factoryUD + "]" + "[" + lpStatusNM + "]";

                    var lstItem = context.LabelingPackagingMng_SendEmailToTeamVN_View.Where(o => o.FactoryID == factoryID).ToList();
                    if(lstItem != null && lstItem.Count > 0)
                    {
                        foreach (var item in lstItem)
                        {
                            if (string.IsNullOrEmpty(sendToEmail))
                            {
                                sendToEmail += item.Email;
                            }
                            else
                            {
                                sendToEmail += ";" + item.Email;
                            }
                            // add to NotificationMessage table
                            NotificationMessage notification1 = new NotificationMessage();
                            notification1.UserID = item.UserID;
                            notification1.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_SALES;
                            notification1.NotificationMessageTitle = emailSubject;
                            notification1.NotificationMessageContent = emailBody;
                            context.NotificationMessage.Add(notification1);
                        }
                    }

                    EmailNotificationMessage dbItem = new EmailNotificationMessage();
                    dbItem.EmailBody = emailBody;
                    dbItem.SendTo = sendToEmail;
                    dbItem.EmailSubject = emailSubject;

                    context.EmailNotificationMessage.Add(dbItem);
                    context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return false;
            }
        }

        public DTO.EditFormData AutoFile (int userID, int id, Hashtable filters, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };
            try
            {
                int? chkPosition = Convert.ToInt32(filters["chkPosition"]);
                DTO.LabelingPackaging dtoLabelingPackaging = ((Newtonsoft.Json.Linq.JObject)filters["data"]).ToObject<DTO.LabelingPackaging>();
                using (LabelingPackagingEntities context = CreateContext())
                {
                    foreach (DTO.LabelingPackagingDetail item in dtoLabelingPackaging.LabelingPackagingDetails)
                    {
                        context.LabelingPackagingMng_function_AutoAddFile(chkPosition, item.ArticleCode, item.LabelingPackagingDetailID);
                    }
                    return GetData(userID, id, out notification);
                }  
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool AutoUpdate(int userId, ref Hashtable dtoItem, out Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.LabelingPackagingTemp dtoLabelingPackaging = ((Newtonsoft.Json.Linq.JObject)dtoItem["data"]).ToObject<DTO.LabelingPackagingTemp>();
            dtoLabelingPackaging.UpdatedBy = userId;
            Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
            int id = Convert.ToInt32(dtoItem["LPRid"]);
            try
            {
                using (LabelingPackagingEntities context = CreateContext())
                {
                    LabelingPackaging dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new LabelingPackaging();
                        context.LabelingPackaging.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.LabelingPackaging.Where(o => o.LabelingPackagingID == id).FirstOrDefault();
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "data not found!";
                        return false;
                    }
                    else
                    {
                        //convert dto to db
                        converter.DTO2DB_LabelingPackagingTemp(dtoLabelingPackaging, ref dbItem);

                        //remove orphan item
                        context.LabelingPackagingDetail.Local.Where(o => o.LabelingPackaging == null).ToList().ForEach(o => context.LabelingPackagingDetail.Remove(o));
                        context.LabelingPackagingRemark.Local.Where(o => o.LabelingPackaging == null).ToList().ForEach(o => context.LabelingPackagingRemark.Remove(o));
                        context.LabelingPackagingOtherFile.Local.Where(o => o.LabelingPackaging == null).ToList().ForEach(o => context.LabelingPackagingOtherFile.Remove(o));
                        context.LabelingPackagingRejection.Local.Where(o => o.LabelingPackaging == null).ToList().ForEach(o => context.LabelingPackagingRejection.Remove(o));

                        context.SaveChanges();

                        //get return data
                        GetData(userId, dbItem.LabelingPackagingID, out notification);

                        return true;
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
                return false;
            }
        }

        private List<DTO.OptionsDTO> getOptions()
        {
            List<DTO.OptionsDTO> options = new List<DTO.OptionsDTO>();

            options.Add(new DTO.OptionsDTO { OptionID = 6, OptionUD = "BC", OptionNM = "Batchcode" });
            options.Add(new DTO.OptionsDTO { OptionID = 7, OptionUD = "WPB", OptionNM = "Warning plastic bag" });
            options.Add(new DTO.OptionsDTO { OptionID = 8, OptionUD = "FR", OptionNM = "FR" });
            options.Add(new DTO.OptionsDTO { OptionID = 9, OptionUD = "CLL", OptionNM = "Color label" });
            options.Add(new DTO.OptionsDTO { OptionID = 10, OptionUD = "CAL", OptionNM = "Case label" });
            options.Add(new DTO.OptionsDTO { OptionID = 11, OptionUD = "COL", OptionNM = "COO label" });
            options.Add(new DTO.OptionsDTO { OptionID = 12, OptionUD = "MCL", OptionNM = "Master carton label" });
            options.Add(new DTO.OptionsDTO { OptionID = 13, OptionUD = "TL", OptionNM = "Tracking label" });
            options.Add(new DTO.OptionsDTO { OptionID = 14, OptionUD = "GFPL", OptionNM = "Global furniture product label" });
            options.Add(new DTO.OptionsDTO { OptionID = 15, OptionUD = "AVL", OptionNM = "Additional variable label" });
            options.Add(new DTO.OptionsDTO { OptionID = 16, OptionUD = "HL", OptionNM = "Hardware label" });
            options.Add(new DTO.OptionsDTO { OptionID = 17, OptionUD = "PDL", OptionNM = "Product description label" });
            options.Add(new DTO.OptionsDTO { OptionID = 18, OptionUD = "O1", OptionNM = "Other 1" });
            options.Add(new DTO.OptionsDTO { OptionID = 19, OptionUD = "O2", OptionNM = "Other 2" });
            options.Add(new DTO.OptionsDTO { OptionID = 20, OptionUD = "O3", OptionNM = "Other 3" });
            return options;
        }
    }
}

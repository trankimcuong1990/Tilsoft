using Library;
using Library.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.CushionColorMng
{
    public class DataFactory : DALBase.FactoryBase2<DTO.CushionColorMng.SearchFormData, DTO.CushionColorMng.EditFormData, DTO.CushionColorMng.CushionColor>
    {
        private DataConverter converter = new DataConverter();
        private Support.DataFactory supportFactory = new Support.DataFactory();
        private string _TempFolder;
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

        public DataFactory(string tempFolder)
        {
            _TempFolder = tempFolder + @"\";
        }
        DataSet ds = new DataSet();
        private CushionColorMngEntities CreateContext()
        {
            return new CushionColorMngEntities(DALBase.Helper.CreateEntityConnectionString("CushionColorMngModel"));
        }       
        public override DTO.CushionColorMng.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.CushionColorMng.SearchFormData data = new DTO.CushionColorMng.SearchFormData();
            data.Data = new List<DTO.CushionColorMng.CushionColorSearchResult>();
            totalRows = 0;

            string CushionColorUD = null;
            string CushionColorNM = null;
            string Season = null;
            bool? IsStandard = null;
            bool? IsEnabled = null;
            int? ProductGroupID = null;
            int? CushionTypeID = null;
            if (filters.ContainsKey("CushionColorUD") && !string.IsNullOrEmpty(filters["CushionColorUD"].ToString()))
            {
                CushionColorUD = filters["CushionColorUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("CushionColorNM") && !string.IsNullOrEmpty(filters["CushionColorNM"].ToString()))
            {
                CushionColorNM = filters["CushionColorNM"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("Season") && filters["Season"] != null && !string.IsNullOrEmpty(filters["Season"].ToString()))
            {
                Season = filters["Season"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("IsStandard") && filters["IsStandard"] != null && !string.IsNullOrEmpty(filters["IsStandard"].ToString()))
            {
                IsStandard = (filters["IsStandard"].ToString() == "true") ? true : false;
            }
            if (filters.ContainsKey("IsEnabled") && filters["IsEnabled"] != null && !string.IsNullOrEmpty(filters["IsEnabled"].ToString()))
            {
                IsEnabled = (filters["IsEnabled"].ToString() == "true") ? true : false;
            }
            if (filters.ContainsKey("ProductGroupID") && filters["ProductGroupID"] != null && !string.IsNullOrEmpty(filters["ProductGroupID"].ToString()))
            {
                ProductGroupID = Convert.ToInt32(filters["ProductGroupID"]);
            }
            if (filters.ContainsKey("CushionTypeID") && filters["CushionTypeID"] != null && !string.IsNullOrEmpty(filters["CushionTypeID"].ToString()))
            {
                CushionTypeID = Convert.ToInt32(filters["CushionTypeID"]);
            }

            //try to get data
            try
            {
                using (CushionColorMngEntities context = CreateContext())
                {
                    totalRows = context.CushionColorMng_function_SearchCushionColor(CushionColorUD, CushionColorNM, Season, IsStandard, IsEnabled, ProductGroupID, CushionTypeID, orderBy, orderDirection).Count();
                    var result = context.CushionColorMng_function_SearchCushionColor(CushionColorUD, CushionColorNM, Season, IsStandard, IsEnabled, ProductGroupID, CushionTypeID, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_CushionColorSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override DTO.CushionColorMng.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.CushionColorMng.EditFormData data = new DTO.CushionColorMng.EditFormData();
            data.Data = new DTO.CushionColorMng.CushionColor();
            data.Data.CushionColorProductGroups = new List<DTO.CushionColorMng.CushionColorProductGroup>();
            data.Data.CushionColorTestReports = new List<DTO.CushionColorMng.CushionColorTestReport>();
            data.Seasons = new List<DTO.Support.Season>();
            data.CushionTypes = new List<DTO.Support.CushionType>();
            data.Data.CushionTestingDTOs = new List<DTO.CushionColorMng.CushionTestingDTO>();

            //try to get data
            try
            {             
                using (CushionColorMngEntities context = CreateContext())
                {
                    if (id == 0)
                    {
                        data.Data.ImageFile_DisplayUrl = FrameworkSetting.Setting.ThumbnailUrl + "no-image.jpg";

                        int index = -1;
                        foreach (DTO.Support.ProductGroup dtoGroup in supportFactory.GetProductGroup())
                        {
                            data.Data.CushionColorProductGroups.Add(new DTO.CushionColorMng.CushionColorProductGroup() { CushionColorProductGroupID = index, ProductGroupID = dtoGroup.ProductGroupID, ProductGroupNM = dtoGroup.ProductGroupNM, IsEnabled = false });
                            index--;
                        }
                    }
                    else
                    {
                        data.Data = converter.DB2DTO_CushionColor(context.CushionColorMng_CushionColor_View.Include("CushionColorMng_CushionColorProductGroup_View").FirstOrDefault(o => o.CushionColorID == id));
                    }
                data.Seasons = supportFactory.GetSeason().ToList();
                data.CushionTypes = supportFactory.GetCushionType().ToList();
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (CushionColorMngEntities context = CreateContext())
                {
                    CushionColor dbItem = context.CushionColor.FirstOrDefault(o => o.CushionColorID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "CushionColor not found!";
                        return false;
                    }
                    else
                    {
                        var item = context.CushionColorMng_CushionColorCheck_View.Where(o => o.CushionColorID == id).FirstOrDefault();
                        //CheckPermission
                        if (item.isUsed.Value == true)
                        {
                            throw new Exception("You can't delete because it used in item other!");
                        }                        
                        context.CushionColor.Remove(dbItem);
                        context.SaveChanges();

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }
        }

        public override bool UpdateData(int id, ref DTO.CushionColorMng.CushionColor dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            int number;
            string indexName;

            try
            {
                using (var context = CreateContext())
                {
                    CushionColor cushionColor = null;

                    if (id == 0)
                    {
                        cushionColor = new CushionColor();

                        context.CushionColor.Add(cushionColor);
                    }
                    else
                    {                        
                        cushionColor = context.CushionColor.FirstOrDefault(o => o.CushionColorID == id);
                    }

                    if (cushionColor == null)
                    {
                        notification.Message = "Cushion Color not found!";

                        return false;
                    }
                    else
                    {
                        // check concurrency
                        if (cushionColor.ConcurrencyFlag != null && !cushionColor.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoItem.ConcurrencyFlag_String)))
                        {
                            notification.Type = NotificationType.Error;
                            notification.Message = Library.Helper.TEXT_CONCURRENCY_CONFLICT;

                            return false;
                        }

                        converter.DTO2BD(dtoItem, _TempFolder, ref cushionColor);

                        // processing image
                        if (dtoItem.ImageFile_HasChange)
                        {
                            cushionColor.ImageFile = fwFactory.CreateFilePointer(this._TempFolder, dtoItem.ImageFile_NewFile, dtoItem.ImageFile);
                        }

                        // processing test report file 1
                        if (dtoItem.TestReportFile_HasChange1)
                        {
                            cushionColor.TestReportFile1 = fwFactory.CreateNoneImageFilePointer(this._TempFolder, dtoItem.TestReportFile_NewFile1, dtoItem.TestReportFile1);
                        }

                        // processing test report file 2
                        if (dtoItem.TestReportFile_HasChange2)
                        {
                            cushionColor.TestReportFile2 = fwFactory.CreateNoneImageFilePointer(this._TempFolder, dtoItem.TestReportFile_NewFile2, dtoItem.TestReportFile2);
                        }

                        // processing test report file 3
                        if (dtoItem.TestReportFile_HasChange3)
                        {
                            cushionColor.TestReportFile3 = fwFactory.CreateNoneImageFilePointer(this._TempFolder, dtoItem.TestReportFile_NewFile3, dtoItem.TestReportFile3);
                        }

                        if (id <= 0)
                        {
                            // Generate code.
                            using (var trans = context.Database.BeginTransaction())
                            {
                                context.Database.ExecuteSqlCommand("SELECT * FROM CushionColor WITH (TABLOCKX, HOLDLOCK)");

                                try
                                {
                                    var newCode = context.CushionColorMng_function_GenerateCode().FirstOrDefault();

                                    if (!"**".Equals(newCode))
                                    {
                                        cushionColor.CushionColorUD = newCode;

                                        context.SaveChanges();
                                    }
                                    else
                                    {
                                        notification.Type = NotificationType.Error;
                                        notification.Message = "Auto generated code exceed maximum option: [ZZ]";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    throw ex;
                                }
                                finally
                                {
                                    trans.Commit();
                                }
                            }
                        }
                        else
                        {
                            context.CushionColorProductGroup.Local.Where(o => o.CushionColor == null).ToList().ForEach(o => context.CushionColorProductGroup.Remove(o));
                            context.CushionColorTestReport.Local.Where(o => o.CushionColor == null).ToList().ForEach(o => context.CushionColorTestReport.Remove(o));

                            context.SaveChanges();
                        }

                        // Handle notification missing information.
                        string emailSubject = (id == 0) ? "TASK REQUEST [CREATE CUSHION COLOR]" : "TASK REQUEST [UPDATE CUSHION COLOR]";
                        string emailBody = string.Empty;

                        if (!IsNullPropertiesCushionColor(cushionColor, ref emailBody))
                        {
                            SendToEmailNotification(context, emailSubject, emailBody);
                        }

                        dtoItem = GetData(cushionColor.CushionColorID, out notification).Data;

                        return true;
                    }
                }
            }
            catch (DataException exData)
            {
                notification.Type = NotificationType.Error;
                ErrorHelper.DataExceptionParser(exData, out number, out indexName);

                if (number == 2601 && !string.IsNullOrEmpty(indexName))
                {
                    if ("CushionColorUDUnique".Equals(indexName))
                        notification.Message = "The Cushion Color Code is already exists.";
                }
                else
                {
                    notification.Message = exData.Message;
                }

                return false;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex, ds);
                return false;
            }
            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (CushionColorMngEntities context = CreateContext())
            //    {
            //        CushionColor dbItem = null;
            //        if (id == 0)
            //        {
            //            dbItem = new CushionColor();
            //            context.CushionColor.Add(dbItem);
            //        }
            //        else
            //        {
            //            dbItem = context.CushionColor.FirstOrDefault(o => o.CushionColorID == id);
            //        }

            //        if (dbItem == null)
            //        {
            //            notification.Message = "CushionColor not found!";
            //            return false;
            //        }
            //        else
            //        {
            //            // check concurrency
            //            if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoItem.ConcurrencyFlag_String)))
            //            {
            //                throw new Exception(DALBase.Helper.TEXT_CONCURRENCY_CONFLICT);
            //            }

            //            converter.DTO2BD(dtoItem, ref dbItem);
            //            context.SaveChanges();

            //            // processing image
            //            if (dtoItem.ImageFile_HasChange)
            //            {
            //                dbItem.ImageFile = fwFactory.CreateFilePointer(this._TempFolder, dtoItem.ImageFile_NewFile, dtoItem.ImageFile);
            //            }

            //            // processing test report file 1
            //            if (dtoItem.TestReportFile_HasChange1)
            //            {
            //                dbItem.TestReportFile1 = fwFactory.CreateNoneImageFilePointer(this._TempFolder, dtoItem.TestReportFile_NewFile1, dtoItem.TestReportFile1);
            //            }

            //            // processing test report file 2
            //            if (dtoItem.TestReportFile_HasChange2)
            //            {
            //                dbItem.TestReportFile2 = fwFactory.CreateNoneImageFilePointer(this._TempFolder, dtoItem.TestReportFile_NewFile2, dtoItem.TestReportFile2);
            //            }

            //            // processing test report file 3
            //            if (dtoItem.TestReportFile_HasChange3)
            //            {
            //                dbItem.TestReportFile3 = fwFactory.CreateNoneImageFilePointer(this._TempFolder, dtoItem.TestReportFile_NewFile3, dtoItem.TestReportFile3);
            //            }
            //            context.SaveChanges();

            //            dtoItem = GetData(dbItem.CushionColorID, out notification).Data;

            //            return true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification.Type = Library.DTO.NotificationType.Error;
            //    notification.Message = ex.Message;
            //    return false;
            //}
        }

        public override bool Approve(int id, ref DTO.CushionColorMng.CushionColor dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.CushionColorMng.CushionColor dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public DTO.CushionColorMng.SearchFilterData GetFilterData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.CushionColorMng.SearchFilterData data = new DTO.CushionColorMng.SearchFilterData();
            data.Seasons = new List<DTO.Support.Season>();
            data.YesNoValues = new List<DTO.Support.YesNo>();
            data.ProductGroups = new List<DTO.Support.ProductGroup>();
            data.CushionTypes = new List<DTO.Support.CushionType>();

            //try to get data
            try
            {
                data.Seasons = supportFactory.GetSeason().ToList();
                data.YesNoValues = supportFactory.GetYesNo().ToList();
                data.ProductGroups = supportFactory.GetProductGroup().ToList();
                data.CushionTypes = supportFactory.GetCushionType().ToList();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        private bool IsNullPropertiesCushionColor(CushionColor cushionColor, ref string emailBody)
        {
            bool isNotNull = true;

            // Set content notification.
            emailBody += "Cushion Color ID: " + cushionColor.CushionColorID;
            emailBody += Environment.NewLine + "Cushion Color UD: " + cushionColor.CushionColorUD;
            emailBody += Environment.NewLine + "Cushion Color NM: " + cushionColor.CushionColorNM;

            // Season is null or not null.
            if (string.IsNullOrEmpty(cushionColor.Season))
            {
                emailBody += Environment.NewLine + "Season: Current value is null.";
                isNotNull = false;
            }
            else
            {
                emailBody += Environment.NewLine + "Season: " + cushionColor.Season;
            }

            // Image is null or not null.
            if (string.IsNullOrEmpty(cushionColor.ImageFile))
            {
                emailBody += Environment.NewLine + "Image: Current value is null.";
                isNotNull = false;
            }
            else
            {
                emailBody += Environment.NewLine + "Image: " + cushionColor.ImageFile;
            }

            return isNotNull;
        }

        private void SendToEmailNotification(CushionColorMngEntities context, string emailSubject, string emailBody)
        {
            try
            {
                string sendToEmail = string.Empty;

                // My(AVT)[20] and Thanh(AVT)[74]
                foreach (var empObj in context.EmployeeMng_Employee_View.Where(o => !string.IsNullOrEmpty( o.Email1) && ( o.UserID == 20 || o.UserID == 74 || o.UserID == 1)).ToList())
                {
                    if (!string.IsNullOrEmpty(sendToEmail))
                        sendToEmail += "; ";

                    sendToEmail += empObj.Email1;

                    // add to NotificationMessage table
                    context.NotificationMessage.Add(new NotificationMessage {
                        UserID = empObj.UserID,
                        NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_PRODUCTION,
                        NotificationMessageTitle = emailSubject,
                        NotificationMessageContent = emailBody
                    });
                }

                // Create data EmailNotificationMessage.
                EmailNotificationMessage emailNotificationMessage = new EmailNotificationMessage();
                emailNotificationMessage.EmailSubject = emailSubject;
                emailNotificationMessage.EmailBody = emailBody;
                emailNotificationMessage.SendTo = sendToEmail;

                context.EmailNotificationMessage.Add(emailNotificationMessage);
                context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
    }
}
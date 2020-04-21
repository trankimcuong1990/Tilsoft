using Library;
using Library.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CushionColorMng.DAL
{
    class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private DataConverter converter = new DataConverter();
        private string _TempFolder;
        private Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();

        public DataFactory()
        {
            _TempFolder = _TempFolder + @"\";
        }


        private CushionColorMngEntities CreateContext()
        {
            return new CushionColorMngEntities(Library.Helper.CreateEntityConnectionString("DAL.CushionColorMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.CushionColorSearchResult>();
            totalRows = 0;
            _TempFolder = filters["tempFoder"].ToString();

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

        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new DTO.CushionColor();
            data.Data.CushionColorProductGroups = new List<DTO.CushionColorProductGroup>();
            data.Data.CushionColorTestReports = new List<DTO.CushionColorTestReport>();
            data.Seasons = new List<DTO.Season>();
            data.CushionTypes = new List<DTO.CushionType>();
            data.Data.CushionTestingDTOs = new List<DTO.CushionTestingDTO>();

            //try to get data
            try
            {
                using (CushionColorMngEntities context = CreateContext())
                {
                    if (id == 0)
                    {
                        data.Data.ImageFile_DisplayUrl = FrameworkSetting.Setting.ThumbnailUrl + "no-image.jpg";

                        int index = -1;
                        foreach (DTO.ProductGroup dtoGroup in GetProductGroup())
                        {
                            data.Data.CushionColorProductGroups.Add(new DTO.CushionColorProductGroup() { CushionColorProductGroupID = index, ProductGroupID = dtoGroup.ProductGroupID, ProductGroupNM = dtoGroup.ProductGroupNM, IsEnabled = false });
                            index--;
                        }
                    }
                    else
                    {
                        data.Data = converter.DB2DTO_CushionColor(context.CushionColorMng_CushionColor_View.Include("CushionColorMng_CushionColorProductGroup_View").FirstOrDefault(o => o.CushionColorID == id));
                    }
                    data.Seasons = GetSeason().ToList();
                    data.CushionTypes = GetCushionType().ToList();
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

        public bool UpdateData(int id, ref object dtoItem,int iRequesterID, out Library.DTO.Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            int number;
            string indexName;
            DTO.CushionColor dtoCushionColor = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.CushionColor>();
            try
            {
                using (var context = CreateContext())
                {
                    CushionColor cushionColor = null;

                    if (id == 0)
                    {
                        dtoCushionColor.CreatedBy = iRequesterID;
                        cushionColor = new CushionColor();

                        context.CushionColor.Add(cushionColor);
                    }
                    else
                    {
                        var item = context.CushionColorMng_CushionColorCheck_View.Where(o => o.CushionColorID == id).FirstOrDefault();
                        //CheckPermission
                        if (item.isUsed.Value == true)
                        {
                            throw new Exception("You can't update because it used in item other!");
                        }
                        dtoCushionColor.UpdatedBy = iRequesterID;
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
                        if (cushionColor.ConcurrencyFlag != null && !cushionColor.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoCushionColor.ConcurrencyFlag_String)))
                        {
                            notification.Type = NotificationType.Error;
                            notification.Message = Library.Helper.TEXT_CONCURRENCY_CONFLICT;

                            return false;
                        }

                        converter.DTO2BD(dtoCushionColor, _TempFolder, ref cushionColor);

                        // processing image
                        if (dtoCushionColor.ImageFile_HasChange)
                        {
                            cushionColor.ImageFile = fwFactory.CreateFilePointer(this._TempFolder, dtoCushionColor.ImageFile_NewFile, dtoCushionColor.ImageFile);
                        }

                        // processing test report file 1
                        if (dtoCushionColor.TestReportFile_HasChange1)
                        {
                            cushionColor.TestReportFile1 = fwFactory.CreateNoneImageFilePointer(this._TempFolder, dtoCushionColor.TestReportFile_NewFile1, dtoCushionColor.TestReportFile1);
                        }

                        // processing test report file 2
                        if (dtoCushionColor.TestReportFile_HasChange2)
                        {
                            cushionColor.TestReportFile2 = fwFactory.CreateNoneImageFilePointer(this._TempFolder, dtoCushionColor.TestReportFile_NewFile2, dtoCushionColor.TestReportFile2);
                        }

                        // processing test report file 3
                        if (dtoCushionColor.TestReportFile_HasChange3)
                        {
                            cushionColor.TestReportFile3 = fwFactory.CreateNoneImageFilePointer(this._TempFolder, dtoCushionColor.TestReportFile_NewFile3, dtoCushionColor.TestReportFile3);
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
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;

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

        public bool Approve(int id, ref DTO.CushionColor dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool Reset(int id, ref DTO.CushionColor dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public DTO.SearchFilterData GetFilterData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFilterData data = new DTO.SearchFilterData();
            data.Seasons = new List<DTO.Season>();
            data.YesNoValues = new List<DTO.YesNo>();
            data.ProductGroups = new List<DTO.ProductGroup>();
            data.CushionTypes = new List<DTO.CushionType>();

            //try to get data
            try
            {
                data.Seasons = GetSeason().ToList();
                data.YesNoValues = GetYesNo().ToList();
                data.ProductGroups = GetProductGroup().ToList();
                data.CushionTypes = GetCushionType().ToList();
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
                List<string> emailAddresses = new List<string>();

                // My(AVT)[20] and Thanh(AVT)[74].
                emailAddresses = context.EmployeeMng_Employee_View.Where(o => o.UserID == 20 || o.UserID == 74).Select(s => s.Email1).ToList();
                foreach (var emailAddress in emailAddresses)
                {
                    if (!string.IsNullOrEmpty(sendToEmail))
                        sendToEmail += "; ";

                    sendToEmail += emailAddress;
                }
                List<int> listUser = new List<int>();
                listUser.Add(20);
                listUser.Add(74);
                foreach(var item in listUser)
                {
                    // add to NotificationMessage table
                    NotificationMessage notification = new NotificationMessage();
                    notification.UserID = item;
                    notification.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_PRODUCTDEVELOPMENT;
                    notification.NotificationMessageTitle = emailSubject;
                    notification.NotificationMessageContent = emailBody;
                    context.NotificationMessage.Add(notification);
                }
                

                // Create data EmailNotificationMessage.
                EmailNotificationMessage emailNotificationMessage = new EmailNotificationMessage();
                emailNotificationMessage.EmailSubject = emailSubject;
                emailNotificationMessage.EmailBody = emailBody;
                emailNotificationMessage.SendTo = sendToEmail;

                context.EmailNotificationMessage.Add(emailNotificationMessage);
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<DTO.ProductGroup> GetProductGroup()
        {
            //try to get data
            try
            {
                using (CushionColorMngEntities context = CreateContext())
                {
                    return converter.DB2DTO_ProductGroup(context.SupportMng_ProductGroup_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.ProductGroup>();
            }
        }

        public IEnumerable<DTO.Season> GetSeason()
        {
            List<DTO.Season> data = new List<DTO.Season>();
            for (int i = 2005; i <= DateTime.Now.Year + 1; i++)
            {
                data.Add(new DTO.Season() { SeasonValue = i.ToString() + "/" + (i + 1).ToString(), SeasonText = i.ToString() + "/" + (i + 1).ToString() });
            }
            var result = data.OrderByDescending(f => f.SeasonValue);

            return result;
        }

        public IEnumerable<DTO.YesNo> GetYesNo()
        {
            List<DTO.YesNo> result = new List<DTO.YesNo>();
            result.Add(new DTO.YesNo() { YesNoText = "Yes", YesNoValue = "true" });
            result.Add(new DTO.YesNo() { YesNoText = "No", YesNoValue = "false" });

            return result;
        }

        public IEnumerable<DTO.CushionType> GetCushionType()
        {
            //try to get data
            try
            {
                using (CushionColorMngEntities context = CreateContext())
                {
                    return converter.DB2DTO_CushionType(context.SupportMng_CushionType_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.CushionType>();
            }
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}

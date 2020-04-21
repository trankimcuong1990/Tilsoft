using Library.DTO;
using Module.BackCushionMng.DAL;
using Module.BackCushionMng.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Module.BackCushionMng
{

    public class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private DataConverters converter = new DataConverters();
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private string _TempFolder;
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

        public DataFactory(string tempFolder)
        {
            _TempFolder = tempFolder + @"\";
        }

        public DataFactory()
        {
        }

        private BackCushionMngEntities CreateContext()
        {
            return new BackCushionMngEntities(Library.Helper.CreateEntityConnectionString("DAL.BackCushionMngModel"));
        }
        public override SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            SearchFormData data = new SearchFormData();
            data.Data = new List<BackCushionSearchResult>();
            totalRows = 0;

            string BackCushionUD = null;
            string BackCushionNM = null;
            string Season = null;
            bool? IsStandard = null;
            bool? IsEnabled = null;
            int? ProductGroupID = null;
            if (filters.ContainsKey("BackCushionUD") && !string.IsNullOrEmpty(filters["BackCushionUD"].ToString()))
            {
                BackCushionUD = filters["BackCushionUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("BackCushionNM") && !string.IsNullOrEmpty(filters["BackCushionNM"].ToString()))
            {
                BackCushionNM = filters["BackCushionNM"].ToString().Replace("'", "''");
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

            //try to get data
            try
            {
                using (BackCushionMngEntities context = CreateContext())
                {
                    totalRows = context.BackCushionMng_function_SearchBackCushionMng(BackCushionUD, BackCushionNM, Season, IsStandard, IsEnabled, ProductGroupID, orderBy, orderDirection).Count();
                    var result = context.BackCushionMng_function_SearchBackCushionMng(BackCushionUD, BackCushionNM, Season, IsStandard, IsEnabled, ProductGroupID, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_BackCushionSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            EditFormData data = new EditFormData();
            data.Data = new BackCushionDTO() { ImageFile_DisplayUrl = FrameworkSetting.Setting.ThumbnailUrl + "no-image.jpg" };
            data.Data.ProductGroups = new List<ProductGroupDTO>();
            data.Seasons = new List<SeasonDTO>();

            //try to get data
            try
            {
                using (BackCushionMngEntities context = CreateContext())
                {
                    if(id==0)
                    {
                        int index = -1;
                        foreach (ProductGroupDTO dtoGroup in GetProductGroup())
                        {
                            data.Data.ProductGroups.Add(new ProductGroupDTO() { BackCushionProductGroupID = index, ProductGroupID = dtoGroup.ProductGroupID, ProductGroupNM = dtoGroup.ProductGroupNM, IsEnabled = false });
                            index--;
                        }
                    }
                    else if (id > 0)
                    {
                        data.Data = converter.DB2DTO_BackCushion(context.BackCushionMng_BackCushion_View.Include("BackCushionMng_BackCushionProductGroup_View").FirstOrDefault(o => o.BackCushionID == id));
                    }                    
                    data.Seasons = GetSeason().ToList();
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
                using (BackCushionMngEntities context = CreateContext())
                {
                    BackCushion dbItem = context.BackCushion.FirstOrDefault(o => o.BackCushionID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Back cushion not found!";
                        return false;
                    }
                    else
                    {
                        var item = context.BackCushionMng_BackCushionCheck_View.Where(o => o.BackCushionID == id).FirstOrDefault();
                        //CheckPermission
                        if (item.isUsed.Value == true)
                        {
                            throw new Exception("You can't delete because it used in item other!");
                        }
                        context.BackCushion.Remove(dbItem);
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

        //public bool UpdateData(int id, ref object dtoItem, out Library.DTO.Notification notification)
        //{
        //    BackCushionDTO dtoPBackCushion = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<BackCushionDTO>();
        //    notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
        //    try
        //    {
        //        using (BackCushionMngEntities context = CreateContext())
        //        {
        //            BackCushion dbItem = null;
        //            if (id == 0)
        //            {
        //                dbItem = new BackCushion();
        //                context.BackCushion.Add(dbItem);
        //            }
        //            else
        //            {
        //                var item = context.BackCushionMng_BackCushionCheck_View.Where(o => o.BackCushionID == id).FirstOrDefault();
        //                //CheckPermission
        //                if (item.isUsed.Value == true)
        //                {
        //                    throw new Exception("You can't update because it used in item other!");
        //                }
        //                dbItem = context.BackCushion.FirstOrDefault(o => o.BackCushionID == id);
        //            }

        //            if (dbItem == null)
        //            {
        //                notification.Message = "Back cushion not found!";
        //                return false;
        //            }
        //            else
        //            {
        //                // check concurrency
        //                if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoPBackCushion.ConcurrencyFlag_String)))
        //                {
        //                    throw new Exception(DALBase.Helper.TEXT_CONCURRENCY_CONFLICT);
        //                }
        //                converter.DTO2BD(dtoPBackCushion, ref dbItem);
        //                // processing image
        //                if (dtoPBackCushion.ImageFile_HasChange)
        //                {
        //                    dbItem.ImageFile = fwFactory.CreateFilePointer(this._TempFolder, dtoPBackCushion.ImageFile_NewFile, dtoPBackCushion.ImageFile);
        //                }

        //                if (id <= 0)
        //                {
        //                    // generate code
        //                    using (DbContextTransaction scope = context.Database.BeginTransaction())
        //                    {
        //                        context.Database.ExecuteSqlCommand("SELECT * FROM BackCushion WITH (TABLOCKX, HOLDLOCK)");
        //                        try
        //                        {
        //                            var newCode = context.BackCushionMng_function_GenerateCode().FirstOrDefault();
        //                            if (newCode != "*")
        //                            {
        //                                dbItem.BackCushionUD = newCode;
        //                            }
        //                            else
        //                            {
        //                                throw new Exception("Auto generated code exceed maximum option: [Z]");
        //                            }
        //                            context.SaveChanges();
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            throw ex;
        //                        }
        //                        finally
        //                        {
        //                            scope.Commit();
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    context.SaveChanges();
        //                }

        //                // Handle notification missing information.
        //                string emailSubject = (id == 0) ? "TASK REQUEST [CREATE BACK CUSHION]" : "TASK REQUEST [UPDATE BACK CUSHION]";
        //                string emailBody = string.Empty;

        //                if (!IsNullPropertiesBackCushion(dbItem, ref emailBody))
        //                {
        //                    SendToEmailNotification(context, emailSubject, emailBody);
        //                }

        //                dtoItem = GetData(dbItem.BackCushionID, out notification).Data;

        //                return true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification.Type = Library.DTO.NotificationType.Error;
        //        notification.Message = ex.Message;

        //        return false;
        //    }
        //}


        public SearchFilterData GetFilterData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            SearchFilterData data = new SearchFilterData();
            data.Seasons = new List<Support.DTO.Season>();
            data.YesNoValues = new List<Support.DTO.YesNo>();
            data.ProductGroups = new List<Support.DTO.ProductGroup>();

            //try to get data
            try
            {
                data.Seasons = supportFactory.GetSeason();
                data.YesNoValues = supportFactory.GetYesNo();
                data.ProductGroups = supportFactory.GetProductGroup();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }
        public IEnumerable<ProductGroupDTO> GetProductGroup()
        {
            //try to get data
            try
            {
                using (BackCushionMngEntities context = CreateContext())
                {
                    return converter.DB2DTO_ProductGroup(context.SupportMng_ProductGroup_View.ToList());
                }
            }
            catch
            {
                return new List<ProductGroupDTO>();
            }
        }
        public IEnumerable<SeasonDTO> GetSeason()
        {
            List<SeasonDTO> data = new List<SeasonDTO>();
            for (int i = 2005; i <= DateTime.Now.Year + 1; i++)
            {
                data.Add(new SeasonDTO() { SeasonValue = i.ToString() + "/" + (i + 1).ToString(), SeasonText = i.ToString() + "/" + (i + 1).ToString() });
            }
            var result = data.OrderByDescending(f => f.SeasonValue);

            return result;
        }
        public IEnumerable<YesNo> GetYesNo()
        {
            List<YesNo> result = new List<YesNo>();
            result.Add(new YesNo() { YesNoText = "Yes", YesNoValue = "true" });
            result.Add(new YesNo() { YesNoText = "No", YesNoValue = "false" });

            return result;
        }
        private bool IsNullPropertiesBackCushion(BackCushion backCushion, ref string emailBody)
        {
            bool isNotNull = true;

            // Set content notification.
            emailBody += "Back Cushion ID: " + backCushion.BackCushionID;
            emailBody += Environment.NewLine + "Back Cushion UD: " + backCushion.BackCushionUD;
            emailBody += Environment.NewLine + "Back Cushion NM: " + backCushion.BackCushionNM;

            // Season is null or not null.
            if (string.IsNullOrEmpty(backCushion.Season))
            {
                emailBody += Environment.NewLine + "Season: Current value is null.";
                isNotNull = false;
            }
            else
            {
                emailBody += Environment.NewLine + "Season: " + backCushion.Season;
            }

            // Image is null or not null.
            if (string.IsNullOrEmpty(backCushion.ImageFile))
            {
                emailBody += Environment.NewLine + "Image: Current value is null.";
                isNotNull = false;
            }
            else
            {
                emailBody += Environment.NewLine + "Image: " + backCushion.ImageFile;
            }

            return isNotNull;
        }

        private void SendToEmailNotification(BackCushionMngEntities context, string emailSubject, string emailBody)
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
                foreach (var item in listUser)
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
            catch
            {
                throw;
            }
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            BackCushionDTO dtoPBackCushion = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<BackCushionDTO>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (BackCushionMngEntities context = CreateContext())
                {
                    BackCushion dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new BackCushion();
                        context.BackCushion.Add(dbItem);
                        dtoPBackCushion.CreatedBy = userId;                        
                    }
                    else
                    {
                        var item = context.BackCushionMng_BackCushionCheck_View.Where(o => o.BackCushionID == id).FirstOrDefault();
                        //CheckPermission
                        if (item.isUsed.Value == true)
                        {
                            throw new Exception("You can't update because it used in item other!");
                        }
                        dbItem = context.BackCushion.FirstOrDefault(o => o.BackCushionID == id);
                        dtoPBackCushion.UpdatedBy = userId;                      
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Back cushion not found!";
                        return false;
                    }
                    else
                    {
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoPBackCushion.ConcurrencyFlag_String)))
                        {
                            throw new Exception(DALBase.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }
                        converter.DTO2BD(dtoPBackCushion, ref dbItem);
                        // processing image
                        if (dtoPBackCushion.ImageFile_HasChange)
                        {
                            dbItem.ImageFile = fwFactory.CreateFilePointer(this._TempFolder, dtoPBackCushion.ImageFile_NewFile, dtoPBackCushion.ImageFile);
                        }

                        if (id <= 0)
                        {
                            // generate code
                            using (DbContextTransaction scope = context.Database.BeginTransaction())
                            {
                                context.Database.ExecuteSqlCommand("SELECT * FROM BackCushion WITH (TABLOCKX, HOLDLOCK)");
                                try
                                {
                                    var newCode = context.BackCushionMng_function_GenerateCode().FirstOrDefault();
                                    if (newCode != "*")
                                    {
                                        dbItem.BackCushionUD = newCode;
                                    }
                                    else
                                    {
                                        throw new Exception("Auto generated code exceed maximum option: [Z]");
                                    }
                                    context.SaveChanges();
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                                finally
                                {
                                    scope.Commit();
                                }
                            }
                        }
                        else
                        {
                            context.SaveChanges();
                        }

                        // Handle notification missing information.
                        string emailSubject = (id == 0) ? "TASK REQUEST [CREATE BACK CUSHION]" : "TASK REQUEST [UPDATE BACK CUSHION]";
                        string emailBody = string.Empty;

                        if (!IsNullPropertiesBackCushion(dbItem, ref emailBody))
                        {
                            SendToEmailNotification(context, emailSubject, emailBody);
                        }

                        dtoItem = GetData(dbItem.BackCushionID, out notification).Data;

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

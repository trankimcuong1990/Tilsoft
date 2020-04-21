using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;
using Module.SeatCushionMng.DTO;

namespace Module.SeatCushionMng.DAL
{
    class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private DataConverter converter = new DataConverter();
        //private Support.DataFactory supportFactory = new Support.DataFactory();
        private string _TempFolder;
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

        public DataFactory()
        {
            _TempFolder = _TempFolder + @"\";
        }
        private SeatCushionMngEntities CreateContext()
        {
            return new SeatCushionMngEntities(Library.Helper.CreateEntityConnectionString("DAL.SeatCushionMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.SeatCushionSearchResult>();
            totalRows = 0;
            _TempFolder = filters["tempFoder"].ToString();

            string SeatCushionUD = null;
            string SeatCushionNM = null;
            string Season = null;
            bool? IsStandard = null;
            bool? IsEnabled = null;
            int? ProductGroupID = null;
            if (filters.ContainsKey("SeatCushionUD") && !string.IsNullOrEmpty(filters["SeatCushionUD"].ToString()))
            {
                SeatCushionUD = filters["SeatCushionUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("SeatCushionNM") && !string.IsNullOrEmpty(filters["SeatCushionNM"].ToString()))
            {
                SeatCushionNM = filters["SeatCushionNM"].ToString().Replace("'", "''");
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
                using (SeatCushionMngEntities context = CreateContext())
                {
                    totalRows = context.SeatCushionMng_function_SearchSeatCushionMng(SeatCushionUD, SeatCushionNM, Season, IsStandard, IsEnabled, ProductGroupID, orderBy, orderDirection).Count();
                    var result = context.SeatCushionMng_function_SearchSeatCushionMng(SeatCushionUD, SeatCushionNM, Season, IsStandard, IsEnabled, ProductGroupID, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_SeatCushionSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
            data.Data = new DTO.SeatCushion() { ImageFile_DisplayUrl = FrameworkSetting.Setting.ThumbnailUrl + "no-image.jpg" };
            data.Data.spProductGroups = new List<DTO.spProductGroup>();
            data.Seasons = new List<DTO.Season>();

            //try to get data
            try
            {
                using (SeatCushionMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        data.Data = converter.DB2DTO_SeatCushion(context.SeatCushionMng_SeatCushion_View.Include("SeatCushionMng_SeatCushionProductGroup_View").FirstOrDefault(o => o.SeatCushionID == id));
                    }
                    else
                    {
                        data.Data.spProductGroups = converter.DB2DTO_spProductGroup(context.SupportMng_ProductGroup_View.ToList());
                        data.Data.ProductGroups = CopyspProductGroups(data.Data.spProductGroups, data.Data.ProductGroups);

                        //int index = -1;
                        //foreach (spProductGroup dtoGroup in GetProductGroup())
                        //{
                        //    data.Data.ProductGroups.Add(new ProductGroup() { SeatCushionProductGroupID = index, ProductGroupID = dtoGroup.ProductGroupID, ProductGroupNM = dtoGroup.ProductGroupNM, IsEnabled = false });
                        //    index--;
                        //}
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

        public List<DTO.ProductGroup> CopyspProductGroups(List<DTO.spProductGroup> supportList, List<DTO.ProductGroup> data)
        {
            data = new List<DTO.ProductGroup>();
            int i = -1;

            foreach (var item in supportList)
            {
                DTO.ProductGroup newItem = new DTO.ProductGroup()
                {
                    SeatCushionProductGroupID = i,
                    ProductGroupNM = item.ProductGroupNM,
                    ProductGroupID = item.ProductGroupID
                };

                data.Add(newItem);

                i = i - 1;
            }
            return data;
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (SeatCushionMngEntities context = CreateContext())
                {
                    SeatCushion dbItem = context.SeatCushion.FirstOrDefault(o => o.SeatCushionID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Back cushion not found!";
                        return false;
                    }
                    else
                    {
                        var item = context.SeatCushionMng_SeatCushionCheck_View.Where(o => o.SeatCushionID == id).FirstOrDefault();
                        //CheckPermission
                        if (item.isUsed.Value == true)
                        {
                            throw new Exception("You can't delete because it used in item other!");
                        }
                        context.SeatCushion.Remove(dbItem);
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

        public bool UpdateData(int id, ref object dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SeatCushion dtoSeatCushion = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.SeatCushion>();
            try
            {
                using (SeatCushionMngEntities context = CreateContext())
                {
                    SeatCushion dbItem = null;
                    if (id == 0)
                    {
                        dtoSeatCushion.CreatedBy = iRequesterID;
                        dbItem = new SeatCushion();
                        context.SeatCushion.Add(dbItem);
                    }
                    else
                    {
                        var item = context.SeatCushionMng_SeatCushionCheck_View.Where(o => o.SeatCushionID == id).FirstOrDefault();
                        //CheckPermission
                        if (item.isUsed.Value == true)
                        {
                            throw new Exception("You can't update because it used in item other!");
                        }
                        dtoSeatCushion.UpdatedBy = iRequesterID;
                        dbItem = context.SeatCushion.FirstOrDefault(o => o.SeatCushionID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Back cushion not found!";
                        return false;
                    }
                    else
                    {
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoSeatCushion.ConcurrencyFlag_String)))
                        {
                            throw new Exception(Library.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }
                        converter.DTO2BD(dtoSeatCushion, ref dbItem);
                        // processing image
                        if (dtoSeatCushion.ImageFile_HasChange)
                        {
                            dbItem.ImageFile = fwFactory.CreateFilePointer(this._TempFolder, dtoSeatCushion.ImageFile_NewFile, dtoSeatCushion.ImageFile);
                        }

                        if (id <= 0)
                        {
                            // generate code
                            using (DbContextTransaction scope = context.Database.BeginTransaction())
                            {
                                context.Database.ExecuteSqlCommand("SELECT * FROM SeatCushion WITH (TABLOCKX, HOLDLOCK)");
                                try
                                {
                                    var newCode = context.SeatCushionMng_function_GenerateCode().FirstOrDefault();
                                    if (newCode != "*")
                                    {
                                        dbItem.SeatCushionUD = newCode;
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
                        string emailSubject = (id == 0) ? "TASK REQUEST [CREATE SEAT CUSHION]" : "TASK REQUEST [UPDATE SEAT CUSHION]";
                        string emailBody = string.Empty;

                        if (!IsNullPropertiesSeatCushion(dbItem, ref emailBody))
                        {
                            SendToEmailNotification(context, emailSubject, emailBody);
                        }

                        dtoItem = GetData(dbItem.SeatCushionID, out notification).Data;

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

        public bool Approve(int id, ref DTO.SeatCushion dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool Reset(int id, ref DTO.SeatCushion dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public DTO.SearchFilterData GetFilterData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFilterData data = new DTO.SearchFilterData();
            data.Seasons = new List<DTO.Season>();
            data.YesNoValues = new List<DTO.YesNo>();
            data.ProductGroups = new List<DTO.spProductGroup>();

            //try to get data
            try
            {
                data.Seasons = GetSeason().ToList();
                data.YesNoValues = GetYesNo().ToList();
                using (SeatCushionMngEntities context = CreateContext())
                { 
                    data.ProductGroups = converter.DB2DTO_spProductGroup(context.SupportMng_ProductGroup_View.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        private bool IsNullPropertiesSeatCushion(SeatCushion seatCushion, ref string emailBody)
        {
            bool isNotNull = true;

            // Set content notification.
            emailBody += "Seat Cushion ID: " + seatCushion.SeatCushionID;
            emailBody += Environment.NewLine + "Seat Cushion UD: " + seatCushion.SeatCushionUD;
            emailBody += Environment.NewLine + "Seat Cushion NM: " + seatCushion.SeatCushionNM;

            // Season is null or not null.
            if (string.IsNullOrEmpty(seatCushion.Season))
            {
                emailBody += Environment.NewLine + "Season: Current value is null.";
                isNotNull = false;
            }
            else
            {
                emailBody += Environment.NewLine + "Season: " + seatCushion.Season;
            }

            // Image is null or not null.
            if (string.IsNullOrEmpty(seatCushion.ImageFile))
            {
                emailBody += Environment.NewLine + "Image: Current value is null.";
                isNotNull = false;
            }
            else
            {
                emailBody += Environment.NewLine + "Image: " + seatCushion.ImageFile;
            }

            return isNotNull;
        }

        private void SendToEmailNotification(SeatCushionMngEntities context, string emailSubject, string emailBody)
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
            catch(Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<DTO.spProductGroup> GetProductGroup()
        {
            //try to get data
            try
            {
                using (SeatCushionMngEntities context = CreateContext())
                {
                    return converter.DB2DTO_ProductGroup(context.SeatCushionMng_SeatCushionProductGroup_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.spProductGroup>();
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

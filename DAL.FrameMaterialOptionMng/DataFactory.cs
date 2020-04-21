using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.FrameMaterialOptionMng
{
    public class DataFactory : DALBase.FactoryBase2<DTO.FrameMaterialOptionMng.SearchFormData, DTO.FrameMaterialOptionMng.EditFormData, DTO.FrameMaterialOptionMng.FrameMaterialOption>
    {
        private DataConverter converter = new DataConverter();
        private DAL.Support.DataFactory supportFactory = new Support.DataFactory();
        private string _TempFolder;
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

        public DataFactory(string tempFolder)
        {
            _TempFolder = tempFolder + @"\";
        }

        private FrameMaterialOptionMngEntities CreateContext()
        {
            return new FrameMaterialOptionMngEntities(DALBase.Helper.CreateEntityConnectionString("FrameMaterialOptionMngModel"));
        }

        public override DTO.FrameMaterialOptionMng.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.FrameMaterialOptionMng.SearchFormData data = new DTO.FrameMaterialOptionMng.SearchFormData();
            data.Data = new List<DTO.FrameMaterialOptionMng.FrameMaterialOptionSearchResult>();
            totalRows = 0;

            string FrameMaterialOptionUD = null;
            string FrameMaterialOptionNM = null;
            string Remark = null;
            string Season = null;
            bool? IsStandard = null;
            bool? IsEnabled = null;
            int? ProductGroupID = null;
            if (filters.ContainsKey("FrameMaterialOptionUD") && !string.IsNullOrEmpty(filters["FrameMaterialOptionUD"].ToString()))
            {
                FrameMaterialOptionUD = filters["FrameMaterialOptionUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("FrameMaterialOptionNM") && !string.IsNullOrEmpty(filters["FrameMaterialOptionNM"].ToString()))
            {
                FrameMaterialOptionNM = filters["FrameMaterialOptionNM"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("Remark") && !string.IsNullOrEmpty(filters["Remark"].ToString()))
            {
                Remark = filters["Remark"].ToString().Replace("'", "''");
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
                using (FrameMaterialOptionMngEntities context = CreateContext())
                {
                    totalRows = context.FrameMaterialOptionMng_function_SearchFrameMaterialOption(FrameMaterialOptionUD, FrameMaterialOptionNM, Remark, Season, IsStandard, IsEnabled, ProductGroupID, orderBy, orderDirection).Count();
                    var result = context.FrameMaterialOptionMng_function_SearchFrameMaterialOption(FrameMaterialOptionUD, FrameMaterialOptionNM, Remark, Season, IsStandard, IsEnabled, ProductGroupID, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_FrameMaterialOptionSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override DTO.FrameMaterialOptionMng.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.FrameMaterialOptionMng.EditFormData data = new DTO.FrameMaterialOptionMng.EditFormData();
            data.Data = new DTO.FrameMaterialOptionMng.FrameMaterialOption();
            data.Data.ProductGroups = new List<DTO.FrameMaterialOptionMng.ProductGroup>();
            data.Data.MaterialOptions = new List<DTO.FrameMaterialOptionMng.FrameMaterialOptionMaterialOption>();
            data.FrameMaterials = new List<DTO.Support.FrameMaterial>();
            data.FrameMaterialColors = new List<DTO.Support.FrameMaterialColor>();
            data.Seasons = new List<DTO.Support.Season>();

            //try to get data
            try
            {
                using (FrameMaterialOptionMngEntities context = CreateContext())
                {
                    if (id <= 0)
                    {
                        data.Data.ImageFile_DisplayUrl = FrameworkSetting.Setting.ThumbnailUrl + "no-image.jpg";

                        int index = -1;
                        foreach (DTO.Support.ProductGroup dtoGroup in supportFactory.GetProductGroup())
                        {
                            data.Data.ProductGroups.Add(new DTO.FrameMaterialOptionMng.ProductGroup() { FrameMaterialOptionProductGroupID = index, ProductGroupID = dtoGroup.ProductGroupID, ProductGroupNM = dtoGroup.ProductGroupNM, IsEnabled = false });
                            index--;
                        }
                    }
                    else
                    {
                        data.Data = converter.DB2DTO_FrameMaterialOption(context.FrameMaterialOptionMng_FrameMaterialOption_View.Include("FrameMaterialOptionMng_FrameMaterialOptionProductGroup_View").Include("FrameMaterialOptionMng_FrameMaterialOptionMaterialOption_View").FirstOrDefault(o => o.FrameMaterialOptionID == id));
                    }

                    data.FrameMaterialColors = supportFactory.GetFrameMaterialColor().ToList();
                    data.FrameMaterials = supportFactory.GetFrameMaterial().ToList();
                    data.Seasons = supportFactory.GetSeason().ToList();
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
                using (FrameMaterialOptionMngEntities context = CreateContext())
                {
                    FrameMaterialOption dbItem = context.FrameMaterialOption.FirstOrDefault(o => o.FrameMaterialOptionID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Frame material option not found!";
                        return false;
                    }
                    else
                    {
                        context.FrameMaterialOption.Remove(dbItem);
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

        public override bool UpdateData(int id, ref DTO.FrameMaterialOptionMng.FrameMaterialOption dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int number;
            string indexName;

            try
            {
                using (FrameMaterialOptionMngEntities context = CreateContext())
                {
                    FrameMaterialOption dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new FrameMaterialOption();
                        dbItem.CreatedBy = dtoItem.UpdatedBy;
                        dbItem.CreatedDate = System.DateTime.Now;
                        context.FrameMaterialOption.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.FrameMaterialOption.FirstOrDefault(o => o.FrameMaterialOptionID == id);
                    }                    

                    if (dbItem == null)
                    {
                        notification.Message = "Frame material option not found!";
                        return false;
                    }
                    else
                    {
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoItem.ConcurrencyFlag_String)))
                        {
                            throw new Exception(DALBase.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        // save data
                        converter.DTO2BD(dtoItem, ref dbItem);
                        context.FrameMaterialOptionMaterialOption.Local.Where(o => o.FrameMaterialOption == null).ToList().ForEach(o => context.FrameMaterialOptionMaterialOption.Remove(o));
                        dbItem.UpdatedBy = dtoItem.UpdatedBy;
                        dbItem.UpdatedDate = System.DateTime.Now;
                        context.SaveChanges();

                        // processing image
                        if (dtoItem.ImageFile_HasChange)
                        {
                            dbItem.ImageFile = fwFactory.CreateFilePointer(this._TempFolder, dtoItem.ImageFile_NewFile, dtoItem.ImageFile);
                        }                        
                        context.SaveChanges();

                        // Handle notification missing information.
                        string emailSubject = (id == 0) ? "TASK REQUEST [CREATE FRAME MATERIAL OPTION]" : "TASK REQUEST [UPDATE FRAME MATERIAL OPTION]";
                        string emailBody = string.Empty;

                        if (!IsNullPropertiesFrameMaterialOption(dbItem, ref emailBody))
                        {
                            SendToEmailNotification(context, emailSubject, emailBody);
                        }

                        dtoItem = GetData(dbItem.FrameMaterialOptionID, out notification).Data;

                        return true;
                    }
                }
            }
            catch (System.Data.DataException dEx)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                Library.ErrorHelper.DataExceptionParser(dEx, out number, out indexName);
                if (number == 2601 && !string.IsNullOrEmpty(indexName))
                {
                    if (indexName == "IX_FrameMaterialOption")
                    {
                        notification.Message = "The combination of Frame Material and Frame Material Color is already exists";
                    }
                }
                else
                {
                    notification.Message = dEx.Message;
                }

                return false;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }
        }

        public override bool Approve(int id, ref DTO.FrameMaterialOptionMng.FrameMaterialOption dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.FrameMaterialOptionMng.FrameMaterialOption dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public DTO.FrameMaterialOptionMng.SearchFilterData GetFilterData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.FrameMaterialOptionMng.SearchFilterData data = new DTO.FrameMaterialOptionMng.SearchFilterData();
            data.Seasons = new List<DTO.Support.Season>();
            data.YesNoValues = new List<DTO.Support.YesNo>();
            data.ProductGroups = new List<DTO.Support.ProductGroup>();

            //try to get data
            try
            {
                data.Seasons = supportFactory.GetSeason().ToList();
                data.YesNoValues = supportFactory.GetYesNo().ToList();
                data.ProductGroups = supportFactory.GetProductGroup().ToList();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public DTO.FrameMaterialOptionMng.EditFormData GetData(int id, int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.FrameMaterialOptionMng.EditFormData data = new DTO.FrameMaterialOptionMng.EditFormData();
            data.Data = new DTO.FrameMaterialOptionMng.FrameMaterialOption();
            data.Data.ProductGroups = new List<DTO.FrameMaterialOptionMng.ProductGroup>();
            data.Data.MaterialOptions = new List<DTO.FrameMaterialOptionMng.FrameMaterialOptionMaterialOption>();
            data.FrameMaterials = new List<DTO.Support.FrameMaterial>();
            data.FrameMaterialColors = new List<DTO.Support.FrameMaterialColor>();
            data.Seasons = new List<DTO.Support.Season>();

            //try to get data
            try
            {
                using (FrameMaterialOptionMngEntities context = CreateContext())
                {
                    if (id <= 0)
                    {
                        data.Data.ImageFile_DisplayUrl = FrameworkSetting.Setting.ThumbnailUrl + "no-image.jpg";
                        //data.Data.UpdatedBy = userId;
                        //data.Data.UpdatedDate = DateTime.Now.ToString("dd/MM/yyyy");

                        //var employee = context.Employee.Where(s => s.UserID.HasValue && s.UserID.Value == userId).FirstOrDefault();
                        //data.Data.UpdatorName = (employee == null) ? string.Empty : employee.EmployeeNM;

                        int index = -1;
                        foreach (DTO.Support.ProductGroup dtoGroup in supportFactory.GetProductGroup())
                        {
                            data.Data.ProductGroups.Add(new DTO.FrameMaterialOptionMng.ProductGroup() { FrameMaterialOptionProductGroupID = index, ProductGroupID = dtoGroup.ProductGroupID, ProductGroupNM = dtoGroup.ProductGroupNM, IsEnabled = false });
                            index--;
                        }
                    }
                    else
                    {
                        data.Data = converter.DB2DTO_FrameMaterialOption(context.FrameMaterialOptionMng_FrameMaterialOption_View.Include("FrameMaterialOptionMng_FrameMaterialOptionProductGroup_View").Include("FrameMaterialOptionMng_FrameMaterialOptionMaterialOption_View").FirstOrDefault(o => o.FrameMaterialOptionID == id));
                    }

                    data.FrameMaterialColors = supportFactory.GetFrameMaterialColor().ToList();
                    data.FrameMaterials = supportFactory.GetFrameMaterial().ToList();
                    data.Seasons = supportFactory.GetSeason().ToList();
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        private bool IsNullPropertiesFrameMaterialOption(FrameMaterialOption frameMaterialOption, ref string emailBody)
        {
            bool isNotNull = true;

            // Set content notification.
            emailBody += "Frame Material Option ID: " + frameMaterialOption.FrameMaterialOptionID;
            emailBody += Environment.NewLine + "Frame Material Option UD: " + frameMaterialOption.FrameMaterialOptionUD;
            emailBody += Environment.NewLine + "Frame Material Option NM: " + frameMaterialOption.FrameMaterialOptionNM;

            // Season is null or not null.
            if (string.IsNullOrEmpty(frameMaterialOption.Season))
            {
                emailBody += Environment.NewLine + "Season: Current value is null.";
                isNotNull = false;
            }
            else
            {
                emailBody += Environment.NewLine + "Season: " + frameMaterialOption.Season;
            }

            // Image is null or not null.
            if (string.IsNullOrEmpty(frameMaterialOption.ImageFile))
            {
                emailBody += Environment.NewLine + "Image: Current value is null.";
                isNotNull = false;
            }
            else
            {
                emailBody += Environment.NewLine + "Image: " + frameMaterialOption.ImageFile;
            }

            return isNotNull;
        }

        private void SendToEmailNotification(FrameMaterialOptionMngEntities context, string emailSubject, string emailBody)
        {
            try
            {
                string sendToEmail = string.Empty;
                //List<string> emailAddresses = new List<string>();

                // My(AVT)[20] and Thanh(AVT)[74].
                var emailAddresses = context.EmployeeMng_Employee_View.Where(o => o.UserID == 20 || o.UserID == 74).Select(s => new { s.Email1,s.UserID }).ToList();
                foreach (var emailAddress in emailAddresses)
                {
                    if (!string.IsNullOrEmpty(sendToEmail))
                        sendToEmail += "; ";

                    sendToEmail += emailAddress;
                    // add to NotificationMessage table
                    NotificationMessage notification1 = new NotificationMessage();
                    notification1.UserID = emailAddress.UserID;
                    notification1.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_PRODUCTDEVELOPMENT;
                    notification1.NotificationMessageTitle = emailSubject;
                    notification1.NotificationMessageContent = emailBody;
                    context.NotificationMessage.Add(notification1);
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

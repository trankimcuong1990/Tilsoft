using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.MaterialOptionMng
{
    public class DataFactory : DALBase.FactoryBase2<DTO.MaterialOptionMng.SearchFormData, DTO.MaterialOptionMng.EditFormData, DTO.MaterialOptionMng.MaterialOption>
    {
        private DataConverter converter = new DataConverter();
        private DAL.Support.DataFactory supportFactory = new Support.DataFactory();
        private string _TempFolder;
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

        public DataFactory(string tempFolder)
        {
            _TempFolder = tempFolder + @"\";
        }

        private MaterialOptionMngEntities CreateContext()
        {
            return new MaterialOptionMngEntities(DALBase.Helper.CreateEntityConnectionString("MaterialOptionMngModel"));
        }

        public override DTO.MaterialOptionMng.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.MaterialOptionMng.SearchFormData data = new DTO.MaterialOptionMng.SearchFormData();
            data.Data = new List<DTO.MaterialOptionMng.MaterialOptionSearchResult>();
            totalRows = 0;

            string MaterialOptionUD = null;
            string MaterialOptionNM = null;
            string Remark = null;
            string Season = null;
            bool? IsStandard = null;
            bool? IsEnabled = null;
            int? ProductGroupID = null;
            if (filters.ContainsKey("MaterialOptionUD") && !string.IsNullOrEmpty(filters["MaterialOptionUD"].ToString()))
            {
                MaterialOptionUD = filters["MaterialOptionUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("MaterialOptionNM") && !string.IsNullOrEmpty(filters["MaterialOptionNM"].ToString()))
            {
                MaterialOptionNM = filters["MaterialOptionNM"].ToString().Replace("'", "''");
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
                using (MaterialOptionMngEntities context = CreateContext())
                {
                    totalRows = context.MaterialOptionMng_function_SearchMaterialOption(MaterialOptionUD, MaterialOptionNM, Remark, Season, IsStandard, IsEnabled, ProductGroupID, orderBy, orderDirection).Count();
                    var result = context.MaterialOptionMng_function_SearchMaterialOption(MaterialOptionUD, MaterialOptionNM, Remark, Season, IsStandard, IsEnabled, ProductGroupID, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_MaterialOptionSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override DTO.MaterialOptionMng.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.MaterialOptionMng.EditFormData data = new DTO.MaterialOptionMng.EditFormData();
            data.Data = new DTO.MaterialOptionMng.MaterialOption();
            data.Data.ProductGroups = new List<DTO.MaterialOptionMng.ProductGroup>();
            data.MaterialColors = new List<DTO.Support.MaterialColor>();
            data.Materials = new List<DTO.Support.Material>();
            data.MaterialTypes = new List<DTO.Support.MaterialType>();
            data.Seasons = new List<DTO.Support.Season>();

            //try to get data
            try
            {
                using (MaterialOptionMngEntities context = CreateContext())
                {
                    if (id <= 0)
                    {
                        data.Data.ImageFile_DisplayUrl = FrameworkSetting.Setting.ThumbnailUrl + "no-image.jpg";

                        int index = -1;
                        foreach (DTO.Support.ProductGroup dtoGroup in supportFactory.GetProductGroup())
                        {
                            data.Data.ProductGroups.Add(new DTO.MaterialOptionMng.ProductGroup() { MaterialOptionProductGroupID = index, ProductGroupID = dtoGroup.ProductGroupID, ProductGroupNM = dtoGroup.ProductGroupNM, IsEnabled = false });
                            index--;
                        }
                    }
                    else
                    {
                        data.Data = converter.DB2DTO_MaterialOption(context.MaterialOptionMng_MaterialOption_View.FirstOrDefault(o => o.MaterialOptionID == id));
                    }

                    data.MaterialColors = supportFactory.GetMaterialColor().ToList();
                    data.Materials = supportFactory.GetMaterial().ToList();
                    data.MaterialTypes = supportFactory.GetMaterialType().ToList();
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
                using (MaterialOptionMngEntities context = CreateContext())
                {
                    MaterialOption dbItem = context.MaterialOption.FirstOrDefault(o => o.MaterialOptionID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Material not found!";
                        return false;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(dbItem.ImageFile))
                        {
                            // remove image
                            fwFactory.RemoveImageFile(dbItem.ImageFile);
                        }

                        context.MaterialOption.Remove(dbItem);
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

        public override bool UpdateData(int id, ref DTO.MaterialOptionMng.MaterialOption dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int number;
            string indexName;

            try
            {
                using (MaterialOptionMngEntities context = CreateContext())
                {
                    MaterialOption dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new MaterialOption();
                        context.MaterialOption.Add(dbItem);
                        dbItem.CreatedBy = dtoItem.UpdatedBy;
                        dbItem.CreatedDate = DateTime.Now;
                    }
                    else
                    {
                        dbItem = context.MaterialOption.FirstOrDefault(o => o.MaterialOptionID == id);
                        dbItem.UpdatedBy = dtoItem.UpdatedBy;
                        dbItem.UpdatedDate = DateTime.Now;
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Material not found!";
                        return false;
                    }
                    else
                    {
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoItem.ConcurrencyFlag_String)))
                        {
                            throw new Exception(DALBase.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        // process test report
                        foreach (var dtoReport in dtoItem.MaterialOptionTestReports.Where(o => o.TestReportHasChange))
                        {
                            dtoReport.FileUD = fwFactory.CreateNoneImageFilePointer(this._TempFolder, dtoReport.TestReportNewFile, dtoReport.FileUD);
                        }

                        // save data
                        converter.DTO2DB(dtoItem, ref dbItem);

                        context.MaterialOptionTestReport.Local.Where(o => o.MaterialOption == null).ToList().ForEach(o => context.MaterialOptionTestReport.Remove(o));
                        // processing image
                        if (dtoItem.ImageFile_HasChange)
                        {
                            dbItem.ImageFile = fwFactory.CreateFilePointer(this._TempFolder, dtoItem.ImageFile_NewFile, dtoItem.ImageFile);
                        }
                        context.SaveChanges();

                        // Handle notification missing information.
                        string emailSubject = (id == 0) ? "TASK REQUEST [CREATE MATERIAL OPTION]" : "TASK REQUEST [UPDATE MATERIAL OPTION]";
                        string emailBody = string.Empty;
                        
                        if (!IsNullPropertiesMaterialOption(dbItem, ref emailBody))
                        {
                            SendToEmailNotification(context, emailSubject, emailBody);
                        }

                        dtoItem = GetData(dbItem.MaterialOptionID, out notification).Data;

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
                    if (indexName == "MaterialTypeColorUnique")
                    {
                        notification.Message = "The combination of Material, Material Type and Material Color is already exists";
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

        public override bool Approve(int id, ref DTO.MaterialOptionMng.MaterialOption dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.MaterialOptionMng.MaterialOption dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public DTO.MaterialOptionMng.SearchFilterData GetFilterData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.MaterialOptionMng.SearchFilterData data = new DTO.MaterialOptionMng.SearchFilterData();
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

        public DTO.MaterialOptionMng.EditFormData GetData(int id, int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.MaterialOptionMng.EditFormData data = new DTO.MaterialOptionMng.EditFormData();
            data.Data = new DTO.MaterialOptionMng.MaterialOption
            {
                ProductGroups = new List<DTO.MaterialOptionMng.ProductGroup>(),
                MaterialOptionTestReports = new List<DTO.MaterialOptionMng.MaterialOptionTestReport>()
            };
            data.MaterialColors = new List<DTO.Support.MaterialColor>();
            data.Materials = new List<DTO.Support.Material>();
            data.MaterialTypes = new List<DTO.Support.MaterialType>();
            data.Seasons = new List<DTO.Support.Season>();

            data.Data.MaterialTestingDTOs = new List<DTO.MaterialOptionMng.MaterialTestingDTO>();

            //try to get data
            try
            {
                using (MaterialOptionMngEntities context = CreateContext())
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
                            data.Data.ProductGroups.Add(new DTO.MaterialOptionMng.ProductGroup() { MaterialOptionProductGroupID = index, ProductGroupID = dtoGroup.ProductGroupID, ProductGroupNM = dtoGroup.ProductGroupNM, IsEnabled = false });
                            index--;
                        }
                    }
                    else
                    {
                        data.Data = converter.DB2DTO_MaterialOption(context.MaterialOptionMng_MaterialOption_View.FirstOrDefault(o => o.MaterialOptionID == id));
                    }

                    data.MaterialColors = supportFactory.GetMaterialColor().ToList();
                    data.Materials = supportFactory.GetMaterial().ToList();
                    data.MaterialTypes = supportFactory.GetMaterialType().ToList();
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

        private bool IsNullPropertiesMaterialOption(MaterialOption materialOption, ref string emailBody)
        {
            bool isNotNull = true;

            // Set content notification.
            emailBody += "Material Option ID: " + materialOption.MaterialOptionID;
            emailBody += Environment.NewLine + "Material Option UD: " + materialOption.MaterialOptionUD;
            emailBody += Environment.NewLine + "Material Option NM: " + materialOption.MaterialOptionNM;

            // Season is null or not null.
            if (string.IsNullOrEmpty(materialOption.Season))
            {
                emailBody += Environment.NewLine + "Season: Current value is null.";
                isNotNull = false;
            }
            else
            {
                emailBody += Environment.NewLine + "Season: " + materialOption.Season;
            }

            // Image is null or not null.
            if (string.IsNullOrEmpty(materialOption.ImageFile))
            {
                emailBody += Environment.NewLine + "Image: Current value is null.";
                isNotNull = false;
            }
            else
            {
                emailBody += Environment.NewLine + "Image: " + materialOption.ImageFile;
            }

            return isNotNull;
        }

        private void SendToEmailNotification(MaterialOptionMngEntities context, string emailSubject, string emailBody)
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
                        NotificationMessage notification = new NotificationMessage();
                        notification.UserID = emailAddress.UserID;
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
    }
}

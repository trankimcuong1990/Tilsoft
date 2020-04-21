using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.SeatCushionMng
{
    public class DataFactory : DALBase.FactoryBase2<DTO.SeatCushionMng.SearchFormData, DTO.SeatCushionMng.EditFormData, DTO.SeatCushionMng.SeatCushion>
    {
        private DataConverter converter = new DataConverter();
        private Support.DataFactory supportFactory = new Support.DataFactory();
        private string _TempFolder;
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

        public DataFactory(string tempFolder)
        {
            _TempFolder = tempFolder + @"\";
        }
        private SeatCushionMngEntities CreateContext()
        {
            return new SeatCushionMngEntities(DALBase.Helper.CreateEntityConnectionString("SeatCushionMngModel"));
        }

        public override DTO.SeatCushionMng.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SeatCushionMng.SearchFormData data = new DTO.SeatCushionMng.SearchFormData();
            data.Data = new List<DTO.SeatCushionMng.SeatCushionSearchResult>();
            totalRows = 0;

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

        public override DTO.SeatCushionMng.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SeatCushionMng.EditFormData data = new DTO.SeatCushionMng.EditFormData();
            data.Data = new DTO.SeatCushionMng.SeatCushion() { ImageFile_DisplayUrl = FrameworkSetting.Setting.ThumbnailUrl + "no-image.jpg" };
            data.Data.ProductGroups = new List<DTO.SeatCushionMng.ProductGroup>();
            data.Seasons = new List<DTO.Support.Season>();

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
                        int index = -1;
                        foreach (DTO.Support.ProductGroup dtoGroup in supportFactory.GetProductGroup())
                        {
                            data.Data.ProductGroups.Add(new DTO.SeatCushionMng.ProductGroup() { SeatCushionProductGroupID = index, ProductGroupID = dtoGroup.ProductGroupID, ProductGroupNM = dtoGroup.ProductGroupNM, IsEnabled = false });
                            index--;
                        }
                    }

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

        public override bool UpdateData(int id, ref DTO.SeatCushionMng.SeatCushion dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (SeatCushionMngEntities context = CreateContext())
                {
                    SeatCushion dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new SeatCushion();
                        context.SeatCushion.Add(dbItem);
                    }
                    else
                    {
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
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoItem.ConcurrencyFlag_String)))
                        {
                            throw new Exception(DALBase.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }
                        converter.DTO2BD(dtoItem, ref dbItem);
                        // processing image
                        if (dtoItem.ImageFile_HasChange)
                        {
                            dbItem.ImageFile = fwFactory.CreateFilePointer(this._TempFolder, dtoItem.ImageFile_NewFile, dtoItem.ImageFile);
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

        public override bool Approve(int id, ref DTO.SeatCushionMng.SeatCushion dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.SeatCushionMng.SeatCushion dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public DTO.SeatCushionMng.SearchFilterData GetFilterData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SeatCushionMng.SearchFilterData data = new DTO.SeatCushionMng.SearchFilterData();
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

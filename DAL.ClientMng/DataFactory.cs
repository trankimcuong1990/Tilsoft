using Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace DAL.ClientMng
{
    public class DataFactory : DALBase.FactoryBase<DTO.ClientMng.ClientSearchResult, DTO.ClientMng.Client>
    {
        private DataConverter converter = new DataConverter();
        private string _tempFolder;
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
        private DAL.Support.DataFactory supportFactory = new Support.DataFactory();
        private string eurofarstockURL = System.Configuration.ConfigurationManager.AppSettings["eurofarstock_api_url"].ToString();
        private string eurofarstockToken = System.Configuration.ConfigurationManager.AppSettings["eurofarstock_api_token"].ToString();
        private System.Globalization.CultureInfo en = new System.Globalization.CultureInfo("en-US");

        private ClientMngEntities CreateContext()
        {
            var mContext = new ClientMngEntities(DALBase.Helper.CreateEntityConnectionString("ClientMngModel"));
            mContext.Database.CommandTimeout = 180;
            return mContext;
        }

        public DataFactory()
        { }

        public DataFactory(string tempFolder)
        {
            _tempFolder = tempFolder + @"\";
        }

        public override IEnumerable<DTO.ClientMng.ClientSearchResult> GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override DTO.ClientMng.Client GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try to get data
            try
            {
                using (ClientMngEntities context = CreateContext())
                {
                    DTO.ClientMng.Client dtoItem = new DTO.ClientMng.Client();

                    ClientMng_Client_View dbItem;
                    dbItem = context.ClientMng_Client_View
                        .Include("ClientMng_ClientContact_View")
                        .Include("ClientMng_ClientContactHistory_View")
                        .Include("ClientMng_DDCDetail_View")
                        .Include("ClientMng_ClientContract_View.ClientMng_ClientContractDetail_View")
                        .Include("ClientMng_Appointment_View")
                        .Include("ClientMng_ClientAdditionalCondition_View")
                        .Include("ClientMng_ClientBusinessCard_View")
                        .FirstOrDefault(o => o.ClientID == id);
                    DTO.ClientMng.Client clientData = converter.DB2DTO_Client(dbItem);
                    clientData.ClientContactHistories = clientData.ClientContactHistories.OrderByDescending(o => o.ClientContactHistoryID).ToList();
                    if (clientData.AppointmentDTOs == null) clientData.AppointmentDTOs = new List<DTO.ClientMng.AppointmentDTO>();
                    
                    dtoItem = clientData;
                    return dtoItem;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return new DTO.ClientMng.Client();
            }
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            return true;
            //try
            //{
            //    using (ClientMngEntities context = CreateContext())
            //    {
            //        Client dbItem = context.Client.FirstOrDefault(o => o.ClientID == id);
            //        if (dbItem == null)
            //        {
            //            notification.Message = "Client not found!";
            //            return false;
            //        }
            //        else
            //        {
            //            context.Client.Remove(dbItem);
            //            context.SaveChanges();
            //            return true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification.Type = Library.DTO.NotificationType.Error;
            //    notification.Message = ex.Message;
            //    if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
            //    {
            //        notification.DetailMessage.Add(ex.InnerException.Message);
            //    }
            //    return false;
            //}
        }

        public override bool UpdateData(int id, ref DTO.ClientMng.Client dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ClientMngEntities context = CreateContext())
                {
                    Client dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new Client();
                        dbItem.CreatedBy = dtoItem.UpdatedBy;
                        dbItem.CreatedDate = dtoItem.UpdatedDate;
                        context.Client.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.Client.FirstOrDefault(o => o.ClientID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Client not found!";
                        notification.Type = Library.DTO.NotificationType.Error;
                        return false;
                    }
                    else
                    {
                        if(id > 0)
                        {
                            if (dtoItem.AgentID != null)
                            {
                                foreach (var item in dtoItem.ClientEstimatedAdditionalCost)
                                {
                                    if (item.DefaultCommissionPercent == null && item.IsConfirmed == true && item.Season == Library.Helper.GetCurrentSeason())
                                    {
                                        notification.Message = "This client have Agent, please input % Default Commission";
                                        notification.Type = Library.DTO.NotificationType.Error;
                                        return false;
                                    }
                                }
                            }
                            else
                            {
                                foreach (var item in dtoItem.ClientEstimatedAdditionalCost)
                                {
                                    if (item.DefaultCommissionPercent != null && item.IsConfirmed == true)
                                    {
                                        notification.Message = "This client haven't Agent, please select Agent before input % Default Commission";
                                        notification.Type = Library.DTO.NotificationType.Error;
                                        return false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            //Gen Code
                            if (dtoItem.AutoGenerateCode.HasValue && dtoItem.AutoGenerateCode.Value == true)
                            {
                                dtoItem.ClientUD = context.ClientMng_function_GenerateCode().FirstOrDefault();
                            }
                            else
                            {
                                dtoItem.ClientUD = context.ClientMng_function_CheckExistsCode(dtoItem.ClientUD).FirstOrDefault();
                                if (dtoItem.ClientUD == "")
                                {
                                    notification.Message = "Client Code exists or over range";
                                    notification.Type = Library.DTO.NotificationType.Error;
                                    return false;
                                }
                            }
                        }
                        // Process business card image
                        foreach (DTO.ClientMng.ClientBusinessCardDTO dtoCard in dtoItem.ClientBusinessCardDTO)
                        {
                            if (dtoCard.FrontHasChange)
                            {
                                dtoCard.FrontFileUD = fwFactory.CreateFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + dtoItem.UpdatedBy.ToString() + @"\", dtoCard.FrontNewFile, dtoCard.FrontFileUD);
                            }

                            if (dtoCard.BehindHasChange)
                            {
                                dtoCard.BehindFileUD = fwFactory.CreateFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + dtoItem.UpdatedBy.ToString() + @"\", dtoCard.BehindNewFile, dtoCard.BehindFileUD);
                            }
                        }
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoItem.ConcurrencyFlag_String)))
                        {
                            throw new Exception(DALBase.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        //file processing
                        Library.FileHelper.FileManager fileMng = new Library.FileHelper.FileManager(FrameworkSetting.Setting.AbsoluteFileFolder);
                        string fileNeedDeleted = string.Empty;
                        string thumbnailFileNeedDeleted = string.Empty;

                        foreach (var contractItem in dtoItem.ClientContracts.Where(o => o.ContractFileHasChange))
                        {
                            if (!string.IsNullOrEmpty(contractItem.ContractFile))
                            {
                                fwFactory.GetDBFileLocation(contractItem.ContractFile, out fileNeedDeleted, out thumbnailFileNeedDeleted);
                                if (!string.IsNullOrEmpty(fileNeedDeleted))
                                {
                                    try
                                    {
                                        fileMng.DeleteFile(fileNeedDeleted);
                                    }
                                    catch { }
                                }
                            }

                            if (string.IsNullOrEmpty(contractItem.NewContractFile))
                            {
                                // remove file registration in File table
                                fwFactory.RemoveFile(contractItem.ContractFile);

                                // reset file in table Contract
                                contractItem.ContractFile = string.Empty;
                            }
                            else
                            {
                                string outDBFileLocation = "";
                                string outFileFullPath = "";
                                string outFilePointer = "";
                                // copy new file
                                fileMng.StoreFile(this._tempFolder + contractItem.NewContractFile, out outDBFileLocation, out outFileFullPath);


                                if (File.Exists(outFileFullPath))
                                {
                                    FileInfo info = new FileInfo(outFileFullPath);

                                    // insert/update file registration in database
                                    fwFactory.UpdateFile(contractItem.ContractFile, contractItem.NewContractFile, outDBFileLocation, info.Extension, "", (int)info.Length, out outFilePointer);

                                    // set file database pointer
                                    contractItem.ContractFile = outFilePointer;
                                }
                            }

                        }

                        foreach (var clientAdditionalConditionDTO in dtoItem.ClientAdditionalConditionDTO.Where(o => o.AttachFileHasChange))
                        {
                            if (!string.IsNullOrEmpty(clientAdditionalConditionDTO.AttachFile))
                            {
                                fwFactory.GetDBFileLocation(clientAdditionalConditionDTO.AttachFile, out fileNeedDeleted, out thumbnailFileNeedDeleted);
                                if (!string.IsNullOrEmpty(fileNeedDeleted))
                                {
                                    try
                                    {
                                        fileMng.DeleteFile(fileNeedDeleted);
                                    }
                                    catch { }
                                }
                            }

                            if (string.IsNullOrEmpty(clientAdditionalConditionDTO.NewAttachFile))
                            {
                                // remove file registration in File table
                                fwFactory.RemoveFile(clientAdditionalConditionDTO.AttachFile);

                                // reset file in table Contract
                                clientAdditionalConditionDTO.AttachFile = string.Empty;
                            }
                            else
                            {
                                string outDBFileLocation = "";
                                string outFileFullPath = "";
                                string outFilePointer = "";
                                // copy new file
                                fileMng.StoreFile(this._tempFolder + clientAdditionalConditionDTO.NewAttachFile, out outDBFileLocation, out outFileFullPath);


                                if (File.Exists(outFileFullPath))
                                {
                                    FileInfo info = new FileInfo(outFileFullPath);

                                    // insert/update file registration in database
                                    fwFactory.UpdateFile(clientAdditionalConditionDTO.AttachFile, clientAdditionalConditionDTO.NewAttachFile, outDBFileLocation, info.Extension, "", (int)info.Length, out outFilePointer);

                                    // set file database pointer
                                    clientAdditionalConditionDTO.AttachFile = outFilePointer;
                                }
                            }

                        }

                        if (dtoItem.HasChanged)
                        {
                            dbItem.LogoImage = fwFactory.CreateFilePointer(this._tempFolder, dtoItem.NewFile, dbItem.LogoImage);
                        }
                        //convert dto to db
                        converter.DTO2DB_Client(dtoItem, ref dbItem);                        

                        context.SaveChanges();
                        context.ClientSpecialPackagingMethod.Where(o => o.Client == null).ToList().ForEach(o => context.ClientSpecialPackagingMethod.Remove(o));
                        context.PenaltyTerm.Where(o => o.Client == null).ToList().ForEach(o => context.PenaltyTerm.Remove(o));
                        context.ClientEstimatedAdditionalCost.Where(o => o.Client == null).ToList().ForEach(o => context.ClientEstimatedAdditionalCost.Remove(o));
                        context.ClientAdditionalCondition.Where(o => o.Client == null).ToList().ForEach(o => context.ClientAdditionalCondition.Remove(o));
                        context.ClientBusinessCard.Local.Where(o => o.Client == null).ToList().ForEach(o => context.ClientBusinessCard.Remove(o));
                        context.SaveChanges();

                        if (id == 0)
                        {
                            context.ClientMng_function_PrepareEstimatedCostCurrentSeason();
                        }

                        dtoItem = GetData(dbItem.ClientID, out notification);
                        return true;
                    }
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
                return false;
            }
        }

        public bool SaveGIC (int id, List<DTO.ClientMng.ClientContactHistory> dtoItems, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ClientMngEntities context = CreateContext())
                {
                    if(dtoItems != null)
                    {
                        var dtoItem = dtoItems.FirstOrDefault(o => o.HasChanged);
                        if (dtoItem != null)
                        {
                            context.ClientContactHistory.Add(
                                new ClientContactHistory()
                                {
                                    ClientID = id,
                                    CommunicationContent = dtoItem.CommunicationContent,
                                    ContactDate = DateTime.Now
                                }
                            );
                            context.SaveChanges();
                        }
                    }
                    
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return false;
            }
        }

        public DTO.ClientMng.SearchFormData SearchClient(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ClientMng.SearchFormData searchFormData = new DTO.ClientMng.SearchFormData();
            totalRows = 0;

            string ClientUD = null;
            string ClientNM = null;
            string StreetAddress1 = null;
            string Telephone = null;
            string Email = null;
            string ClientCityNM = null;
            string ClientCountryNM = null;
            int? SaleID = null;
            int? Sale2ID = null;
            int? SaleVNID = null;
            int? SaleMerAssistID = null;
            int? SaleVN2ID = null;
            int? AgentID = null;
            int? PaymentTermID = null;
            int? DeliveryTermID = null;
            bool? IsActive = true;
            bool? IsBuyClient = null;
            bool? IsBuyingOrangePie = null;
            string BuyingOrganization = null;
            int? RelationshipTypeID = null;

            try
            {
                if (filters.ContainsKey("ClientUD") && !string.IsNullOrEmpty(filters["ClientUD"].ToString()))
                {
                    ClientUD = filters["ClientUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("ClientNM") && !string.IsNullOrEmpty(filters["ClientNM"].ToString()))
                {
                    ClientNM = filters["ClientNM"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("StreetAddress1") && !string.IsNullOrEmpty(filters["StreetAddress1"].ToString()))
                {
                    StreetAddress1 = filters["StreetAddress1"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("Telephone") && !string.IsNullOrEmpty(filters["Telephone"].ToString()))
                {
                    Telephone = filters["Telephone"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("Email") && !string.IsNullOrEmpty(filters["Email"].ToString()))
                {
                    Email = filters["Email"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("ClientCityNM") && !string.IsNullOrEmpty(filters["ClientCityNM"].ToString()))
                {
                    ClientCityNM = filters["ClientCityNM"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("ClientCountryNM") && !string.IsNullOrEmpty(filters["ClientCountryNM"].ToString()))
                {
                    ClientCountryNM = filters["ClientCountryNM"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("SaleID") && filters["SaleID"] != null && !string.IsNullOrEmpty(filters["SaleID"].ToString()))
                {
                    SaleID = Convert.ToInt32(filters["SaleID"].ToString());
                }
                if (filters.ContainsKey("Sale2ID") && filters["Sale2ID"] != null && !string.IsNullOrEmpty(filters["Sale2ID"].ToString()))
                {
                    Sale2ID = Convert.ToInt32(filters["Sale2ID"].ToString());
                }
                if (filters.ContainsKey("SaleVNID") && filters["SaleVNID"] != null && !string.IsNullOrEmpty(filters["SaleVNID"].ToString()))
                {
                    SaleVNID = Convert.ToInt32(filters["SaleVNID"].ToString());
                }
                if (filters.ContainsKey("SaleMerAssistID") && filters["SaleMerAssistID"] != null && !string.IsNullOrEmpty(filters["SaleMerAssistID"].ToString()))
                {
                    SaleMerAssistID = Convert.ToInt32(filters["SaleMerAssistID"].ToString());
                }
                if (filters.ContainsKey("SaleVN2ID") && filters["SaleVN2ID"] != null && !string.IsNullOrEmpty(filters["SaleVN2ID"].ToString()))
                {
                    SaleVN2ID = Convert.ToInt32(filters["SaleVN2ID"].ToString());
                }
                if (filters.ContainsKey("AgentID") && filters["AgentID"] != null && !string.IsNullOrEmpty(filters["AgentID"].ToString()))
                {
                    AgentID = Convert.ToInt32(filters["AgentID"].ToString());
                }
                if (filters.ContainsKey("PaymentTermID") && filters["PaymentTermID"] != null && !string.IsNullOrEmpty(filters["PaymentTermID"].ToString()))
                {
                    PaymentTermID = Convert.ToInt32(filters["PaymentTermID"].ToString());
                }
                if (filters.ContainsKey("DeliveryTermID") && filters["DeliveryTermID"] != null && !string.IsNullOrEmpty(filters["DeliveryTermID"].ToString()))
                {
                    DeliveryTermID = Convert.ToInt32(filters["DeliveryTermID"].ToString());
                }
                if (filters.ContainsKey("IsBuyClient") && filters["IsBuyClient"] != null && !string.IsNullOrEmpty(filters["IsBuyClient"].ToString()))
                {
                    IsBuyClient = (filters["IsBuyClient"].ToString() == "true") ? true : false;
                }
                if (filters.ContainsKey("IsActive") && filters["IsActive"] != null && !string.IsNullOrEmpty(filters["IsActive"].ToString()))
                {
                    IsActive = (filters["IsActive"].ToString() == "true") ? true : false;
                }
                if (filters.ContainsKey("IsBuyingOrangePie") && filters["IsBuyingOrangePie"] != null && !string.IsNullOrEmpty(filters["IsBuyingOrangePie"].ToString()))
                {
                    IsBuyingOrangePie = (filters["IsBuyingOrangePie"].ToString() == "true") ? true : false;
                }
                if (filters.ContainsKey("BuyingOrganization") && !string.IsNullOrEmpty(filters["BuyingOrganization"].ToString()))
                {
                    BuyingOrganization = filters["BuyingOrganization"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("RelationshipTypeID") && filters["RelationshipTypeID"] != null && !string.IsNullOrEmpty(filters["RelationshipTypeID"].ToString()))
                {
                    RelationshipTypeID = Convert.ToInt32(filters["RelationshipTypeID"].ToString());
                }

                //get user is login
                int userID = Convert.ToInt32(filters["userID"]);
                bool isSpecialPermission = fwFactory.HasSpecialPermission(userID, Module.Framework.ConstantIdentifier.SPECIAL_PERMISSION_CLIENTSPECIAL);
                bool isSaler = IsExistInSale(userID, out notification);

                // Person is special permission: Mr. Iron, Mr. Rob, Mr. Luc
                // Person is not special permission, but in list salers
                //if (isSpecialPermission || (!isSpecialPermission && isSaler)) {}
                searchFormData.HasPermissionOnGrossMargin = fwFactory.HasSpecialPermission(userID, Module.Framework.ConstantIdentifier.SPECIAL_PERMISSION_CLIENTSEARCH_GROSSMARGIN);
                using (ClientMngEntities context = CreateContext())
                {
                    // prepare est cost
                    context.ClientMng_function_PrepareEstimatedCostCurrentSeason();

                    //delta
                    var deltaTable = context.ClientMng_ClientSearchResult_View.ToList();

                    //cal total
                    searchFormData.PiConfirmedContThisYear_Total = deltaTable.Sum(o => o.PiConfirmedContThisYear);
                    searchFormData.PiConfirmedSaleAmountThisYear_Total = deltaTable.Sum(o => o.PiConfirmedSaleAmountThisYear);
                    searchFormData.PiConfirmedDelta9AmountThisYear_Total = deltaTable.Sum(o => o.PiConfirmedDelta9AmountThisYear);
                    searchFormData.PiConfirmedPurchasingAmountThisYear_Total = deltaTable.Sum(o => o.PiConfirmedPurchasingAmountThisYear);
                    if (searchFormData.PiConfirmedPurchasingAmountThisYear_Total != 0)
                    {
                        searchFormData.PiConfirmedDelta9PercentThisYear_Total = searchFormData.PiConfirmedDelta9AmountThisYear_Total * 100 / searchFormData.PiConfirmedPurchasingAmountThisYear_Total;
                    }

                    //cal sub total
                    var clientIDs = context.ClientMng_function_SearchClient(ClientUD, ClientNM, StreetAddress1, Telephone, Email, ClientCityNM, ClientCountryNM, SaleID, Sale2ID, SaleVNID, SaleMerAssistID, SaleVN2ID, AgentID, PaymentTermID, DeliveryTermID, IsActive, IsBuyClient, IsBuyingOrangePie, BuyingOrganization, RelationshipTypeID, userID, isSpecialPermission, isSaler, orderBy, orderDirection).Select(s => s.ClientID).ToList();
                    totalRows = clientIDs.Count();
                    var subTotalDelta = deltaTable.Where(o => clientIDs.Contains(o.ClientID)).ToList();
                    searchFormData.PiConfirmedContThisYear_SubTotal = subTotalDelta.Sum(o => o.PiConfirmedContThisYear);
                    searchFormData.PiConfirmedSaleAmountThisYear_SubTotal = subTotalDelta.Sum(o => o.PiConfirmedSaleAmountThisYear);
                    searchFormData.PiConfirmedDelta9AmountThisYear_SubTotal = subTotalDelta.Sum(o => o.PiConfirmedDelta9AmountThisYear);
                    searchFormData.PiConfirmedPurchasingAmountThisYear_SubTotal = subTotalDelta.Sum(o => o.PiConfirmedPurchasingAmountThisYear);
                    if (searchFormData.PiConfirmedPurchasingAmountThisYear_SubTotal != 0)
                    {
                        searchFormData.PiConfirmedDelta9PercentThisYear_SubTotal = searchFormData.PiConfirmedDelta9AmountThisYear_SubTotal * 100 / searchFormData.PiConfirmedPurchasingAmountThisYear_SubTotal;
                    }

                    //search result
                    totalRows = clientIDs.Count();
                    var result = context.ClientMng_function_SearchClient(ClientUD, ClientNM, StreetAddress1, Telephone, Email, ClientCityNM, ClientCountryNM, SaleID, Sale2ID, SaleVNID, SaleMerAssistID, SaleVN2ID, AgentID, PaymentTermID, DeliveryTermID, IsActive, IsBuyClient, IsBuyingOrangePie, BuyingOrganization, RelationshipTypeID, userID, isSpecialPermission, isSaler, orderBy, orderDirection);
                    searchFormData.Data = converter.DB2DTO_ClientSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                    
                    //get ExRate
                    string currentSeason = Library.Helper.GetCurrentSeason();
                    System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
                    searchFormData.ExRate = Convert.ToDecimal(context.SystemSetting.Where(o => o.Season == currentSeason && o.SettingKey == "ExRate-EUR-USD").FirstOrDefault().SettingValue, ci.NumberFormat);
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
            }
            //return data
            return searchFormData;
        }

        public DTO.ClientMng.DataContainer GetEditData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DAL.Support.DataFactory support_factory = new Support.DataFactory();

            //try to get data
            try
            {
                using (ClientMngEntities context = CreateContext())
                {
                    DTO.ClientMng.DataContainer dtoItem = new DTO.ClientMng.DataContainer();

                    if (id > 0)
                    {
                        ClientMng_Client_View dbItem;
                        dbItem = context.ClientMng_Client_View
                                .Include("ClientMng_ClientContact_View")
                                .Include("ClientMng_ClientContactHistory_View")
                                .Include("ClientMng_DDCDetail_View")
                                .Include("ClientMng_ClientContract_View.ClientMng_ClientContractDetail_View")
                                .Include("ClientMng_Appointment_View")
                                .Include("ClientMng_ClientAdditionalCondition_View")
                                .Include("ClientMng_ClientAdditionalConditionGetdata_View")
                                .Include("ClientMng_ClientBusinessCard_View")
                                .Include("SupportMng_User2_View")
                                .FirstOrDefault(o => o.ClientID == id);
                        DTO.ClientMng.Client clientData = converter.DB2DTO_Client(dbItem);
                        clientData.ClientContactHistories = clientData.ClientContactHistories.OrderByDescending(o => o.ClientContactHistoryID).ToList();
                        clientData.ClientGICComment = converter.DB2DTO_ClientGICComment(context.ClientMng_GICShowContent_View.ToList());
                        dtoItem.Client = clientData;
                    }
                    else
                    {
                        dtoItem.Client = new DTO.ClientMng.Client();
                        dtoItem.Client.ClientContacts = new List<DTO.ClientMng.ClientContact>();
                        dtoItem.Client.ClientContactHistories = new List<DTO.ClientMng.ClientContactHistory>();
                        dtoItem.Client.DDCDetails = new List<DTO.ClientMng.DDCDetail>();
                        dtoItem.Client.ClientContracts = new List<DTO.ClientMng.ClientContract>();
                        dtoItem.Client.AppointmentDTOs = new List<DTO.ClientMng.AppointmentDTO>();
                        dtoItem.Client.ClientAdditionalConditionDTO = new List<DTO.ClientMng.ClientAdditionalConditionDTO>();
                        dtoItem.ClientAdditionalConditionTypeDTO = new List<DTO.ClientMng.ClientAdditionalConditionTypeDTO>();
                        dtoItem.Client.ClientBusinessCardDTO = new List<DTO.ClientMng.ClientBusinessCardDTO>();
                        dtoItem.UsersDTO = new List<DTO.ClientMng.UsersDTO>();
                    }

                    // get support data
                    dtoItem.Salers = support_factory.GetSaler().Where(o => o.UserID > 0).ToList();
                    //add company
                    dtoItem.Salers.ForEach(x => x.CompanyID = fwFactory.GetCompanyID(x.UserID));


                    dtoItem.AccountManagerDTOs = converter.DB2DTO_AccountManagerDTO(context.SupportMng_AccountManager_View.ToList());
                    dtoItem.PaymentTerms = support_factory.GetPaymentTerm().ToList();
                    dtoItem.DeliveryTerms = support_factory.GetDeliveryTerm().ToList();
                    dtoItem.Seasons = support_factory.GetSeason().ToList();
                    dtoItem.ClientRatings = support_factory.GetClientRating().ToList();
                    dtoItem.Agents = support_factory.GetAgents().ToList(); // new List<DTO.Support.Agents>();
                    dtoItem.ClientCities = support_factory.GetClientCity().ToList();
                    dtoItem.ClientCountries = support_factory.GetClientCountry().ToList();
                    dtoItem.ClientGroups = support_factory.GetClientGroup().ToList();
                    dtoItem.ClientRelationshipTypes = support_factory.GetClientRelationshipType().ToList();
                    dtoItem.ClientTypes = support_factory.GetClientType().ToList();
                    dtoItem.Languages = support_factory.GetClientLanguage().ToList();
                    dtoItem.ReportTemplates = support_factory.GetReportTemplate().ToList();
                    dtoItem.ClientDocumentTypeDTOs = converter.DB2DTO_ClientDocumentTypeDTO(context.SupportMng_ClientDocumentType_View.ToList());
                    
                    //dtoItem.ClientGetDataAdditionalConditionDTO = converter.DB2DTO_ClientGetDataAdditionalCondition(context.ClientMng_ClientAdditionalConditionGetdata_View.ToList());
                    dtoItem.ClientBusinessCardDTO = converter.DB2DTO_ClientBusinessCard(context.ClientMng_ClientBusinessCard_View.ToList());
                    dtoItem.UsersDTO = converter.DB2DTO_UsersDTO(context.SupportMng_User2_View.ToList());
                    dtoItem.ClientAdditionalConditionTypeDTO = converter.DB2DTO_ClientGetDataType(context.Support_ClientAdditionalCondition_View.ToList());

                    return dtoItem;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return new DTO.ClientMng.DataContainer();
            }
        }

        //
        // Change set custom code: MY001
        // Author: Nguyen The My
        // Created date: 09/04/2016
        // Description: print CIS report
        //
        public string PrintCIS(int ClientID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            PrintCISDataObject ds = new PrintCISDataObject();
            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("ClientMng_function_GetReportCIS", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@ClientID", ClientID);
                adap.TableMappings.Add("Table", "CIS_Client_View");
                adap.TableMappings.Add("Table1", "CIS_SalesProformaBySeason_View");
                adap.TableMappings.Add("Table2", "CIS_SalesProformaByItem_View");
                adap.TableMappings.Add("Table3", "CIS_LoadingPlanDetail_View");
                adap.TableMappings.Add("Table4", "CIS_PiceComparison_View");
                adap.Fill(ds);
                // PROCESS LOGO IMAGE
                PrintCISDataObject.CIS_Client_ViewRow hRow = ds.CIS_Client_View.FirstOrDefault();
                if (!hRow.IsLogoImagePathNull())
                {
                    hRow.LogoImagePath = FrameworkSetting.Setting.MediaThumbnailUrl + hRow.LogoImagePath;
                }
                else
                {
                    hRow.LogoImagePath = "[NONE]";
                }

                // PROCESS IMAGE FOR ITEMS
                foreach (PrintCISDataObject.CIS_SalesProformaByItem_ViewRow drItem in ds.CIS_SalesProformaByItem_View)
                {
                    if (!drItem.IsProductImageThumbnailLocationNull())
                    {
                        drItem.ProductImageThumbnailLocation = FrameworkSetting.Setting.MediaThumbnailUrl + drItem.ProductImageThumbnailLocation;
                    }
                    else
                    {
                        drItem.ProductImageThumbnailLocation = "[NONE]";
                    }
                }
                //DALBase.Helper.DevCreateReportXMLSource(ds, "CIS");
                // generate xml file
                //return DALBase.Helper.CreateReportFiles(ds, "CIS");
                return Library.Helper.CreateReportFileWithEPPlus2(ds, "CIS");
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
        //
        // End custom change set: MY001
        //

        public string ExportExcelClient(int userID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("ClientMng_function_SearchClient", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                if (filters.ContainsKey("ClientUD") && !string.IsNullOrEmpty(filters["ClientUD"].ToString()))
                {
                    adap.SelectCommand.Parameters.AddWithValue("@ClientUD", filters["ClientUD"].ToString().Replace("'", "''"));
                }
                if (filters.ContainsKey("ClientNM") && !string.IsNullOrEmpty(filters["ClientNM"].ToString()))
                {
                    adap.SelectCommand.Parameters.AddWithValue("@ClientNM", filters["ClientNM"].ToString().Replace("'", "''"));
                }
                if (filters.ContainsKey("StreetAddress1") && !string.IsNullOrEmpty(filters["StreetAddress1"].ToString()))
                {
                    adap.SelectCommand.Parameters.AddWithValue("@StreetAddress1", filters["StreetAddress1"].ToString().Replace("'", "''"));
                }
                if (filters.ContainsKey("Telephone") && !string.IsNullOrEmpty(filters["Telephone"].ToString()))
                {
                    adap.SelectCommand.Parameters.AddWithValue("@Telephone", filters["Telephone"].ToString().Replace("'", "''"));
                }
                if (filters.ContainsKey("Email") && !string.IsNullOrEmpty(filters["Email"].ToString()))
                {
                    adap.SelectCommand.Parameters.AddWithValue("@Email", filters["Email"].ToString().Replace("'", "''"));
                }
                if (filters.ContainsKey("ClientCityNM") && !string.IsNullOrEmpty(filters["ClientCityNM"].ToString()))
                {
                    adap.SelectCommand.Parameters.AddWithValue("@ClientCityNM", filters["ClientCityNM"].ToString().Replace("'", "''"));
                }
                if (filters.ContainsKey("ClientCountryNM") && !string.IsNullOrEmpty(filters["ClientCountryNM"].ToString()))
                {
                    adap.SelectCommand.Parameters.AddWithValue("@ClientCountryNM", filters["ClientCountryNM"].ToString().Replace("'", "''"));
                }
                if (filters.ContainsKey("SaleID") && filters["SaleID"] != null && !string.IsNullOrEmpty(filters["SaleID"].ToString()))
                {
                    adap.SelectCommand.Parameters.AddWithValue("@SaleID", Convert.ToInt32(filters["SaleID"].ToString()));
                }
                if (filters.ContainsKey("Sale2ID") && filters["Sale2ID"] != null && !string.IsNullOrEmpty(filters["Sale2ID"].ToString()))
                {
                    adap.SelectCommand.Parameters.AddWithValue("@Sale2ID", Convert.ToInt32(filters["Sale2ID"].ToString()));
                }
                if (filters.ContainsKey("SaleVNID") && filters["SaleVNID"] != null && !string.IsNullOrEmpty(filters["SaleVNID"].ToString()))
                {
                    adap.SelectCommand.Parameters.AddWithValue("@SaleVNID", Convert.ToInt32(filters["SaleVNID"].ToString()));
                }
                if (filters.ContainsKey("SaleVN2ID") && filters["SaleVN2ID"] != null && !string.IsNullOrEmpty(filters["SaleVN2ID"].ToString()))
                {
                    adap.SelectCommand.Parameters.AddWithValue("@SaleVN2ID", Convert.ToInt32(filters["SaleVN2ID"].ToString()));
                }
                if (filters.ContainsKey("AgentID") && filters["AgentID"] != null && !string.IsNullOrEmpty(filters["AgentID"].ToString()))
                {
                    adap.SelectCommand.Parameters.AddWithValue("@AgentID", Convert.ToInt32(filters["AgentID"].ToString()));
                }
                if (filters.ContainsKey("PaymentTermID") && filters["PaymentTermID"] != null && !string.IsNullOrEmpty(filters["PaymentTermID"].ToString()))
                {
                    adap.SelectCommand.Parameters.AddWithValue("@PaymentTermID", Convert.ToInt32(filters["PaymentTermID"].ToString()));
                }
                if (filters.ContainsKey("DeliveryTermID") && filters["DeliveryTermID"] != null && !string.IsNullOrEmpty(filters["DeliveryTermID"].ToString()))
                {
                    adap.SelectCommand.Parameters.AddWithValue("@DeliveryTermID", Convert.ToInt32(filters["DeliveryTermID"].ToString()));
                }
                if (filters.ContainsKey("IsBuyClient") && filters["IsBuyClient"] != null && !string.IsNullOrEmpty(filters["IsBuyClient"].ToString()))
                {
                    adap.SelectCommand.Parameters.AddWithValue("@IsBuyClient", (filters["IsBuyClient"].ToString() == "true") ? true : false);
                }
                if (filters.ContainsKey("IsActive") && filters["IsActive"] != null && !string.IsNullOrEmpty(filters["IsActive"].ToString()))
                {
                    adap.SelectCommand.Parameters.AddWithValue("@IsActive", (filters["IsActive"].ToString() == "true") ? true : false);
                }
                if (filters.ContainsKey("RelationshipTypeID") && filters["RelationshipTypeID"] != null && !string.IsNullOrEmpty(filters["RelationshipTypeID"].ToString()))
                {
                    adap.SelectCommand.Parameters.AddWithValue("@RelationshipTypeID", Convert.ToInt32(filters["RelationshipTypeID"].ToString()));
                }

                bool isSpecialPermission = fwFactory.HasSpecialPermission(userID, Module.Framework.ConstantIdentifier.SPECIAL_PERMISSION_CLIENTSPECIAL);
                bool isSaler = IsExistInSale(userID, out notification);

                adap.SelectCommand.Parameters.AddWithValue("@UserID", userID);
                adap.SelectCommand.Parameters.AddWithValue("@IsSpecialPermission", isSpecialPermission);
                adap.SelectCommand.Parameters.AddWithValue("@IsSaler", isSaler);

                adap.TableMappings.Add("Table", "ClientMng_ClientSearchResult_View");
                adap.Fill(ds);
                return Library.Helper.CreateReportFileWithEPPlus(ds, "Client", new List<string>() { "ClientMng_ClientSearchResult_View" });
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

        public DTO.ClientMng.DataContainer GetEditData(int id, int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DAL.Support.DataFactory support_factory = new Support.DataFactory();

            try
            {
                using (ClientMngEntities context = CreateContext())
                {
                    DTO.ClientMng.DataContainer dtoItem = new DTO.ClientMng.DataContainer();
                    if (id > 0)
                    {
                        ClientMng_Client_View dbItem;
                        dbItem = context.ClientMng_Client_View
                                .Include("ClientMng_ClientContact_View")
                                .Include("ClientMng_ClientContactHistory_View")
                                .Include("ClientMng_DDCDetail_View")
                                .Include("ClientMng_ClientContract_View.ClientMng_ClientContractDetail_View")
                                .Include("ClientMng_Appointment_View")
                                .Include("ClientMng_ClientSpecialPackagingMethod_View")
                                .Include("ClientMng_ClientEstimatedAdditionalCost_View")
                                .Include("ClientMng_ClientAdditionalCondition_View")
                                .Include("ClientMng_PenaltyTerm_View")
                                .Include("ClientMng_ClientBusinessCard_View")                                
                                .FirstOrDefault(o => o.ClientID == id);
                        DTO.ClientMng.Client clientData = converter.DB2DTO_Client(dbItem);
                        clientData.ClientContactHistories = clientData.ClientContactHistories.OrderByDescending(o => o.ClientContactHistoryID).ToList();
                        dtoItem.Client = clientData;
                        dtoItem.UsersDTO = converter.DB2DTO_UsersDTO(context.SupportMng_User2_View.ToList());
                        dtoItem.ClientAdditionalConditionTypeDTO = converter.DB2DTO_ClientGetDataType(context.Support_ClientAdditionalCondition_View.ToList());
                        //clientData.ClientGICComment = converter.DB2DTO_ClientGICComment(context.ClientMng_GICShowContent_View.OrderByDescending(o=>o.ContactDate).ToList());
                    }
                    else
                    {
                        dtoItem.Client = new DTO.ClientMng.Client();
                        dtoItem.Client.ClientContacts = new List<DTO.ClientMng.ClientContact>();
                        dtoItem.Client.ClientContactHistories = new List<DTO.ClientMng.ClientContactHistory>();
                        dtoItem.Client.DDCDetails = new List<DTO.ClientMng.DDCDetail>();
                        dtoItem.Client.ClientContracts = new List<DTO.ClientMng.ClientContract>();
                        dtoItem.Client.AppointmentDTOs = new List<DTO.ClientMng.AppointmentDTO>();
                        dtoItem.Client.ClientSpecialPackagingMethods = new List<DTO.ClientMng.ClientSpecialPackagingMethod>();
                        dtoItem.Client.PenaltyTerms = new List<DTO.ClientMng.PenaltyTermDTO>();
                        dtoItem.Client.ClientAdditionalConditionDTO = new List<DTO.ClientMng.ClientAdditionalConditionDTO>();
                        dtoItem.Client.ClientBusinessCardDTO = new List<DTO.ClientMng.ClientBusinessCardDTO>();
                        dtoItem.UsersDTO = new List<DTO.ClientMng.UsersDTO>();
                        dtoItem.ClientAdditionalConditionTypeDTO = new List<DTO.ClientMng.ClientAdditionalConditionTypeDTO>();
                    }
                    
                    // get support data
                    //dtoItem.Salers = support_factory.GetSaler().Where(o=>o != null).Where(o => o.UserID > 0).ToList();
                    dtoItem.Salers = support_factory.GetSaler().Where(o => o.UserID > 0).ToList();
                    //add company
                    dtoItem.Salers.ForEach(x => x.CompanyID = fwFactory.GetCompanyID(x.UserID));

                    dtoItem.AccountManagerDTOs = converter.DB2DTO_AccountManagerDTO(context.SupportMng_AccountManager_View.ToList());
                    dtoItem.PaymentTerms = support_factory.GetPaymentTerm().ToList();
                    dtoItem.DeliveryTerms = support_factory.GetDeliveryTerm().ToList();
                    dtoItem.Seasons = support_factory.GetSeason().ToList();
                    dtoItem.ClientRatings = support_factory.GetClientRating().ToList();
                    dtoItem.Agents = support_factory.GetAgents().ToList(); // new List<DTO.Support.Agents>();
                    dtoItem.ClientCities = support_factory.GetClientCity().ToList();
                    dtoItem.ClientCountries = support_factory.GetClientCountry().ToList();
                    dtoItem.ClientGroups = support_factory.GetClientGroup().ToList();
                    dtoItem.ClientRelationshipTypes = support_factory.GetClientRelationshipType().ToList();
                    dtoItem.ClientTypes = support_factory.GetClientType().ToList();
                    dtoItem.Languages = support_factory.GetClientLanguage().ToList();
                    dtoItem.ReportTemplates = support_factory.GetReportTemplate().ToList();
                    dtoItem.ClientDocumentTypeDTOs = converter.DB2DTO_ClientDocumentTypeDTO(context.SupportMng_ClientDocumentType_View.ToList());

                    //dtoItem.ClientGetDataAdditionalConditionDTO = converter.DB2DTO_ClientGetDataAdditionalCondition(context.ClientMng_ClientAdditionalConditionGetdata_View.ToList());
                    dtoItem.UsersDTO = converter.DB2DTO_UsersDTO(context.SupportMng_User2_View.ToList());
                    dtoItem.ClientBusinessCardDTO = converter.DB2DTO_ClientBusinessCard(context.ClientMng_ClientBusinessCard_View.ToList());
                    dtoItem.ClientAdditionalConditionTypeDTO = converter.DB2DTO_ClientGetDataType(context.Support_ClientAdditionalCondition_View.ToList());
                    dtoItem.Client.HasSpecialPermission = fwFactory.HasSpecialPermission(userId, Module.Framework.ConstantIdentifier.SPECIAL_PERMISSION_CLIENTSPECIAL);
                    
                    return dtoItem;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return new DTO.ClientMng.DataContainer();
            }
        }

        public DTO.ClientMng.SearchFilterData GetSearchFilter(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ClientMng.SearchFilterData searchData = new DTO.ClientMng.SearchFilterData();
            try
            {
                using (ClientMngEntities context = CreateContext())
                {
                    searchData.salers = converter.DB2DTO_AccountManagerDTO(context.SupportMng_AccountManager_View.ToList());
                }

                searchData.buyingClient = supportFactory.GetYesNo().ToList();
                searchData.agents = supportFactory.GetAgents().ToList();
                searchData.deliveryTerms = supportFactory.GetDeliveryTerm().ToList();
                searchData.PaymentTerms = supportFactory.GetPaymentTerm().ToList();
                searchData.ClientRelationshipTypes = supportFactory.GetClientRelationshipType().ToList();
                searchData.Countries = supportFactory.GetClientCountry().ToList();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }
            return searchData;
        }

        public DTO.ClientMng.DataContainer GetDataClientAdditionalCondition(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ClientMng.DataContainer data = new DTO.ClientMng.DataContainer();
            try
            {
                using (ClientMngEntities context = CreateContext())
                {
                    data.ClientGetDataAdditionalConditionDTO = converter.DB2DTO_ClientGetDataAdditionalCondition(context.ClientMng_ClientAdditionalConditionGetdata_View.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }
            return data;
        }

        // Get client overview data
        public DTO.ClientMng.ClientOverview GetClientOverview(int id, int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DAL.Support.DataFactory support_factory = new Support.DataFactory();

            try
            {
                DTO.ClientMng.ClientOverview dtoItem = new DTO.ClientMng.ClientOverview();

                using (ClientMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        ClientMng_Overview_Client_View dbItem;
                        dbItem = context.ClientMng_Overview_Client_View
                                .Include("ClientMng_Overview_Appointment_View")
                                .Include("ClientMng_Overview_ClientContact_View")
                                .Include("ClientMng_Overview_ClientContract_View")
                                .Include("ClientMng_Overview_PenaltyTerm_View")
                                .Include("ClientMng_Overview_DDC_View")
                                .Include("ClientMng_Overview_ClientContactHistory_View")
                                .Include("ClientMng_Overview_ClientBusinessCard_View")
                                .Include("ClientMng_Overview_ClientAdditionalCondition_View")
                                .FirstOrDefault(o => o.ClientID == id);
                        //dbItem.ClientMng_Overview_Offer_View = dbItem.ClientMng_Overview_Offer_View.OrderByDescending(o => o.UpdatedDate).ToList();
                        dbItem.ClientMng_Overview_Appointment_View = dbItem.ClientMng_Overview_Appointment_View.OrderByDescending(o => o.StartTime).ToList();
                        dtoItem = converter.DB2DTO_ClientOverview(dbItem);
                        dtoItem.ClientContactHistories = dtoItem.ClientContactHistories.OrderByDescending(o => o.ClientContactHistoryID).ToList();

                        //get chart data
                        if (fwFactory.HasSpecialPermission(userId, Module.Framework.ConstantIdentifier.SPECIAL_PERMISSION_CLIENTOVERVIEW_CI))
                        {
                            dtoItem.HasPermissionOnCommercialInvoiceChart = true;
                            var dbChartCI = context.ClientMng_Overview_function_GetChartCommercialInvoice(id).ToList();
                            dtoItem.ChartCommercialInvoices = AutoMapper.Mapper.Map<List<ClientMng_Overview_ChartCommercialInvoice_View>, List<DTO.ClientMng.ChartCommercialInvoice>>(dbChartCI);
                        }
                        else
                        {
                            dtoItem.HasPermissionOnCommercialInvoiceChart = false;
                            dtoItem.ChartCommercialInvoices = new List<DTO.ClientMng.ChartCommercialInvoice>();
                        }
                    }

                    dtoItem.ClientComplaintStatuses = AutoMapper.Mapper.Map<List<ClientMng_Overview_ComplaintStatus_View>, List<DTO.ClientMng.ClientComplaintStatusDTO>>(context.ClientMng_Overview_ComplaintStatus_View.ToList());
                    dtoItem.ClientComplaintTypes = AutoMapper.Mapper.Map<List<ClientMng_Overview_ComplaintType_View>, List<DTO.ClientMng.ClientComplaintTypeDTO>>(context.ClientMng_Overview_ComplaintType_View.ToList());
                }
                var result = dtoItem.ClientAdditionalConditionDTO.OrderBy(x => x.AdditionalConditionNM);
                int? type = 0;
                string name = null;
                List<DTO.ClientMng.ClientAdditionalConditionOverviewDTO> ClientAdditionalConditionOverviewDTO = new List<DTO.ClientMng.ClientAdditionalConditionOverviewDTO>();
                foreach (var item in result.ToList())
                {
                    if(type != item.AdditionalConditionTypeID && name != item.AdditionalConditionNM && item.IsSelected==true || (type == item.AdditionalConditionTypeID && name != item.AdditionalConditionNM && item.IsSelected == true))
                    {
                        DTO.ClientMng.ClientAdditionalConditionOverviewDTO listB = new DTO.ClientMng.ClientAdditionalConditionOverviewDTO();
                        listB.AdditionalConditionTypeID = item.AdditionalConditionTypeID;
                        listB.AdditionalConditionNM = item.AdditionalConditionNM;
                        listB.Detail = item.Remark;
                        ClientAdditionalConditionOverviewDTO.Add(listB);
                        type = item.AdditionalConditionTypeID;
                        name = item.AdditionalConditionNM;
                    }
                }
                dtoItem.ClientAdditionalConditionOverviewDTO = ClientAdditionalConditionOverviewDTO;
                //// get CIS data
                //string currentSeason = Library.OMSHelper.Helper.GetCurrentSeason();
                //string prevSeason = Library.OMSHelper.Helper.GetPrevSeason(currentSeason);
                //dtoItem.ShippingPerformanceData = GetCISShippingPerformance(id, currentSeason, out notification);
                //dtoItem.ItemData = GetCISItem(id, currentSeason, out notification);
                //dtoItem.PriceComparisonData = GetCISPriceComparison(id, currentSeason, prevSeason, out notification);

                //Support.DataFactory supportedFactory = new Support.DataFactory();
                //dtoItem.Seasons = supportedFactory.GetSeason().ToList();
                //dtoItem.Seasons = new List<DTO.Support.Season>();

                return dtoItem;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return new DTO.ClientMng.ClientOverview();
            }
        }

        /// <summary>
        /// Get detail Offer, PLC, PI, ECI, SampleOrder, ClientComplaint.
        /// </summary>
        /// <param name="id"> ClientID </param>
        /// <param name="season"> Season </param>
        /// <param name="notification"></param>
        /// <returns></returns>
        public DTO.ClientMng.ClientOverview2 GetDetailClientOverview(int id, string season, out Library.DTO.Notification notification)
        {
            DTO.ClientMng.ClientOverview2 data = new DTO.ClientMng.ClientOverview2();

            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            try
            {
                using (var context = CreateContext())
                {
                    data.Offers = converter.DB2DTO_ClientOffer(context.ClientMng_Overview_Offer_View.Where(o => o.ClientID == id && o.Season.Equals(season)).ToList());
                    //data.PLCs = converter.DB2DTO_ClientPLC(context.ClientMng_Overview_PLC_View.Where(o => o.ClientID == id && o.Season.Equals(season)).ToList());
                    data.CommercialInvoices = converter.DB2DTO_CommercialInvoice(context.ClientMng_Overview_ECommercialInvoice_View.Where(o => o.ClientID == id && o.Season.Equals(season)).ToList());
                    data.SampleOrders = converter.DB2DTO_SampleOrder(context.ClientMng_Overview_SampleOrder_View.Where(o => o.ClientID == id && o.Season.Equals(season)).ToList());
                    data.ClientComplaints = converter.DB2DTO_ClientComplaint(context.ClientMng_Overview_ClientComplaint_View.Where(o => o.ClientID == id && o.YearSeason.Equals(season)).ToList());
                    data.SaleOrders = converter.DB2DTO_SaleOrder(context.ClientMng_Overview_SaleOrder_View.Where(o => o.ClientID == id && o.Season.Equals(season)).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;

                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }

                return new DTO.ClientMng.ClientOverview2();
            }

            return data;
        }

        public bool SetActivateStatus(int userId, int id, bool status, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ClientMngEntities context = CreateContext())
                {
                    Client dbItem = context.Client.FirstOrDefault(o => o.ClientID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("Client note found!");
                    }
                    dbItem.UpdatedBy = userId;
                    dbItem.UpdatedDate = DateTime.Now;
                    dbItem.IsActive = status;
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return false;
        }

        public DTO.ClientMng.ClientOverview2 GetOfferData(int id, out Library.DTO.Notification notification)
        {
            DTO.ClientMng.ClientOverview2 data = new DTO.ClientMng.ClientOverview2();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (var context = CreateContext())
                {
                    data.Offers = converter.DB2DTO_ClientOffer(context.ClientMng_Overview_Offer_View.Where(o => o.ClientID == id).OrderByDescending(o => o.UpdatedDate).OrderByDescending(o => o.Season).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public DTO.ClientMng.ClientOverview2 GetCIData(int id, out Library.DTO.Notification notification)
        {
            DTO.ClientMng.ClientOverview2 data = new DTO.ClientMng.ClientOverview2();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (var context = CreateContext())
                {
                    data.CommercialInvoices = converter.DB2DTO_CommercialInvoice(context.ClientMng_Overview_ECommercialInvoice_View.Where(o => o.ClientID == id).OrderByDescending(o => o.UpdatedDate).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public DTO.ClientMng.ClientOverview2 GetPLCData(int id, out Library.DTO.Notification notification)
        {
            DTO.ClientMng.ClientOverview2 data = new DTO.ClientMng.ClientOverview2();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (var context = CreateContext())
                {
                    List<int> plcIDs;
                    data.PLCs = converter.DB2DTO_ClientPLC(context.ClientMng_Overview_PLC_View.Where(o => o.ClientID == id).OrderByDescending(o => o.UpdatedDate).ToList());
                    plcIDs = data.PLCs.Select(o => o.PLCID.Value).ToList();
                    data.PLCSaleOrders = converter.DB2DTO_PLCSaleOrder(context.ClientMng_Overview_PLC_SaleOrder_View.Where(o => plcIDs.Contains(o.PLCID)).OrderBy(o => o.ProformaInvoiceNo).ToList());
                    data.PLCLoadingPLans = converter.DB2DTO_PLCLoadingPLan(context.ClientMng_Overview_PLC_LoadingPlan_View.Where(o => plcIDs.Contains(o.PLCID)).OrderBy(o => o.LoadingPlanUD).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public DTO.ClientMng.ClientOverview2 GetPIData(int id, out Library.DTO.Notification notification)
        {
            DTO.ClientMng.ClientOverview2 data = new DTO.ClientMng.ClientOverview2();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (var context = CreateContext())
                {
                    data.SaleOrders = converter.DB2DTO_SaleOrder(context.ClientMng_Overview_SaleOrder_View.Where(o => o.ClientID == id).OrderByDescending(o => o.UpdatedDate).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public DTO.ClientMng.ClientOverview2 GetClientComplainData(int id, out Library.DTO.Notification notification)
        {
            DTO.ClientMng.ClientOverview2 data = new DTO.ClientMng.ClientOverview2();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (var context = CreateContext())
                {
                    data.ClientComplaints = converter.DB2DTO_ClientComplaint(context.ClientMng_Overview_ClientComplaint_View.Where(o => o.ClientID == id).OrderByDescending(o => o.UpdatedDate).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public DTO.ClientMng.ClientOverview2 GetSampleOrderData(int id, out Library.DTO.Notification notification)
        {
            DTO.ClientMng.ClientOverview2 data = new DTO.ClientMng.ClientOverview2();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (var context = CreateContext())
                {
                    data.SampleOrders = converter.DB2DTO_SampleOrder(context.ClientMng_Overview_SampleOrder_View.Where(o => o.ClientID == id).OrderByDescending(o => o.Season).ThenByDescending(o => o.UpdatedDate).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public List<DTO.ClientMng.OfferLine> GetOfferLine(int id, out Library.DTO.Notification notification)
        {
            List<DTO.ClientMng.OfferLine> data = new List<DTO.ClientMng.OfferLine>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (var context = CreateContext())
                {
                    var x = context.ClientMng_Overview_OfferLine_View.Where(o => o.OfferID == id).ToList();
                    data = AutoMapper.Mapper.Map<List<ClientMng_Overview_OfferLine_View>, List<DTO.ClientMng.OfferLine>>(x);
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
            return data;
        }

        public List<DTO.ClientMng.SaleOrderDetail> GetSaleOrderDetail(int id, out Library.DTO.Notification notification)
        {
            List<DTO.ClientMng.SaleOrderDetail> data = new List<DTO.ClientMng.SaleOrderDetail>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (var context = CreateContext())
                {
                    var x = context.ClientMng_Overview_SaleOrderDetail_View.Where(o => o.SaleOrderID == id).ToList();
                    data = AutoMapper.Mapper.Map<List<ClientMng_Overview_SaleOrderDetail_View>, List<DTO.ClientMng.SaleOrderDetail>>(x);
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
            return data;
        }

        public List<DTO.ClientMng.ECommercialInvoiceDetail> GetECommercialInvoiceDetail(int id, out Library.DTO.Notification notification)
        {
            List<DTO.ClientMng.ECommercialInvoiceDetail> data = new List<DTO.ClientMng.ECommercialInvoiceDetail>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (var context = CreateContext())
                {
                    var x = context.ClientMng_Overview_ECommercialInvoiceDetail_View.Where(o => o.ECommercialInvoiceID == id).ToList();
                    data = AutoMapper.Mapper.Map<List<ClientMng_Overview_ECommercialInvoiceDetail_View>, List<DTO.ClientMng.ECommercialInvoiceDetail>>(x);
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
            return data;
        }

        public List<DTO.ClientMng.WarehouseInvoiceProductDetail> GetWarehouseInvoiceProductDetail(int id, out Library.DTO.Notification notification)
        {
            List<DTO.ClientMng.WarehouseInvoiceProductDetail> data = new List<DTO.ClientMng.WarehouseInvoiceProductDetail>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (var context = CreateContext())
                {
                    var x = context.ClientMng_Overview_WarehouseInvoiceProductDetail_View.Where(o => o.ECommercialInvoiceID == id).ToList();
                    data = AutoMapper.Mapper.Map<List<ClientMng_Overview_WarehouseInvoiceProductDetail_View>, List<DTO.ClientMng.WarehouseInvoiceProductDetail>>(x);
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
            return data;
        }

        public List<DTO.ClientMng.ECommercialInvoiceExtDetail> GetECommercialInvoiceExtDetail(int id, out Library.DTO.Notification notification)
        {
            List<DTO.ClientMng.ECommercialInvoiceExtDetail> data = new List<DTO.ClientMng.ECommercialInvoiceExtDetail>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (var context = CreateContext())
                {
                    var x = context.ClientMng_Overview_ECommercialInvoiceExtDetail_View.Where(o => o.ECommercialInvoiceID == id).ToList();
                    data = AutoMapper.Mapper.Map<List<ClientMng_Overview_ECommercialInvoiceExtDetail_View>, List<DTO.ClientMng.ECommercialInvoiceExtDetail>>(x);
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
            return data;
        }

        public DTO.ClientMng.EurofarstockAccountDTO CreateEurofarstockAccount(int contactId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = string.Empty };
            try
            {
                //
                // TODO
                //
                using (ClientMngEntities context = CreateContext())
                {
                    var contact = context.ClientContact.FirstOrDefault(o => o.ClientContactID == contactId);
                    if (contact == null)
                    {
                        throw new Exception("Client contact not found!");
                    }
                    if (string.IsNullOrEmpty(contact.Email))
                    {
                        throw new Exception("Invalid client contact email!");
                    }
                    if (string.IsNullOrEmpty(contact.FullName))
                    {
                        throw new Exception("Invalid client contact fullname!");
                    }

                    Library.DTO.FirstLastNameDTO contactName = Library.Common.Helper.ConvertFullnameToFirstLastName(contact.FullName);
                    if (contactName == null)
                    {
                        contactName = new Library.DTO.FirstLastNameDTO() { FirstName = ".", LastName = contact.FullName };
                    }

                    //eurofarstockURL = "http://localhost/magento217/rest/";
                    //eurofarstockToken = "72uu5kc82v7h7s0tijf79fnft2j7auev";
                    string errorMsg = string.Empty;
                    Communicator.Magento2.Controller mCtrl = new Communicator.Magento2.Controller(eurofarstockURL, eurofarstockToken);
                    Communicator.Magento2.DTO.POST.Customer data = new Communicator.Magento2.DTO.POST.Customer();

                    data.password = System.Guid.NewGuid().ToString().Replace("-", "").ToLower();
                    data.password = data.password.Substring(data.password.Length - 8, 8);

                    data.customer = new Communicator.Magento2.DTO.POST.CustomerDetail();
                    data.customer.email = contact.Email;
                    data.customer.firstname = contactName.FirstName;
                    data.customer.lastname = contactName.LastName;
                    //data.customer.addresses = new List<Communicator.Magento2.DTO.POST.CustomerAddress>();
                    //data.customer.addresses.Add(new Communicator.Magento2.DTO.POST.CustomerAddress()
                    //{
                    //    defaultBilling = true,
                    //    defaultShipping = true,
                    //    company = "Au Viet Soft",
                    //    city = "Ho Chi Minh",
                    //    countryId = "VN",
                    //    firstname = data.customer.firstname,
                    //    lastname = data.customer.lastname,
                    //    postcode = "700000",
                    //    telephone = "+84 909 105 720",
                    //    street = new List<string>() { "194.10D Bui Minh Truc, P5, Q8, HCMC" }
                    //});
                    Communicator.Magento2.DTO.EurofarstockAccount newAccount = mCtrl.CreateCustomer(data, out errorMsg);
                    if (newAccount == null)
                    {
                        throw new Exception(errorMsg);
                    }
                    contact.EurofarstockUserName = newAccount.Username;
                    contact.EurofarstockPassword = newAccount.Password;
                    context.SaveChanges();

                    return new DTO.ClientMng.EurofarstockAccountDTO() { Username = newAccount.Username, Password = newAccount.Password };
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return null;
        }

        public DTO.ClientMng.Overview.CIS.CISDataContainer GetCISData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            DTO.ClientMng.Overview.CIS.CISDataContainer data = new DTO.ClientMng.Overview.CIS.CISDataContainer();
            data.ItemData = new DTO.ClientMng.Overview.CIS.ItemDataContainer();
            data.ItemData.ItemDTOs = new List<DTO.ClientMng.Overview.CIS.ItemDTO>();
            data.ItemData.ModelDTOs = new List<DTO.ClientMng.Overview.CIS.ModelDTO>();
            data.PriceComparisonData = new DTO.ClientMng.Overview.CIS.PriceComparisonDataContainer();
            data.PriceComparisonData.PriceComparisonDTOs = new List<DTO.ClientMng.Overview.CIS.PriceComparisonDTO>();
            data.ShippingPerformanceData = new DTO.ClientMng.Overview.CIS.ShippingPerformanceDataContainer();
            data.ShippingPerformanceData.ShippingPerformanceChartDTOs = new List<DTO.ClientMng.Overview.CIS.ShippingPerformanceChartDTO>();
            data.ShippingPerformanceData.ShippingPerformanceChart2DTOs = new List<DTO.ClientMng.Overview.CIS.ShippingPerformanceChartDTO2>();
            data.ShippingPerformanceData.ShippingPerformanceDTOs = new List<DTO.ClientMng.Overview.CIS.ShippingPerformanceDTO>();

            try
            {
                using (ClientMngEntities context = CreateContext())
                {
                    // get CIS data
                    string currentSeason = Library.OMSHelper.Helper.GetCurrentSeason();
                    string prevSeason = Library.OMSHelper.Helper.GetPrevSeason(currentSeason);
                    data.ShippingPerformanceData = GetCISShippingPerformance(id, currentSeason, out notification);
                    data.ItemData = GetCISItem(id, currentSeason, out notification);
                    data.ItemData.ModelDTOs = GetCISModel2(id, currentSeason, out notification);
                    data.PriceComparisonData = GetCISPriceComparison(id, currentSeason, prevSeason, out notification);
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public DTO.ClientMng.Overview.Delta.DataContainer GetDeltaData(int userId, int id, string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ClientMng.Overview.Delta.DataContainer data = new DTO.ClientMng.Overview.Delta.DataContainer();
            data.EurofarPriceOverviewDTOs = new List<DTO.ClientMng.Overview.Delta.EurofarPriceOverviewDTO>();
            if (string.IsNullOrEmpty(season))
            {
                data.Season = Library.OMSHelper.Helper.GetCurrentSeason();
            }
            else
            {
                data.Season = season;
            }
            data.ExRate = Convert.ToDecimal(fwFactory.GetSystemSetting(Module.Framework.ConstantIdentifier.SYSTEM_SETTING_EXCHANGE_RATE, data.Season), en);

            if (fwFactory.HasSpecialPermission(userId, Module.Framework.ConstantIdentifier.SPECIAL_PERMISSION_CLIENTOVERVIEW_CI))
            {
                try
                {
                    using (ClientMngEntities context = CreateContext())
                    {
                        data.EurofarPriceOverviewDTOs = converter.DB2DTO_Overview_Delta_EurofarPriceOverviewDTO(
                            context
                            .ClientMng_Overview_EurofarPriceOverview_View
                            .Where(o => o.ClientID == id && o.Season == season)
                            .OrderByDescending(o => (o.UnitPrice * o.OrderQnt))
                            .ToList()
                        );
                    }
                }
                catch (Exception ex)
                {
                    notification.Type = Library.DTO.NotificationType.Error;
                    notification.Message = Library.Helper.GetInnerException(ex).Message;
                }
            }

            return data;
        }

        public DTO.ClientMng.Overview.Delta2.DataContainer GetDelta2Data(int userId, int id, string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ClientMng.Overview.Delta2.DataContainer data = new DTO.ClientMng.Overview.Delta2.DataContainer();
            data.EurofarPriceOverviewDTOs = new List<DTO.ClientMng.Overview.Delta2.EurofarPriceOverviewDTO>();
            if (string.IsNullOrEmpty(season))
            {
                data.Season = Library.OMSHelper.Helper.GetCurrentSeason();
            }
            else
            {
                data.Season = season;
            }
            data.ExRate = Convert.ToDecimal(fwFactory.GetSystemSetting(Module.Framework.ConstantIdentifier.SYSTEM_SETTING_EXCHANGE_RATE, data.Season), en);

            if (fwFactory.HasSpecialPermission(userId, Module.Framework.ConstantIdentifier.SPECIAL_PERMISSION_CLIENTOVERVIEW_CI))
            {
                try
                {
                    using (ClientMngEntities context = CreateContext())
                    {
                        data.EurofarPriceOverviewDTOs = converter.DB2DTO_Overview_Delta2_EurofarPriceOverviewDTO(
                            context
                            .ClientMng_Overview_EurofarPriceOverviewGroupByItem_View
                            .Where(o => o.ClientID == id && o.Season == season)
                            .OrderByDescending(o => o.TotalSaleAmount)
                            .ToList()
                        );
                    }
                }
                catch (Exception ex)
                {
                    notification.Type = Library.DTO.NotificationType.Error;
                    notification.Message = Library.Helper.GetInnerException(ex).Message;
                }
            }

            return data;
        }

        public DTO.ClientMng.Overview.Delta3.DataContainer GetDelta3Data(int userId, int clientID, string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ClientMng.Overview.Delta3.DataContainer data = new DTO.ClientMng.Overview.Delta3.DataContainer();
            data.EurofarPriceOverviewDTOs = new List<DTO.ClientMng.Overview.Delta3.EurofarPriceOverviewDTO>();
            if (string.IsNullOrEmpty(season))
            {
                data.Season = Library.OMSHelper.Helper.GetCurrentSeason();
            }
            else
            {
                data.Season = season;
            }
            data.ExRate = Convert.ToDecimal(fwFactory.GetSystemSetting(Module.Framework.ConstantIdentifier.SYSTEM_SETTING_EXCHANGE_RATE, data.Season), en);

            if (fwFactory.HasSpecialPermission(userId, Module.Framework.ConstantIdentifier.SPECIAL_PERMISSION_CLIENTOVERVIEW_CI))
            {
                try
                {
                    using (ClientMngEntities context = CreateContext())
                    {
                        var dbDelta3 = context.ClientMng_Overview_EurofarPriceOverviewGroupByModel_View.Where(o => o.ClientID == clientID && o.Season == season).OrderByDescending(s => s.TotalSaleAmount).ToList();
                        data.EurofarPriceOverviewDTOs = AutoMapper.Mapper.Map<List<ClientMng_Overview_EurofarPriceOverviewGroupByModel_View>, List<DTO.ClientMng.Overview.Delta3.EurofarPriceOverviewDTO>>(dbDelta3);
                    }
                }
                catch (Exception ex)
                {
                    notification.Type = Library.DTO.NotificationType.Error;
                    notification.Message = Library.Helper.GetInnerException(ex).Message;
                }
            }

            return data;
        }

        public DTO.ClientMng.Overview.CIS.ShippingPerformanceDataContainer GetCISShippingPerformance(int id, string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ClientMng.Overview.CIS.ShippingPerformanceDataContainer data = new DTO.ClientMng.Overview.CIS.ShippingPerformanceDataContainer();

            try
            {
                using (ClientMngEntities context = CreateContext())
                {
                    context.Database.CommandTimeout = 180;
                    data.Season = season;
                    data.ShippingPerformanceDTOs = converter.DB2DTO_Overview_ShippingPerformaceDTO(context.ClientMng_Overview_function_getShippingPerformance(id, season).OrderByDescending(o => o.LDS).ToList());
                    data.ShippingPerformanceChart2DTOs = converter.DB2DTO_Overview_ShippingPerformanceChartDTO2(context.ClientMng_Overview_function_getShippingPerformanceChartData2(season, id).ToList());
                    data.ShippingPerformanceChartDTOs = converter.DB2DTO_Overview_ShippingPerformanceChartDTO(context.ClientMng_Overview_function_getShippingPerformanceChartData(season, id).ToList());
                    var dbConclusion = context.ClientMng_Overview_function_getShippingPerformanceConclusion(id, season).FirstOrDefault();
                    data.Total20DC = dbConclusion.Total20DC.Value;
                    data.Total40DC = dbConclusion.Total40DC.Value;
                    data.Total40HC = dbConclusion.Total40HC.Value;
                    data.TotalUnknowContainerType = dbConclusion.TotalUnknowContainerType.Value;
                    data.TotalLateContainer = dbConclusion.TotalShippedNotOnTime.Value;
                    data.TotalOnTimeContainer = dbConclusion.TotalShippedOnTime.Value;
                    data.TotalNotYetShipped = dbConclusion.TotalNotYetShipped.Value;

                    data.TotalOrdered40HC = dbConclusion.TotalOrdered40HC.Value;
                    data.TotalShipped40HC = dbConclusion.TotalShipped40HC.Value;
                    data.TotalToBeShipped = dbConclusion.TotalToBeShipped40HC.Value;

                    data.AvgLateInDays = 0;
                    if (data.ShippingPerformanceDTOs.Count(o => o.OverDue < 0) > 0)
                    {
                        data.AvgLateInDays = (decimal)data.ShippingPerformanceDTOs.Where(o => o.OverDue < 0).Average(s => s.OverDue).Value;
                    }


                    // generate rating
                    int totalOntime = data.ShippingPerformanceChartDTOs.Where(o => o.LateInWeek <= 1).Sum(o => o.TotalContainer).Value;
                    int totalShipped = data.ShippingPerformanceChartDTOs.Sum(o => o.TotalContainer).Value;
                    double onTimePercent = Math.Round((double)totalOntime * 100 / totalShipped, 2, MidpointRounding.AwayFromZero);
                    if (onTimePercent >= 90)
                    {
                        data.FinalPerformanceConclusion = "GOOD";
                    }
                    else if (onTimePercent < 90 && onTimePercent >= 70)
                    {
                        data.FinalPerformanceConclusion = "OK";
                    }
                    else if (onTimePercent < 70)
                    {
                        data.FinalPerformanceConclusion = "NOT OK";
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public DTO.ClientMng.Overview.CIS.ItemDataContainer GetCISItem(int id, string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            decimal exchangeRate = 1;
            try
            {
                exchangeRate = Convert.ToDecimal(fwFactory.GetSystemSetting(Module.Framework.ConstantIdentifier.SYSTEM_SETTING_EXCHANGE_RATE, season), en);
            }
            catch
            {
                exchangeRate = 1;
            }

            DTO.ClientMng.Overview.CIS.ItemDataContainer data = new DTO.ClientMng.Overview.CIS.ItemDataContainer();

            try
            {
                using (ClientMngEntities context = CreateContext())
                {
                    data.Season = season;
                    data.ExRate = exchangeRate;
                    data.ItemDTOs = converter.DB2DTO_Overview_ItemDTO(context.ClientMng_Overview_Item_View.Where(o => o.ClientID == id && o.Season == season).OrderBy(o => o.ArticleCode).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public DTO.ClientMng.Overview.CIS.PriceComparisonDataContainer GetCISPriceComparison(int id, string season, string seasonToCompare, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            decimal exchangeRate = 1;
            try
            {
                exchangeRate = Convert.ToDecimal(fwFactory.GetSystemSetting(Module.Framework.ConstantIdentifier.SYSTEM_SETTING_EXCHANGE_RATE, season), en);
            }
            catch
            {
                exchangeRate = 1;
            }

            decimal exchangeRateToCompare = 1;
            try
            {
                exchangeRateToCompare = Convert.ToDecimal(fwFactory.GetSystemSetting(Module.Framework.ConstantIdentifier.SYSTEM_SETTING_EXCHANGE_RATE, seasonToCompare), en);
            }
            catch
            {
                exchangeRateToCompare = 1;
            }

            DTO.ClientMng.Overview.CIS.PriceComparisonDataContainer data = new DTO.ClientMng.Overview.CIS.PriceComparisonDataContainer();
            try
            {
                using (ClientMngEntities context = CreateContext())
                {
                    data.Season = season;
                    data.ExRate = exchangeRate;
                    data.SeasonToCompare = seasonToCompare;
                    data.ExRateToCompare = exchangeRateToCompare;
                    data.PriceComparisonDTOs = converter.DB2DTO_Overview_PriceComparisonDTO(context.ClientMng_Overview_function_getPriceCompareData(id, season, seasonToCompare).OrderBy(o => o.ArticleCode).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public List<DTO.ClientMng.Overview.LoadingPlanDTO> GetLoadingPlan(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.ClientMng.Overview.LoadingPlanDTO> data = new List<DTO.ClientMng.Overview.LoadingPlanDTO>();

            try
            {
                using (ClientMngEntities context = CreateContext())
                {
                    data = converter.DB2DTO_Overview_LoadingPlanDTO(context.ClientMng_Overview_LoadingPlan_View.Where(o => o.ClientID == id).OrderByDescending(o => o.Season).ThenByDescending(o => o.ETA).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public List<DTO.ClientMng.Overview.PISearchResultDTO> QuickSearchPI_ByArticleCode(string articleCode, int clientId, string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.ClientMng.Overview.PISearchResultDTO> data = new List<DTO.ClientMng.Overview.PISearchResultDTO>();
            try
            {
                using (ClientMngEntities context = CreateContext())
                {
                    data = converter.DB2DTO_Overview_PISearchResultDTO(context.ClientMng_Overview_function_getPIByArticleCode(clientId, season, articleCode).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return data;
        }
        public List<DTO.ClientMng.Overview.PISearchResultDTO> QuickSearchPI_ByCommercialInvoice(int commercialInvoiceId, int clientId, string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.ClientMng.Overview.PISearchResultDTO> data = new List<DTO.ClientMng.Overview.PISearchResultDTO>();
            try
            {
                using (ClientMngEntities context = CreateContext())
                {
                    data = converter.DB2DTO_Overview_PISearchResultDTO(context.ClientMng_Overview_function_getPIByCI(clientId, season, commercialInvoiceId).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return data;
        }
        public List<DTO.ClientMng.Overview.PISearchResultDTO> QuickSearchPI_ByLoadingPlan(int loadingPlanId, int clientId, string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.ClientMng.Overview.PISearchResultDTO> data = new List<DTO.ClientMng.Overview.PISearchResultDTO>();
            try
            {
                using (ClientMngEntities context = CreateContext())
                {
                    data = converter.DB2DTO_Overview_PISearchResultDTO(context.ClientMng_Overview_function_getPIByLoadingPlan(clientId, season, loadingPlanId).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return data;
        }

        public bool IsExistInSale(int userID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            try
            {
                var dtoItem = supportFactory.GetSaler();

                return (dtoItem.Where(o => o.UserID == userID).Count() > 0);
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;

                return false;
            }
        }

        // For breakdown Legend :(

        public DTO.ClientMng.Overview.Breakdown.SupportDataDTO BreakdownGetSearchFilter(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };

            DTO.ClientMng.Overview.Breakdown.SupportDataDTO data = new DTO.ClientMng.Overview.Breakdown.SupportDataDTO();
            data.quotationStatusDTOs = new List<DTO.ClientMng.Overview.Breakdown.QuotationStatusDTO>();
            data.orderTypeDTOs = new List<DTO.ClientMng.Overview.Breakdown.OrderTypeDTO>();
            data.breakdownCategoryDTOs = new List<DTO.ClientMng.Overview.Breakdown.BreakdownCategoryDTO>();

            try
            {
                using (var context = CreateContext())
                {
                    data.quotationStatusDTOs = converter.DB2DTO_QuotationSupportList(context.SupportMng_QuotationStatus_View.ToList());
                    data.orderTypeDTOs = converter.DB2DTO_OrderTypeSupport(context.SupportMng_OrderType_View.ToList());
                    data.breakdownCategoryDTOs = converter.DB2DTO_BreakdownCategory(context.ClientMng_BreakdownCategory_view.ToList());

                    //
                    // ADD CUSTOM STATUS AS REQUESTED - HARD CODE
                    //
                    List<int> toBeRemoveItems = new List<int>();
                    foreach (var item in data.quotationStatusDTOs)
                    {
                        if (item.QuotationStatusID != 1 && item.QuotationStatusID != 3)
                        {
                            toBeRemoveItems.Add(item.QuotationStatusID);
                        }
                    }
                    foreach (int id in toBeRemoveItems)
                    {
                        data.quotationStatusDTOs.Remove(data.quotationStatusDTOs.FirstOrDefault(o => o.QuotationStatusID == id));
                    }
                    data.quotationStatusDTOs.Add(new DTO.ClientMng.Overview.Breakdown.QuotationStatusDTO { QuotationStatusID = -1, QuotationStatusNM = "ITEMS COSTPRICE" });
                    data.quotationStatusDTOs.Add(new DTO.ClientMng.Overview.Breakdown.QuotationStatusDTO { QuotationStatusID = -2, QuotationStatusNM = "ITEMS NO COSTPRICE" });
                }
                using (var context = CreateContext())
                {
                    string currency = "USD";
                    var masterExchangeRate = context.MasterExchangeRateMng_function_GetExchangeRate(DateTime.Now, currency).FirstOrDefault();
                    data.Exchange = masterExchangeRate.ExchangeRate;
                }

            }
            catch (Exception ex)
            {

                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }
            return data;
        }

        public DTO.ClientMng.Overview.Breakdown.SearchFormDTO BreakdownGetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };

            DTO.ClientMng.Overview.Breakdown.SearchFormDTO data = new DTO.ClientMng.Overview.Breakdown.SearchFormDTO();
            data.breakdownCategoryOptionDTOs = new List<DTO.ClientMng.Overview.Breakdown.BreakdownCategoryOptionDTO>();
            data.breakdownManagementFeeDTOs = new List<DTO.ClientMng.Overview.Breakdown.BreakdownManagementFeeDTO>();
            data.breakdownSeasonSpecPercentDTOs = new List<DTO.ClientMng.Overview.Breakdown.BreakdownSeasonSpecPercentDTO>();

            totalRows = 0;

            string Season = null;
            string Description = null;
            string FactoryUD = null;
            string ProformaInvoiceNo = null;
            int? ItemTypeID = null;
            int? OrderTypeID = null;
            int? StatusID = null;
            int? QuotationOfferDirectionID = null;

            string ClientIDSt = filters["ClientID"].ToString();
            int ClientID = Convert.ToInt32(ClientIDSt);

            if (filters.ContainsKey("Season") && filters["Season"] != null && !string.IsNullOrEmpty(filters["Season"].ToString()))
            {
                Season = filters["Season"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("Description") && !string.IsNullOrEmpty(filters["Description"].ToString()))
            {
                Description = filters["Description"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("FactoryUD") && !string.IsNullOrEmpty(filters["FactoryUD"].ToString()))
            {
                FactoryUD = filters["FactoryUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("ProformaInvoiceNo") && !string.IsNullOrEmpty(filters["ProformaInvoiceNo"].ToString()))
            {
                ProformaInvoiceNo = filters["ProformaInvoiceNo"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("ItemTypeID") && filters["ItemTypeID"] != null && !string.IsNullOrEmpty(filters["ItemTypeID"].ToString()))
            {
                ItemTypeID = Convert.ToInt32(filters["ItemTypeID"].ToString());
            }
            if (filters.ContainsKey("StatusID") && filters["StatusID"] != null && !string.IsNullOrEmpty(filters["StatusID"].ToString()))
            {
                StatusID = Convert.ToInt32(filters["StatusID"].ToString());
            }
            if (filters.ContainsKey("OrderTypeID") && filters["OrderTypeID"] != null && !string.IsNullOrEmpty(filters["OrderTypeID"].ToString()))
            {
                OrderTypeID = Convert.ToInt32(filters["OrderTypeID"].ToString());
            }
            if (filters.ContainsKey("QuotationOfferDirectionID") && filters["QuotationOfferDirectionID"] != null && !string.IsNullOrEmpty(filters["QuotationOfferDirectionID"].ToString()))
            {
                QuotationOfferDirectionID = Convert.ToInt32(filters["QuotationOfferDirectionID"].ToString());
            }

            //Try to get Data 
            try
            {
                using (ClientMngEntities context = CreateContext())
                {
                    // get conclusion by season
                    //var dbConclusion = context.ClientMng_function_GetQuotaionConclusion(Season).FirstOrDefault();
                    //data.TotalItem = dbConclusion.TotalItem.Value;
                    //data.TotalConfirmedItem = dbConclusion.TotalConfirmedItem.Value;
                    //data.TotalWaitForEurofar = dbConclusion.TotalWaitEurofar.Value;
                    orderBy = null;
                    totalRows = context.ClientMng_Breakdown_function_SearchFactoryQuotation(Season, ClientID, Description, FactoryUD, ItemTypeID, StatusID, QuotationOfferDirectionID, OrderTypeID, ProformaInvoiceNo, orderBy, orderDirection).Count();
                    var result = context.ClientMng_Breakdown_function_SearchFactoryQuotation(Season, ClientID, Description, FactoryUD, ItemTypeID, StatusID, QuotationOfferDirectionID, OrderTypeID, ProformaInvoiceNo, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_SearchfactoryQuotation(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());

                    data.SubTotalRow = converter.DB2DTO_SubtotalDTO(context.ClientMng_function_GetSubTotalFactoryQuotation(Season, ClientID, Description, FactoryUD, ItemTypeID, StatusID, QuotationOfferDirectionID, OrderTypeID, ProformaInvoiceNo).FirstOrDefault());
                }
                using (ClientMngEntities context2 = CreateContext())
                {
                    var x = context2.ClientMng_function_GetSubTotalFactoryQuotation(Season, ClientID, null, null, null, null, null, null, null).FirstOrDefault();
                    data.TotalRow = converter.DB2DTO_SubtotalDTO(x);
                }

            }
            catch (Exception ex)
            {

                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            if (data.Data.Count() > 0)
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        List<int?> ModelIDs = data.Data.Select(o => o.ModelID).ToList();
                        var clientBreakdownCategoryOptionDTOs = context.ClientMng_BreakdownCategoryOption_View.Where(o => ModelIDs.Contains(o.ModelID)).ToList();
                        data.breakdownCategoryOptionDTOs = AutoMapper.Mapper.Map<List<ClientMng_BreakdownCategoryOption_View>, List<DTO.ClientMng.Overview.Breakdown.BreakdownCategoryOptionDTO>>(clientBreakdownCategoryOptionDTOs);
                    }
                }
                catch (Exception ex)
                {

                    notification.Type = Library.DTO.NotificationType.Error;
                    notification.Message = Library.Helper.GetInnerException(ex).Message;
                }
            }

            if (data.breakdownCategoryOptionDTOs.Count() > 0)
            {
                try
                {
                    using (var contex = CreateContext())
                    {
                        List<int?> BreakdownIDs = data.breakdownCategoryOptionDTOs.Select(o => o.BreakdownID).Distinct().ToList();
                        var clientBreakdownSeasonSpecPercent = contex.ClientMng_BreakdownSeasonSpecPercent_View.Where(o => BreakdownIDs.Contains(o.BreakdownID)).ToList();
                        data.breakdownSeasonSpecPercentDTOs = AutoMapper.Mapper.Map<List<ClientMng_BreakdownSeasonSpecPercent_View>, List<DTO.ClientMng.Overview.Breakdown.BreakdownSeasonSpecPercentDTO>>(clientBreakdownSeasonSpecPercent);

                        var clientBreakdownManagementFee = contex.ClientMng_BreakdownManagementFee_View.Where(o => BreakdownIDs.Contains(o.BreakdownID)).ToList();
                        data.breakdownManagementFeeDTOs = AutoMapper.Mapper.Map<List<ClientMng_BreakdownManagementFee_View>, List<DTO.ClientMng.Overview.Breakdown.BreakdownManagementFeeDTO>>(clientBreakdownManagementFee);
                    }
                }
                catch (Exception ex)
                {
                    notification.Type = Library.DTO.NotificationType.Error;
                    notification.Message = Library.Helper.GetInnerException(ex).Message;
                }
            }

            return data;
        }

        public DTO.ClientMng.Overview.CIS.ItemDataContainer GetCISModel(int id, string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            decimal exchangeRate = 1;
            try
            {
                exchangeRate = Convert.ToDecimal(fwFactory.GetSystemSetting(Module.Framework.ConstantIdentifier.SYSTEM_SETTING_EXCHANGE_RATE, season), en);
            }
            catch
            {
                exchangeRate = 1;
            }

            DTO.ClientMng.Overview.CIS.ItemDataContainer data = new DTO.ClientMng.Overview.CIS.ItemDataContainer();

            try
            {
                using (ClientMngEntities context = CreateContext())
                {
                    data.Season = season;
                    data.ExRate = exchangeRate;
                    data.ModelDTOs = converter.DB2DTO_Overview_ModelDTO(context.ClientMng_Overview_Model_View.Where(o => o.ClientID == id && o.Season == season).OrderBy(o => o.ModelUD).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public List<DTO.ClientMng.Overview.CIS.ModelDTO> GetCISModel2(int id, string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            DTO.ClientMng.Overview.CIS.ItemDataContainer data = new DTO.ClientMng.Overview.CIS.ItemDataContainer();

            try
            {
                using (ClientMngEntities context = CreateContext())
                {
                    data.ModelDTOs = converter.DB2DTO_Overview_ModelDTO(context.ClientMng_Overview_Model_View.Where(o => o.ClientID == id && o.Season == season).OrderBy(o => o.ModelUD).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data.ModelDTOs;
        }

        public List<DTO.ClientMng.ClientEstimatedAdditionalCostDTO> GetClientEstimatedAdditionalCost(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            List<DTO.ClientMng.ClientEstimatedAdditionalCostDTO> data = new List<DTO.ClientMng.ClientEstimatedAdditionalCostDTO>();

            try
            {
                using (var context = CreateContext())
                {
                    data = AutoMapper.Mapper.Map<List<ClientMng_ClientEstimatedAdditionalCost_View>, List<DTO.ClientMng.ClientEstimatedAdditionalCostDTO>>(context.ClientMng_ClientEstimatedAdditionalCost_View.Where(o => o.ClientID == id).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public List<DTO.ClientMng.ClientAdditionalConditionTypeDTO> GetClientAdditionalConditionType(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            List<DTO.ClientMng.ClientAdditionalConditionTypeDTO> data = new List<DTO.ClientMng.ClientAdditionalConditionTypeDTO>();

            try
            {
                using (var context = CreateContext())
                {
                    data = AutoMapper.Mapper.Map<List<Support_ClientAdditionalCondition_View>, List<DTO.ClientMng.ClientAdditionalConditionTypeDTO>>(context.Support_ClientAdditionalCondition_View.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public DTO.ClientMng.Overview.Offer.DataContainer GetOfferOverviewData(int id, string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;
            DTO.ClientMng.Overview.Offer.DataContainer data = new DTO.ClientMng.Overview.Offer.DataContainer();
            data.offerMarginDTOs = new List<DTO.ClientMng.Overview.Offer.OfferMarginDTO>();
            if (string.IsNullOrEmpty(season))
            {
                data.Season = Library.OMSHelper.Helper.GetCurrentSeason();
            }
            else
            {
                data.Season = season;
            }
            data.ExRate = Convert.ToDecimal(fwFactory.GetSystemSetting(Module.Framework.ConstantIdentifier.SYSTEM_SETTING_EXCHANGE_RATE, data.Season), en);
            try
            {
                using (var context = CreateContext())
                {
                    List<ClientMng_OfferMargin_View> dbItems = context.ClientMng_function_GetOfferMargin(season, id).OrderByDescending(o=>o.CreatedDate).ToList();

                    data.offerMarginDTOs = converter.DB2DTO_OfferMargin(dbItems);
                    //List<ClientMng_Offer_OverView> dbItems;
                    //dbItems= context.ClientMng_Offer_OverView
                    //    .Include("ClientMng_OfferLine_OverView")
                    //    .Include("ClientMng_OfferLineSparepart_OverView").Where(o=>o.ClientID == id && o.Season == season).OrderByDescending(o=>o.OfferCreatedDate).ToList();
                    //data.OfferDatas = converter.DB2DTO_OfferOverView(dbItems);
                    var temp = context.SupportMng_PlaningPurchasingPriceSource_View.ToList();
                    data.planingPurchasingPriceSourceDTOs = converter.DB2DTO_PlaningPurchasingPriceSourceDTO(temp);
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public List<DTO.ClientMng.Overview.Offer.OfferMarginDetailDTO> getOfferMarginDetail(int id, string season, int status, int clientID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            List<DTO.ClientMng.Overview.Offer.OfferMarginDetailDTO> data = new List<DTO.ClientMng.Overview.Offer.OfferMarginDetailDTO>();
            try
            {
                using (var context = CreateContext())
                {
                    if(status == 1)
                    {
                        List<ClientMng_OfferMarginDetail_View> dbItems = context.ClientMng_function_GetOfferMarginDetail(season, id, clientID).OrderByDescending(o => o.Quantity).ToList();
                        data = converter.DB2DTO_OfferMarginDetail(dbItems);
                    }
                    else if(status == 0)
                    {
                        List<ClientMng_OfferMarginDetail_View> dbItems = context.ClientMng_function_GetOfferMarginDetail_NoPrice(season, id, clientID).OrderByDescending(o => o.Quantity).ToList();
                        data = converter.DB2DTO_OfferMarginDetail(dbItems);
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public string DetalExportExcel(int clientId, string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("ClientMng_Overview_function_GetDeltaExcelData", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@ClientID", clientId);
                adap.SelectCommand.Parameters.AddWithValue("@Season", season);
                adap.Fill(ds.ClientMng_Overview_Delta_ExcelData_View);

                ReportDataObject.DeltaReportHeaderRow hRow = ds.DeltaReportHeader.NewDeltaReportHeaderRow();
                ds.DeltaReportHeader.AddDeltaReportHeaderRow(hRow);
                hRow.Season = season;
                hRow.ExchangeRate = 1;
                using (ClientMngEntities context = CreateContext())
                {
                    // get client info
                    var dbClient = context.Client.FirstOrDefault(o => o.ClientID == clientId);
                    if (dbClient != null)
                    {
                        hRow.ClientUD = dbClient.ClientUD;
                        hRow.ClientNM = dbClient.ClientNM;
                    }

                    // get exchange rate
                    var dbExchangeRate = context.SystemSetting.FirstOrDefault(o => o.Season == season && o.SettingKey == "ExRate-EUR-USD");
                    if (dbExchangeRate != null)
                    {
                        hRow.ExchangeRate = Convert.ToDecimal(dbExchangeRate.SettingValue, en);
                    }
                }                
                ds.AcceptChanges();

                return Library.Helper.CreateReportFileWithEPPlus(ds, "ClientOverviewDelta", new List<string>() { "ClientMng_Overview_Delta_ExcelData_View", "DeltaReportHeader" });
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

        public string Detal2ExportExcel(int clientId, string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("ClientMng_Overview_function_GetDelta2ExcelData", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@ClientID", clientId);
                adap.SelectCommand.Parameters.AddWithValue("@Season", season);
                adap.Fill(ds.ClientMng_Overview_EurofarPriceOverviewGroupByItem_View);

                ReportDataObject.DeltaReportHeaderRow hRow = ds.DeltaReportHeader.NewDeltaReportHeaderRow();
                ds.DeltaReportHeader.AddDeltaReportHeaderRow(hRow);
                hRow.Season = season;
                hRow.ExchangeRate = 1;
                using (ClientMngEntities context = CreateContext())
                {
                    // get client info
                    var dbClient = context.Client.FirstOrDefault(o => o.ClientID == clientId);
                    if (dbClient != null)
                    {
                        hRow.ClientUD = dbClient.ClientUD;
                        hRow.ClientNM = dbClient.ClientNM;
                    }

                    // get exchange rate
                    var dbExchangeRate = context.SystemSetting.FirstOrDefault(o => o.Season == season && o.SettingKey == "ExRate-EUR-USD");
                    if (dbExchangeRate != null)
                    {
                        hRow.ExchangeRate = Convert.ToDecimal(dbExchangeRate.SettingValue, en);
                    }
                }
                ds.AcceptChanges();

                return Library.Helper.CreateReportFileWithEPPlus(ds, "ClientOverviewDelta2", new List<string>() { "ClientMng_Overview_EurofarPriceOverviewGroupByItem_View", "DeltaReportHeader" });
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

        public string Detal3ExportExcel(int clientId, string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("ClientMng_Overview_function_GetDelta3ExcelData", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@ClientID", clientId);
                adap.SelectCommand.Parameters.AddWithValue("@Season", season);
                adap.Fill(ds.ClientMng_Overview_EurofarPriceOverviewGroupByModel_View);

                ReportDataObject.DeltaReportHeaderRow hRow = ds.DeltaReportHeader.NewDeltaReportHeaderRow();
                ds.DeltaReportHeader.AddDeltaReportHeaderRow(hRow);
                hRow.Season = season;
                hRow.ExchangeRate = 1;
                using (ClientMngEntities context = CreateContext())
                {
                    // get client info
                    var dbClient = context.Client.FirstOrDefault(o => o.ClientID == clientId);
                    if (dbClient != null)
                    {
                        hRow.ClientUD = dbClient.ClientUD;
                        hRow.ClientNM = dbClient.ClientNM;
                    }

                    // get exchange rate
                    var dbExchangeRate = context.SystemSetting.FirstOrDefault(o => o.Season == season && o.SettingKey == "ExRate-EUR-USD");
                    if (dbExchangeRate != null)
                    {
                        hRow.ExchangeRate = Convert.ToDecimal(dbExchangeRate.SettingValue, en);
                    }
                }
                ds.AcceptChanges();

                return Library.Helper.CreateReportFileWithEPPlus(ds, "ClientOverviewDelta3", new List<string>() { "ClientMng_Overview_EurofarPriceOverviewGroupByModel_View", "DeltaReportHeader" });
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

        public bool UpdateOfferPotentialStatus(int userId, List<DTO.ClientMng.Overview.Offer.OfferMarginDTO> dtoItems, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ClientMngEntities context = CreateContext())
                {
                    Offer dbItem = null;
                    foreach (DTO.ClientMng.Overview.Offer.OfferMarginDTO dtoItem in dtoItems)
                    {
                        dbItem = context.Offer.FirstOrDefault(o => o.OfferID == dtoItem.OfferID);
                        if (dbItem != null)
                        {
                            dbItem.IsPotential = dtoItem.IsPotential;
                            dbItem.UpdatedBy = userId;
                            dbItem.UpdatedDate = DateTime.Now;
                        }
                    }
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }
            return false;
        }

        public List<DTO.ClientMng.PlaningPurchasingPriceDTO> GetPlaningPurchasingPrice(int userId, int offerLineID, out Library.DTO.Notification notification)
        {
            List<DTO.ClientMng.PlaningPurchasingPriceDTO> Data = new List<DTO.ClientMng.PlaningPurchasingPriceDTO>();
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ClientMngEntities context = CreateContext())
                {
                    Data = converter.DB2DTO_PlaningPurchasingPriceDTO(context.ClientMng_function_GetOfferLinePlaningPurchasingPrice(offerLineID).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }
            return Data;
        }       
    }
}

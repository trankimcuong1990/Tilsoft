using Library;
using Library.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace Module.PurchaseQuotationMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchDataDTO, DTO.EditDataDTO>
    {
        #region ** Code common Purchase Quotation **

        private Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();

        private DataConverter converter = new DataConverter();

        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();

        public PurchaseQuotationMngEntities CreatContext()
        {
            return new PurchaseQuotationMngEntities(Library.Helper.CreateEntityConnectionString("DAL.PurchaseQuotationMngModel"));
        }

        void SendMailToNotificationGroup(PurchaseQuotation dbItem)
        {
            using (var context = CreatContext())
            {
                List<int?> NotificationGroupMembers = context.NotificationGroupMember.Where(x => x.NotificationGroup.NotificationGroupUD == "PQG").Select(y => y.UserID).ToList();
                var employees = context.Employee.Where(x => NotificationGroupMembers.Contains(x.UserID)).Select(y => new { y.Email1, y.UserID }).ToList();
                string Subject = string.Format("[Tilsoft] - Purchasing Quotation - {0} - {1}", dbItem.PurchaseQuotationUD, dbItem.FactoryRawMaterial.FactoryRawMaterialShortNM);
                string Content = string.Format("Please approve PQ: {0} - {1} - {2}", dbItem.PurchaseQuotationUD, dbItem.FactoryRawMaterial.FactoryRawMaterialShortNM, "http://app.tilsoft.bg/PurchaseQuotationMng/Edit/" + dbItem.PurchaseQuotationID);
                string sendToEmail = string.Empty;
                foreach (var eAddress in employees)
                {
                    if (string.IsNullOrEmpty(sendToEmail))
                    {
                        sendToEmail += eAddress;
                    }
                    else
                    {
                        sendToEmail += ";" + eAddress;
                    }

                    // add to NotificationMessage table
                    NotificationMessage notification = new NotificationMessage();
                    notification.UserID = eAddress.UserID;
                    notification.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_PURCHASING;
                    notification.NotificationMessageTitle = Subject;
                    notification.NotificationMessageContent = Content;
                    context.NotificationMessage.Add(notification);
                }
                                                   
                EmailNotificationMessage dbEmail = new EmailNotificationMessage();
                dbEmail.EmailBody = Content;
                dbEmail.EmailSubject = Subject;
                dbEmail.SendTo = sendToEmail;
                context.EmailNotificationMessage.Add(dbEmail);
                context.SaveChanges();
            }
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;
            try
            {
                using (var context = CreatContext())
                {
                    PurchaseQuotation dbItem = context.PurchaseQuotation.FirstOrDefault(o => o.PurchaseQuotationID == id);

                    if (dbItem == null)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Can not find data!";

                        return false;
                    }

                    dbItem.IsApproved = true;
                    dbItem.ApprovedBy = userId;
                    dbItem.ApprovedDate = DateTime.Now;

                    context.SaveChanges();

                    dtoItem = GetData(dbItem.PurchaseQuotationID, out notification).Data;
                    SendMailToNotificationGroup(dbItem);
                }

            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;

                return false;
            }

            return true;
        }

        public override bool DeleteData(int id, out Notification notification)
        {
            notification = new Notification { Type = NotificationType.Success };
            try
            {
                using (var context = CreatContext())
                {
                    var dbItem = context.PurchaseQuotation.Where(o => o.PurchaseQuotationID == id).FirstOrDefault();
                    context.PurchaseQuotation.Remove(dbItem);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                //notification = new Notification { Type = NotificationType.Error, Message = ex.Message };
                return false;
            }
        }

        public override DTO.EditDataDTO GetData(int id, out Notification notification)
        {
            DTO.EditDataDTO editFormData = new DTO.EditDataDTO();
            editFormData.Data = new DTO.PurchaseQuotationDTO();
            editFormData.Data.PurchaseQuotationDetailDTOs = new List<DTO.PurchaseQuotationDetailDTO>();

            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            try
            {

                Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();

                using (var context = CreatContext())
                {
                    if (id > 0)
                    {
                        PurchaseQuotationMng_PurchaseQuotation_view dbItem;
                        dbItem = context.PurchaseQuotationMng_PurchaseQuotation_view.Include("PurchaseQuotationMng_PurchaseQuotationDetail_View").FirstOrDefault(o => o.PurchaseQuotationID == id);
                        editFormData.Data = converter.DB2DTO_PurchaseQuotation(dbItem);

                        foreach (var item in editFormData.Data.PurchaseQuotationDetailDTOs)
                        {
                            if (item != null && item.FrameWeight.HasValue)
                            {
                                item.IsHasWeight = true;
                            }
                        }
                    }
                    return editFormData;
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return editFormData;
            }


        }

        public override DTO.SearchDataDTO GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            //throw new NotImplementedException();
            totalRows = 0;
            notification = new Notification { Type = NotificationType.Success };

            DTO.SearchDataDTO data = new DTO.SearchDataDTO();
            data.Data = new List<DTO.PurchaseQuotationSearchDto>();
            data.supportDeliveryTermDTOs = new List<DTO.SupportDeliveryTermDTO>();
            data.supportPaymentTermDTOs = new List<DTO.SupportPaymentTermDTO>();
            data.supportFactoryRawMaterialDTOs = new List<DTO.SupportFactoryRawMaterialDTO>();

            try
            {
                string purchaseQuotationUD = null;
                int? factoryRawMaterialID = null;
                string validFrom = null;
                string validTo = null;
                int? purchaseDeliveryID = null;
                int? purchasePaymentTermID = null;
                string currency = null;


                if (filters.ContainsKey("purchaseQuotationUD") && !string.IsNullOrEmpty(filters["purchaseQuotationUD"].ToString()))
                {
                    purchaseQuotationUD = filters["purchaseQuotationUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("factoryRawMaterialID") && !string.IsNullOrEmpty(filters["factoryRawMaterialID"].ToString()))
                {
                    factoryRawMaterialID = Convert.ToInt32(filters["factoryRawMaterialID"].ToString());
                }
                if (filters.ContainsKey("validFrom") && !string.IsNullOrEmpty(filters["validFrom"].ToString()))
                {
                    validFrom = filters["validFrom"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("validTo") && !string.IsNullOrEmpty(filters["validTo"].ToString()))
                {
                    validTo = filters["validTo"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("purchaseDeliveryID") && !string.IsNullOrEmpty(filters["purchaseDeliveryID"].ToString()))
                {
                    purchaseDeliveryID = Convert.ToInt32(filters["purchaseDeliveryID"].ToString());
                }
                if (filters.ContainsKey("purchasePaymentTermID") && !string.IsNullOrEmpty(filters["purchasePaymentTermID"].ToString()))
                {
                    purchasePaymentTermID = Convert.ToInt32(filters["purchasePaymentTermID"].ToString());
                }
                if (filters.ContainsKey("currency") && !string.IsNullOrEmpty(filters["currency"].ToString()))
                {
                    currency = filters["currency"].ToString().Replace("'", "''");
                }

                DateTime? validFromDate = validFrom.ConvertStringToDateTime();
                DateTime? validToDate = validTo.ConvertStringToDateTime();

                using (var context = CreatContext())
                {

                    totalRows = context.PurchaseQuotationMng_function_PurchaseQoutationSearchReasult(purchaseQuotationUD, factoryRawMaterialID, validFromDate, validToDate, purchaseDeliveryID, purchasePaymentTermID, currency, orderBy, orderDirection).Count();
                    var result = context.PurchaseQuotationMng_function_PurchaseQoutationSearchReasult(purchaseQuotationUD, factoryRawMaterialID, validFromDate, validToDate, purchaseDeliveryID, purchasePaymentTermID, currency, orderBy, orderDirection);
                    data.Data = converter.BD2DTO_PurchaseQuotationSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());


                    //Get Support list
                    data.supportDeliveryTermDTOs = converter.DB2DTO_SpDeliveryTerm(context.SupportMng_PurchaseDeliveryTerm_View.ToList());
                    data.supportPaymentTermDTOs = converter.DB2DTO_SpPaymentTerm(context.SupportMng_PurchasePaymentTerm_View.ToList());
                    data.supportFactoryRawMaterialDTOs = converter.DB2DTO_GetFactoryRawMaterial(context.PurchaseQoutationMng_GetFactoryRawMaterial_View.ToList());
                }
            }

            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                //notification = new Notification { Type = NotificationType.Error, Message = ex.Message };
            }

            return data;
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            notification = new Notification { Type = NotificationType.Success };

            DTO.PurchaseQuotationDTO dtoPurchaseQuotation = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.PurchaseQuotationDTO>();

            try
            {
                // 1. Check validation validFrom and validTo
                var validFrom = dtoPurchaseQuotation.ValidFrom.ConvertStringToDateTime();
                var validTo = dtoPurchaseQuotation.ValidTo.ConvertStringToDateTime();

                // 1.1. Case validFrom not value
                if (!validFrom.HasValue)
                {
                    notification.Type = NotificationType.Error;
                    notification.Message = "Valid From is invalid!";
                    return false;
                }

                // 1.2. Compare 
                if (validTo.HasValue)
                {
                    if (validTo.Value.CompareTo(validFrom.Value) < 0)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Valid From is less than Valid To!";
                        return false;
                    }
                }

                using (var context = CreatContext())
                {
                    PurchaseQuotation dbItem;

                    if (id == 0)
                    {
                        dbItem = new PurchaseQuotation();
                        context.PurchaseQuotation.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.PurchaseQuotation.Where(o => o.PurchaseQuotationID == id).FirstOrDefault();
                    }

                    if (dbItem == null)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Data Not Found !";
                        return false;
                    }
                    else
                    {
                        //converter
                        converter.DTO2DB_PurchaseQuatation(dtoPurchaseQuotation, ref dbItem);

                        //upload file
                        Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
                        string tempFolder = FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\";
                        if (dtoPurchaseQuotation.File_HasChange.HasValue && dtoPurchaseQuotation.File_HasChange.Value)
                        {
                            dbItem.AttachedFile = fwFactory.CreateFilePointer(tempFolder, dtoPurchaseQuotation.File_NewFile, dtoPurchaseQuotation.AttachedFile, dtoPurchaseQuotation.FriendlyName);
                        }

                        //Remove
                        context.PurchaseQuotationDetail.Local.Where(o => o.PurchaseQuotation == null).ToList().ForEach(o => context.PurchaseQuotationDetail.Remove(o));

                        int? companyID = fwFactory.GetCompanyID(userId);
                        dbItem.CompanyID = companyID;
                        dbItem.UpdatedBy = userId;
                        dbItem.UpdatedDate = DateTime.Now;

                        if (id == 0)
                        {
                            dbItem.PurchaseQuotationUD = context.PurchaseQuotationMng_function_GeneratePurchaseQuotationUD().FirstOrDefault();
                            dbItem.CreatedBy = userId;
                            dbItem.CreatedDate = DateTime.Now;
                        }

                        /// cuong.tran: ProductionFrameWeight - start
                        foreach (var item in dtoPurchaseQuotation.PurchaseQuotationDetailDTOs)
                        {
                            /// Only material is component
                            if (item.ProductionItemTypeID == 1 && item.FrameWeight.HasValue)
                            {
                                var dtoFrameWeight = context.ProductionFrameWeight.FirstOrDefault(o => o.ProductionItemID == item.ProductionItemID);

                                if (dtoFrameWeight == null)
                                {
                                    // Insert table ProductionFrameWeight
                                    ProductionFrameWeight dbFrameWeight = new ProductionFrameWeight();
                                    context.ProductionFrameWeight.Add(dbFrameWeight);

                                    dbFrameWeight.ProductionItemID = item.ProductionItemID;
                                    dbFrameWeight.FrameWeight = item.FrameWeight;
                                    dbFrameWeight.UpdatedBy = userId;
                                    dbFrameWeight.UpdatedDate = DateTime.Now;

                                    context.SaveChanges();

                                    // Insert table ProductionFrameWeightHistory
                                    ProductionFrameWeightHistory dbFrameWeightHistory = new ProductionFrameWeightHistory();
                                    context.ProductionFrameWeightHistory.Add(dbFrameWeightHistory);

                                    dbFrameWeightHistory.ProductionFrameWeightID = dbFrameWeight.ProductionFrameWeightID;
                                    dbFrameWeightHistory.FrameWeight = item.FrameWeight;
                                    dbFrameWeightHistory.UpdatedBy = userId;
                                    dbFrameWeightHistory.UpdatedDate = DateTime.Now;

                                    context.SaveChanges();
                                }
                            }
                        }
                        /// cuong.tran: ProductionFrameWeight - e n d

                        //Save
                        context.SaveChanges();

                        dtoItem = GetData(dbItem.PurchaseQuotationID, out notification).Data;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return false;
            }

        }

        public IEnumerable<DTO.SupportDeliveryTermDTO> GetDeliveryTerm()
        {
            try
            {
                using (var context = this.CreatContext())
                {
                    return this.converter.DB2DTO_SpDeliveryTerm(context.SupportMng_PurchaseDeliveryTerm_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.SupportDeliveryTermDTO>();
            }
        }

        #endregion

        #region ** Code developer Luu Nhut **

        #endregion

        #region ** Code developer Truong Son **

        public DTO.InitDefaultPriceDTO GetInitDefaultPrice(Hashtable filters, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };
            DTO.InitDefaultPriceDTO data = new DTO.InitDefaultPriceDTO()
            {
                Data = new List<DTO.PurchaseQuotationDTO>(),
                Suppliers = new List<DTO.SupplierDTO>()
            };

            //string orderDirection = "";

            try
            {
                using (var context = this.CreatContext())
                {
                    //data.Data = this.converter.DB2DTO_GetProductionItem(context.PurchaseQuotationMng_function_DefaultPrice(orderDirection).ToList());
                    data.Suppliers = this.converter.DB2DTO_GetSuppliers(context.PurchaseQuotationMng_FactoryRawMaterial_View.ToList());
                }
            }
            catch (System.Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        #endregion

        public object PreparingDefaultPriceData(int userID, Hashtable filters, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            DTO.DefaultPriceData data = new DTO.DefaultPriceData();

            try
            {
                Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
                int? company = fwFactory.GetCompanyID(userID);

                int? supplier = (filters.ContainsKey("factoryRawMaterialID") && filters["factoryRawMaterialID"] != null && !string.IsNullOrEmpty(filters["factoryRawMaterialID"].ToString().Trim())) ? (int?)Convert.ToInt32(filters["factoryRawMaterialID"].ToString().Trim()) : null;
                string productionItem = (filters.ContainsKey("productionItemUD") && filters["productionItemUD"] != null && !string.IsNullOrEmpty(filters["productionItemUD"].ToString().Trim())) ? filters["productionItemUD"].ToString().Trim() : null;
                bool? isDefault = (filters.ContainsKey("yesNoValue") && filters["yesNoValue"] != null && !string.IsNullOrEmpty(filters["yesNoValue"].ToString().Trim())) ? (bool?)Convert.ToBoolean(filters["yesNoValue"].ToString().Trim()) : null;

                using (var context = CreatContext())
                {
                    var count = context.PurchaseQuotationMng_function_GetMaxPurchasing(supplier, productionItem, isDefault, company).FirstOrDefault();
                    if (count.HasValue)
                    {
                        data.CountPurchasing = count.Value;
                    }

                    data.DefaultPriceProductionItem = converter.DB2DTO_DefaultProductionItem(context.PurchaseQuotationMng_function_GetProductionItem(supplier, productionItem, isDefault, company).ToList());
                    data.DefaultPricePurchaseQuotationDetail = converter.DB2DTO_DefaultPurchasingDetail(context.PurchaseQuotationMng_function_GetPurchaseQuotationDetail(supplier, productionItem, isDefault, company).ToList());
                }

            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public object SetDefaultPrice(int userID, Hashtable filters, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            List<DTO.DefaultPricePurchaseQuotationDetailData> dtoItem = ((Newtonsoft.Json.Linq.JArray)filters["PurchasingDetail"]).ToObject<List<DTO.DefaultPricePurchaseQuotationDetailData>>();

            try
            {
                using (var context = CreatContext())
                {
                    foreach (var item in dtoItem)
                    {
                        if (item.UnitPrice.HasValue)
                        {
                            var dbItem = context.PurchaseQuotationDetail.FirstOrDefault(o => o.PurchaseQuotationDetailID == item.PurchaseQuotationDetailID);

                            if (dbItem != null)
                            {
                                dbItem.IsDefault = item.IsDefault;

                                dbItem.SetDefaultBy = userID;
                                dbItem.SetDefaultDate = DateTime.Now;
                            }
                        }
                    }

                    context.SaveChanges();
                }

                return PreparingDefaultPriceData(userID, filters, out notification);
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return null;
        }

        public List<DTO.ProductionItemDefaultPriceDTO> GetProductionItemDefaultPrice(int userId, string searchString, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            List<DTO.ProductionItemDefaultPriceDTO> data = new List<DTO.ProductionItemDefaultPriceDTO>();

            try
            {
                Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
                int? companyID = fwFactory.GetCompanyID(userId);

                using (var context = CreatContext())
                {
                    var dbItem = context.PurchaseQuotationMng_ProductionItemDefaultPrice_View.Where(o => o.CompanyID == companyID && (o.Value.Contains(searchString) || o.ProductionItemUD.Contains(searchString)));
                    data = converter.DB2DTO_ProductionItemDefaultPrice(dbItem.ToList());

                    foreach (var item in data)
                    {
                        if (item != null && item.FrameWeight.HasValue)
                        {
                            item.IsHasWeight = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public DTO.InitDataDefaultPricePurchase GetInitDataDefaultPrice(int userID, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            DTO.InitDataDefaultPricePurchase data = new DTO.InitDataDefaultPricePurchase();
            data.SubSuppliers = new List<DTO.SupportFactoryRawMaterialDTO>();
            data.FactoryWarehouses = new List<Support.DTO.FactoryWarehouseDto>();
            data.StatusDefaults = new List<Support.DTO.YesNo>();

            try
            {
                int? companyID = fwFactory.GetCompanyID(userID);
                data.FactoryWarehouses = supportFactory.GetFactoryWarehouse(companyID);
                data.StatusDefaults = supportFactory.GetYesNo();

                using (var context = CreatContext())
                {
                    data.SubSuppliers = converter.DB2DTO_GetFactoryRawMaterial(context.PurchaseQoutationMng_GetFactoryRawMaterial_View.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public object GetInitData(out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            DTO.EditDataDTO data = new DTO.EditDataDTO();

            data.supportFactoryRawMaterialDTOs = new List<DTO.SupportFactoryRawMaterialDTO>();
            data.supportDeliveryTermDTOs = new List<DTO.SupportDeliveryTermDTO>();
            data.supportPaymentTermDTOs = new List<DTO.SupportPaymentTermDTO>();

            try
            {
                using (var context = CreatContext())
                {
                    data.supportDeliveryTermDTOs = converter.DB2DTO_SpDeliveryTerm(context.SupportMng_PurchaseDeliveryTerm_View.ToList());
                    data.supportPaymentTermDTOs = converter.DB2DTO_SpPaymentTerm(context.SupportMng_PurchasePaymentTerm_View.ToList());
                    data.supportFactoryRawMaterialDTOs = converter.DB2DTO_GetFactoryRawMaterial(context.PurchaseQoutationMng_GetFactoryRawMaterial_View.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public object GetContentPurchasingPriceFactory(int userID, Hashtable filters, out Notification notification)
        {
            string fileName = string.Empty;
            notification = new Notification();
            notification.Type = NotificationType.Success;

            PurchasingPriceFactory ds = new PurchasingPriceFactory();

            try
            {
                string validDate = (filters.ContainsKey("validDate") && filters["validDate"] != null && !string.IsNullOrEmpty(filters["validDate"].ToString().Trim())) ? filters["validDate"].ToString().Trim() : null;
                int? supplierID = (filters.ContainsKey("supplierID") && filters["supplierID"] != null && !string.IsNullOrEmpty(filters["supplierID"].ToString().Trim())) ? (int?)Convert.ToInt32(filters["supplierID"].ToString().Trim()) : null;
                int? materialGroupID = (filters.ContainsKey("materialGroupID") && filters["materialGroupID"] != null && !string.IsNullOrEmpty(filters["materialGroupID"].ToString().Trim())) ? (int?)Convert.ToInt32(filters["materialGroupID"].ToString().Trim()) : null;
                int? materialID = (filters.ContainsKey("materialID") && filters["materialID"] != null && !string.IsNullOrEmpty(filters["materialID"].ToString().Trim())) ? (int?)Convert.ToInt32(filters["materialID"].ToString().Trim()) : null;
                bool? statusID = (filters.ContainsKey("statusID") && filters["statusID"] != null && !string.IsNullOrEmpty(filters["statusID"].ToString().Trim())) ? (bool?)Convert.ToBoolean(filters["statusID"].ToString().Trim()) : null;
                string orderBy = (filters.ContainsKey("orderBy") && filters["orderBy"] != null && !string.IsNullOrEmpty(filters["orderBy"].ToString().Trim())) ? filters["orderBy"].ToString().Trim() : null;
                string orderDirection = (filters.ContainsKey("orderDirection") && filters["orderDirection"] != null && !string.IsNullOrEmpty(filters["orderDirection"].ToString().Trim())) ? filters["orderDirection"].ToString().Trim() : null;

                System.Data.SqlClient.SqlDataAdapter adap = new System.Data.SqlClient.SqlDataAdapter();
                adap.SelectCommand = new System.Data.SqlClient.SqlCommand("PurchasingPriceFactoryOverview_function_GetPurchasingPrice", new System.Data.SqlClient.SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

                adap.SelectCommand.Parameters.AddWithValue("@validDate", validDate);
                adap.SelectCommand.Parameters.AddWithValue("@supplierID", supplierID);
                adap.SelectCommand.Parameters.AddWithValue("@materialGroupID", materialGroupID);
                //adap.SelectCommand.Parameters.AddWithValue("@materialID", materialID);
                adap.SelectCommand.Parameters.AddWithValue("@statusID", statusID);
                adap.SelectCommand.Parameters.AddWithValue("@orderBy", "KeyID");
                adap.SelectCommand.Parameters.AddWithValue("@orderDirection", orderDirection);

                adap.TableMappings.Add("Table", "PurchasingPriceData");
                adap.Fill(ds);

                ds.AcceptChanges();

                fileName = Library.Helper.CreateReportFileWithEPPlus2(ds, "PurchasingPriceFactory");
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return fileName;
        }

        public object GetInitForm(out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            DTO.InitData data = new DTO.InitData();

            try
            {
                using (var context = CreatContext())
                {
                    data.MaterialGroups = AutoMapper.Mapper.Map<List<ProductionItemGroup>, List<DTO.MaterialGroup>>(context.ProductionItemGroup.ToList());
                    data.Suppliers = AutoMapper.Mapper.Map<List<FactoryRawMaterial>, List<DTO.SubSupplier>>(context.FactoryRawMaterial.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public object GetMaterial(int userID, Hashtable filters, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            List<DTO.Material> materials = new List<DTO.Material>();

            try
            {
                int? companyID = fwFactory.GetCompanyID(userID);

                string searchQuery = (filters.ContainsKey("searchQuery") && filters["searchQuery"] != null && !string.IsNullOrEmpty(filters["searchQuery"].ToString())) ? filters["searchQuery"].ToString() : null;
                List<string> input = searchQuery.Split(' ').ToList();
                input = input.Where(x => !string.IsNullOrEmpty(x)).ToList();
                using (var context = CreatContext())
                {
                    var tempResult = context.SupportMng_ProductionItem_View.Where(o => o.CompanyID == companyID);
                    foreach (var word in input)
                    {
                        tempResult = tempResult.Where(o => o.ProductionItemNM.Contains(word) || o.ProductionItemUD.Contains(word));
                    }
                    materials = AutoMapper.Mapper.Map<List<SupportMng_ProductionItem_View>, List<DTO.Material>>(tempResult.ToList()).ToList();
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return materials;
        }

        public object GetPurchasingPriceFactory(int userID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            totalRows = 0;

            notification = new Notification();
            notification.Type = NotificationType.Success;

            List<DTO.PurchasingPriceFactory> data = new List<DTO.PurchasingPriceFactory>();

            try
            {
                string validDate = (filters.ContainsKey("validDate") && filters["validDate"] != null && !string.IsNullOrEmpty(filters["validDate"].ToString().Trim())) ? filters["validDate"].ToString().Trim() : null;
                int? supplierID = (filters.ContainsKey("supplierID") && filters["supplierID"] != null && !string.IsNullOrEmpty(filters["supplierID"].ToString().Trim())) ? (int?)Convert.ToInt32(filters["supplierID"].ToString().Trim()) : null;
                int? materialGroupID = (filters.ContainsKey("materialGroupID") && filters["materialGroupID"] != null && !string.IsNullOrEmpty(filters["materialGroupID"].ToString().Trim())) ? (int?)Convert.ToInt32(filters["materialGroupID"].ToString().Trim()) : null;
                //int? materialID = (filters.ContainsKey("materialID") && filters["materialID"] != null && !string.IsNullOrEmpty(filters["materialID"].ToString().Trim())) ? (int?)Convert.ToInt32(filters["materialID"].ToString().Trim()) : null;
                string materialNM = (filters.ContainsKey("materialNM") && filters["materialNM"] != null && !string.IsNullOrEmpty(filters["materialNM"].ToString().Trim())) ? filters["materialNM"].ToString().Trim() : null;
                bool? statusID = (filters.ContainsKey("statusID") && filters["statusID"] != null && !string.IsNullOrEmpty(filters["statusID"].ToString().Trim())) ? (bool?)Convert.ToBoolean(filters["statusID"].ToString().Trim()) : null;

                using (var context = CreatContext())
                {
                    string materialNMTemp = null;
                    if(materialNM != null)
                    {
                        List<string> input = materialNM.Split(' ').ToList();
                        input = input.Where(x => !string.IsNullOrEmpty(x)).ToList();
                        foreach (string word in input)
                        {
                            materialNMTemp += " AND ProductionItemNM LIKE '%" + word + "%' ";
                        }
                    }

                    totalRows = context.PurchasingPriceFactoryOverview_function_GetPurchasingPrice(validDate, supplierID, materialGroupID, materialNMTemp, statusID, orderBy, orderDirection).Count();
                    filters["totalRows"] = totalRows;

                    var dbItem = context.PurchasingPriceFactoryOverview_function_GetPurchasingPrice(validDate, supplierID, materialGroupID, materialNMTemp, statusID, orderBy, orderDirection);
                    data = AutoMapper.Mapper.Map<List<PurchasingPriceFactoryOverview_PurchasingPriceFactory_View>, List<DTO.PurchasingPriceFactory>>(dbItem.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public bool Cancel(int userId, int id, ref object dtoItem, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            try
            {
                using (var context = CreatContext())
                {
                    var dbItem = context.PurchaseQuotation.FirstOrDefault(o => o.PurchaseQuotationID == id);
                    if (dbItem == null)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Can not find data!";
                        return false;
                    }
                    dbItem.IsCancelled = true;
                    dbItem.CanceledBy = userId;
                    dbItem.CancelelDate = DateTime.Now;

                    foreach (var item in dbItem.PurchaseQuotationDetail.ToList())
                    {
                        item.IsDefault = false;
                        item.SetDefaultBy = null;
                        item.SetDefaultDate = null;
                    }

                    context.SaveChanges();

                    dtoItem = GetData(dbItem.PurchaseQuotationID, out notification).Data;
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }

            return true;
        }
    }
}

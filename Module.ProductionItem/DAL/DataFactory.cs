using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Library;

namespace Module.ProductionItem.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
        private Module.Framework.BLL fwBll = new Module.Framework.BLL();
        private string moduleCode = "ProductionItem";
        private string _tempFolder;
        private DataConverter converter = new DataConverter();
        public DataFactory() { }
        public DataFactory(string tempFolder)
        {
            this._tempFolder = tempFolder + @"\";
        }
        private ProductionItemEntities CreateContext()
        {
            return new ProductionItemEntities(Library.Helper.CreateEntityConnectionString("DAL.ProductionItemModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.ProductionItemSearchResult>();
            totalRows = 0;

            //try to get data
            try
            {
                using (ProductionItemEntities context = CreateContext())
                {
                    int UserID = -1; ;
                    string ProductionItemUD = null;
                    string ProductionItemNM = null;
                    string ProductionItemVNNM = null;
                    string FactoryWarehouseIDs = "";
                    int? ProductionItemGroupID = null;
                    int? ProductionItemMaterialTypeID = null;
                    bool? Status = null;
                    int? DefaultWarehouseID = null;
                    int? ProductionItemTypeID = null;
                    bool? isAVTGroup = null;

                    if (filters.ContainsKey("UserID") && !string.IsNullOrEmpty(filters["UserID"].ToString()))
                    {
                        UserID = Convert.ToInt32(filters["UserID"].ToString());
                    }

                    int? CompanyID = fwFactory.GetCompanyID(UserID);

                    if (filters.ContainsKey("ProductionItemUD") && !string.IsNullOrEmpty(filters["ProductionItemUD"].ToString()))
                    {
                        ProductionItemUD = filters["ProductionItemUD"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("ProductionItemNM") && !string.IsNullOrEmpty(filters["ProductionItemNM"].ToString()))
                    {
                        ProductionItemNM = filters["ProductionItemNM"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("ProductionItemVNNM") && !string.IsNullOrEmpty(filters["ProductionItemVNNM"].ToString()))
                    {
                        ProductionItemVNNM = filters["ProductionItemVNNM"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("ProductionItemGroupID") && !string.IsNullOrEmpty(filters["ProductionItemGroupID"].ToString()))
                    {
                        ProductionItemGroupID = Convert.ToInt32(filters["ProductionItemGroupID"]);
                    }

                    if (filters.ContainsKey("ProductionItemMaterialTypeID") && !string.IsNullOrEmpty(filters["ProductionItemMaterialTypeID"].ToString()))
                    {
                        ProductionItemMaterialTypeID = Convert.ToInt32(filters["ProductionItemMaterialTypeID"]);
                    }

                    if (filters.ContainsKey("FactoryWarehouseIDs") && filters["FactoryWarehouseIDs"] != null)
                    {
                        //pare object to list string of supplierID
                        List<string> fwID = ((Newtonsoft.Json.Linq.JArray)filters["FactoryWarehouseIDs"]).ToObject<List<string>>();
                        foreach (var item in fwID)
                        {
                            FactoryWarehouseIDs += item + ",";
                        }
                    }

                    if (filters.ContainsKey("Status") && !string.IsNullOrEmpty(filters["Status"].ToString()))
                    {
                        Status = Convert.ToBoolean(filters["Status"].ToString());
                    }

                    if (filters.ContainsKey("DefaultWarehouseID") && !string.IsNullOrEmpty(filters["DefaultWarehouseID"].ToString()))
                    {
                        DefaultWarehouseID = Convert.ToInt32(filters["DefaultWarehouseID"]);
                    }

                    if (filters.ContainsKey("ProductionItemTypeID") && !string.IsNullOrEmpty(filters["ProductionItemTypeID"].ToString()))
                    {
                        ProductionItemTypeID = Convert.ToInt32(filters["ProductionItemTypeID"]);
                    }

                    if (filters.ContainsKey("isAVTGroup") && filters["isAVTGroup"] != null && !string.IsNullOrEmpty(filters["isAVTGroup"].ToString()))
                    {
                        switch (filters["isAVTGroup"].ToString().ToLower())
                        {
                            case "true":
                                isAVTGroup = true;
                                break;
                            case "false":
                                isAVTGroup = false;
                                break;
                            default:
                                isAVTGroup = null;
                                break;
                        }
                    }

                    totalRows = context.ProductionItemMng_function_ProductionItemSearchResult(ProductionItemTypeID, ProductionItemUD, ProductionItemNM, ProductionItemVNNM, ProductionItemGroupID, isAVTGroup, DefaultWarehouseID, CompanyID, orderBy, orderDirection).Count();

                    var dbItem = context.ProductionItemMng_function_ProductionItemSearchResult(ProductionItemTypeID, ProductionItemUD, ProductionItemNM, ProductionItemVNNM, ProductionItemGroupID, isAVTGroup, DefaultWarehouseID, CompanyID, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_ProductionItemSearchResultList(dbItem.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());

                    data.FactoryWarehouseSearchResults = AutoMapper.Mapper.Map<List<ProductionItemMng_FactoryWarehouseSearchResultVirtual_View>, List<DTO.FactoryWarehouseSearchResultDTO>>(context.ProductionItemMng_function_FactoryWarehouseSearchResult(ProductionItemTypeID, ProductionItemUD, ProductionItemNM, ProductionItemVNNM, ProductionItemGroupID, isAVTGroup, DefaultWarehouseID, CompanyID, null, null).ToList());
                    //totalRows = context.ProductionItemMng_function_SearchProductionItem(ProductionItemUD, ProductionItemNM, ProductionItemVNNM, FactoryWarehouseIDs, ProductionItemGroupID, ProductionItemMaterialTypeID, Status, DefaultWarehouseID, CompanyID, ProductionItemTypeID, isAVTGroup, orderBy, orderDirection).Count();
                    //var result = context.ProductionItemMng_function_SearchProductionItem(ProductionItemUD, ProductionItemNM, ProductionItemVNNM, FactoryWarehouseIDs, ProductionItemGroupID, ProductionItemMaterialTypeID, Status, DefaultWarehouseID, CompanyID, ProductionItemTypeID, isAVTGroup, orderBy, orderDirection);
                    //data.Data = converter.DB2DTO_ProductionItemSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.ProductionItem dtoProductionItem = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.ProductionItem>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            string countryCode = null;
            int number;
            string indexName;
            bool permissionApprove = false;
            if (fwBll.CanPerformAction(userId, moduleCode, Library.DTO.ModuleAction.CanApprove)) { permissionApprove = true; }
            try
            {
                using (ProductionItemEntities context = CreateContext())
                {
                    ProductionItem dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new ProductionItem();
                        context.ProductionItem.Add(dbItem);
                        // Auto generate code
                        switch (dtoProductionItem.CountryID)
                        {
                            case 1:
                                countryCode = "VN";
                                break;
                            case 2:
                                countryCode = "IN";
                                break;
                            case 3:
                                countryCode = "HL";
                                break;
                        }

                        if (dtoProductionItem.ProductionItemUD == null)
                        {
                            dtoProductionItem.ProductionItemUD = context.ProductionItemMng_function_GenerateItemCode(dtoProductionItem.CountryID, countryCode, dtoProductionItem.ProductionItemGroupID, dtoProductionItem.ProductionItemMaterialTypeID, dtoProductionItem.ProductionItemTypeID).FirstOrDefault();

                            // Check length of production item code enough 12 characters
                            if (dtoProductionItem.ProductionItemUD.Length < 12)
                            {
                                notification.Type = Library.DTO.NotificationType.Error;
                                notification.Message = "Create Production Item is error. Please connect to Mr. Hanh to help!";

                                return false;
                            }
                        }

                    }
                    else
                    {
                        dbItem = context.ProductionItem.FirstOrDefault(o => o.ProductionItemID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Production Item not found!";
                        return false;
                    }
                    else
                    {
                        // processing image
                        if (dtoProductionItem.FileLocation_HasChange)
                        {
                            dtoProductionItem.ProductImage = fwFactory.CreateFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoProductionItem.FileLocation_NewFile, dtoProductionItem.ProductImage);
                        }

                        //only one Defaut warehouse accepted
                        int c = dtoProductionItem.ProductionItemWarehouses.Where(o => o.IsDefault.Equals(true)).Count();
                        if (c == 0 && dtoProductionItem.ProductionItemTypeID != 7)
                        {
                            throw new System.Data.DataException("Please select at leat one default warehouse!");
                        }
                        //if (c > 1)
                        //{
                        //    throw new System.Data.DataException("Only one warehouse can be a default!");
                        //}

                        //only one Defaut vender accepted
                        int v = dtoProductionItem.ProductionItemVenders.Where(o => o.IsDefault.Equals(true)).Count();
                        if (v == 0)
                        {
                            //throw new System.Data.DataException("Please select at leat one default vender!");
                        }
                        if (v > 1)
                        {
                            throw new System.Data.DataException("Only one vender can be default option");
                        }

                        foreach (var item in dtoProductionItem.productionItemSubUnitDTOs)
                        {
                            if (!item.ConversionFactor.HasValue)
                            {
                                throw new Exception("You need fill-in conversion factory for unit");
                            }
                            if (item.ValidFrom == null || item.ValidFrom.Equals(""))
                            {
                                throw new Exception("You need fill-in validDate for sub unit");
                            }

                            if (item.ProductionItemUnitID > 0)
                            {
                                if (permissionApprove) {
                                    var getInforItem = context.ProductionItemUnit.FirstOrDefault(o => o.ProductionItemUnitID == item.ProductionItemUnitID);
                                    string validDateStr = getInforItem.ValidFrom != null ? getInforItem.ValidFrom.Value.ToString("dd/MM/yyyy") : "";
                                    if (item.ConversionFactor != getInforItem.ConversionFactor || item.ValidFrom != validDateStr)
                                    {
                                        ProductionItemUnitHistory productionItemUnitHistory;
                                        productionItemUnitHistory = new ProductionItemUnitHistory();
                                        //Get data;
                                        ProductionItemUnit xxx = dbItem.ProductionItemUnit.FirstOrDefault(o => o.ProductionItemUnitID == item.ProductionItemUnitID);
                                        xxx.ProductionItemUnitHistory.Add(productionItemUnitHistory);
                                        //Mapping Data
                                        productionItemUnitHistory.Remark = getInforItem.Remark != null ?getInforItem.Remark : item.Remark;
                                        productionItemUnitHistory.UpdateDate = DateTime.Now;
                                        productionItemUnitHistory.UpdatedBy = userId;
                                        productionItemUnitHistory.HsqdHistory = getInforItem.ConversionFactor != null ? getInforItem.ConversionFactor : item.ConversionFactor;
                                        productionItemUnitHistory.ValidFromHistory = getInforItem.ValidFrom != null ? getInforItem.ValidFrom : item.ValidFrom.ConvertStringToDateTime();
                                    }
                                }
                            }
                        }

                        converter.DTO2BD(dtoProductionItem, ref dbItem, permissionApprove, userId);

                        //update hidden data
                        int? companyID = fwFactory.GetCompanyID(userId);
                        dbItem.CompanyID = companyID;

                        // ProductionItemWarehouse
                        context.ProductionItemWarehouse.Local.Where(o => o.ProductionItem == null).ToList().ForEach(o => context.ProductionItemWarehouse.Remove(o));
                        // ProductionItemWarehouse
                        context.ProductionItemVender.Local.Where(o => o.ProductionItem == null).ToList().ForEach(o => context.ProductionItemVender.Remove(o));
                        //ProductionItemUnit
                        context.ProductionItemUnit.Local.Where(o => o.ProductionItem == null).ToList().ForEach(o => context.ProductionItemUnit.Remove(o));

                        dbItem.UpdatedDate = DateTime.Now;
                        dbItem.UpdatedBy = userId;

                        // Update by hand. (unneccessary)
                        dbItem.CountryID = dtoProductionItem.CountryID;
                        dbItem.ProductionItemMaterialTypeID = dtoProductionItem.ProductionItemMaterialTypeID;
                        dbItem.ProductionItemTypeID = dtoProductionItem.ProductionItemTypeID;

                        context.SaveChanges();

                        dtoItem = GetData(userId, dbItem.ProductionItemID, out notification).Data;

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
                    switch (indexName)
                    {
                        case "ProductItemFactoryWarehouseUnique":
                            notification.Message = "Duplicate Factory Warehouse ";
                            break;
                        case "IX_ProductionItemVender_FactoryRawMaterial":
                            notification.Message = "Duplicate Vender";
                            break;
                    }
                }
                else
                {
                    notification.Message = dEx.Message;
                }
                return false;
            }
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        //
        // CUSTOM FUNCTION
        //
        public DTO.EditFormData GetData(int userId, int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new DTO.ProductionItem();

            data.Data.ProductionItemWarehouses = new List<DTO.ProductionItemWarehouse>();
            data.Data.ProductionItemVenders = new List<DTO.ProductionItemVender>();

            //data.FactoryWarehouses = new List<Support.DTO.FactoryWarehouseDto>();
            data.FactoryWarehouses = new List<DTO.FactoryWarehouse>();
            data.FactoryRawMaterialSuppliers = new List<DTO.FactoryRawMaterialSupplier>();

            // Support list
            data.ProductionItemGroups = new List<DTO.ProductionItemGroup>();
            data.ProductionItemMaterialTypes = new List<DTO.ProductionItemType>();
            data.ProductionItemSpecs = new List<DTO.ProductionItemSpec>();
            data.Units = new List<Support.DTO.Unit>();
            data.ProductionItemTypes = new List<Support.DTO.ProductionItemType>();
            data.assetClassDTOs = new List<DTO.AssetClassDTO>();
            data.depreciationTypeDTOs = new List<DTO.DepreciationTypeDTO>();
            data.breakDownCategoryDTOs = new List<DTO.BreakDownCategoryDTO>();

            // Add new support: Branch.
            data.Branches = new List<DTO.BranchDTO>();

            //try to get data
            try
            {
                if (id > 0)
                {
                    using (ProductionItemEntities context = CreateContext())
                    {
                        var ProductionItem = context.ProductionItemMng_ProductionItem_View.FirstOrDefault(o => o.ProductionItemID == id);
                        if (ProductionItem == null)
                        {
                            throw new Exception("Can not found the Production Item to edit");
                        }

                        data.Data = converter.DB2DTO_ProductionItem(ProductionItem);

                    }
                }

                if (id == 0)
                {
                    data.Data.Status = true;
                }

                int? companyID = fwFactory.GetCompanyID(userId);
                //data.FactoryWarehouses = supportFactory.GetFactoryWarehouse(companyID).ToList();
                //data.FactoryWarehouses = this.GetFactoryWarehouse().Where(o => o.CompanyID == companyID).ToList();

                // Not using view: SupportMng_FactoryWarehouse_View.
                //data.FactoryWarehouses = this.GetFactoryWarehouse().ToList();

                data.FactoryRawMaterialSuppliers = GetFactoryRawMaterialSupplierList().ToList();
                data.ProductionItemGroups = GetProductionItemGroup().ToList();
                data.ProductionItemMaterialTypes = GetProductionItemType().ToList();
                data.ProductionItemSpecs = GetProductionItemSpec().ToList();
                data.Units = supportFactory.GetUnit(1).ToList();
                data.ProductionItemTypes = supportFactory.GetProductionItemType().ToList();

                using (var context = CreateContext())
                {
                    data.assetClassDTOs = converter.DB2DTO_AssetClassItem(context.ProductionItemMng_AssetClass_View.ToList());
                    data.depreciationTypeDTOs = converter.DB2DTO_DepreciationTypeItem(context.ProductionItemMng_DepreciationType_View.ToList());
                    data.breakDownCategoryDTOs = converter.DB2DTO_BreakDownCategory(context.ProductionItemMng_BreakDownCategory_View.ToList());

                    // Add get data: FactoryWarehouse, Branch.
                    data.FactoryWarehouses = AutoMapper.Mapper.Map<List<ProductionItemMng_FactoryWarehouse_View>, List<DTO.FactoryWarehouse>>(context.ProductionItemMng_FactoryWarehouse_View.Where(o => o.CompanyID == companyID).ToList());
                    data.Branches = AutoMapper.Mapper.Map<List<ProductionItemMng_Branch_View>, List<DTO.BranchDTO>>(context.ProductionItemMng_Branch_View.Where(o => o.CompanyID == companyID).ToList());
                    data.outSourcingCostTypeDTOs = AutoMapper.Mapper.Map<List<ProductionItemMng_OutSourcingCostType_View>, List<DTO.OutSourcingCostTypeDTO>>(context.ProductionItemMng_OutSourcingCostType_View.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }
        public DTO.SearchFilterData GetSearchFilter(int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFilterData data = new DTO.SearchFilterData();

            try
            {
                int? companyID = fwFactory.GetCompanyID(userId);
                //data.FactoryWarehouses = this.GetFactoryWarehouse().ToList();
                data.ProductionItemGroups = this.GetProductionItemGroup().ToList();
                data.ProductionItemMaterialTypes = this.GetProductionItemType().ToList();
                data.ProductionItemSpecs = this.GetProductionItemSpec().ToList();
                data.productionItemTypeSupports = this.GetProductionItemType2().ToList();
                //data.YesNoValues = supportFactory.GetYesNo().ToList();

                using (ProductionItemEntities context = CreateContext())
                {
                    data.FactoryWarehouses = AutoMapper.Mapper.Map<List<ProductionItemMng_FactoryWarehouse_View>, List<DTO.FactoryWarehouse>>(context.ProductionItemMng_FactoryWarehouse_View.Where(o => o.CompanyID == companyID).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public List<DTO.ProductionItemGroup> GetProductionItemGroup()
        {
            List<DTO.ProductionItemGroup> result = new List<DTO.ProductionItemGroup>();
            try
            {
                using (ProductionItemEntities context = CreateContext())
                {
                    result = converter.DB2DTO_ProductionItemGroup(context.ProductionItemMng_ProductionItemGroup_View.ToList());
                }
            }
            catch { }

            return result;
        }

        public List<DTO.ProductionItemType> GetProductionItemType()
        {
            List<DTO.ProductionItemType> result = new List<DTO.ProductionItemType>();
            try
            {
                using (ProductionItemEntities context = CreateContext())
                {
                    result = converter.DB2DTO_ProductionItemType(context.ProductionItemMng_ProductionItemType_View.OrderBy(o=>o.ProductionItemTypeNM).ToList());
                }
            }
            catch { }

            return result;
        }

        public List<DTO.ProductionItemSpec> GetProductionItemSpec()
        {
            List<DTO.ProductionItemSpec> result = new List<DTO.ProductionItemSpec>();
            try
            {
                using (ProductionItemEntities context = CreateContext())
                {
                    result = converter.DB2DTO_ProductionItemSpec(context.ProductionItemMng_ProductionItemSpec_View.ToList());
                }
            }
            catch { }

            return result;
        }

        public List<DTO.ProductionItemTypeSupport> GetProductionItemType2()
        {
            List<DTO.ProductionItemTypeSupport> result = new List<DTO.ProductionItemTypeSupport>();
            try
            {
                using (var context = CreateContext())
                {
                    result = converter.DB2DTO_ProductionItemType(context.SupportMng_ProductionItemType_View.ToList());
                }
            }
            catch { }
            return result;
        }
        //Get FactoryRawMaterialSupplier List
        public List<DTO.FactoryRawMaterialSupplier> GetFactoryRawMaterialSupplierList()
        {
            List<DTO.FactoryRawMaterialSupplier> result = new List<DTO.FactoryRawMaterialSupplier>();
            try
            {
                using (ProductionItemEntities context = CreateContext())
                {
                    result = converter.DB2DTO_FactoryRawMaterialSupplier(context.ProductionItemMng_FactoryRawMaterialSupplier_List.ToList());
                }
            }
            catch { }

            return result;
        }

        // Get full factory warehouse for testing , replace with GetFactoryWarehouse function in Module.Support
        public List<DTO.FactoryWarehouse> GetFactoryWarehouse()
        {
            List<DTO.FactoryWarehouse> result = new List<DTO.FactoryWarehouse>();
            try
            {
                using (ProductionItemEntities context = CreateContext())
                {
                    result = converter.DB2DTO_FactoryWarehouse(context.SupportMng_FactoryWarehouse_View.ToList());
                }
            }
            catch { }

            return result;
        }
        
        public string ExportExcelReportData(int userId, int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success};
            ProductionItemObjectData ds = new ProductionItemObjectData();
            Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
            int? companyID = fwFactory.GetCompanyID(userId);
            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("ProductionItemMng_function_GetExcelData", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@ProductionItemID", id);
                adap.SelectCommand.Parameters.AddWithValue("@CompanyID", companyID);
                adap.TableMappings.Add("Table", "ProductionItemExportToExcel");
                adap.Fill(ds);
                return Library.Helper.CreateReportFileWithEPPlus2(ds, "ProductionItem");
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if(ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return string.Empty;               
            }
        }
        public string GetProductionItemExportToExcelList(System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ProductionItemListObject ds = new ProductionItemListObject();

            //try to get data
            try
            {
                //int UserID = -1; ;
                string ProductionItemUD = null;
                string ProductionItemNM = null;
                string ProductionItemVNNM = null;
                int? ProductionItemGroupID = null;
                int? DefaultWarehouseID = null;
                int? ProductionItemTypeID = null;
                bool? isAVTGroup = null;

                //if (filters.ContainsKey("UserID") && !string.IsNullOrEmpty(filters["UserID"].ToString()))
                //{
                //    UserID = Convert.ToInt32(filters["UserID"].ToString());
                //}

                //int? CompanyID = fwFactory.GetCompanyID(UserID);

                if (filters.ContainsKey("ProductionItemUD") && !string.IsNullOrEmpty(filters["ProductionItemUD"].ToString()))
                {
                    ProductionItemUD = filters["ProductionItemUD"].ToString().Replace("'", "''");
                }

                if (filters.ContainsKey("ProductionItemNM") && !string.IsNullOrEmpty(filters["ProductionItemNM"].ToString()))
                {
                    ProductionItemNM = filters["ProductionItemNM"].ToString().Replace("'", "''");
                }

                if (filters.ContainsKey("ProductionItemVNNM") && !string.IsNullOrEmpty(filters["ProductionItemVNNM"].ToString()))
                {
                    ProductionItemVNNM = filters["ProductionItemVNNM"].ToString().Replace("'", "''");
                }

                if (filters.ContainsKey("ProductionItemGroupID") && !string.IsNullOrEmpty(filters["ProductionItemGroupID"].ToString()))
                {
                    ProductionItemGroupID = Convert.ToInt32(filters["ProductionItemGroupID"]);
                }

                if (filters.ContainsKey("DefaultWarehouseID") && !string.IsNullOrEmpty(filters["DefaultWarehouseID"].ToString()))
                {
                    DefaultWarehouseID = Convert.ToInt32(filters["DefaultWarehouseID"]);
                }

                if (filters.ContainsKey("ProductionItemTypeID") && !string.IsNullOrEmpty(filters["ProductionItemTypeID"].ToString()))
                {
                    ProductionItemTypeID = Convert.ToInt32(filters["ProductionItemTypeID"]);
                }

                if (filters.ContainsKey("isAVTGroup") && filters["isAVTGroup"] != null && !string.IsNullOrEmpty(filters["isAVTGroup"].ToString()))
                {
                    switch (filters["isAVTGroup"].ToString().ToLower())
                    {
                        case "true":
                            isAVTGroup = true;
                            break;
                        case "false":
                            isAVTGroup = false;
                            break;
                        default:
                            isAVTGroup = null;
                            break;
                    }
                }
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("ProductionItemMng_function_ExportToExcelList", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@ProductionItemUD", ProductionItemUD);
                adap.SelectCommand.Parameters.AddWithValue("@ProductionItemNM", ProductionItemNM);
                adap.SelectCommand.Parameters.AddWithValue("@ProductionItemVNNM", ProductionItemVNNM);
                adap.SelectCommand.Parameters.AddWithValue("@ProductionItemGroupID", ProductionItemGroupID);
                adap.SelectCommand.Parameters.AddWithValue("@DefaultWarehouseID", DefaultWarehouseID);
                adap.SelectCommand.Parameters.AddWithValue("@ProductionItemTypeID", ProductionItemTypeID);
                adap.SelectCommand.Parameters.AddWithValue("@isAVTGroup", isAVTGroup);
                adap.TableMappings.Add("Table", "ProductionItemExportToExcelList");
                adap.TableMappings.Add("Table1", "ExportToExcelSubUnit");
                adap.Fill(ds);
                return Library.Helper.CreateReportFileWithEPPlus2(ds, "ListProductionItem");
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return string.Empty;
            }
        }
    }
}

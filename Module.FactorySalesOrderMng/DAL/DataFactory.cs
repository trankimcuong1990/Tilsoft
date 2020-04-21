using Library.DTO;
using System;
using Module.FactorySalesOrderMng.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using DAL.AccountMng;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Module.FactorySalesOrderMng.DAL
{
    public class FactorySalesOrderMngFactory : Library.Base.DataFactory<SearchFormData, FactorySalesOrderData>
    {
        private DataConverter converter = new DataConverter();
        private DataFactory _AccountMngFactory = new DataFactory();
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
        public FactorySalesOrderMngFactory()
        {

        }
        private FactorySalesOrderMngEntities CreateContext()
        {
            return new FactorySalesOrderMngEntities(Library.Helper.CreateEntityConnectionString("DAL.FactorySalesOrderMngModel"));
        }

        // get detail a Factory Sale Order
        public FactorySalesOrderData GetData(int UserId, int id, out Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.FactorySalesOrderData data = new DTO.FactorySalesOrderData();
            data.Data = new DTO.FactorySalesOrderDTO();
            DTO.SupplierContactQuickInfo SupplierContactQuickInfo = new DTO.SupplierContactQuickInfo();
            //try to get data
            try
            {
                using (FactorySalesOrderMngEntities context = CreateContext())
                {
                    /// get list to use for create/edit a Factory Sale Order
                    data.lstListRawMaterial = converter.GetListRawMaterial(context.FactorySaleQuotationMng_FactorySaleQuotation_ListRawMaterial_View.ToList());
                    //filter List Customer by company
                    var userInformation = _AccountMngFactory.GetUserInformation(UserId, out notification);
                    if (data.lstListRawMaterial != null && data.lstListRawMaterial.Count > 0)
                    {
                        data.lstListRawMaterial = data.lstListRawMaterial.Where(x => x.CompanyID.HasValue).ToList();
                        if (data.lstListRawMaterial.Count > 0)
                        {
                            if (userInformation != null)
                            {
                                data.lstListRawMaterial.Where(x => x.CompanyID == userInformation.CompanyID).ToList();
                            }
                        }
                    }
                    //end filter                                        
                    data.lstStatus = converter.GetListStatus(context.SupportMng_FactorySaleQuotationStatus_View.ToList());
                    data.lstPaymentTerm = converter.GetListPaymentTearm(context.FactorySaleQuotationMng_FactorySaleQuotation_ListPaymentTearm_View.ToList());
                    data.lstShipmentTypeOption = converter.GetListShipmentTypeOption(context.SupportMng_ShipmentTypeOption_View.ToList());
                    data.lstShipmentToOption = converter.GetListShipmentToOption(context.SupportMng_ShipmentToOption_View.ToList());
                    data.lstWarehouse = converter.GetListListWarehouse(context.FactorySaleQuotationMng_FactorySaleQuotation_ListWarehouse_View.ToList());
                    data.SupplierContactQuickInfo = converter.getSupplierContactQuickInfo(context.FactoryRawMaterialMng_SupplierContactQuickInfo_View.ToList());
                    ///end get list
                    ///update
                    if (id > 0)
                    {
                        var FactorySaleOrder = context.FactorySaleOrderMng_FactorySaleOrder_View.FirstOrDefault(o => o.FactorySaleOrderID == id);
                        if (FactorySaleOrder == null)
                        {
                            throw new Exception("Can not found the Factory Sale Order to edit");

                        }
                        else
                        {
                            data.Data = converter.GetDetailFactorySaleOrder(FactorySaleOrder);
                        }
                    }
                    ///create
                    else
                    {
                        data.Data = new FactorySalesOrderDTO();
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }
        // get list Factory Sale Order
        public DTO.SearchFormData GetDataWithFilter(int UserId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.lstFactorySalesOrder = new List<DTO.FactorySalesOrderSearchDTO>();
            totalRows = 0;

            //try to get data
            try
            {
                string factorySaleOrderUD = null;
                string factorySaleQuotationUD = null;
                int? factoryRawMaterialID = null;
                int? factorySaleOrderStatusID = null;
                string postingDate = null;
                string expectedPaidDate = null;
                string documentDate = null;
                if (filters.ContainsKey("factorySaleOrderUD") && !string.IsNullOrEmpty(filters["factorySaleOrderUD"].ToString()))
                {
                    factorySaleOrderUD = filters["factorySaleOrderUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("factorySaleQuotationUD") && !string.IsNullOrEmpty(filters["factorySaleQuotationUD"].ToString()))
                {
                    factorySaleQuotationUD = filters["factorySaleQuotationUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("factoryRawMaterialID") && !string.IsNullOrEmpty(filters["factoryRawMaterialID"].ToString()))
                {
                    factoryRawMaterialID = Convert.ToInt32(filters["factoryRawMaterialID"]);
                }
                if (filters.ContainsKey("factorySaleOrderStatusID") && !string.IsNullOrEmpty(filters["factorySaleOrderStatusID"].ToString()))
                {
                    factorySaleOrderStatusID = Convert.ToInt32(filters["factorySaleOrderStatusID"]);
                }
                if (filters.ContainsKey("postingDate") && !string.IsNullOrEmpty(filters["postingDate"].ToString()))
                {
                    postingDate = filters["postingDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("expectedPaidDate") && !string.IsNullOrEmpty(filters["expectedPaidDate"].ToString()))
                {
                    expectedPaidDate = filters["expectedPaidDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("documentDate") && !string.IsNullOrEmpty(filters["documentDate"].ToString()))
                {
                    documentDate = filters["documentDate"].ToString().Replace("'", "''");
                }

                DateTime? valpostingDate = postingDate.ConvertStringToDateTime();
                DateTime? valexpectedPaidDate = expectedPaidDate.ConvertStringToDateTime();
                DateTime? valdocumentDate = documentDate.ConvertStringToDateTime();

                using (FactorySalesOrderMngEntities context = CreateContext())
                {
                    data.lstListRawMaterial = converter.GetListRawMaterial(context.FactorySaleQuotationMng_FactorySaleQuotation_ListRawMaterial_View.ToList());
                    //filter List Customer by company
                    var userInformation = _AccountMngFactory.GetUserInformation(UserId, out notification);
                    if (data.lstListRawMaterial != null && data.lstListRawMaterial.Count > 0)
                    {
                        data.lstListRawMaterial = data.lstListRawMaterial.Where(x => x.CompanyID.HasValue).ToList();
                        if (data.lstListRawMaterial.Count > 0)
                        {
                            if (userInformation != null)
                            {
                                data.lstListRawMaterial.Where(x => x.CompanyID == userInformation.CompanyID).ToList();
                            }
                        }
                    }
                    data.lstStatus = converter.GetListStatus(context.SupportMng_FactorySaleQuotationStatus_View.ToList());
                    data.lstSaleQuotation = converter.GetListFactorySaleQuotation(context.FactorySaleOrderMng_ListFactorySaleQuotion_View.ToList());                     

                    totalRows = context.FactorySaleOrderMng_function_SearchResult(factorySaleOrderUD, factoryRawMaterialID, factorySaleQuotationUD, factorySaleOrderStatusID, valpostingDate, valexpectedPaidDate, valdocumentDate, orderBy, orderDirection).Count();
                    var result = context.FactorySaleOrderMng_function_SearchResult(factorySaleOrderUD, factoryRawMaterialID, factorySaleQuotationUD, factorySaleOrderStatusID, valpostingDate, valexpectedPaidDate, valdocumentDate, orderBy, orderDirection);
                    data.lstFactorySalesOrder = converter.FactorySaleOrderGetList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());

                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {

            DTO.FactorySalesOrderDTO dtoFactorySaleOrder = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.FactorySalesOrderDTO>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {

                // Validation for input
                ValidationContext validationDTO = new ValidationContext(dtoFactorySaleOrder, null, null);
                List<ValidationResult> results = new List<ValidationResult>();
                bool valid = Validator.TryValidateObject(dtoFactorySaleOrder, validationDTO, results, true);
                if (!valid)
                {
                    string error = "";
                    List<string> detailError = new List<string>();
                    foreach (ValidationResult vr in results)
                    {
                        notification.Type = NotificationType.Error;
                        error = vr.ErrorMessage;
                        notification.Message = "Value not valid";
                        return false;                        
                    }                    
                }
                
                using (FactorySalesOrderMngEntities context = CreateContext())
                {
                    //var lstSaleQuotation = converter.GetListFactorySaleQuotation(context.FactorySaleOrderMng_ListFactorySaleQuotion_View.ToList());
                    //var existQuotation = lstSaleQuotation.Where(x => x.FactorySaleQuotationID == dtoFactorySaleOrder.FactorySaleQuotationID && x.FactorySaleQuotationUD == dtoFactorySaleOrder.FactorySaleQuotationUD).FirstOrDefault();
                    //if (existQuotation == null)
                    //{
                    //    notification.Type = NotificationType.Error;
                    //    notification.Message = "Sales Quotation Code not exist";                       
                    //    return false;
                    //}
                    var lstcustomer = converter.GetListRawMaterial(context.FactorySaleQuotationMng_FactorySaleQuotation_ListRawMaterial_View.ToList());
                    var existcustomer = lstcustomer.Where(x => x.FactoryRawMaterialID == dtoFactorySaleOrder.FactoryRawMaterialID && x.FactoryRawMaterialShortNM == dtoFactorySaleOrder.FactoryRawMaterialShortNM).FirstOrDefault();
                    if (existcustomer == null)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Customer not exist";                        
                        return false;
                    }
                    //var existClientContact = converter.GetListClientContact(context.FactorySaleQuotationMng_FactorySaleQuotation_ClientContact_View.ToList());
                    //var existClientContacts = existClientContact.Where(x => x.FullName == dtoFactorySaleOrder.FactoryRawMaterialContactPersonNM).FirstOrDefault();
                    //if (existClientContacts == null)
                    //{
                    //    notification.Type = NotificationType.Error;
                    //    notification.Message = "Contact Person Name not exist";                      
                    //    return false;
                    //}
                    //var existEmp = converter.GetListListEmployee(context.FactorySaleQuotationMng_Employee_View.ToList());
                    //var existEmps = existEmp.Where(x => x.EmployeeNM == dtoFactorySaleOrder.EmployeeNM).FirstOrDefault();
                    //if (existEmps == null)
                    //{
                    //    notification.Type = NotificationType.Error;
                    //    notification.Message = "Employee Name not exist";                       
                    //    return false;
                    //}
                    //List<DTO.ProductionItem> data = new List<DTO.ProductionItem>();
                    //var lstchhooseProId = dtoFactorySaleOrder.LstFactorySaleOrderDetail.Select(x => x.ProductionItemID).ToList();
                    //if(lstchhooseProId == null || lstchhooseProId.Count <= 0)
                    //{
                    //    notification.Type = NotificationType.Error;
                    //    notification.Message = "Please choose at least one product Product";                       
                    //    return false;
                    //}
                    var lstProDB = converter.GetListListProductionItem(context.FactorySaleQuotationMng_FactorySaleQuotation_ListProductionItem_View.Where(o=>o.ProductionItemNM != null).ToList());
                    var lstchhooseProName = dtoFactorySaleOrder.LstFactorySaleOrderDetail.Select(x => x.ProductionItemNM).ToList();
                    if (lstchhooseProName != null && lstchhooseProName.Count > 0)
                    {
                        if (lstProDB != null && lstProDB.Count > 0)
                        {

                            foreach (var item in lstchhooseProName)
                            {
                                var existProductName = lstProDB.Where(x => x.ProductionItemNM.Contains(item)).FirstOrDefault();

                                if (existProductName == null)
                                {
                                    notification.Type = NotificationType.Error;
                                    notification.Message = "Product Name not exist";
                                    return false;
                                }
                            }
                        }
                    }
                    if (notification.Type == NotificationType.Error)
                    {
                        return false;
                    }
                    ///end validation

                    FactorySaleOrder dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new FactorySaleOrder();
                        context.FactorySaleOrder.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.FactorySaleOrder.FirstOrDefault(o => o.FactorySaleOrderID == id);
                    }
                    // process Attached files 
                    foreach (FactorySalesOrderAttachedFileDTO dtoattachedFile in dtoFactorySaleOrder.LstFactorySaleOrderAttachedFile)
                    {
                        if (dtoattachedFile.OtherFileHasChange)
                        {
                            if (string.IsNullOrEmpty(dtoattachedFile.NewOtherFile))
                            {
                                fwFactory.RemoveFile(dtoattachedFile.OtherFile);
                            }
                            else
                            {
                                dtoattachedFile.FileUD = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoattachedFile.NewOtherFile, dtoattachedFile.OtherFile, dtoattachedFile.OtherFileFriendlyName);
                            }
                        }
                    }
                    if (dbItem == null)
                    {
                        notification.Message = "Factory Sale Order not found!";
                        return false;
                    }
                    else
                    {
                        
                        var userInformation = _AccountMngFactory.GetUserInformation(userId, out notification);
                        ///convert dto to db
                        converter.updateFactorySaleOrder(dtoFactorySaleOrder, ref dbItem);
                        context.FactorySaleOrderDetail.Local.Where(o => o.FactorySaleOrder == null).ToList().ForEach(o => context.FactorySaleOrderDetail.Remove(o));
                        dbItem.UpdatedDate = DateTime.Now;
                        dbItem.UpdatedBy = userId;
                        dbItem.FactorySaleOrderStatusID = 1;
                        dbItem.DocumentDate = dtoFactorySaleOrder.DocumentDate.ConvertStringToDateTime();
                        dbItem.ExpectedPaidDate = dtoFactorySaleOrder.ExpectedPaidDate.ConvertStringToDateTime();
                        dbItem.CreatedDate = dtoFactorySaleOrder.CreatedDate.ConvertStringToDateTime();
                        dbItem.ValidUntil = dtoFactorySaleOrder.ValidUntil.ConvertStringToDateTime();
                        if (userInformation != null)
                        {
                            dbItem.CompanyID = userInformation.CompanyID;
                        }
                        context.SaveChanges();

                        // Generate FactorySaleOrderUD.
                        if (id == 0)
                        {
                            context.FactorySaleOrderMng_function_GenerateFactorySaleOrderUD(dbItem.FactorySaleOrderID, dbItem.CreatedDate.Value.Year, dbItem.CreatedDate.Value.Month);
                        }

                        context.SaveChanges();
                        dtoItem = GetData(userId, dbItem.FactorySaleOrderID, out notification).Data;
                        return true;
                    }
                }
            }

            catch (Exception ex)
            {
                notification.Message = ex.Message;
                notification.Type = Library.DTO.NotificationType.Error;
                return false;
            }
        }


        // delete Factory Sale Order
        public bool DeleteData(int userId, int id, out Notification notification)
        {
            notification = new Notification { Type = NotificationType.Success };
            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.FactorySaleOrder.Where(o => o.FactorySaleOrderID == id).FirstOrDefault();
                    context.FactorySaleOrder.Remove(dbItem);
                    dbItem.DeletedBy = userId;
                    dbItem.DeletedDate = DateTime.Now;
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                notification = new Notification { Type = NotificationType.Error, Message = ex.Message };
                return false;
            }
        }
        public override SearchFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.FactorySaleOrder.FirstOrDefault(o => o.FactorySaleOrderID == id);
                    if (dbItem == null)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Can not find data!";
                        return false;
                    }

                    dbItem.IsConfirmed = true;
                    dbItem.ConfirmedBy = userId;
                    dbItem.ConfirmedDate = DateTime.Now;
                    dbItem.FactorySaleOrderStatusID = 2;
                    context.SaveChanges();

                    dtoItem = GetData(userId, dbItem.FactorySaleOrderID, out notification).Data;
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

        public override FactorySalesOrderData GetData(int id, out Notification notification)
        {
            throw new NotImplementedException();
        }
        // get filter Factory Quotation
        public List<DTO.FactorySaleOrder> GetFilterOrder(int userID, string searchQuery, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            List<DTO.FactorySaleOrder> data = new List<DTO.FactorySaleOrder>();

            try
            {
                using (var context = CreateContext())
                {

                    Framework.DAL.DataFactory frameworkFactory = new Framework.DAL.DataFactory();
                    int? companyID = frameworkFactory.GetCompanyID(userID);
                    data = converter.GetFilterFactorySaleOrder(context.FactorySaleOrderMng_FilterSaleOrder_View.Where(o => o.FactorySaleOrderUD.Contains(searchQuery)).ToList());
                    if (data != null && data.Count > 0)
                    {
                        data = data.Where(x => !string.IsNullOrEmpty(x.FactorySaleOrderUD)).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }
        // get filter Factory Quotation
        public List<DTO.FactorySaleQuotation> GetFilterQuotation(int userID, string searchQuery, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            List<DTO.FactorySaleQuotation> data = new List<DTO.FactorySaleQuotation>();

            try
            {
                using (var context = CreateContext())
                {

                    Framework.DAL.DataFactory frameworkFactory = new Framework.DAL.DataFactory();
                    int? companyID = frameworkFactory.GetCompanyID(userID);
                    data = converter.GetListFactorySaleQuotation(context.FactorySaleOrderMng_ListFactorySaleQuotion_View.Where(o => o.FactorySaleQuotationUD.Contains(searchQuery)).ToList());
                    if (data != null && data.Count > 0)
                    {
                        data = data.Where(x => !string.IsNullOrEmpty(x.FactorySaleQuotationUD)).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }
        // filter list raw material
        public List<DTO.RawMaterial> GetFilterRawMaterial(int userID, string searchQuery, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            List<DTO.RawMaterial> data = new List<DTO.RawMaterial>();

            try
            {
                Framework.DAL.DataFactory frameworkFactory = new Framework.DAL.DataFactory();
                int? companyID = frameworkFactory.GetCompanyID(userID);
                using (var context = CreateContext())
                {
                    var userInformation = _AccountMngFactory.GetUserInformation(userID, out notification);
                    data = converter.GetListRawMaterial(context.FactorySaleQuotationMng_FactorySaleQuotation_ListRawMaterial_View.Where(o => (o.FactoryRawMaterialShortNM.Contains(searchQuery) || o.FactoryRawMaterialOfficialNM.Contains(searchQuery))).ToList());
                    if (data != null && data.Count > 0)
                    {
                        data = data.Where(x => !string.IsNullOrEmpty(x.FactoryRawMaterialUD)).ToList();
                        //if (userInformation != null)
                        //{
                        //    data = data.Where(x => x.CompanyID == userInformation.CompanyID).ToList();
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }
        // get filter list client contact
        public List<DTO.ClientContact> GetFilterClientcontact(int userID, string searchQuery, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            List<DTO.ClientContact> data = new List<DTO.ClientContact>();

            Framework.DAL.DataFactory frameworkFactory = new Framework.DAL.DataFactory();

            try
            {
                using (var context = CreateContext())
                {
                    data = converter.GetListClientContact(context.FactorySaleQuotationMng_FactorySaleQuotation_ClientContact_View.Where(o => (o.FullName.Contains(searchQuery))).ToList());
                    if (data != null && data.Count > 0)
                    {
                        data = data.Where(x => !string.IsNullOrEmpty(x.FullName)).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }
        //get filter sale employee        
        public List<DTO.SaleEmployee> GetFilterSaleEmployee(int userID, string searchQuery, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            List<DTO.SaleEmployee> data = new List<DTO.SaleEmployee>();

            try
            {
                Framework.DAL.DataFactory frameworkFactory = new Framework.DAL.DataFactory();
                int? companyID = frameworkFactory.GetCompanyID(userID);
                using (var context = CreateContext())
                {
                    data = converter.GetListListEmployee(context.FactorySaleQuotationMng_Employee_View.Where(o => (o.CompanyID == companyID && o.EmployeeNM.Contains(searchQuery))).ToList());
                    if (data != null && data.Count > 0)
                    {
                        data = data.Where(x => !string.IsNullOrEmpty(x.EmployeeNM)).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }
        // get filter product item
        public List<DTO.ProductionItem> GetFilterProductItem(int userID, string searchQuery, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            List<DTO.ProductionItem> data = new List<DTO.ProductionItem>();

            try
            {
                Framework.DAL.DataFactory frameworkFactory = new Framework.DAL.DataFactory();
                int? companyID = frameworkFactory.GetCompanyID(userID);
                using (var context = CreateContext())
                {
                    data = converter.GetListListProductionItem(context.FactorySaleQuotationMng_FactorySaleQuotation_ListProductionItem_View.Where(o => o.ProductionItemNM.Contains(searchQuery) || o.ProductionItemUD.Contains(searchQuery)).ToList());
                    if (data != null && data.Count > 0)
                    {
                        data = data.Where(x => !string.IsNullOrEmpty(x.ProductionItemNM)).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public List<DTO.FactorySaleQuotationDTO> GetFactorySalesQuotation(int userId, string factorySalesQuotationUD, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.FactorySaleQuotationDTO> data = new List<DTO.FactorySaleQuotationDTO>();
            try
            {
                Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                int? companyID = fw_factory.GetCompanyID(userId);
                using (FactorySalesOrderMngEntities context = CreateContext())
                {
                    var x = context.FactorySaleOrderMng_QuickSearchFactorySaleQuotation_View.Where(o => o.CompanyID == companyID && o.FactorySaleQuotationUD.Contains(factorySalesQuotationUD)).ToList();
                    data = AutoMapper.Mapper.Map<List<FactorySaleOrderMng_QuickSearchFactorySaleQuotation_View>, List<DTO.FactorySaleQuotationDTO>>(x);
                    return data;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return new List<DTO.FactorySaleQuotationDTO>();
            }
        }
        public bool Cancel(int userId, int id, ref object dtoItem, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.FactorySaleOrder.FirstOrDefault(o => o.FactorySaleOrderID == id);
                    if (dbItem == null)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Can not find data!";
                        return false;
                    }
                    
                    dbItem.IsDeleted = true;
                    dbItem.DeletedBy = userId;
                    dbItem.DeletedDate = DateTime.Now;
                    dbItem.FactorySaleOrderStatusID = 3;
                    context.SaveChanges();

                    dtoItem = GetData(userId, dbItem.FactorySaleOrderID, out notification).Data;
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

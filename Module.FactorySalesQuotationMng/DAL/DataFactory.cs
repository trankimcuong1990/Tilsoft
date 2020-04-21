using Library.DTO;
using Module.FactorySalesQuotationMng.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.AccountMng;
using Library;
using System.ComponentModel.DataAnnotations;

namespace Module.FactorySalesQuotationMng.DAL
{
    internal class FactorySalesQuotationMngFactory : Library.Base.DataFactory<SearchFormData, FactorySaleQuotationData>
    {
        private DataConverter converter = new DataConverter();
        private DataFactory _AccountMngFactory = new DataFactory();
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
        public FactorySalesQuotationMngFactory()
        {

        }

        private FactorySalesQuotationMngEntities CreateContext()
        {
            return new FactorySalesQuotationMngEntities(Library.Helper.CreateEntityConnectionString("DAL.FactorySalesQuotationMngModel"));
        }
        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.FactorySaleQuotation.FirstOrDefault(o => o.FactorySaleQuotationID == id);
                    if (dbItem == null)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Can not find data!";
                        return false;
                    }

                    dbItem.IsConfirmed = true;
                    dbItem.ConfirmedBy = userId;
                    dbItem.ConfirmedDate = DateTime.Now;
                    dbItem.FactorySaleQuotationStatusID = 2;
                    context.SaveChanges();

                    dtoItem = GetData(userId, dbItem.FactorySaleQuotationID, out notification).Data;
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

        // get detail a Factory Sale Quotation
        public FactorySaleQuotationData GetData(int UserId, int id, out Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.FactorySaleQuotationData data = new DTO.FactorySaleQuotationData();
            data.Data = new DTO.FactorySaleQuotationDTO();
            //try to get data
            try
            {
                using (FactorySalesQuotationMngEntities context = CreateContext())
                {
                    /// get list to use for create/edit a Factory Sales Quotation 
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
                    if (data.lstStatus != null && data.lstStatus.Count > 0)
                    {
                        data.lstStatus = data.lstStatus.Where(x => x.FactorySaleQuotationStatusID.HasValue).ToList();
                    }                
                    data.lstPaymentTerm = converter.GetListPaymentTearm(context.FactorySaleQuotationMng_FactorySaleQuotation_ListPaymentTearm_View.ToList());
                    data.lstShipmentTypeOption = converter.GetListShipmentTypeOption(context.SupportMng_ShipmentTypeOption_View.ToList());
                    data.lstShipmentToOption = converter.GetListShipmentToOption(context.SupportMng_ShipmentToOption_View.ToList());                    
                    data.lstWarehouse = converter.GetListListWarehouse(context.FactorySaleQuotationMng_FactorySaleQuotation_ListWarehouse_View.ToList());
                    data.SupplierContactQuickInfo = converter.getSupplierContactQuickInfo(context.FactoryRawMaterialMng_SupplierContactQuickInfo_View.ToList());
                    ///end get list
                    ///update
                    if (id > 0)
                    {
                        var FactorySaleQuotation = context.FactorySaleQuotationMng_FactorySaleQuotation_View.FirstOrDefault(o => o.FactorySaleQuotationID == id);
                        if (FactorySaleQuotation == null)
                        {
                            throw new Exception("Can not found the Factory Sale Quotation to edit");

                        }
                        else
                        {
                            data.Data = converter.GetDetailFactorySaleQuotation(FactorySaleQuotation);                           

                        }
                    }
                    ///create
                    else
                    {
                        data.Data = new FactorySaleQuotationDTO();
                        data.Data.ValidUntil = DateTime.Now.ToString("dd/MM/yyyy");
                        data.Data.CreatedDate = DateTime.Now.ToString("dd/MM/yyyy");
                        data.Data.ExpectedPaidDate = DateTime.Now.ToString("dd/MM/yyyy");
                        data.Data.DocumentDate = DateTime.Now.ToString("dd/MM/yyyy");
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
        
        // get list Factory Sale Quotation
        public DTO.SearchFormData GetDataWithFilter(int UserId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.lstFactorySaleQuotation = new List<DTO.FactorySaleQuotationSearchDTO>();
            totalRows = 0;

            //try to get data
            try
            {
                string factorySaleQuotationUD = null;
                int? factoryRawMaterialID = null;
                int? factorySaleQuotationStatusID = null;
                string postingDate = null;
                string validUntil = null;
                string documentDate = null;
                string factoryRawMaterialOfficialNM = null;


                if (filters.ContainsKey("factorySaleQuotationUD") && !string.IsNullOrEmpty(filters["factorySaleQuotationUD"].ToString()))
                {
                    factorySaleQuotationUD = filters["factorySaleQuotationUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("factoryRawMaterialID") && !string.IsNullOrEmpty(filters["factoryRawMaterialID"].ToString()))
                {
                    factoryRawMaterialID = Convert.ToInt32(filters["factoryRawMaterialID"]);
                }
                if (filters.ContainsKey("factorySaleQuotationStatusID") && !string.IsNullOrEmpty(filters["factorySaleQuotationStatusID"].ToString()))
                {
                    factorySaleQuotationStatusID = Convert.ToInt32(filters["factorySaleQuotationStatusID"]);
                }
                if (filters.ContainsKey("postingDate") && !string.IsNullOrEmpty(filters["postingDate"].ToString()))
                {
                    postingDate = filters["postingDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("validUntil") && !string.IsNullOrEmpty(filters["validUntil"].ToString()))
                {
                    validUntil = filters["validUntil"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("documentDate") && !string.IsNullOrEmpty(filters["documentDate"].ToString()))
                {
                    documentDate = filters["documentDate"].ToString().Replace("'", "''");
                }
                if(filters.ContainsKey("factoryRawMaterialOfficialNM")  && !string.IsNullOrEmpty(filters["factoryRawMaterialOfficialNM"].ToString()))
                {
                    factoryRawMaterialOfficialNM = filters["factoryRawMaterialOfficialNM"].ToString().Replace("'", "''");
                }

                DateTime? valpostingDate = postingDate.ConvertStringToDateTime();
                DateTime? valValidUntil = validUntil.ConvertStringToDateTime();
                DateTime? valdocumentDate = documentDate.ConvertStringToDateTime();

                using (FactorySalesQuotationMngEntities context = CreateContext())
                {
                    data.lstListRawMaterial = converter.GetListRawMaterial(context.FactorySaleQuotationMng_FactorySaleQuotation_ListRawMaterial_View.ToList());
                    //filter List Customer by company
                    var userInformation = _AccountMngFactory.GetUserInformation(UserId, out notification);
                    if (data.lstListRawMaterial != null && data.lstListRawMaterial.Count > 0)
                    {
                        data.lstListRawMaterial = data.lstListRawMaterial.Where(x => x.CompanyID.HasValue).ToList();                        
                    }                  
                    data.lstStatus = converter.GetListStatus(context.SupportMng_FactorySaleQuotationStatus_View.ToList());
                    if (data.lstStatus != null && data.lstStatus.Count > 0)
                    {
                        data.lstStatus = data.lstStatus.Where(x => x.FactorySaleQuotationStatusID.HasValue).ToList();
                    }
                    data.lstSaleQuotation = converter.GetListFactorySaleQuotation(context.FactorySaleOrderMng_ListFactorySaleQuotion_View.ToList());

                    totalRows = context.FactorySaleQuotationMng_function_SearchResult(factorySaleQuotationUD, factoryRawMaterialID, factorySaleQuotationStatusID, valpostingDate, valValidUntil, valdocumentDate, orderBy, orderDirection).Count();
                    var result = context.FactorySaleQuotationMng_function_SearchResult(factorySaleQuotationUD, factoryRawMaterialID, factorySaleQuotationStatusID, valpostingDate, valValidUntil, valdocumentDate, orderBy, orderDirection);


                    data.lstFactorySaleQuotation = converter.FactorySaleQuotationGetList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());


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

            DTO.FactorySaleQuotationDTO dtoFactorySaleQuotation = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.FactorySaleQuotationDTO>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };           
            try
            {
                if (dtoFactorySaleQuotation.FactoryRawMaterialID == null)
                {
                    notification.Type = NotificationType.Error;
                    notification.Message = "Customer not exist";
                    return false;
                }

                using (FactorySalesQuotationMngEntities context = CreateContext())
                {                   
                    FactorySaleQuotation dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new FactorySaleQuotation();
                        context.FactorySaleQuotation.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.FactorySaleQuotation.FirstOrDefault(o => o.FactorySaleQuotationID == id);
                    }
                    // process Attached files 
                    foreach (FactorySaleQuotationAttachedFileDTO dtoattachedFile in dtoFactorySaleQuotation.LstFactorySaleQuotationAttachedFile)
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
                        notification.Message = "Factory Sale Quotation not found!";
                        return false;
                    }
                    

                    else
                    {
                      
                        ///convert dto to db
                        var userInformation = _AccountMngFactory.GetUserInformation(userId, out notification);                       
                        converter.updateFactorySaleQuotation(dtoFactorySaleQuotation, ref dbItem);
                        context.FactorySaleQuotationDetail.Local.Where(o => o.FactorySaleQuotation == null).ToList().ForEach(o => context.FactorySaleQuotationDetail.Remove(o));
                        context.FactorySaleQuotationAttachedFile.Local.Where(o => o.FactorySaleQuotation == null).ToList().ForEach(o => context.FactorySaleQuotationAttachedFile.Remove(o));
                        dbItem.UpdatedDate = DateTime.Now;
                        dbItem.UpdatedBy = userId;
                        dbItem.FactorySaleQuotationStatusID = 1;
                        dbItem.DocumentDate = dtoFactorySaleQuotation.DocumentDate.ConvertStringToDateTime();                       
                        dbItem.ExpectedPaidDate = dtoFactorySaleQuotation.ExpectedPaidDate.ConvertStringToDateTime();
                        dbItem.CreatedDate = dtoFactorySaleQuotation.CreatedDate.ConvertStringToDateTime();
                        dbItem.ValidUntil = dtoFactorySaleQuotation.ValidUntil.ConvertStringToDateTime();
                        if (userInformation != null)
                        {
                            dbItem.CompanyID = userInformation.CompanyID;
                        }
                        // Generate FactorySaleQuotationUD.
                        if (id == 0)
                        {
                            dbItem.FactorySaleQuotationUD = context.FactorySaleQuotationMng_function_GenerateFactorySaleQuotationUD(dbItem.CreatedDate.Value.Year, dbItem.CreatedDate.Value.Month).FirstOrDefault();
                        }
                        context.SaveChanges();

                        dtoItem = GetData(userId, dbItem.FactorySaleQuotationID, out notification).Data;

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

        public override FactorySaleQuotationData GetData(int id, out Notification notification)
        {
            throw new NotImplementedException();
        }
        
        // delete Factory Sale Quotation
        public bool DeleteData(int userId, int id, out Notification notification)
        {
            notification = new Notification { Type = NotificationType.Success };
            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.FactorySaleQuotation.Where(o => o.FactorySaleQuotationID == id).FirstOrDefault();
                    context.FactorySaleQuotation.Remove(dbItem);
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
                    data = converter.GetListFactorySaleQuotation(context.FactorySaleOrderMng_ListFactorySaleQuotion_View.Where(o => (o.CompanyID == companyID && o.FactorySaleQuotationUD.Contains(searchQuery))).ToList());
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
                using (var context = CreateContext())
                {
                    var userInformation = _AccountMngFactory.GetUserInformation(userID, out notification);
                    data = converter.GetListRawMaterial(context.FactorySaleQuotationMng_FactorySaleQuotation_ListRawMaterial_View.Where(o => (o.FactoryRawMaterialShortNM.Contains(searchQuery) || o.FactoryRawMaterialOfficialNM.Contains(searchQuery))).ToList());
                    if (data != null && data.Count > 0)
                    {
                        data = data.Where(x => !string.IsNullOrEmpty(x.FactoryRawMaterialUD)).ToList();                       
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
        public List<DTO.SaleEmployee> GetFilterSaleEmployee (int userID, string searchQuery, out Library.DTO.Notification notification)
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
                    data = converter.GetListListEmployee(context.FactorySaleQuotationMng_Employee_View.Where(o => o.EmployeeNM.Contains(searchQuery)).ToList());
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
                        data = data.Where(x => !string.IsNullOrEmpty(x.ProductionItemNM) && x.ProductionItemID > 0).ToList();                        
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

        public bool Cancel(int userId, int id, ref object dtoItem, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.FactorySaleQuotation.FirstOrDefault(o => o.FactorySaleQuotationID == id);            
                    if (dbItem == null)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Can not find data!";
                        return false;
                    }          
                    dbItem.IsDeleted = true;
                    dbItem.DeletedBy = userId;
                    dbItem.DeletedDate = DateTime.Now;
                    dbItem.FactorySaleQuotationStatusID = 3;
                    context.SaveChanges();

                    dtoItem = GetData(userId, dbItem.FactorySaleQuotationID, out notification).Data;
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

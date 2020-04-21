using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using System.Data;
using Library.DTO;
using Module.PurchaseInvoiceMng.DTO;

namespace Module.PurchaseInvoiceMng.DAL
{
    internal partial class DataFactory : Library.Base.DataFactory2<DTO.SearchFormData, DTO.EditFormData>
    {
        private Module.Support.DAL.DataFactory supportFactory = new Module.Support.DAL.DataFactory();
        Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();
        private PurchaseInvoiceMngEntities CreateContext()
        {
            return new PurchaseInvoiceMngEntities(Library.Helper.CreateEntityConnectionString("DAL.PurchaseInvoiceMngModel"));
        }
       
        public override bool DeleteData(int userId, int id, out Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (PurchaseInvoiceMngEntities context = CreateContext())
                {
                    var dbItem = context.PurchaseInvoice.Where(o => o.PurchaseInvoiceID == id).FirstOrDefault();
                    context.PurchaseInvoice.Remove(dbItem);                                   
                    context.SaveChanges();
                }
                return true;
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
                return false;
            }
        }

        public override EditFormData GetData(int userId, int id, Hashtable param, out Notification notification)
        {
            DTO.EditFormData editFormData = new DTO.EditFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (PurchaseInvoiceMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        var dbItem = context.PurchaseInvoiceMng_PurchaseInvoice_View.Where(o => o.PurchaseInvoiceID == id).FirstOrDefault();
                        editFormData.Data = converter.DB2DTO_PurchaseInvoice(dbItem);


                    }
                    else
                    {
                        int typeID = Convert.ToInt32(param["typeID"]);
                        if (typeID > 0)
                        {
                            editFormData.Data.PurchaseInvoiceTypeID = typeID;
                            editFormData.Data.PurchaseInvoiceTypeNM = context.SupportMng_PurchaseInvoiceType_View.Where(o => o.PurchaseInvoiceTypeID == typeID).FirstOrDefault().PurchaseInvoiceTypeNM;
                        }
                        //initialize
                        editFormData.Data.PurchaseInvoiceStatusID = 1;
                        editFormData.Data.VAT = 0;
                        editFormData.Data.PurchaseInvoiceDate = DateTime.Now.ToString("dd/MM/yyyy");
                        editFormData.Data.PostingDate = DateTime.Now.ToString("dd/MM/yyyy");
                        editFormData.Data.CreatedDate = DateTime.Now.ToString("dd/MM/yyyy");
                    }
                    //get support data
                    editFormData.PurchaseInvoiceStatusDTOs = AutoMapper.Mapper.Map<List<SupportMng_PurchaseInvoiceStatus_View>, List<DTO.PurchaseInvoiceStatusDTO>>(context.SupportMng_PurchaseInvoiceStatus_View.ToList());
                    editFormData.PurchaseInvoiceTypeDTOs = AutoMapper.Mapper.Map<List<SupportMng_PurchaseInvoiceType_View>, List<DTO.PurchaseInvoiceTypeDTO>>(context.SupportMng_PurchaseInvoiceType_View.ToList());
                    editFormData.FactoryWarehouseDTOs = AutoMapper.Mapper.Map<List<PurchaseInvoiceMng_FactoryWarehouse_View>, List<DTO.FactoryWarehouseDTO>>(context.PurchaseInvoiceMng_FactoryWarehouse_View.ToList());
                    editFormData.FactoryRawMaterialDTOs = converter.DB2DTO_GetFactoryRawMaterial(context.PurchaseInvoiceMng_GetFactoryRawMaterial_View.ToList());
                    editFormData.PaymentTermDTOs = AutoMapper.Mapper.Map<List<PurchaseInvoiceMng_SupplierPaymentTerm_View>, List<DTO.PaymentTermDTO>>(context.PurchaseInvoiceMng_SupplierPaymentTerm_View.Where(o=>o.FactoryRawMaterialID == editFormData.Data.FactoryRawMaterialID).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return editFormData;
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            DTO.PurchaseInvoiceDTO dtoPurchaseInvoice = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.PurchaseInvoiceDTO>();
            notification = new Notification();
            notification.Type = NotificationType.Success;

            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.PurchaseInvoice.FirstOrDefault(o => o.PurchaseInvoiceID == id);
                    if (dbItem == null)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Can not find data!";
                        return false;
                    }
                                       
                    var checkConfirm = false;
                    var itemName = "";
                    if (dtoPurchaseInvoice.PurchaseInvoiceTypeID == 1)
                    {
                        decimal? remain = 0;
                        decimal? total = 0;
                        for (int i = 0; i < dtoPurchaseInvoice.PurchaseInvoiceDetailDTOs.Count; i++)
                        {
                            remain = dtoPurchaseInvoice.PurchaseInvoiceDetailDTOs[i].POReceivedQnt;
                            total = total + dtoPurchaseInvoice.PurchaseInvoiceDetailDTOs[i].Quantity;

                            if ((i + 1) >= dtoPurchaseInvoice.PurchaseInvoiceDetailDTOs.Count || dtoPurchaseInvoice.PurchaseInvoiceDetailDTOs[i + 1].PurchaseOrderID != dtoPurchaseInvoice.PurchaseInvoiceDetailDTOs[i].PurchaseOrderID || dtoPurchaseInvoice.PurchaseInvoiceDetailDTOs[i + 1].ProductionItemID != dtoPurchaseInvoice.PurchaseInvoiceDetailDTOs[i].ProductionItemID)
                            {
                                if (total > remain)
                                {
                                    checkConfirm = true;
                                    itemName = dtoPurchaseInvoice.PurchaseInvoiceDetailDTOs[i].ProductionItemNM;
                                    break;
                                }
                                else
                                {
                                    remain = 0;
                                    total = 0;
                                }
                            }
                        }
                    }
                    if (checkConfirm)
                    {
                        throw new Exception(itemName + " has Quantity larger than ReceivedQnt, Can not confirm!");
                    }
                    else
                    {
                            
                        dbItem.PurchaseInvoiceStatusID = 2;
                        dbItem.SetStatusBy = userId;
                        dbItem.SetStatusDate = DateTime.Now;
                        context.SaveChanges();
                    }
                    
                   
                    // Auto update unit price in receiving note
                    

                    dtoItem = GetData(userId,dbItem.PurchaseInvoiceID, null,out notification).Data;
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
        public override SearchFormData GetDataWithFilter(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            DTO.SearchFormData searchFormData = new DTO.SearchFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            try
            {
                string purchaseInvoiceUD = null;
                string purchaseInvoiceDate = null;
                string postingDate = null;
                int? factoryRawMaterialID = null;
                int? purchaseInvoiceStatusID = null;
                int? purchaseInvoiceTypeID = null;
                string invoiceNo = null;

                if (filters.ContainsKey("purchaseInvoiceUD") && !string.IsNullOrEmpty(filters["purchaseInvoiceUD"].ToString()))
                {
                    purchaseInvoiceUD = filters["purchaseInvoiceUD"].ToString().Replace("'", "''");
                }

                if (filters.ContainsKey("purchaseInvoiceDate") && !string.IsNullOrEmpty(filters["purchaseInvoiceDate"].ToString()))
                {
                    purchaseInvoiceDate = filters["purchaseInvoiceDate"].ToString().Replace("'", "''");
                }

                if (filters.ContainsKey("postingDate") && !string.IsNullOrEmpty(filters["postingDate"].ToString()))
                {
                    postingDate = filters["postingDate"].ToString().Replace("'", "''");
                }

                if (filters.ContainsKey("factoryRawMaterialID") && filters["factoryRawMaterialID"] != null)
                {
                    factoryRawMaterialID = Convert.ToInt32(filters["factoryRawMaterialID"]);
                }

                if (filters.ContainsKey("purchaseInvoiceStatusID") && filters["purchaseInvoiceStatusID"] != null)
                {
                    purchaseInvoiceStatusID = Convert.ToInt32(filters["purchaseInvoiceStatusID"]);
                }

                if (filters.ContainsKey("purchaseInvoiceTypeID") && filters["purchaseInvoiceTypeID"] != null)
                {
                    purchaseInvoiceTypeID = Convert.ToInt32(filters["purchaseInvoiceTypeID"]);
                }

                if (filters.ContainsKey("invoiceNo") && !string.IsNullOrEmpty(filters["invoiceNo"].ToString()))
                {
                    invoiceNo = filters["invoiceNo"].ToString().Replace("'", "''");
                }
                DateTime? valpurchaseInvoiceDate = purchaseInvoiceDate.ConvertStringToDateTime();
                DateTime? valpostingDate = postingDate.ConvertStringToDateTime();
                using (PurchaseInvoiceMngEntities context = CreateContext())
                {
                    var result1 = context.PurchaseInvoiceMng_function_SearchPurchaseInvoice(purchaseInvoiceUD, valpurchaseInvoiceDate, valpostingDate, factoryRawMaterialID, purchaseInvoiceStatusID, purchaseInvoiceTypeID, invoiceNo, orderBy, orderDirection).ToList();
                    totalRows = result1.Count();
                    if(totalRows > 0)
                    {
                        searchFormData.TotalAmountDTO.totalAmountVND = result1.Where(x => x.Currency == "VND").Sum(x => x.Amount);
                        searchFormData.TotalAmountDTO.totalAMountUSD = result1.Where(x => x.Currency == "USD").Sum(x => x.Amount);
                        searchFormData.TotalAmountDTO.totalVATAmount = result1.Sum(x => x.VATAmount);
                        searchFormData.TotalAmountDTO.totalAllAmountVND = searchFormData.TotalAmountDTO.totalVATAmount + searchFormData.TotalAmountDTO.totalAmountVND + (searchFormData.TotalAmountDTO.totalAMountUSD * result1[0].ExchangeRate);
                    }
                    
                    var result = context.PurchaseInvoiceMng_function_SearchPurchaseInvoice(purchaseInvoiceUD, valpurchaseInvoiceDate, valpostingDate, factoryRawMaterialID, purchaseInvoiceStatusID, purchaseInvoiceTypeID, invoiceNo, orderBy, orderDirection);
                    searchFormData.Data = converter.DB2DTO_Search(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                    //get support list
                    searchFormData.PurchaseInvoiceStatusDTOs = AutoMapper.Mapper.Map<List<SupportMng_PurchaseInvoiceStatus_View>, List<DTO.PurchaseInvoiceStatusDTO>>(context.SupportMng_PurchaseInvoiceStatus_View.ToList());
                    searchFormData.PurchaseInvoiceTypeDTOs = AutoMapper.Mapper.Map<List<SupportMng_PurchaseInvoiceType_View>, List<DTO.PurchaseInvoiceTypeDTO>>(context.SupportMng_PurchaseInvoiceType_View.ToList());
                    searchFormData.FactoryRawMaterialDTOs = AutoMapper.Mapper.Map<List<PurchaseInvoiceMng_GetFactoryRawMaterial_View>, List<DTO.FactoryRawMaterialDTO>>(context.PurchaseInvoiceMng_GetFactoryRawMaterial_View.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return searchFormData;
        }

        public override object GetInitData(int userId, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override object GetSearchFilter(int userId, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.PurchaseInvoiceDTO dtoPurchaseInvoice = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.PurchaseInvoiceDTO>();
            try
            {
                using (PurchaseInvoiceMngEntities context = CreateContext())
                {
                    PurchaseInvoice dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new PurchaseInvoice();
                        context.PurchaseInvoice.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.PurchaseInvoice.Where(o => o.PurchaseInvoiceID == id).FirstOrDefault();
                    }
                    if (dbItem == null)
                    {
                        notification.Message = "data not found!";
                        return false;
                    }
                    else
                    {
                        if (!dtoPurchaseInvoice.PurchaseInvoiceTypeID.HasValue)
                        {
                            throw new Exception("Please select type of purchasing invoice");
                        }

                        //upload file
                        Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
                        string tempFolder = FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\";
                        if (dtoPurchaseInvoice.File_HasChange.HasValue && dtoPurchaseInvoice.File_HasChange.Value)
                        {
                            dtoPurchaseInvoice.AttachedFile = fwFactory.CreateFilePointer(tempFolder, dtoPurchaseInvoice.File_NewFile, dtoPurchaseInvoice.AttachedFile, dtoPurchaseInvoice.FriendlyName);
                        }
                        //convert dto to db
                        converter.DTO2DB_PurchaseInvoice(dtoPurchaseInvoice, ref dbItem, userId);

                        if (id == 0)
                        {
                            dbItem.PurchaseInvoiceStatusID = 1;
                            dbItem.SetStatusBy = userId;
                            dbItem.SetStatusDate = DateTime.Now;
                        }
                        //remove orphan
                        context.PurchaseInvoiceDetail.Local.Where(o => o.PurchaseInvoice == null).ToList().ForEach(o => context.PurchaseInvoiceDetail.Remove(o));
                        
                        //save data
                        context.SaveChanges();

                        //generate purchase invoice code
                        if (string.IsNullOrEmpty(dbItem.PurchaseInvoiceUD))
                        {
                            context.PurchaseInvoiceMng_function_GeneratePurchaseInvoiceUD(dbItem.PurchaseInvoiceID, dbItem.PurchaseInvoiceDate.Value.Year, dbItem.PurchaseInvoiceDate.Value.Month);
                        }

                        //get return data
                        dtoItem = GetData(userId, dbItem.PurchaseInvoiceID, null, out notification).Data;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return false;
            }
        }
    }
}

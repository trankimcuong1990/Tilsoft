using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using Library.DTO;
using Module.FactorySaleInvoice.DTO;

namespace Module.FactorySaleInvoice.DAL
{
    internal partial class DataFactory : Library.Base.DataFactory2<DTO.SearchFormData, DTO.EditFormData>
    {
        private Module.Support.DAL.DataFactory supportFactory = new Module.Support.DAL.DataFactory();
        Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int userId, int id, out Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FactorySaleInvoiceEntities context = CreateContext())
                {
                    var dbItem = context.FactorySaleInvoice.Where(o => o.FactorySaleInvoiceID == id).FirstOrDefault();
                    context.FactorySaleInvoice.Remove(dbItem);

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
            Module.Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
            try
            {
                using (FactorySaleInvoiceEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        var dbItem = context.FactorySaleInvoice_FactorySaleInvoice_View.Where(o => o.FactorySaleInvoiceID == id).FirstOrDefault();
                        editFormData.Data = converter.DB2DTO_FactorySaleInvoice(dbItem);
                    }
                    else
                    {
                        //initialize
                        editFormData.Data.FactorySaleInvoiceStatusID = 1;
                        editFormData.Data.VAT = 0;
                        editFormData.Data.InvoiceDate = DateTime.Now.ToString("dd/MM/yyyy");
                        editFormData.Data.PostingDate = DateTime.Now.ToString("dd/MM/yyyy");
                    }
                    //get support data
                    editFormData.FactorySaleInvoiceStatus = AutoMapper.Mapper.Map<List<SupportMng_FactorySaleInvoiceStatus_View>, List<FactorySaleInvoiceStatusDTO>>(context.SupportMng_FactorySaleInvoiceStatus_View.ToList());
                    editFormData.FactoryRawMaterialDTOs = converter.DB2DTO_GetFactoryRawMaterial(context.SupportMng_SubSupplier_View.ToList());
                    editFormData.PaymentTermDTOs = AutoMapper.Mapper.Map<List<FactorySaleInvoice_SupplierPaymentTerm_View>, List<DTO.PaymentTermDTO>>(context.FactorySaleInvoice_SupplierPaymentTerm_View.Where(o => o.FactoryRawMaterialID == editFormData.Data.FactoryRawMaterialID).ToList());
                    editFormData.Seasons = supportFactory.GetSeason();
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return editFormData;
        }

        public override SearchFormData GetDataWithFilter(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            DTO.SearchFormData searchFormData = new DTO.SearchFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            Module.Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();

            try
            {
                string DocCode = null;
                string PostingDate = null;
                int? FactoryRawMaterialID = null;
                string InvoiceStatusID = null;
                int? InvoiceTypeID = null;
                string Season = null;
                string fromDate = null;
                string toDate = null;

                if (filters.ContainsKey("docCode") && !string.IsNullOrEmpty(filters["docCode"].ToString()))
                {
                    DocCode = filters["docCode"].ToString().Replace("'", "''");
                }

                if (filters.ContainsKey("postingDate") && !string.IsNullOrEmpty(filters["postingDate"].ToString()))
                {
                    PostingDate = filters["postingDate"].ToString().Replace("'", "''");
                }

                if (filters.ContainsKey("factoryRawMaterialID") && filters["factoryRawMaterialID"] != null)
                {
                    FactoryRawMaterialID = Convert.ToInt32(filters["factoryRawMaterialID"]);
                }

                if (filters.ContainsKey("invoiceStatusID") && filters["invoiceStatusID"] != null)
                {
                    InvoiceStatusID = filters["invoiceStatusID"].ToString().Replace("'", "''");
                }

                if (filters.ContainsKey("invoiceTypeID") && filters["invoiceTypeID"] != null)
                {
                    InvoiceTypeID = Convert.ToInt32(filters["invoiceTypeID"]);
                }

                if (filters.ContainsKey("season") && filters["season"] != null)
                {
                    Season = filters["season"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("FromDate") && !string.IsNullOrEmpty(filters["FromDate"].ToString()))
                {
                    fromDate = filters["FromDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("ToDate") && !string.IsNullOrEmpty(filters["ToDate"].ToString()))
                {
                    toDate = filters["ToDate"].ToString().Replace("'", "''");
                }
                //DateTime? valInvoiceDate = InvoicedDate.ConvertStringToDateTime();
                DateTime? valpostingDate = PostingDate.ConvertStringToDateTime();

                DateTime? _fromDate = fromDate.ConvertStringToDateTime();
                DateTime? _toDate = toDate.ConvertStringToDateTime();
                using (FactorySaleInvoiceEntities context = CreateContext())
                {
                    var result1 = context.FactorySaleInvoice_function_SearchFactorySaleInvoice(DocCode, valpostingDate, FactoryRawMaterialID, InvoiceStatusID, InvoiceTypeID, Season, _fromDate, _toDate, orderBy, orderDirection).ToList();
                    totalRows = result1.Count();

                    searchFormData.TotalAmountDTO.totalAmountVND = result1.Where(x => x.Currency == "VND").Sum(x => x.Amount);
                    searchFormData.TotalAmountDTO.totalAMountUSD = result1.Where(x => x.Currency == "USD").Sum(x => x.Amount);
                    searchFormData.TotalAmountDTO.totalVATAmount = result1.Sum(x => x.VATAmount);
                    searchFormData.TotalAmountDTO.totalAllAmountVND = searchFormData.TotalAmountDTO.totalVATAmount + searchFormData.TotalAmountDTO.totalAmountVND + (searchFormData.TotalAmountDTO.totalAMountUSD * result1[0].ExchangeRate);

                    //totalRows = context.FactorySaleInvoice_function_SearchFactorySaleInvoice(DocCode, valpostingDate, FactoryRawMaterialID, InvoiceStatusID, InvoiceTypeID, Season, _fromDate, _toDate, orderBy, orderDirection).Count();
                    var result = context.FactorySaleInvoice_function_SearchFactorySaleInvoice(DocCode, valpostingDate, FactoryRawMaterialID, InvoiceStatusID, InvoiceTypeID, Season, _fromDate, _toDate, orderBy, orderDirection);
                    searchFormData.Data = converter.DB2DTO_Search(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                    
                    //get support list
                    searchFormData.FactorySaleInvoiceStatusDTOs = AutoMapper.Mapper.Map<List<SupportMng_FactorySaleInvoiceStatus_View>, List<DTO.FactorySaleInvoiceStatusDTO>>(context.SupportMng_FactorySaleInvoiceStatus_View.ToList());
                //    //searchFormData.PurchaseInvoiceTypeDTOs = AutoMapper.Mapper.Map<List<SupportMng_PurchaseInvoiceType_View>, List<DTO.PurchaseInvoiceTypeDTO>>(context.SupportMng_PurchaseInvoiceType_View.ToList());
                    searchFormData.FactoryRawMaterialDTOs = AutoMapper.Mapper.Map<List<SupportMng_SubSupplier_View>, List<DTO.FactoryRawMaterialDTO>>(context.SupportMng_SubSupplier_View.ToList());
                    searchFormData.Seasons = supportFactory.GetSeason();
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
            DTO.FactorySaleInvoiceDTO dtoFactorySaleInvoice = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.FactorySaleInvoiceDTO>();
            try
            {
                using (FactorySaleInvoiceEntities context = CreateContext())
                {
                    FactorySaleInvoice dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new FactorySaleInvoice();
                        context.FactorySaleInvoice.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.FactorySaleInvoice.Where(o => o.FactorySaleInvoiceID == id).FirstOrDefault();
                    }
                    if (dbItem == null)
                    {
                        notification.Message = "data not found!";
                        return false;
                    }
                    else
                    {
                        //upload file
                        Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
                        string tempFolder = FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\";
                        if (dtoFactorySaleInvoice.File_HasChange.HasValue && dtoFactorySaleInvoice.File_HasChange.Value)
                        {
                            dtoFactorySaleInvoice.AttachedFile = fwFactory.CreateFilePointer(tempFolder, dtoFactorySaleInvoice.File_NewFile, dtoFactorySaleInvoice.AttachedFile, dtoFactorySaleInvoice.FriendlyName);
                        }

                        //convert dto to db
                        converter.DTO2DB_FactorySaleInvoice(dtoFactorySaleInvoice, ref dbItem, userId);

                        if (id == 0)
                        {
                            dbItem.FactorySaleInvoiceStatusID = 1;
                            //dbItem.SetStatusBy = userId;
                            //dbItem.SetStatusDate = DateTime.Now;
                        }
                        //remove orphan
                        context.FactorySaleInvoiceDetail.Local.Where(o => o.FactorySaleInvoice == null).ToList().ForEach(o => context.FactorySaleInvoiceDetail.Remove(o));

                        //save data
                        context.SaveChanges();

                        //generate purchase invoice code
                        if (string.IsNullOrEmpty(dbItem.DocCode))
                        {
                            context.FactorySaleInvoice_function_GenerateFactorySaleInvoiceUD(dbItem.FactorySaleInvoiceID, dbItem.InvoiceDate.Value.Year, dbItem.InvoiceDate.Value.Month);
                        }

                        //get return data
                        dtoItem = GetData(userId, dbItem.FactorySaleInvoiceID, null, out notification).Data;
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

        private FactorySaleInvoiceEntities CreateContext()
        {
            return new FactorySaleInvoiceEntities(Library.Helper.CreateEntityConnectionString("DAL.FactorySaleInvoiceModel"));
        }
    }
}

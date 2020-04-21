using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;
using Library;

namespace Module.ReceiptNoteMng.DAL
{
    internal partial class DataFactory : Library.Base.DataFactory2<DTO.SearchForm, DTO.EditForm>
    {
        Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();
        private ReceiptNoteMngEntities CreateContext()
        {
            return new ReceiptNoteMngEntities(Library.Helper.CreateEntityConnectionString("DAL.ReceiptNoteMngModel"));
        }
        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int userId, int id, out Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            var result = false;
            try
            {
                using (ReceiptNoteMngEntities context = CreateContext())
                {
                    var deleteItem = context.ReceiptNote.Where(o => o.ReceiptNoteID == id).FirstOrDefault();
                    if (deleteItem == null)
                    {
                        throw new Exception("Cound not found item");
                    }
                    context.ReceiptNote.Remove(deleteItem);
                    context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return result;
        }

        public override DTO.EditForm GetData(int userId, int id, Hashtable param, out Notification notification)
        {
            DTO.EditForm editFormData = new DTO.EditForm();
            editFormData.Data = new DTO.ReceiptNoteEditResult();
            editFormData.supportData = new DTO.ReceiptNoteSupport();

            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (var context = CreateContext())
                {
                    if (id > 0)
                    {
                        var dbItem = context.ReceiptNoteMng_ReceiptNoteEdit_View.FirstOrDefault(o => o.ReceiptNoteID == id);
                        editFormData.Data = converter.DB2DTO_EditData(dbItem);
                    }
                    else
                    {
                        editFormData.Data.PostingDate = DateTime.Now.ToString("dd/MM/yyyy");
                        editFormData.Data.StatusID = 1;
                        editFormData.Data.ReceiptNoteTypeID = 1;
                        editFormData.Data.SupplierID = 28;
                        editFormData.Data.SupplierUD = "SHR";
                        editFormData.Data.SupplierNM = "AN VIET THINH CO., LTD";
                        //Exchangerate
                        string currency = "USD";
                        editFormData.Data.ExchangeRate = context.ReceiptNoteMng_MasterExchangeRate_View.Where(s => s.Currency == currency).Select(o => o.ExchangeRate).FirstOrDefault();
                    }

                    //Support Data
                    editFormData.supportData.receiptNoteSupportItemTypes = converter.DB2DTO_SupportItemType(context.ReceiptNoteMng_SupportReceiptNoteItemType_View.ToList());
                    editFormData.supportData.receiptNoteSupportTypes = converter.DB2DTO_SupportType(context.ReceiptNoteMng_SupportReceiptNoteType_View.ToList());
                    editFormData.supportData.receiveSupportTypes = converter.DB2DTO_ReceiveSupportType(context.ReceiptNoteMng_SupportReceiveType_View.ToList());
                    editFormData.supportData.receiptNoteBankAccounts = converter.DB2DTO_BankAccunt(context.ReceiptNoteMng_Support_SupplierBank_View.ToList());

                    return editFormData;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return editFormData;
            }
        }

        public override DTO.SearchForm GetDataWithFilter(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };
            DTO.SearchForm searchData = new DTO.SearchForm();
            totalRows = 0;

            try
            {
                string receiptNoteNo = null;
                string receiptNoteDateString = null;
                string postingDateString = null;
                string clientNM = null;
                string receiverName = null;
                int? statusID = null;
                int? receiptNoteTypeID = null;

                if (filters.ContainsKey("receiptNoteNo") && !string.IsNullOrEmpty(filters["receiptNoteNo"].ToString()))
                {
                    receiptNoteNo = filters["receiptNoteNo"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("receiptNoteDate") && !string.IsNullOrEmpty(filters["receiptNoteDate"].ToString()))
                {
                    receiptNoteDateString = filters["receiptNoteDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("postingDate") && !string.IsNullOrEmpty(filters["postingDate"].ToString()))
                {
                    postingDateString = filters["postingDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("clientNM") && !string.IsNullOrEmpty(filters["clientNM"].ToString()))
                {
                    clientNM = filters["clientNM"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("receiverName") && !string.IsNullOrEmpty(filters["receiverName"].ToString()))
                {
                    receiverName = filters["receiverName"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("statusID") && filters["statusID"] != null)
                {
                    statusID = Convert.ToInt32(filters["statusID"]);
                }
                if (filters.ContainsKey("receiptNoteTypeID") && filters["receiptNoteTypeID"] != null)
                {
                    receiptNoteTypeID = Convert.ToInt32(filters["receiptNoteTypeID"]);
                }

                DateTime? receiptNoteDate = receiptNoteDateString.ConvertStringToDateTime();
                DateTime? postingDate = postingDateString.ConvertStringToDateTime();

                //Add Support Status
                searchData.receiptSupportStatuses.Add(new DTO.ReceiptSupportStatus { StatusID = 1, StatusNM = "Open" });
                searchData.receiptSupportStatuses.Add(new DTO.ReceiptSupportStatus { StatusID = 2, StatusNM = "Confirm" });
                searchData.receiptSupportStatuses.Add(new DTO.ReceiptSupportStatus { StatusID = 3, StatusNM = "Canceled" });

                using (var context = CreateContext())
                {
                    totalRows = context.ReceiptNoteMng_Function_SearchResult(receiptNoteNo, receiptNoteDate, postingDate, receiverName, statusID, receiptNoteTypeID, orderBy, orderDirection).Count();
                    var result = context.ReceiptNoteMng_Function_SearchResult(receiptNoteNo, receiptNoteDate, postingDate, receiverName, statusID, receiptNoteTypeID, orderBy, orderDirection);
                    searchData.Data = converter.DB2DTO_SearchResult(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                    searchData.receiptNoteSupportTypes = converter.DB2DTO_SupportType(context.ReceiptNoteMng_SupportReceiptNoteType_View.ToList());
                }
                return searchData;
            }
            catch (Exception ex)
            {

                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return searchData;
            }
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
            DTO.ReceiptNoteEditResult dtoReceipt = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.ReceiptNoteEditResult>();
            try
            {
                //get companyID
                Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                int? companyID = fw_factory.GetCompanyID(userId);
                using (var context = CreateContext())
                {
                    //Check Client
                    if (dtoReceipt.ReceiptNoteTypeID == 2)
                    {
                        foreach (var item in dtoReceipt.receiptNoteClientResults)
                        {
                            if (item.FactoryRawMaterialID == null)
                            {
                                throw new Exception("missing Client !!!");
                            }
                        }
                    }
                    //if (dtoReceipt.ReceiptNoteTypeID == 3)
                    //{
                    //    foreach (var item in dtoReceipt.receiptNoteOtherResults)
                    //    {
                    //        if (item.EmployeeID == null)
                    //        {
                    //            throw new Exception("missing Client !!!");
                    //        }
                    //    }
                    //}

                    if (dtoReceipt.ReceiptNoteTypeID == 4 || dtoReceipt.ReceiptNoteTypeID == 3)
                    {
                        dtoReceipt.SupplierID = null;
                        dtoReceipt.SupplierNM = null;
                        dtoReceipt.SupplierUD = null;
                    }

                    ReceiptNote dbItem = null;

                    if (id == 0)
                    {
                        if (dtoReceipt.StatusID == 2 || dtoReceipt.StatusID == 3)
                        {
                            throw new Exception("Set Status open to Save !!!");
                        }

                        dbItem = new ReceiptNote();
                        context.ReceiptNote.Add(dbItem);

                        //Automatically generate code
                        if (dtoReceipt.ReceiveTypeID == 1)// Receive type Cash
                        {
                            int year = DateTime.Now.Year;
                            string month = DateTime.Now.Month.ToString().PadLeft(2, '0');
                            string receipt_pattern = "PT" + "_" + year.ToString().Substring(2) + month;
                            var db_receiptNo = context.ReceiptNote.Where(o => o.ReceiptNoteNo.Substring(0, 7) == receipt_pattern).OrderByDescending(o => o.ReceiptNoteNo);
                            if (db_receiptNo.ToList().Count() == 0)
                            {
                                dtoReceipt.ReceiptNoteNo = receipt_pattern + "_" + "001";
                            }
                            else
                            {
                                var select_receipt = db_receiptNo.FirstOrDefault();
                                int iNo = Convert.ToInt32(select_receipt.ReceiptNoteNo.Substring(8, 3)) + 1;
                                dtoReceipt.ReceiptNoteNo = select_receipt.ReceiptNoteNo.Substring(0, 7) + "_" + iNo.ToString().PadLeft(3, '0');
                            }
                        }
                        else if (dtoReceipt.ReceiveTypeID == 2)// Receive type Bank
                        {
                            int year = DateTime.Now.Year;
                            string month = DateTime.Now.Month.ToString().PadLeft(2, '0');
                            string receipt_pattern = "BC" + "_" + year.ToString().Substring(2) + month;
                            var db_receiptNo = context.ReceiptNote.Where(o => o.ReceiptNoteNo.Substring(0, 7) == receipt_pattern).OrderByDescending(o => o.ReceiptNoteNo);
                            if (db_receiptNo.ToList().Count() == 0)
                            {
                                dtoReceipt.ReceiptNoteNo = receipt_pattern + "_" + "001";
                            }
                            else
                            {
                                var select_receipt = db_receiptNo.FirstOrDefault();
                                int iNo = Convert.ToInt32(select_receipt.ReceiptNoteNo.Substring(8, 3)) + 1;
                                dtoReceipt.ReceiptNoteNo = select_receipt.ReceiptNoteNo.Substring(0, 7) + "_" + iNo.ToString().PadLeft(3, '0');
                            }
                        }
                    }
                    else
                    {
                        dbItem = context.ReceiptNote.Where(o => o.ReceiptNoteID == id).FirstOrDefault();

                        if (dbItem.ReceiptNoteTypeID != dtoReceipt.ReceiptNoteTypeID)
                        {
                            throw new Exception("Can't change Receipt Note type !!!");
                        }
                    }
                    if (dbItem == null)
                    {
                        notification.Message = "data not found!";
                        return false;
                    }
                    else
                    {
                        //check  status
                        /*
                         *  WorkOrderStatusID :
                         *      1 : Open
                         *      2 : Confimred                   
                         */

                        if (dbItem.StatusID == 2 || dbItem.StatusID == 3)
                        {
                            throw new Exception("Can't Update because receipt comfirmed !!!");
                        }

                        if (dtoReceipt.StatusID == 2 && dtoReceipt.ReceiptNoteTypeID == 1)
                        {
                            ValidatePayment(dbItem.ReceiptNoteID, 2);
                        }

                        //convert dto to db
                        converter.DTO2DB_Update(dtoReceipt, ref dbItem, userId);

                        //upload file
                        Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
                        string tempFolder = FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\";
                        if (dtoReceipt.File_HasChange.HasValue && dtoReceipt.File_HasChange.Value)
                        {
                            dbItem.AttachedFile = fwFactory.CreateFilePointer(tempFolder, dtoReceipt.File_NewFile, dtoReceipt.AttachedFile, dtoReceipt.FriendlyName);
                        }

                        if (id == 0)
                        {
                            dbItem.CreateDate = DateTime.Now;
                            dbItem.CreateBy = userId;
                        }
                        else
                        {
                            dbItem.UpdateDate = DateTime.Now;
                            dbItem.UpdateBy = userId;
                        }

                        //remove orphan
                        context.ReceiptNoteClient.Local.Where(o => o.ReceiptNote == null).ToList().ForEach(o => context.ReceiptNoteClient.Remove(o));
                        context.ReceiptNoteInvoice.Local.Where(o => o.ReceiptNote == null).ToList().ForEach(o => context.ReceiptNoteInvoice.Remove(o));
                        context.ReceiptNoteOther.Local.Where(o => o.ReceiptNote == null).ToList().ForEach(o => context.ReceiptNoteOther.Remove(o));

                        //save data
                        context.SaveChanges();
                        dtoItem = GetData(userId, dbItem.ReceiptNoteID, null, out notification).Data;
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

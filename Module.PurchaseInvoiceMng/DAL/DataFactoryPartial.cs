using Library;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseInvoiceMng.DAL
{
    internal partial class DataFactory
    {
        public DTO.PurchaseOrderSearchFromData GetPurchaseOrderData(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.PurchaseOrderSearchFromData data = new DTO.PurchaseOrderSearchFromData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            try
            {
                string receivingNoteUD = null;
                string purchaseOrderUD = null;
                string purchaseOrderDate = null;
                string productionItemUD = null;
                string productionItemNM = null;
                int? factoryRawMaterialID = null;
                string FromDate = null;
                string ToDate = null;

                if (filters.ContainsKey("receivingNoteUD") && !string.IsNullOrEmpty(filters["receivingNoteUD"].ToString()))
                {
                    receivingNoteUD = filters["receivingNoteUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("purchaseOrderUD") && !string.IsNullOrEmpty(filters["purchaseOrderUD"].ToString()))
                {
                    purchaseOrderUD = filters["purchaseOrderUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("purchaseOrderDate") && !string.IsNullOrEmpty(filters["purchaseOrderDate"].ToString()))
                {
                    purchaseOrderDate = filters["purchaseOrderDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("productionItemUD") && !string.IsNullOrEmpty(filters["productionItemUD"].ToString()))
                {
                    productionItemUD = filters["productionItemUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("productionItemNM") && !string.IsNullOrEmpty(filters["productionItemNM"].ToString()))
                {
                    productionItemNM = filters["productionItemNM"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("factoryRawMaterialID") && !string.IsNullOrEmpty(filters["factoryRawMaterialID"].ToString()))
                {
                    factoryRawMaterialID = Convert.ToInt32(filters["factoryRawMaterialID"].ToString());
                }
                if (filters.ContainsKey("FromDate") && !string.IsNullOrEmpty(filters["FromDate"].ToString()))
                {
                    FromDate = filters["FromDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("ToDate") && !string.IsNullOrEmpty(filters["ToDate"].ToString()))
                {
                    ToDate = filters["ToDate"].ToString().Replace("'", "''");
                }
                DateTime? valpurchaseOrderDate = purchaseOrderDate.ConvertStringToDateTime();
                DateTime? valFromDate = FromDate.ConvertStringToDateTime();
                DateTime? valToDate = ToDate.ConvertStringToDateTime();
                using (PurchaseInvoiceMngEntities context = CreateContext())
                {
                    totalRows = context.PurchaseInvoiceMng_function_SearchPurchaseOrderItem(orderBy, orderDirection, purchaseOrderUD, valpurchaseOrderDate, productionItemUD, productionItemNM, factoryRawMaterialID, receivingNoteUD, valFromDate, valToDate).Count();
                    var result = context.PurchaseInvoiceMng_function_SearchPurchaseOrderItem(orderBy, orderDirection, purchaseOrderUD, valpurchaseOrderDate, productionItemUD, productionItemNM, factoryRawMaterialID, receivingNoteUD, valFromDate, valToDate).OrderByDescending(o => o.PurchaseOrderUD).OrderByDescending(o => o.PurchaseOrderDate);
                    data.Data = converter.DB2DTO_SearchPurchaseOrderData(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                    data.TotalRows = totalRows;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return data;
        }
        public List<DTO.ProductionItemDTO> GetProductionItem(int userId, Hashtable filters)
        {
            List<DTO.ProductionItemDTO> result = new List<DTO.ProductionItemDTO>();
            using (PurchaseInvoiceMngEntities context = CreateContext())
            {
                Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                string searchQuery = filters["searchQuery"].ToString();
                int? type = Convert.ToInt32(filters["itemType"]);
                var dbItems = new List<PurchaseInvoiceMng_ProductionItem_View>();
                if (type == 0)
                {
                    dbItems = context.PurchaseInvoiceMng_ProductionItem_View.Where(o => (o.ProductionItemNM.Contains(searchQuery) || o.ProductionItemUD.Contains(searchQuery))).ToList();
                }
                if (type == 1)
                {
                    dbItems = context.PurchaseInvoiceMng_ProductionItem_View.Where(o => o.ProductionItemTypeID == 7 && (o.ProductionItemNM.Contains(searchQuery) || o.ProductionItemUD.Contains(searchQuery))).ToList();
                }
                return AutoMapper.Mapper.Map<List<PurchaseInvoiceMng_ProductionItem_View>, List<DTO.ProductionItemDTO>>(dbItems);
            }
        }

        //Status
        public int SetPurchaseInvoiceStatus(int userId, int purchaseInvoiceID, int purchaseInvoiceStatusID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int? oldPurchaseInvoiceStatusID = null;
            try
            {
                using (PurchaseInvoiceMngEntities context = CreateContext())
                {
                    var purchaseInvoiceStatuses = context.SupportMng_PurchaseInvoiceStatus_View.ToList();
                    var currentPurchaseInvoice = context.PurchaseInvoice.Where(o => o.PurchaseInvoiceID == purchaseInvoiceID).FirstOrDefault();
                    oldPurchaseInvoiceStatusID = currentPurchaseInvoice.PurchaseInvoiceStatusID;
                    Hashtable input = new Hashtable();
                    DTO.PurchaseInvoiceDTO dtoItem = GetData(userId, currentPurchaseInvoice.PurchaseInvoiceID, input, out notification).Data;


                    //check valid status
                    int? oldDisplayOrder = purchaseInvoiceStatuses.Where(o => o.PurchaseInvoiceStatusID == oldPurchaseInvoiceStatusID).FirstOrDefault().DisplayOrder;
                    int? newDisplayOrder = purchaseInvoiceStatuses.Where(o => o.PurchaseInvoiceStatusID == purchaseInvoiceStatusID).FirstOrDefault().DisplayOrder;

                    if (newDisplayOrder <= oldDisplayOrder)
                    {
                        string currentStatusName = purchaseInvoiceStatuses.Where(o => o.PurchaseInvoiceStatusID == oldPurchaseInvoiceStatusID).FirstOrDefault().PurchaseInvoiceStatusNM;
                        string newStatusName = purchaseInvoiceStatuses.Where(o => o.PurchaseInvoiceStatusID == purchaseInvoiceStatusID).FirstOrDefault().PurchaseInvoiceStatusNM;
                        throw new Exception("Can not set status from " + currentStatusName + " to " + newStatusName);
                    }
                    else
                    {
                        if (newDisplayOrder == 2)
                        {
                            var checkConfirm = false;
                            var itemName = "";
                            if (dtoItem.PurchaseInvoiceTypeID == 1)
                            {
                                decimal? remain = 0;
                                decimal? total = 0;
                                for (int i = 0; i < dtoItem.PurchaseInvoiceDetailDTOs.Count; i++)
                                {
                                    remain = dtoItem.PurchaseInvoiceDetailDTOs[i].POReceivedQnt;
                                    total = total + dtoItem.PurchaseInvoiceDetailDTOs[i].Quantity;

                                    if ((i + 1) >= dtoItem.PurchaseInvoiceDetailDTOs.Count || dtoItem.PurchaseInvoiceDetailDTOs[i + 1].PurchaseOrderID != dtoItem.PurchaseInvoiceDetailDTOs[i].PurchaseOrderID || dtoItem.PurchaseInvoiceDetailDTOs[i + 1].ProductionItemID != dtoItem.PurchaseInvoiceDetailDTOs[i].ProductionItemID)
                                    {
                                        if (total > remain)
                                        {
                                            checkConfirm = true;
                                            itemName = dtoItem.PurchaseInvoiceDetailDTOs[i].ProductionItemNM;
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
                                notification.Message = purchaseInvoiceStatuses.Where(o => o.PurchaseInvoiceStatusID == purchaseInvoiceStatusID).FirstOrDefault().PurchaseInvoiceStatusNM + " success !!!";
                                currentPurchaseInvoice.PurchaseInvoiceStatusID = purchaseInvoiceStatusID;
                                currentPurchaseInvoice.SetStatusBy = userId;
                                currentPurchaseInvoice.SetStatusDate = DateTime.Now;
                                context.SaveChanges();
                            }

                        }
                        else
                        {
                            if (newDisplayOrder == 3)
                            {
                                var paymentNoteInvoices = context.PurchaseInvoiceMng_ListPaymentNoteInvoice_View.ToList();
                                var checkInvoiceNoCancel = false;
                                foreach (var item in paymentNoteInvoices)
                                {
                                    if (item.PurchaseInvoiceID == purchaseInvoiceID)
                                    {
                                        if (item.StatusID != 3)
                                        {
                                            checkInvoiceNoCancel = true;
                                            break;
                                        }
                                    }
                                }
                                if (checkInvoiceNoCancel == true)
                                {
                                    throw new Exception("This Purchasing Invoice exists in Payment Note and The status is not Cancel");
                                }
                                notification.Message = purchaseInvoiceStatuses.Where(o => o.PurchaseInvoiceStatusID == purchaseInvoiceStatusID).FirstOrDefault().PurchaseInvoiceStatusNM + " success !!!";
                                currentPurchaseInvoice.PurchaseInvoiceStatusID = purchaseInvoiceStatusID;
                                currentPurchaseInvoice.SetStatusBy = userId;
                                currentPurchaseInvoice.SetStatusDate = DateTime.Now;
                                currentPurchaseInvoice.FinishDate = DateTime.Now;
                                context.SaveChanges();
                            }
                            else
                            {
                                notification.Message = purchaseInvoiceStatuses.Where(o => o.PurchaseInvoiceStatusID == purchaseInvoiceStatusID).FirstOrDefault().PurchaseInvoiceStatusNM + " success !!!";
                                currentPurchaseInvoice.PurchaseInvoiceStatusID = purchaseInvoiceStatusID;
                                currentPurchaseInvoice.SetStatusBy = userId;
                                currentPurchaseInvoice.SetStatusDate = DateTime.Now;
                                context.SaveChanges();
                            }
                        }
                    }
                    return purchaseInvoiceStatusID;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return oldPurchaseInvoiceStatusID.Value;
            }
        }

        public List<DTO.PaymentTermDTO> GetSupplierPaymentTerm(int factoryRawMaterialID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            var dbItems = new List<PurchaseInvoiceMng_SupplierPaymentTerm_View>();
            using (PurchaseInvoiceMngEntities context = CreateContext())
            {
                dbItems = context.PurchaseInvoiceMng_SupplierPaymentTerm_View.Where(o => o.FactoryRawMaterialID == factoryRawMaterialID).ToList();
            }
            return AutoMapper.Mapper.Map<List<PurchaseInvoiceMng_SupplierPaymentTerm_View>, List<DTO.PaymentTermDTO>>(dbItems);
        }

        public int Cancel(int userId, int purchaseInvoiceID, int purchaseInvoiceStatusID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int? oldPurchaseInvoiceStatusID = null;
            try
            {
                using (PurchaseInvoiceMngEntities context = CreateContext())
                {
                    var purchaseInvoiceStatuses = context.SupportMng_PurchaseInvoiceStatus_View.ToList();
                    var currentPurchaseInvoice = context.PurchaseInvoice.Where(o => o.PurchaseInvoiceID == purchaseInvoiceID).FirstOrDefault();
                    oldPurchaseInvoiceStatusID = currentPurchaseInvoice.PurchaseInvoiceStatusID;

                    //check valid status
                    int? oldDisplayOrder = purchaseInvoiceStatuses.Where(o => o.PurchaseInvoiceStatusID == oldPurchaseInvoiceStatusID).FirstOrDefault().DisplayOrder;
                    int? newDisplayOrder = purchaseInvoiceStatuses.Where(o => o.PurchaseInvoiceStatusID == purchaseInvoiceStatusID).FirstOrDefault().DisplayOrder;

                    if(oldDisplayOrder == 2)
                    {
                        notification.Message = purchaseInvoiceStatuses.Where(o => o.PurchaseInvoiceStatusID == 4).FirstOrDefault().PurchaseInvoiceStatusNM + " success !!!";
                        currentPurchaseInvoice.PurchaseInvoiceStatusID = 4;
                        currentPurchaseInvoice.SetStatusBy = userId;
                        currentPurchaseInvoice.SetStatusDate = DateTime.Now;
                        context.SaveChanges();
                    }
                    else
                    {
                        if(oldDisplayOrder == 3)
                        {

                            if (context.PaymentNoteInvoice.Where(o=>o.PurchaseInvoiceID == purchaseInvoiceID).Count() > 0)
                            {
                                throw new Exception("Invoice has been created Payment Note. Can not cancel");
                            }
                            else
                            {
                                notification.Message = purchaseInvoiceStatuses.Where(o => o.PurchaseInvoiceStatusID == 4).FirstOrDefault().PurchaseInvoiceStatusNM + " success !!!";
                                currentPurchaseInvoice.PurchaseInvoiceStatusID = 4;
                                currentPurchaseInvoice.SetStatusBy = userId;
                                currentPurchaseInvoice.SetStatusDate = DateTime.Now;
                                context.SaveChanges();
                            }
                        }
                    }
                    return purchaseInvoiceID;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return oldPurchaseInvoiceStatusID.Value;
            }
        }
    }
}

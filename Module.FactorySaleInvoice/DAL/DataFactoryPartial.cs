using Library;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactorySaleInvoice.DAL
{
    internal partial class DataFactory
    {
        public DTO.FactorySaleOrderSearchFromData GetFactorySaleOrderData(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.FactorySaleOrderSearchFromData data = new DTO.FactorySaleOrderSearchFromData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            try
            {
                string factorySaleOrderUD = null;
                string documentDate = null;
                string productionItemUD = null;
                string productionItemNM = null;
                int? factoryRawMaterialID = null;

                if (filters.ContainsKey("factorySaleOrderUD") && !string.IsNullOrEmpty(filters["factorySaleOrderUD"].ToString()))
                {
                    factorySaleOrderUD = filters["factorySaleOrderUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("factorySaleOrderDate") && !string.IsNullOrEmpty(filters["factorySaleOrderDate"].ToString()))
                {
                    documentDate = filters["factorySaleOrderDate"].ToString().Replace("'", "''");
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
                DateTime? valFactorySaleOrderDate = documentDate.ConvertStringToDateTime();
                using (FactorySaleInvoiceEntities context = CreateContext())
                {
                    totalRows = context.FactorySaleInvoice_function_SearchFactorySaleOrderItem(orderBy, orderDirection, factorySaleOrderUD, valFactorySaleOrderDate, productionItemUD, productionItemNM, factoryRawMaterialID).Count();
                    var result = context.FactorySaleInvoice_function_SearchFactorySaleOrderItem(orderBy, orderDirection, factorySaleOrderUD, valFactorySaleOrderDate, productionItemUD, productionItemNM, factoryRawMaterialID).OrderByDescending(o => o.FactorySaleOrderUD).OrderByDescending(o => o.DocumentDate);
                    data.Data = converter.DB2DTO_SearchFactorySaleOrderData(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
            using (FactorySaleInvoiceEntities context = CreateContext())
            {
                Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                string searchQuery = filters["searchQuery"].ToString();
                int? type = Convert.ToInt32(filters["itemType"]);
                var dbItems = new List<FactorySaleInvoice_ProductionItem_View>();
                if (type == 0)
                {
                    dbItems = context.FactorySaleInvoice_ProductionItem_View.Where(o => (o.ProductionItemNM.Contains(searchQuery) || o.ProductionItemUD.Contains(searchQuery))).ToList();
                }
                if (type == 1)
                {
                    dbItems = context.FactorySaleInvoice_ProductionItem_View.Where(o => o.ProductionItemTypeID == 7 && (o.ProductionItemNM.Contains(searchQuery) || o.ProductionItemUD.Contains(searchQuery))).ToList();
                }
                return AutoMapper.Mapper.Map<List<FactorySaleInvoice_ProductionItem_View>, List<DTO.ProductionItemDTO>>(dbItems);
            }
        }

        //Status
        public int SetFactorySaleInvoiceStatus(int userId, int factorySaleInvoiceID, int factorySaleInvoiceStatusID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int? oldFactorySaleInvoiceStatusID = null;
            try
            {
                using (FactorySaleInvoiceEntities context = CreateContext())
                {
                    var purchaseInvoiceStatuses = context.SupportMng_FactorySaleInvoiceStatus_View.ToList();
                    var currentFactorySaleInvoice = context.FactorySaleInvoice.Where(o => o.FactorySaleInvoiceID == factorySaleInvoiceID).FirstOrDefault();
                    oldFactorySaleInvoiceStatusID = currentFactorySaleInvoice.FactorySaleInvoiceStatusID;
                    Hashtable input = new Hashtable();
                    DTO.FactorySaleInvoiceDTO dtoItem = GetData(userId, currentFactorySaleInvoice.FactorySaleInvoiceID, input, out notification).Data;


                    //check valid status
                    int? oldDisplayOrder = purchaseInvoiceStatuses.Where(o => o.FactorySaleInvoiceStatusID == oldFactorySaleInvoiceStatusID).FirstOrDefault().DisplayOrder;
                    int? newDisplayOrder = purchaseInvoiceStatuses.Where(o => o.FactorySaleInvoiceStatusID == factorySaleInvoiceStatusID).FirstOrDefault().DisplayOrder;

                    if (newDisplayOrder <= oldDisplayOrder)
                    {
                        var checkinvoice = context.ReceiptNoteSaleInvoice.Where(o => o.FactorySaleInvoiceID == factorySaleInvoiceID).Count();
                        if(newDisplayOrder == 1 && oldDisplayOrder == 2 && checkinvoice == 0)
                        {
                            notification.Message = purchaseInvoiceStatuses.Where(o => o.FactorySaleInvoiceStatusID == factorySaleInvoiceStatusID).FirstOrDefault().FactorySaleInvoiceStatusNM + " success !!!";
                            currentFactorySaleInvoice.FactorySaleInvoiceStatusID = factorySaleInvoiceStatusID;
                            currentFactorySaleInvoice.UpdatedBy = userId;
                            currentFactorySaleInvoice.UpdatedDate = DateTime.Now;
                            context.SaveChanges();
                        }
                        else
                        {
                            string currentStatusName = purchaseInvoiceStatuses.Where(o => o.FactorySaleInvoiceStatusID == oldFactorySaleInvoiceStatusID).FirstOrDefault().FactorySaleInvoiceStatusNM;
                            string newStatusName = purchaseInvoiceStatuses.Where(o => o.FactorySaleInvoiceStatusID == factorySaleInvoiceStatusID).FirstOrDefault().FactorySaleInvoiceStatusNM;
                            if (checkinvoice > 0)
                            {
                                throw new Exception("Invoice has been created Receipt Note .Can not set status from " + currentStatusName + " to " + newStatusName);
                            }
                            else
                            {
                                throw new Exception("Can not set status from " + currentStatusName + " to " + newStatusName);
                            }
                        }
                    }
                    else
                    {
                        if (newDisplayOrder == 2)
                        {
                            notification.Message = purchaseInvoiceStatuses.Where(o => o.FactorySaleInvoiceStatusID == factorySaleInvoiceStatusID).FirstOrDefault().FactorySaleInvoiceStatusNM + " success !!!";
                            currentFactorySaleInvoice.FactorySaleInvoiceStatusID = factorySaleInvoiceStatusID;
                            //currentFactorySaleInvoice.SetStatusBy = userId;
                            //currentFactorySaleInvoice.SetStatusDate = DateTime.Now;
                            currentFactorySaleInvoice.UpdatedBy = userId;
                            currentFactorySaleInvoice.UpdatedDate = DateTime.Now;
                            context.SaveChanges();

                        }
                        else
                        {
                            if (newDisplayOrder == 3)
                            {
                                notification.Message = purchaseInvoiceStatuses.Where(o => o.FactorySaleInvoiceStatusID == factorySaleInvoiceStatusID).FirstOrDefault().FactorySaleInvoiceStatusNM + " success !!!";
                                currentFactorySaleInvoice.FactorySaleInvoiceStatusID = factorySaleInvoiceStatusID;
                                //currentFactorySaleInvoice.SetStatusBy = userId;
                                //currentFactorySaleInvoice.SetStatusDate = DateTime.Now;
                                //currentFactorySaleInvoice.FinishDate = DateTime.Now;
                                currentFactorySaleInvoice.UpdatedBy = userId;
                                currentFactorySaleInvoice.UpdatedDate = DateTime.Now;
                                context.SaveChanges();
                            }
                            else
                            {
                                notification.Message = purchaseInvoiceStatuses.Where(o => o.FactorySaleInvoiceStatusID == factorySaleInvoiceStatusID).FirstOrDefault().FactorySaleInvoiceStatusNM + " success !!!";
                                currentFactorySaleInvoice.FactorySaleInvoiceStatusID = factorySaleInvoiceStatusID;
                                //currentFactorySaleInvoice.SetStatusBy = userId;
                                //currentFactorySaleInvoice.SetStatusDate = DateTime.Now;
                                currentFactorySaleInvoice.UpdatedBy = userId;
                                currentFactorySaleInvoice.UpdatedDate = DateTime.Now;
                                context.SaveChanges();
                            }
                        }
                    }
                    return factorySaleInvoiceStatusID;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return oldFactorySaleInvoiceStatusID.Value;
            }
        }

        public List<DTO.PaymentTermDTO> GetSupplierPaymentTerm(int factoryRawMaterialID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            var dbItems = new List<FactorySaleInvoice_SupplierPaymentTerm_View>();
            using (FactorySaleInvoiceEntities context = CreateContext())
            {
                dbItems = context.FactorySaleInvoice_SupplierPaymentTerm_View.Where(o => o.FactoryRawMaterialID == factoryRawMaterialID).ToList();
            }
            return AutoMapper.Mapper.Map<List<FactorySaleInvoice_SupplierPaymentTerm_View>, List<DTO.PaymentTermDTO>>(dbItems);
        }
    }
}

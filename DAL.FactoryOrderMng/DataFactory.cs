using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using Module.Support.DAL;
using System.Data.SqlClient;
using Library;

namespace DAL.FactoryOrderMng
{
    public class DataFactory : DALBase.FactoryBase<DTO.FactoryOrderMng.SaleOrderSearch, DTO.FactoryOrderMng.FactoryOrder>
    {
        private DataConverter converter = new DataConverter();
        private DAL.Support.DataFactory supportFactory = new Support.DataFactory();
        private readonly Module.Support.DAL.DataFactory _supportFactory = new Module.Support.DAL.DataFactory();
        private string frontendURL = "http://app.tilsoft.bg/";
        private FactoryOrderMngEntities CreateContext()
        {
            FactoryOrderMngEntities context = new FactoryOrderMngEntities(DALBase.Helper.CreateEntityConnectionString("FactoryOrderMngModel"));
            context.Database.CommandTimeout = 300;
            return context;
        }

        public override IEnumerable<DTO.FactoryOrderMng.SaleOrderSearch> GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            string Season = string.Empty;
            string ProformaInvoiceNo = string.Empty;
            string OfferUD = string.Empty;
            string ClientUD = string.Empty;
            string ClientNM = string.Empty;
            int SaleID = 0;
            int FactoryID = 0;
            string ArticleCode = string.Empty;
            string Description = string.Empty;

            if (filters.ContainsKey("Season") && filters["Season"] != null) Season = filters["Season"].ToString();
            if (filters.ContainsKey("ProformaInvoiceNo")) ProformaInvoiceNo = filters["ProformaInvoiceNo"].ToString();
            if (filters.ContainsKey("OfferUD")) OfferUD = filters["OfferUD"].ToString();
            if (filters.ContainsKey("ClientUD")) ClientUD = filters["ClientUD"].ToString();
            if (filters.ContainsKey("ClientNM")) ClientNM = filters["ClientNM"].ToString();
            if (filters.ContainsKey("SaleID") && filters["SaleID"] != null) SaleID = Convert.ToInt32(filters["SaleID"]);
            if (filters.ContainsKey("FactoryID") && filters["FactoryID"] != null) FactoryID = Convert.ToInt32(filters["FactoryID"]);
            if (filters.ContainsKey("ArticleCode")) ArticleCode = filters["ArticleCode"].ToString();
            if (filters.ContainsKey("Description")) Description = filters["Description"].ToString();

            //try to get data
            try
            {
                using (FactoryOrderMngEntities context = CreateContext())
                {
                    totalRows = context.FactoryOrderMng_function_SearchSaleOrder(orderBy,
                                                                                    orderDirection,
                                                                                    Season,
                                                                                    ProformaInvoiceNo,
                                                                                    OfferUD,
                                                                                    ClientUD,
                                                                                    ClientNM,
                                                                                    SaleID,
                                                                                    FactoryID,
                                                                                    ArticleCode,
                                                                                    Description).Count();

                    var result = context.FactoryOrderMng_function_SearchSaleOrder(orderBy,
                                                                                    orderDirection,
                                                                                    Season,
                                                                                    ProformaInvoiceNo,
                                                                                    OfferUD,
                                                                                    ClientUD,
                                                                                    ClientNM,
                                                                                    SaleID,
                                                                                    FactoryID,
                                                                                    ArticleCode,
                                                                                    Description);

                    return converter.DB2DTO_SaleOrderSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
                return new List<DTO.FactoryOrderMng.SaleOrderSearch>();
            }
        }

        public override DTO.FactoryOrderMng.FactoryOrder GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            //try to get data
            try
            {
                using (FactoryOrderMngEntities context = CreateContext())
                {
                    FactoryOrderMng_FactoryOrder_View dbItem;
                    dbItem = context.FactoryOrderMng_FactoryOrder_View
                        .Include("FactoryOrderMng_FactoryOrderDetail_View.FactoryOrderMng_FactoryOrderColliDetail_View")
                        .Include("FactoryOrderMng_FactoryOrderSparepartDetail_View")
                        .FirstOrDefault(o => o.FactoryOrderID == id);
                    return converter.DB2DTO_FactoryOrder(dbItem);
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
                return new DTO.FactoryOrderMng.FactoryOrder();
            }
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            return true;
        }

        public override bool UpdateData(int id, ref DTO.FactoryOrderMng.FactoryOrder dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FactoryOrderMngEntities context = CreateContext())
                {
                    FactoryOrder dbItem = null;
                    FactoryOrderStatus dbStatus = null;
                    if (id == 0)
                    {
                        dbItem = new FactoryOrder();
                        context.FactoryOrder.Add(dbItem);

                        //SET PI STATUS
                        dbStatus = new FactoryOrderStatus();
                        dbStatus.TrackingStatusID = DALBase.Helper.CREATED;
                        dbStatus.StatusDate = DateTime.Now;
                        dbStatus.UpdatedBy = dtoItem.UpdatedBy;
                        dbStatus.IsCurrentStatus = true;
                        dbItem.FactoryOrderStatus.Add(dbStatus);

                        //SET PI No
                        dtoItem.FactoryOrderUD = GenerateFactoryOrderCode(dtoItem.SaleOrderID);

                        //SET TRACKING INFO
                        dtoItem.CreatedBy = dtoItem.UpdatedBy;
                        dtoItem.CreatedDate = DateTime.Now;                        
                    }
                    else
                    {
                        dbItem = context.FactoryOrder.FirstOrDefault(o => o.FactoryOrderID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Factory Order not found!";
                        return false;
                    }
                    else
                    {
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoItem.ConcurrencyFlag_String)))
                        {
                            throw new Exception(DALBase.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        //check order payment status
                        int? saleOrderID = dtoItem.SaleOrderID;
                        var saleOrder = context.FactoryOrderMng_SaleOrder_View.Where(o => o.SaleOrderID == saleOrderID).FirstOrDefault();
                        if (saleOrder.PaymentStatusID == null)
                        {
                            throw new Exception("You can not create factory order because payment status does not fill-in. You have to go to module sale order to fill-in payment status");
                        }
                        else if (saleOrder.PaymentStatusID == 1 || saleOrder.PaymentStatusID == 2) //LC DONE or DP DONE
                        {
                            if (saleOrder.PaymentDate == null) throw new Exception("You can not create factory order because payment date does not fill-in. You have to go to module sale order to fill-in payment date in case payment status is LC DONE or DP DONE");
                        }
                        else if (saleOrder.PaymentStatusID == 3) //Put In Production Without LC/DP
                        {
                            if (string.IsNullOrEmpty(saleOrder.PaymentRemark)) throw new Exception("You can not create factory order because payment remark does not fill-in. You have to go to module sale order to fill-in payment remark in case put in production without LC/DP");
                        }

                        //check quantity is larger than total loaded
                        var factoryOrderDetails = context.FactoryOrderMng_FactoryOrderDetail_View.Where(o => o.FactoryOrderID == id).ToList();
                        if (factoryOrderDetails != null)
                        {
                            foreach (var item in dtoItem.FactoryOrderDetails.Where(o => o.FactoryOrderDetailID > 0))
                            {
                                int orderQnt = (item.OrderQnt.HasValue ? item.OrderQnt.Value : 0);
                                //get loaded qnt
                                int loadedQnt = 0;
                                var x = factoryOrderDetails.Where(o => o.FactoryOrderDetailID == item.FactoryOrderDetailID).FirstOrDefault();
                                if (x != null)
                                {
                                    loadedQnt = (x.LoadedQnt.HasValue ? x.LoadedQnt.Value : 0);
                                }
                                if (orderQnt < loadedQnt)
                                {
                                    throw new Exception("Product " + item.ArticleCode + "(" + item.Description + ") has been loaded " + loadedQnt.ToString() + ". You can not change order quantity less than loaded quantity");
                                }
                            }
                        }

                        //Check quantity of sample
                        if (dtoItem.FactoryOrderSampleDetails.Count() > 0)
                        {
                            var SaleOrderID_temp = dtoItem.SaleOrderID;
                            var factoryOrderSamples = context.FactoryOrderMng_SaleOrderDetailSample_View.Where(o => o.SaleOrderID == SaleOrderID_temp).ToList();
                            foreach (var item in dtoItem.FactoryOrderSampleDetails)
                            {
                                var factoryOrderSampleItem = factoryOrderSamples.Where(o => o.SaleOrderDetailSampleID == item.SaleOrderDetailSampleID).ToList();
                                if (item.OrderQnt > factoryOrderSampleItem[0].OrderQnt)
                                {
                                    notification.Type = Library.DTO.NotificationType.Warning;
                                    notification.Message = "Quantity of sample <b>" + item.ArticleCode + "</b> can't greater than quantity on Sale Order (Quantity must less than or equal " + factoryOrderSampleItem[0].OrderQnt + ")";
                                    return false;
                                }
                            }
                        }

                        //convert data
                        converter.DTO2DB_FactoryOrder(dtoItem, ref dbItem);
                        context.SaveChanges();

                        //reload data
                        dtoItem = GetData(dbItem.FactoryOrderID, out notification);
                        return true;
                    }
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
                return false;
            }
        }

        public bool DeleteFactoryOrder(int id, int deletedBy, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Factory Order has been deleted" };
            try
            {
                using (FactoryOrderMngEntities context = CreateContext())
                {
                    FactoryOrderStatus dbStatus = context.FactoryOrderStatus.Where(o => o.FactoryOrderID == id && o.IsCurrentStatus == true).FirstOrDefault();
                    if (dbStatus.TrackingStatusID == DALBase.Helper.CONFIRMED || dbStatus.TrackingStatusID == DALBase.Helper.REVISION_CONFIRMED)
                    {
                        throw new Exception("Factory Order was confirmed. You can not delete");
                    }

                    if (dbStatus.TrackingStatusID == DALBase.Helper.REVISED)
                    {
                        throw new Exception("Factory Order was revised. You can not delete");
                    }

                    context.FactoryOrderMng_function_DeleteFactoryOrder(id, deletedBy);
                    return true;
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
                return false;
            }
        }

        public DTO.FactoryOrderMng.DataContainer GetDataContainer(int id, int saleOrderID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            //try to get data
            try
            {
                DTO.FactoryOrderMng.DataContainer dtoItem = new DTO.FactoryOrderMng.DataContainer();
                using (FactoryOrderMngEntities context = CreateContext())
                {

                    if (id > 0)
                    {
                        FactoryOrderMng_FactoryOrder_View dbItem;
                        dbItem = context.FactoryOrderMng_FactoryOrder_View
                                .Include("FactoryOrderMng_FactoryOrderDetail_View.FactoryOrderMng_FactoryOrderColliDetail_View")
                                .Include("FactoryOrderMng_FactoryOrderSparepartDetail_View")
                                .Include("FactoryOrderMng_FactoryOrderSampleDetail_View")
                                .FirstOrDefault(o => o.FactoryOrderID == id);
                        DTO.FactoryOrderMng.FactoryOrder FactoryOrderDTOItem = converter.DB2DTO_FactoryOrder(dbItem);
                        dtoItem.FactoryOrderData = FactoryOrderDTOItem;
                        ////tracking confirm
                        //foreach (FactoryOrderStatus item in context.FactoryOrderStatus.Where(o => o.FactoryOrderID == id))
                        //{
                        //    if (item.IsCurrentStatus == true)
                        //    {
                        //        dtoItem.FactoryOrderData.UpdatedBy = item.UpdatedBy;
                        //        dtoItem.FactoryOrderData.UpdatedDateFormated = Convert.ToString(item.StatusDate);
                        //    }
                        //}
                    }
                    else
                    {
                        FactoryOrderMng_SaleOrder_View dbSaleOrder = context.FactoryOrderMng_SaleOrder_View
                                                                    //.Include("FactoryOrderMng_SaleOrderDetail_View")
                                                                    //.Include("FactoryOrderMng_SaleOrderDetailSparepart_View")
                                                                    .FirstOrDefault(o => o.SaleOrderID == saleOrderID);

                        dtoItem.FactoryOrderData = new DTO.FactoryOrderMng.FactoryOrder();
                        dtoItem.FactoryOrderData.FactoryOrderDetails = new List<DTO.FactoryOrderMng.FactoryOrderDetail>();
                        dtoItem.FactoryOrderData.FactoryOrderSparepartDetails = new List<DTO.FactoryOrderMng.FactoryOrderSparepartDetail>();
                        dtoItem.FactoryOrderData.FactoryOrderSampleDetails = new List<DTO.FactoryOrderMng.FactoryOrderSampleDetail>();

                        //SET INIT VALUE
                        dtoItem.FactoryOrderData.SaleOrderID = saleOrderID;
                        dtoItem.FactoryOrderData.ProformaInvoiceNo = dbSaleOrder.ProformaInvoiceNo;
                        dtoItem.FactoryOrderData.OrderDateFormated = DateTime.Now.ToString("dd/MM/yyyy");
                        dtoItem.FactoryOrderData.TrackingStatusNM = DALBase.Helper.TEXT_STATUS_CREATED;

                        if(dbSaleOrder.FactoryOrderMng_SaleOrderDetailSample_View.Count() > 0)
                        {
                            dtoItem.FactoryOrderData.OrderTypeID = 2;
                        }
                        else
                        {
                            dtoItem.FactoryOrderData.OrderTypeID = 1;
                        }

                        dtoItem.FactoryOrderData.ItemStandardID = 3;
                        dtoItem.FactoryOrderData.TestSamplingOptionID = 4;
                        dtoItem.FactoryOrderData.LabelingOptionID = 2;
                        dtoItem.FactoryOrderData.PackagingOptionID = 2;
                        dtoItem.FactoryOrderData.InspectionOptionID = 4;
                        dtoItem.FactoryOrderData.ShipmentToOptionID = 1;
                        dtoItem.FactoryOrderData.ShipmentTypeOptionID = 3;

                        //Get totalQuantityOrdered
                        List<FactoryOrderMng_function_GetTotalOrderQntBySaleOrderID_Result> lstTotalQnt = context.FactoryOrderMng_function_GetTotalOrderQntBySaleOrderID(saleOrderID).ToList();

                        int i = -1;
                        foreach (FactoryOrderMng_SaleOrderDetail_View dbDetail in dbSaleOrder.FactoryOrderMng_SaleOrderDetail_View.Where(o=>o.OfferSeasonTypeID == 2 || o.OfferSeasonTypeID == 3))
                        {
                            DTO.FactoryOrderMng.FactoryOrderDetail dtoDetail = new DTO.FactoryOrderMng.FactoryOrderDetail();

                            dtoDetail.FactoryOrderDetailID = i;
                            dtoDetail.SaleOrderDetailID = dbDetail.SaleOrderDetailID;
                            dtoDetail.ArticleCode = dbDetail.ArticleCode;
                            dtoDetail.Description = dbDetail.Description;
                            dtoDetail.SaleOrderQnt = dbDetail.OrderQnt;

                            //calc Quantity Remain
                            var tmp = lstTotalQnt.Where(s => s.SaleOrderDetailID == dbDetail.SaleOrderDetailID).FirstOrDefault();
                            dtoDetail.OrderQnt = dbDetail.OrderQnt - ((tmp != null) ? tmp.TotalOrderQnt : 0);
                            dtoDetail.RemainQnt = dtoDetail.OrderQnt;
                            if(dtoDetail.RemainQnt > 0)
                            {
                                dtoItem.FactoryOrderData.FactoryOrderDetails.Add(dtoDetail);
                                i = i - 1;
                            }
                        }

                        //Get totalQuantityOrdered
                        List<FactoryOrderMng_function_GetSparepartTotalOrderQntBySaleOrderID_Result> lstSparepartTotalQnt = context.FactoryOrderMng_function_GetSparepartTotalOrderQntBySaleOrderID(saleOrderID).ToList();

                        i = -1;
                        foreach (var item in dbSaleOrder.FactoryOrderMng_SaleOrderDetailSparepart_View)
                        {
                            DTO.FactoryOrderMng.FactoryOrderSparepartDetail dtoSparepart = new DTO.FactoryOrderMng.FactoryOrderSparepartDetail();

                            dtoSparepart.FactoryOrderSparepartDetailID = i;
                            dtoSparepart.SaleOrderDetailSparepartID = item.SaleOrderDetailSparepartID;
                            dtoSparepart.ArticleCode = item.ArticleCode;
                            dtoSparepart.Description = item.Description;

                            //calc Quantity Remain
                            var tmp = lstSparepartTotalQnt.Where(s => s.SaleOrderDetailSparepartID == item.SaleOrderDetailSparepartID).FirstOrDefault();
                            dtoSparepart.OrderQnt = item.OrderQnt - ((tmp != null) ? tmp.TotalOrderQnt : 0);
                            dtoSparepart.RemainQnt = item.OrderQnt;
                            if (dtoSparepart.RemainQnt > 0)
                            {
                                dtoItem.FactoryOrderData.FactoryOrderSparepartDetails.Add(dtoSparepart);
                                i = i - 1;
                            }
                        }

                        //Get totalQuantityOrdered
                        //List<FactoryOrderMng_function_GetSampleTotalOrderQntBySaleOrderID_Result> lstSampleTotalQnt = context.FactoryOrderMng_function_GetSampleTotalOrderQntBySaleOrderID(saleOrderID).ToList();

                        i = -1;
                        foreach (var item in dbSaleOrder.FactoryOrderMng_SaleOrderDetailSample_View)
                        {
                            DTO.FactoryOrderMng.FactoryOrderSampleDetail dtoSample = new DTO.FactoryOrderMng.FactoryOrderSampleDetail();

                            dtoSample.FactoryOrderSampleDetailID = i;
                            dtoSample.SaleOrderDetailSampleID = item.SaleOrderDetailSampleID;
                            dtoSample.ArticleCode = item.ArticleCode;
                            dtoSample.Description = item.Description;
                            dtoSample.OrderQnt = item.OrderQnt;
                            dtoSample.PackagingMethodText = item.PackagingMethodText;
                            dtoSample.PlaningPurchasingPrice = item.PlaningPurchasingPrice;
                            dtoSample.PlaningPurchasingPriceSourceID = item.PlaningPurchasingPriceSourceID;
                            dtoSample.Qnt40HC = item.Qnt40HC;
                            dtoSample.FileLocation = FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(item.FileLocation) ? item.FileLocation : "no-image.jpg");
                            dtoSample.ThumbnailLocation = FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(item.ThumbnailLocation) ? item.ThumbnailLocation : "no-image.jpg");
                            dtoSample.SalePrice = item.SalePrice;
                            dtoItem.FactoryOrderData.FactoryOrderSampleDetails.Add(dtoSample);
                        }                        
                    }

                    // get support data
                    dtoItem.Factories = converter.DB2DTO_GetFacctories(context.FactoryOrderMng_GetFactories_View.ToList());
                    //dtoItem.Factories = supportFactory.GetFactory().ToList();
                    dtoItem.Users = supportFactory.GetUser().ToList();
                    dtoItem.RateOrder = GetRateOrder().ToList();
                    dtoItem.SupplyChainPersons = supportFactory.GetSupplyChainPerson().ToList();
                    dtoItem.OrderTypes = supportFactory.GetOrderType().ToList();
                    dtoItem.ItemStandards = supportFactory.GetItemStandard().ToList();
                    dtoItem.TestSamplingOptions = supportFactory.GetTestSamplingOption().ToList();
                    dtoItem.LabelingOptions = supportFactory.GetLabelingOption().ToList();
                    dtoItem.PackagingOptions = supportFactory.GetPackagingOption().ToList();
                    dtoItem.InspectionOptions = supportFactory.GetInspectionOption().ToList();
                    dtoItem.ShipmentToOptions = supportFactory.GetShipmentToOption().ToList();
                    dtoItem.ShipmentTypeOptions = supportFactory.GetShipmentTypeOption().ToList();

                }

                using (var context2 = CreateContext())
                {
                    var clientComplaint = context2.ClientComplaint_ClientComplaintItem_View.Where(o => o.CreatedPINumberSolution == dtoItem.FactoryOrderData.ProformaInvoiceNo);
                    if (dtoItem.FactoryOrderData != null)
                    {
                        dtoItem.FactoryOrderData.ListClientComplaintIDs = new List<DTO.FactoryOrderMng.ClientComplaintLink>();
                    }

                    DTO.FactoryOrderMng.ClientComplaintLink ComplaintLink = new DTO.FactoryOrderMng.ClientComplaintLink();
                    if (clientComplaint.Count() > 0)
                    {
                        foreach (var complaint in clientComplaint.ToList())
                        {
                            var clientCode = context2.ClientComplaint_ClientComplaint_View.Where(o => o.ClientComplaintID == complaint.ClientComplaintID).FirstOrDefault().ClientUD.ToString();
                            if (!string.IsNullOrEmpty(clientCode))
                            {
                                ComplaintLink.ArticleCode = complaint.ArticleCode;
                                ComplaintLink.ClientComplaintID = complaint.ClientComplaintID;
                                ComplaintLink.ClientCode = clientCode;
                                dtoItem.FactoryOrderData.ListClientComplaintIDs.Add(ComplaintLink);
                            }
                        }
                    }

                    return dtoItem;
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
                return new DTO.FactoryOrderMng.DataContainer();
            }
        }

        private IEnumerable<DTO.FactoryOrderMng.RateOrder> GetRateOrder()
        {
            List<DTO.FactoryOrderMng.RateOrder> ReteOrders = new List<DTO.FactoryOrderMng.RateOrder>();

            ReteOrders.Add(new DTO.FactoryOrderMng.RateOrder { RateValue = "NORMAL", RateText = "NORMAL" });
            ReteOrders.Add(new DTO.FactoryOrderMng.RateOrder { RateValue = "HIGH", RateText = "HIGH" });
            ReteOrders.Add(new DTO.FactoryOrderMng.RateOrder { RateValue = "EXTREME", RateText = "EXTREME" });

            return ReteOrders;
        }

        public IEnumerable<DTO.FactoryOrderMng.FactoryOrderSearch> SearchFactoryOrders(int saleOrderID, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            //try to get data
            try
            {
                using (FactoryOrderMngEntities context = CreateContext())
                {
                    totalRows = context.FactoryOrderMng_FactoryOrderSearch_View.Where(o => o.SaleOrderID == saleOrderID).Count();
                    var result = context.FactoryOrderMng_FactoryOrderSearch_View.Where(o => o.SaleOrderID == saleOrderID);
                    return converter.DB2DTO_FactoryOrderSearch(result.ToList());
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
                return new List<DTO.FactoryOrderMng.FactoryOrderSearch>();
            }
        }

        public IEnumerable<DTO.FactoryOrderMng.FactoryOrderDetailSearch> SearchFactoryOrderDetails(int factoryOrderID, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            //try to get data
            try
            {
                using (FactoryOrderMngEntities context = CreateContext())
                {
                    totalRows = context.FactoryOrderMng_FactoryOrderDetailSearch_View.Where(o => o.FactoryOrderID == factoryOrderID).Count();
                    var result = context.FactoryOrderMng_FactoryOrderDetailSearch_View.Where(o => o.FactoryOrderID == factoryOrderID);
                    return converter.DB2DTO_FactoryOrderDetailSearch(result.ToList());
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
                return new List<DTO.FactoryOrderMng.FactoryOrderDetailSearch>();
            }
        }

        private void SetStatus(int factoryOrderID, int trackingStatusID, int setStatusBy)
        {
            using (FactoryOrderMngEntities context = CreateContext())
            {
                foreach (FactoryOrderStatus item in context.FactoryOrderStatus.Where(o => o.FactoryOrderID == factoryOrderID))
                {
                    item.IsCurrentStatus = false;
                }

                FactoryOrderStatus dbStatus = new FactoryOrderStatus();
                dbStatus.FactoryOrderID = factoryOrderID;
                dbStatus.TrackingStatusID = trackingStatusID;
                dbStatus.StatusDate = DateTime.Now;
                dbStatus.UpdatedBy = setStatusBy;
                dbStatus.IsCurrentStatus = true;
                context.FactoryOrderStatus.Add(dbStatus);

                FactoryOrder factoryOrder = context.FactoryOrder.Where(o => o.FactoryOrderID == factoryOrderID).FirstOrDefault();
                factoryOrder.UpdatedBy = setStatusBy;
                factoryOrder.UpdatedDate = DateTime.Now;

                context.SaveChanges();

                // add item to quotation if needed
                //context.FW_Quotation_function_AddFactoryOrderItem(null, null, factoryOrderID); // table lockx and also check if item is available on sql server side
            }
        }

        public bool Confirm(int factoryOrderID, ref DTO.FactoryOrderMng.FactoryOrder dtoItem, int setStatusBy, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Factory Order has been confirmed success" };
            List<DTO.FactoryOrderMng.EmailNotificationConfirmed> dataEmailNotificationConfirmed = new List<DTO.FactoryOrderMng.EmailNotificationConfirmed>();
            try
            {
                //SAVE DATA
                Library.DTO.Notification save_nofication = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
                if (!UpdateData(factoryOrderID, ref dtoItem, out save_nofication))
                {
                    notification = save_nofication;
                    return false;
                }

                //VALIDATE && CONFIRM
                using (FactoryOrderMngEntities context = CreateContext())
                {
                    FactoryOrder dbFactoryOrder = context.FactoryOrder.Where(o => o.FactoryOrderID == factoryOrderID).FirstOrDefault();
                    SaleOrderStatus dbSaleOrderStatus = context.SaleOrderStatus.Where(o => o.SaleOrderID == dbFactoryOrder.SaleOrderID && o.IsCurrentStatus == true).FirstOrDefault();

                    if (dbSaleOrderStatus.TrackingStatusID != DALBase.Helper.CONFIRMED && dbSaleOrderStatus.TrackingStatusID != DALBase.Helper.REVISION_CONFIRMED)
                    {
                        notification = new Library.DTO.Notification() { Message = "PI must be in CONFIRMED status before confirm Factory Order", Type = Library.DTO.NotificationType.Warning };
                        return false;
                    }

                    FactoryOrderStatus dbFactoryOrderStatus = context.FactoryOrderStatus.Where(o => o.FactoryOrderID == factoryOrderID && o.IsCurrentStatus == true).FirstOrDefault();
                    if (dbFactoryOrderStatus.TrackingStatusID != DALBase.Helper.REVISED && dbFactoryOrderStatus.TrackingStatusID != DALBase.Helper.CREATED)
                    {
                        notification = new Library.DTO.Notification() { Message = "Factory Order must be in REVISED/CREATED status before confirm", Type = Library.DTO.NotificationType.Warning };
                        return false;
                    }

                    int TrackingStatusID = (dbFactoryOrderStatus.TrackingStatusID == DALBase.Helper.CREATED ? DALBase.Helper.CONFIRMED : DALBase.Helper.REVISION_CONFIRMED);

                    SetStatus(factoryOrderID, TrackingStatusID, setStatusBy);
                    dtoItem.TrackingStatusNM = (dbFactoryOrderStatus.TrackingStatusID == DALBase.Helper.CREATED ? DALBase.Helper.TEXT_STATUS_CONFIRMED : DALBase.Helper.TEXT_STATUS_REVISION_CONFIRMED);
                    dtoItem.TrackingStatusID = TrackingStatusID;                   
                    string bodyEmail = "";
                    string season = Library.Helper.GetCurrentSeason();
                    dataEmailNotificationConfirmed = converter.DB2DTO_EmailNotificationConfirmed(context.FactoryOrderMng_EmailNotificationCofirmed_View.Where(s => s.Season == season && s.FactoryOrderID == factoryOrderID && s.TrackingStatusID == TrackingStatusID).ToList());
                    for (int j = 0; j < dataEmailNotificationConfirmed.Count; j++)
                    {
                        var list = dataEmailNotificationConfirmed[j];
                        if (j == 0)
                        {
                            bodyEmail = bodyEmail + "Proforma InvoiceNo: " + list.ProformaInvoiceNo + Environment.NewLine;
                            bodyEmail = bodyEmail + "  - Factory Order Code: " + list.FactoryOrderUD + Environment.NewLine;
                        }
                        if (j > 0)
                        {
                            bodyEmail = bodyEmail + Environment.NewLine;
                        }
                        bodyEmail = bodyEmail + "     + ArticleCode: " + list.ArticleCode;
                    }
                    string emailSubject = "Factory Order: ArticleCode have product type = set but no piece defined";
                    if (bodyEmail != "")
                    {
                        SendNotification(emailSubject, bodyEmail);
                    }

                    // add item to quotation if needed
                    context.FW_Quotation_function_AddFactoryOrderItem(factoryOrderID); // table lockx and also check if item is available on sql server side

                    // add QAQC
                    if (!string.IsNullOrEmpty(dtoItem.ProductionStatus))
                        foreach (var item in dtoItem.FactoryOrderDetails)
                        {
                            if ((item.OrderQnt.HasValue && item.OrderQnt.Value > 0) || (item.QAQCID.HasValue && item.QAQCID.Value > 0))
                                context.FactoryOrderMng_function_UpdateQAQC(item.QAQCID, item.FactoryOrderDetailID, item.OrderQnt
                                    , item.Remark, setStatusBy);
                        }                    
                    return true;
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
                return false;
            }

        }

        public bool Revise(int factoryOrderID, ref DTO.FactoryOrderMng.FactoryOrder dtoItem, int setStatusBy, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Factory Order has been revised success" };
            try
            {
                //SAVE DATA
                Library.DTO.Notification save_nofication = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
                if (!UpdateData(factoryOrderID, ref dtoItem, out save_nofication))
                {
                    notification = save_nofication;
                    return false;
                }

                //VALIDATE && CONFIRM
                using (FactoryOrderMngEntities context = CreateContext())
                {
                    FactoryOrderStatus dbFactoryOrderStatus = context.FactoryOrderStatus.Where(o => o.FactoryOrderID == factoryOrderID && o.IsCurrentStatus == true).FirstOrDefault();
                    if (dbFactoryOrderStatus.TrackingStatusID != DALBase.Helper.CONFIRMED && dbFactoryOrderStatus.TrackingStatusID != DALBase.Helper.REVISION_CONFIRMED)
                    {
                        notification = new Library.DTO.Notification() { Message = "Factory Order must be in CONFIRMED status before revise", Type = Library.DTO.NotificationType.Warning };
                        return false;
                    }

                    SetStatus(factoryOrderID, DALBase.Helper.REVISED, setStatusBy);
                    dtoItem.TrackingStatusNM = DALBase.Helper.TEXT_STATUS_REVISED;
                    dtoItem.TrackingStatusID = DALBase.Helper.REVISED;

                    var clientLP = context.ClientLP.ToList();
                    foreach (var item in clientLP)
                    {
                        if (item.ClientID == dtoItem.ClientID && item.LPManagingTeamID == 1)
                        {
                            string emailSubject = "";
                            string emailBody = "";
                            string foUrl = "";
                            string lpUrl = "";
                            var FoID = dtoItem.FactoryOrderID;
                            var lpRequest = context.LabelingPackaging.Where(o => o.FactoryOrderID == FoID).FirstOrDefault();

                            foUrl = this.frontendURL + "FactoryOrder/Edit/" + dtoItem.FactoryOrderID + "/" + dtoItem.SaleOrderID;
                            if (lpRequest.LPStatusID == 6)
                                lpUrl = this.frontendURL + "lpoverview/Edit/" + lpRequest.LabelingPackagingID;
                            else
                                lpUrl = this.frontendURL + "labelingpackaging/Edit/" + lpRequest.LabelingPackagingID;

                            emailSubject = "[Factory Order]" + " - " + dtoItem.FactoryOrderUD +" - ClientID: " + dtoItem.ClientID + " - Change to Revision, please check LP Request";
                            emailBody = "Factory Order - " + dtoItem.FactoryOrderUD + " - ClientID: " + dtoItem.ClientID + " - has been changed to Revision, please check your LP Request.";
                            emailBody = emailBody + Environment.NewLine;
                            emailBody = emailBody + "Link to Factory Order: " + foUrl;
                            emailBody = emailBody + Environment.NewLine;
                            emailBody = emailBody + "Link to LP Request: " + lpUrl;

                            if (emailBody != "")
                            {
                                SendNotificationRevise(emailSubject, emailBody);
                            }
                        }
                    }

                    // add item to quotation if needed
                    //context.FW_Quotation_function_AddFactoryOrderItem(factoryOrderID); // table lockx and also check if item is available on sql server side

                    return true;
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
                return false;
            }
        }

        public DTO.FactoryOrderMng.DataSearchContainer SearchDataContainer(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.FactoryOrderMng.DataSearchContainer dtoSearch = new DTO.FactoryOrderMng.DataSearchContainer();
            DAL.Support.DataFactory dal_support = new Support.DataFactory();
            dtoSearch.SaleOrderSearchs = GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification).ToList();
            dtoSearch.Seasons = dal_support.GetSeason().ToList();
            dtoSearch.Factories = dal_support.GetFactory().ToList();
            dtoSearch.Salers = dal_support.GetSaler().ToList();

            return dtoSearch;
        }

        private string GenerateFactoryOrderCode(int? SaleOrderID)
        {
            try
            {
                using (FactoryOrderMngEntities context = CreateContext())
                {
                    var saleOrder = context.FactoryOrderMng_SaleOrder_View.Where(o => o.SaleOrderID == SaleOrderID).FirstOrDefault();
                    var factoryOrder = context.FactoryOrder.Where(o => (o.IsDeleted == null || o.IsDeleted == false) && o.SaleOrderID == SaleOrderID).OrderByDescending(o => o.FactoryOrderUD);

                    if (factoryOrder.Count() == 0)
                        return saleOrder.ProformaInvoiceNo + "-1";

                    string factoryOrderUD = factoryOrder.FirstOrDefault().FactoryOrderUD;
                    if (string.IsNullOrEmpty(factoryOrderUD))
                    {
                        return saleOrder.ProformaInvoiceNo + "-1";
                    }
                    return saleOrder.ProformaInvoiceNo + "-" + (Convert.ToInt32(factoryOrderUD.Substring(factoryOrderUD.IndexOf('-') + 1)) + 1).ToString();
                }
            }
            catch
            {
                return "";
            }
        }

        public IEnumerable<int> MultiConfirm(List<int> factoryOrderIDs, int setStatusBy, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
                List<string> ErrorMessages = new List<string>();
                List<int> SuccessFactoryOrderIDs = new List<int>();
                List<string> SuccessFactoryOrderCodes = new List<string>();
                List<DTO.FactoryOrderMng.EmailNotificationConfirmed> dataEmailNotificationConfirmed = new List<DTO.FactoryOrderMng.EmailNotificationConfirmed>();

                if (factoryOrderIDs.Count() == 0)
                {
                    notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Warning, Message = "You need select factory orders to confirm" };
                    return SuccessFactoryOrderIDs;
                }
                //VALIDATE && CONFIRM
                FactoryOrder dbFactoryOrder;
                SaleOrderStatus dbSaleOrderStatus;
                FactoryOrderStatus dbFactoryOrderStatus;
                using (FactoryOrderMngEntities context = CreateContext())
                {
                    string bodyEmail = "";
                    for (int i = 0; i < factoryOrderIDs.Count(); i++)
                    {
                        int factoryOrderID = factoryOrderIDs[i];
                        bool isValid = true;
                        dbFactoryOrder = context.FactoryOrder.Where(o => o.FactoryOrderID == factoryOrderID).FirstOrDefault();
                        dbSaleOrderStatus = context.SaleOrderStatus.Where(o => o.SaleOrderID == dbFactoryOrder.SaleOrderID && o.IsCurrentStatus == true).FirstOrDefault();

                        if (dbSaleOrderStatus.TrackingStatusID != DALBase.Helper.CONFIRMED && dbSaleOrderStatus.TrackingStatusID != DALBase.Helper.REVISION_CONFIRMED)
                        {
                            ErrorMessages.Add("<li>" + dbFactoryOrder.FactoryOrderUD + " : PI must be in CONFIRMED status before confirm Factory Order</li>");
                            isValid = false;
                        }

                        dbFactoryOrderStatus = context.FactoryOrderStatus.Where(o => o.FactoryOrderID == factoryOrderID && o.IsCurrentStatus == true).FirstOrDefault();
                        if (dbFactoryOrderStatus.TrackingStatusID != DALBase.Helper.REVISED && dbFactoryOrderStatus.TrackingStatusID != DALBase.Helper.CREATED)
                        {
                            ErrorMessages.Add("<li>" + dbFactoryOrder.FactoryOrderUD + " : Factory Order must be in REVISED/CREATED status before confirm</li>");
                            isValid = false;
                        }
                        int TrackingStatusID = 0;
                        if (isValid)
                        {
                            TrackingStatusID = (dbFactoryOrderStatus.TrackingStatusID == DALBase.Helper.CREATED ? DALBase.Helper.CONFIRMED : DALBase.Helper.REVISION_CONFIRMED);
                            SetStatus(factoryOrderID, TrackingStatusID, setStatusBy);

                            SuccessFactoryOrderIDs.Add(factoryOrderID);
                            SuccessFactoryOrderCodes.Add(dbFactoryOrder.FactoryOrderUD);
                        }
                        string season = Library.Helper.GetCurrentSeason();
                        dataEmailNotificationConfirmed = converter.DB2DTO_EmailNotificationConfirmed(context.FactoryOrderMng_EmailNotificationCofirmed_View.Where(s => s.Season == season && s.FactoryOrderID == factoryOrderID && s.TrackingStatusID == TrackingStatusID).ToList());
                        if (i > 0 && dataEmailNotificationConfirmed.Count > 0)
                        {
                            bodyEmail = bodyEmail + Environment.NewLine;
                        }
                        for (int j = 0; j < dataEmailNotificationConfirmed.Count; j++)
                        {
                            var list = dataEmailNotificationConfirmed[j];
                            if (j == 0)
                            {
                                if (i == 0)
                                {
                                    bodyEmail = bodyEmail + "Proforma InvoiceNo: " + list.ProformaInvoiceNo + Environment.NewLine;
                                }
                                bodyEmail = bodyEmail + "   - Factory Order Code: " + list.FactoryOrderUD + Environment.NewLine;
                            }
                            if (j > 0)
                            {
                                bodyEmail = bodyEmail + Environment.NewLine;
                            }
                            bodyEmail = bodyEmail + "     + ArticleCode: " + list.ArticleCode;
                        }
                    }

                    string emailSubject = "List Factory Order: ArticleCode have product type = set but no piece defined";
                    if (bodyEmail != "")
                    {
                        SendNotification(emailSubject, bodyEmail);
                    }
                }

                string message = "<ul>";
                string successCode = "";

                for (int i = 0; i < SuccessFactoryOrderCodes.Count(); i++)
                {
                    successCode += SuccessFactoryOrderCodes[i] + ", ";
                }
                if (!string.IsNullOrEmpty(successCode))
                {
                    successCode = successCode.Substring(0, successCode.Length - 2);
                    message += "<li>" + successCode + " : Confirmed Successfull</li>";
                }

                for (int i = 0; i < ErrorMessages.Count(); i++)
                {
                    message += ErrorMessages[i];
                }
                message += "</ul>";


                if (ErrorMessages.Count() > 0)
                {
                    notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Warning, Message = message };
                }
                else
                {
                    notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = message };
                }
                return SuccessFactoryOrderIDs;
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
                return null;
            }
        }

        public IEnumerable<int> MultiRevise(List<int> factoryOrderIDs, int setStatusBy, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                List<string> ErrorMessages = new List<string>();
                List<int> SuccessFactoryOrderIDs = new List<int>();
                List<string> SuccessFactoryOrderCodes = new List<string>();

                if (factoryOrderIDs.Count() == 0)
                {
                    notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Warning, Message = "You need select factory orders to revise" };
                    return SuccessFactoryOrderIDs;
                }
                //VALIDATE && CONFIRM
                FactoryOrder dbFactoryOrder;
                FactoryOrderStatus dbFactoryOrderStatus;
                using (FactoryOrderMngEntities context = CreateContext())
                {
                    var clientLP = context.ClientLP.ToList();
                    for (int i = 0; i < factoryOrderIDs.Count(); i++)
                    {
                        int factoryOrderID = factoryOrderIDs[i];
                        bool isValid = true;
                        dbFactoryOrder = context.FactoryOrder.Where(o => o.FactoryOrderID == factoryOrderID).FirstOrDefault();
                        var factoryOrderView = context.FactoryOrderMng_FactoryOrder_View.Where(o => o.FactoryOrderID == factoryOrderID).FirstOrDefault();

                        dbFactoryOrderStatus = context.FactoryOrderStatus.Where(o => o.FactoryOrderID == factoryOrderID && o.IsCurrentStatus == true).FirstOrDefault();
                        if (dbFactoryOrderStatus.TrackingStatusID != DALBase.Helper.CONFIRMED && dbFactoryOrderStatus.TrackingStatusID != DALBase.Helper.REVISION_CONFIRMED)
                        {
                            ErrorMessages.Add("<li>" + dbFactoryOrder.FactoryOrderUD + " : Factory Order must be in CONFIRMED status before revise</li>");
                            isValid = false;
                        }

                        if (isValid)
                        {
                            SetStatus(factoryOrderID, DALBase.Helper.REVISED, setStatusBy);
                            SuccessFactoryOrderIDs.Add(factoryOrderID);
                            SuccessFactoryOrderCodes.Add(dbFactoryOrder.FactoryOrderUD);

                            foreach (var item in clientLP)
                            {
                                if (item.ClientID == factoryOrderView.ClientID && item.LPManagingTeamID == 1)
                                {
                                    //Send Mail
                                    string emailSubject = "";
                                    string emailBody = "";
                                    string foUrl = "";
                                    string lpUrl = "";
                                    var lpRequest = context.LabelingPackaging.Where(o => o.FactoryOrderID == factoryOrderID).FirstOrDefault();

                                    foUrl = this.frontendURL + "FactoryOrder/Edit/" + factoryOrderID + "/" + dbFactoryOrder.SaleOrderID;
                                    if (lpRequest.LPStatusID == 6)
                                        lpUrl = this.frontendURL + "lpoverview/Edit/" + lpRequest.LabelingPackagingID;
                                    else
                                        lpUrl = this.frontendURL + "labelingpackaging/Edit/" + lpRequest.LabelingPackagingID;

                                    emailSubject = "[Factory Order]" + " - " + dbFactoryOrder.FactoryOrderUD + " - ClientID: " + factoryOrderView.ClientID + " - Change to Revision, please check LP Request";
                                    emailBody = "Factory Order - " + dbFactoryOrder.FactoryOrderUD + " - ClientID: " + factoryOrderView.ClientID + " - has been changed to Revision, please check your LP Request.";
                                    emailBody = emailBody + Environment.NewLine;
                                    emailBody = emailBody + "Link to Factory Order: " + foUrl;
                                    emailBody = emailBody + Environment.NewLine;
                                    emailBody = emailBody + "Link to LP Request: " + lpUrl;

                                    if (emailBody != "")
                                    {
                                        SendNotificationRevise(emailSubject, emailBody);
                                    }
                                }
                            }
                        }
                    }
                }

                string message = "<ul>";
                string successCode = "";

                for (int i = 0; i < SuccessFactoryOrderCodes.Count(); i++)
                {
                    successCode += SuccessFactoryOrderCodes[i] + ", ";
                }
                if (!string.IsNullOrEmpty(successCode))
                {
                    successCode = successCode.Substring(0, successCode.Length - 2);
                    message += "<li>" + successCode + " : Revised Successfull</li>";
                }

                for (int i = 0; i < ErrorMessages.Count(); i++)
                {
                    message += ErrorMessages[i];
                }
                message += "</ul>";


                if (ErrorMessages.Count() > 0)
                {
                    notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Warning, Message = message };
                }
                else
                {
                    notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = message };
                }
                return SuccessFactoryOrderIDs;
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
                return null;
            }
        }

        public IEnumerable<int> MultiDelete(List<int> factoryOrderIDs, int deletedBy, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                List<string> ErrorMessages = new List<string>();
                List<int> SuccessFactoryOrderIDs = new List<int>();
                List<string> SuccessFactoryOrderCodes = new List<string>();

                if (factoryOrderIDs.Count() == 0)
                {
                    notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Warning, Message = "You need select factory orders to delete" };
                    return SuccessFactoryOrderIDs;
                }
                //VALIDATE && CONFIRM
                FactoryOrder dbFactoryOrder;
                FactoryOrderStatus dbFactoryOrderStatus;
                using (FactoryOrderMngEntities context = CreateContext())
                {
                    for (int i = 0; i < factoryOrderIDs.Count(); i++)
                    {
                        int factoryOrderID = factoryOrderIDs[i];
                        bool isValid = true;
                        dbFactoryOrder = context.FactoryOrder.Where(o => o.FactoryOrderID == factoryOrderID).FirstOrDefault();
                        dbFactoryOrderStatus = context.FactoryOrderStatus.Where(o => o.FactoryOrderID == factoryOrderID && o.IsCurrentStatus == true).FirstOrDefault();

                        if (dbFactoryOrderStatus.TrackingStatusID == DALBase.Helper.CONFIRMED || dbFactoryOrderStatus.TrackingStatusID == DALBase.Helper.REVISION_CONFIRMED)
                        {
                            ErrorMessages.Add("<li>" + dbFactoryOrder.FactoryOrderUD + " : Factory Order was confirmed. You can not delete</li>");
                            isValid = false;
                        }

                        if (dbFactoryOrderStatus.TrackingStatusID == DALBase.Helper.REVISED)
                        {
                            ErrorMessages.Add("<li>" + dbFactoryOrder.FactoryOrderUD + " : Factory Order was revised. You can not delete</li>");
                            isValid = false;
                        }

                        if (isValid)
                        {
                            context.FactoryOrderMng_function_DeleteFactoryOrder(factoryOrderID, deletedBy);
                            SuccessFactoryOrderIDs.Add(factoryOrderID);
                            SuccessFactoryOrderCodes.Add(dbFactoryOrder.FactoryOrderUD);
                        }
                    }
                }

                string message = "<ul>";
                string successCode = "";

                for (int i = 0; i < SuccessFactoryOrderCodes.Count(); i++)
                {
                    successCode += SuccessFactoryOrderCodes[i] + ", ";
                }
                if (!string.IsNullOrEmpty(successCode))
                {
                    successCode = successCode.Substring(0, successCode.Length - 2);
                    message += "<li>" + successCode + " : Deleted Successfull</li>";
                }

                for (int i = 0; i < ErrorMessages.Count(); i++)
                {
                    message += ErrorMessages[i];
                }
                message += "</ul>";


                if (ErrorMessages.Count() > 0)
                {
                    notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Warning, Message = message };
                }
                else
                {
                    notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = message };
                }
                return SuccessFactoryOrderIDs;
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
                return null;
            }
        }

        public IEnumerable<DTO.FactoryOrderMng.QCReport> GetQCReport(int factoryOrderDetailID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FactoryOrderMngEntities context = CreateContext())
                {
                    return converter.DB2DTO_QCReport(context.FactoryOrderMng_QCReport_View.Where(o => o.FactoryOrderDetailID == factoryOrderDetailID).ToList());
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
                return new List<DTO.FactoryOrderMng.QCReport>();
            }
        }

        public List<DTO.FactoryOrderMng.ProductColli> GetProductColli(int factoryOrderID, out Library.DTO.Notification notification)
        {
            List<DTO.FactoryOrderMng.ProductColli> data = new List<DTO.FactoryOrderMng.ProductColli>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FactoryOrderMngEntities context = CreateContext())
                {
                    var result = context.FactoryOrderMng_ProductColli_View.Where(o => o.FactoryOrderID == factoryOrderID);
                    data = converter.DB2DTO_ProductColli(result.ToList());
                    int i = 1;
                    foreach (var item in data)
                    {
                        item.RowIndex = i;
                        //item.IsSelected = item.ProductColliID.HasValue;
                        if (!item.ProductSetEANCodeID.HasValue)
                        {
                            item.ProductSetEANCodeID = -i;
                            item.ProductEANCode = "NONE";
                        }
                        if (!item.ProductColliID.HasValue)
                        {
                            item.ProductColliID = -i;
                            item.ColliEANCode = "NONE";
                        }
                        i++;
                    }
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

        public List<DTO.FactoryOrderMng.ProductColli> CreateFactoryOrderProductColli(List<DTO.FactoryOrderMng.ProductColli> dtoProductColli, out Library.DTO.Notification notification)
        {
            List<DTO.FactoryOrderMng.ProductColli> data = new List<DTO.FactoryOrderMng.ProductColli>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                if (dtoProductColli.Where(o => o.IsSelected.HasValue && o.IsSelected.Value).Count() == 0)
                {
                    throw new Exception("There are no colli ean code are selected.");
                }
                using (FactoryOrderMngEntities context = CreateContext())
                {
                    var factoryOrderDetail = from item in dtoProductColli.Where(o => o.IsSelected.HasValue && o.IsSelected.Value && o.ProductSetEANCodeID.HasValue && o.ProductSetEANCodeID.Value > 0 && o.ProductColliID.HasValue && o.ProductColliID.Value > 0)
                                             group item by new { item.FactoryOrderDetailID } into g
                                             select new { g.Key.FactoryOrderDetailID };

                    FactoryOrderDetail dbFactoryOrderDetail;
                    FactoryOrderColliDetail dbColliDetail;
                    foreach (var item in factoryOrderDetail)
                    {
                        dbFactoryOrderDetail = context.FactoryOrderDetail.Where(o => o.FactoryOrderDetailID == item.FactoryOrderDetailID).FirstOrDefault();
                        foreach (var cItem in dtoProductColli.Where(o => o.FactoryOrderDetailID == item.FactoryOrderDetailID && o.IsSelected.HasValue && o.IsSelected.Value
                            && o.ProductSetEANCodeID.HasValue && o.ProductSetEANCodeID.Value > 0
                            && o.ProductColliID.HasValue && o.ProductColliID.Value > 0))
                        {
                            dbColliDetail = new FactoryOrderColliDetail();
                            dbFactoryOrderDetail.FactoryOrderColliDetail.Add(dbColliDetail);
                            dbColliDetail.ProductColliID = cItem.ProductColliID;
                        }
                    }
                    context.SaveChanges();
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

        public DTO.FactoryOrderMng.DataContainerOverView GetViewData(int id, int saleOrderID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            //try to get data
            try
            {
                using (FactoryOrderMngEntities context = CreateContext())
                {
                    DTO.FactoryOrderMng.DataContainerOverView dtoItem = new DTO.FactoryOrderMng.DataContainerOverView();

                    if (id > 0)
                    {
                        FactoryOrderMng_FactoryOrder_OverView dbItem;
                        dbItem = context.FactoryOrderMng_FactoryOrder_OverView
                                .Include("FactoryOrderMng_FactoryOrderDetail_OverView.FactoryOrderMng_FactoryOrderColliDetail_OverView")
                                .Include("FactoryOrderMng_FactoryOrderSparepartDetail_OverView")
                                .FirstOrDefault(o => o.FactoryOrderID == id);
                        DTO.FactoryOrderMng.FactoryOrderOverView FactoryOrderDTOItem = converter.DB2DTO_FactoryOrderOverView(dbItem);
                        dtoItem.FactoryOrderData = FactoryOrderDTOItem;
                    }
                    return dtoItem;
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
                return new DTO.FactoryOrderMng.DataContainerOverView();
            }
        }

        public List<DTO.FactoryOrderMng.GeneralBreakDownDTO> GetGeneralBreakDown(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            List<DTO.FactoryOrderMng.GeneralBreakDownDTO> data = new List<DTO.FactoryOrderMng.GeneralBreakDownDTO>();
            try
            {
                using (FactoryOrderMngEntities context = CreateContext())
                {
                    data = converter.DB2DTO_GeneralBreakDown(context.FactoryOrderMng_GeneralBreakDown_OverView.Where(o => o.ModelID == id).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        private void SendNotification(string emailSubject, string emailBody)
        {
            try
            {
                List<string> emailAddress = new List<string>();

                using (FactoryOrderMngEntities context = CreateContext())
                {
                    // Custome email group
                    var dbNotificationEmails = context.SupportMng_NotificationGroup_View.Where(o => o.NotificationGroupUD == "MissImageP" /*&& dbEmployeeIDs.ToList().Contains(o.UserID)*/);
                    foreach (var dbNotificationEmail in dbNotificationEmails.ToArray())
                    {
                        if (!string.IsNullOrEmpty(dbNotificationEmail.Email1) && !emailAddress.Contains(dbNotificationEmail.Email1))
                        {
                            emailAddress.Add(dbNotificationEmail.Email1);
                        }
                        
                        // add to NotificationMessage table
                        NotificationMessage notification = new NotificationMessage();
                        notification.UserID = dbNotificationEmail.UserID;
                        notification.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_PURCHASING;
                        notification.NotificationMessageTitle = emailSubject;
                        notification.NotificationMessageContent = emailBody;
                        context.NotificationMessage.Add(notification);
                    }

                    string sendToEmail = string.Empty;
                    foreach (string eAddress in emailAddress)
                    {
                        if (string.IsNullOrEmpty(sendToEmail))
                        {
                            sendToEmail += eAddress;
                        }
                        else
                        {
                            sendToEmail += ";" + eAddress;
                        }
                    }

                    EmailNotificationMessage dbEmail = new EmailNotificationMessage();
                    dbEmail.EmailBody = emailBody;
                    dbEmail.EmailSubject = emailSubject;
                    dbEmail.SendTo = sendToEmail;
                    context.EmailNotificationMessage.Add(dbEmail);
                    context.SaveChanges();
                }

            }
            catch { }
        }

        private void SendNotificationRevise(string emailSubject, string emailBody)
        {
            try
            {
                List<string> emailAddress = new List<string>();

                using (FactoryOrderMngEntities context = CreateContext())
                {
                    // Custome email group
                    var dbNotificationEmails = context.SupportMng_NotificationGroup_View.Where(o => o.NotificationGroupUD == "LPStatus");
                    foreach (var dbNotificationEmail in dbNotificationEmails.ToArray())
                    {
                        if (!string.IsNullOrEmpty(dbNotificationEmail.Email1) && !emailAddress.Contains(dbNotificationEmail.Email1))
                        {
                            emailAddress.Add(dbNotificationEmail.Email1);
                        }

                        // add to NotificationMessage table
                        NotificationMessage notification = new NotificationMessage();
                        notification.UserID = dbNotificationEmail.UserID;
                        notification.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_PURCHASING;
                        notification.NotificationMessageTitle = emailSubject;
                        notification.NotificationMessageContent = emailBody;
                        context.NotificationMessage.Add(notification);
                    }

                    string sendToEmail = string.Empty;
                    foreach (string eAddress in emailAddress)
                    {
                        if (string.IsNullOrEmpty(sendToEmail))
                        {
                            sendToEmail += eAddress;
                        }
                        else
                        {
                            sendToEmail += ";" + eAddress;
                        }
                    }

                    EmailNotificationMessage dbEmail = new EmailNotificationMessage();
                    dbEmail.EmailBody = emailBody;
                    dbEmail.EmailSubject = emailSubject;
                    dbEmail.SendTo = sendToEmail;
                    context.EmailNotificationMessage.Add(dbEmail);
                    context.SaveChanges();
                }

            }
            catch { }
        }

        public DTO.FactoryOrderMng.ComplaintData_View GetClientComplaintData(int ClientComplaintID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            DTO.FactoryOrderMng.ComplaintData_View data = new DTO.FactoryOrderMng.ComplaintData_View();
            try
            {
                using (FactoryOrderMngEntities context = CreateContext())
                {
                    data.Data = converter.DB2DTO_ClientComplaintView(context.ClientComplaint_ClientComplaint_View.Where(o => o.ClientComplaintID == ClientComplaintID).FirstOrDefault());
                    data.ComplaintStatuses = converter.DB2DTO_ConstantEntry(context.SupportMng_ConstantEntry_View.Where(o => o.EntryGroup == "complaint-status").ToList());
                    data.ComplaintTypes = converter.DB2DTO_ConstantEntry(context.SupportMng_ConstantEntry_View.Where(o => o.EntryGroup == "complaint-type").ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return data;
        }

        public string ExportExcelClientComplaintItem(int clientComplaintItemID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            var ds = new ReportDataObject();

            try
            {
                var da = new SqlDataAdapter()
                {
                    SelectCommand = new SqlCommand("ClientComplaint_function_ExportExcelDetailItem", new SqlConnection(Helper.GetSQLConnectionString()))
                };

                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@ClientComplaintItemID", clientComplaintItemID);
                da.TableMappings.Add("Table", "Data1");
                da.TableMappings.Add("Table1", "SRFData");
                da.TableMappings.Add("Table2", "SRFDataImages");


                da.Fill(ds);

                foreach (DataRow dr in ds.Tables["Data1"].Rows)
                {
                    if (dr["ModelThumbnail"].ToString() != "")
                    {
                        dr["ModelThumbnail"] = FrameworkSetting.Setting.MediaThumbnailUrl + dr["ModelThumbnail"].ToString();
                    }

                    if (dr["MaterialThumbnail"].ToString() != "")
                    {
                        dr["MaterialThumbnail"] = FrameworkSetting.Setting.MediaThumbnailUrl + dr["MaterialThumbnail"].ToString();
                    }

                    if (dr["SubMaterialThumbnail"].ToString() != "")
                    {
                        dr["SubMaterialThumbnail"] = FrameworkSetting.Setting.MediaThumbnailUrl + dr["SubMaterialThumbnail"].ToString();
                    }

                    if (dr["SignatureThumbnail"].ToString() != "")
                    {
                        dr["SignatureThumbnail"] = FrameworkSetting.Setting.MediaThumbnailUrl + dr["SignatureThumbnail"].ToString();
                    }
                }


                foreach (DataRow dr in ds.Tables["SRFDataImages"].Rows)
                {
                    if (dr["FileLocation"].ToString() != "")
                    {
                        dr["FileLocation"] = FrameworkSetting.Setting.MediaThumbnailUrl + dr["FileLocation"].ToString();
                    }
                }

                return Helper.CreateReportFileWithEPPlus2(ds, "ClientComplaintItemDetail");
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;

                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                    notification.DetailMessage.Add(ex.InnerException.Message);

                return string.Empty;
            }
        }

        public List<DTO.FactoryOrderMng.FactoryOrderDetail> GetSaleOrderDetail(int saleOrderId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            List<DTO.FactoryOrderMng.FactoryOrderDetail> data = new List<DTO.FactoryOrderMng.FactoryOrderDetail>();
            try
            {
                using (FactoryOrderMngEntities context = CreateContext())
                {
                    data = converter.DB2DTO_FactoryOrderDetail(context.FactoryOrderMng_SaleOrderDetail_View.Where(o => o.SaleOrderID == saleOrderId && (o.OfferSeasonTypeID == 2 || o.OfferSeasonTypeID == 3)).ToList());
                    foreach (var item in data)
                    {
                        item.SaleOrderQnt = item.OrderQnt;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }
            return data;
        }

        public List<DTO.FactoryOrderMng.FactoryOrderSparepartDetail> GetSaleOrderSparepartDetail(int saleOrderId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            List<DTO.FactoryOrderMng.FactoryOrderSparepartDetail> data = new List<DTO.FactoryOrderMng.FactoryOrderSparepartDetail>();
            try
            {
                using (FactoryOrderMngEntities context = CreateContext())
                {
                    data = converter.DB2DTO_FactoryOrderSparepartDetail(context.FactoryOrderMng_SaleOrderDetailSparepart_View.Where(o => o.SaleOrderID == saleOrderId).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }
            return data;
        }
        
        public List<DTO.FactoryOrderMng.FactoryOrderSampleDetail> GetSaleOrderSampleDetail(int saleOrderId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            List<DTO.FactoryOrderMng.FactoryOrderSampleDetail> data = new List<DTO.FactoryOrderMng.FactoryOrderSampleDetail>();
            DTO.FactoryOrderMng.FactoryOrderSampleDetail temp = new DTO.FactoryOrderMng.FactoryOrderSampleDetail();
            
            try
            {
                using (FactoryOrderMngEntities context = CreateContext())
                {
                    data = converter.DB2DTO_FactoryOrderSampleDetail(context.FactoryOrderMng_SaleOrderDetailSample_View.Where(o => o.SaleOrderID == saleOrderId).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }
            return data;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using System.IO;

namespace DAL.PurchasingInvoiceMng2
{
    public class DataFactory : DALBase.FactoryBase<DTO.PurchasingInvoiceMng.PurchasingInvoiceSearch, DTO.PurchasingInvoiceMng.PurchasingInvoice>
    {
        private DataConverter converter = new DataConverter();
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

        private PurchasingInvoiceMngEntities CreateContext()
        {
            return new PurchasingInvoiceMngEntities(DALBase.Helper.CreateEntityConnectionString("PurchasingInvoiceMngModel"));
        }

        public DataFactory() { }

        public override IEnumerable<DTO.PurchasingInvoiceMng.PurchasingInvoiceSearch> GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            string Season = string.Empty;
            string InvoiceNo = string.Empty;
            string ProformaInvoiceNo = string.Empty;
            string ClientUD = string.Empty;
            string ContainerNo = string.Empty;
            string BLNo = string.Empty;
            string PurchasingCreditNoteNo = string.Empty;
            int? CompanyID = null;
            int? SupplierID = null;
            string FactoryInvoiceNo = string.Empty;
            int? ConfirmAll = null;
            string UpdateName = string.Empty;

            int? iRequesterID = (int)filters["iRequesterID"];
            if (filters.ContainsKey("Season") && filters["Season"] != null) Season = filters["Season"].ToString();
            if (filters.ContainsKey("InvoiceNo")) InvoiceNo = filters["InvoiceNo"].ToString();
            if (filters.ContainsKey("ProformaInvoiceNo")) ProformaInvoiceNo = filters["ProformaInvoiceNo"].ToString();
            if (filters.ContainsKey("ClientUD")) ClientUD = filters["ClientUD"].ToString();
            if (filters.ContainsKey("ContainerNo")) ContainerNo = filters["ContainerNo"].ToString();
            if (filters.ContainsKey("InvoiceNo")) InvoiceNo = filters["InvoiceNo"].ToString();
            if (filters.ContainsKey("BLNo")) BLNo = filters["BLNo"].ToString();
            if (filters.ContainsKey("PurchasingCreditNoteNo")) PurchasingCreditNoteNo = filters["PurchasingCreditNoteNo"].ToString();
            if (filters.ContainsKey("CompanyID") && filters["CompanyID"] != null && filters["CompanyID"].ToString() != string.Empty) CompanyID = Convert.ToInt32(filters["CompanyID"]);
            if (filters.ContainsKey("SupplierID") && filters["SupplierID"] != null && filters["SupplierID"].ToString() != string.Empty) SupplierID = Convert.ToInt32(filters["SupplierID"]);
            if (filters.ContainsKey("FactoryInvoiceNo")) FactoryInvoiceNo = filters["FactoryInvoiceNo"].ToString();
            if (filters.ContainsKey("ConfirmAll") && filters["ConfirmAll"] != null && filters["ConfirmAll"].ToString() != string.Empty) ConfirmAll = Convert.ToInt32(filters["ConfirmAll"]);
            if (filters.ContainsKey("UpdateName")) FactoryInvoiceNo = filters["UpdateName"].ToString();

            //try to get data
            try
            {
                using (PurchasingInvoiceMngEntities context = CreateContext())
                {
                    List<DTO.PurchasingInvoiceMng.PurchasingInvoiceSearch> searchResult = new List<DTO.PurchasingInvoiceMng.PurchasingInvoiceSearch>();
                    totalRows = context.PurchasingInvoiceMng_function_SearchPurchasingInvoice(iRequesterID, orderBy, orderDirection, Season, InvoiceNo, ProformaInvoiceNo, ClientUD, ContainerNo, BLNo, PurchasingCreditNoteNo, SupplierID, FactoryInvoiceNo, CompanyID, ConfirmAll, UpdateName).Count();
                    var result = context.PurchasingInvoiceMng_function_SearchPurchasingInvoice(iRequesterID, orderBy, orderDirection, Season, InvoiceNo, ProformaInvoiceNo, ClientUD, ContainerNo, BLNo, PurchasingCreditNoteNo, SupplierID, FactoryInvoiceNo, CompanyID, ConfirmAll, UpdateName);
                    searchResult = converter.DB2DTO_PurchasingInvoiceSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());

                    //List<DTO.PurchasingInvoiceMng.PurchasingCreditNote> creditNotes = converter.DB2DTO_PurchasingCreditNote(context.PurchasingInvoiceMng_PurchasingCreditNote_View.ToList());
                    //foreach (var item in searchResult)
                    //{
                    //    item.PurchasingCreditNotes = new List<DTO.PurchasingInvoiceMng.PurchasingCreditNote>();
                    //    foreach (var creditNote in creditNotes.Where(o => o.PurchasingInvoiceID == item.PurchasingInvoiceID))
                    //    {
                    //        item.PurchasingCreditNotes.Add(converter.DTO2DTO_PurchasingCreditNote(creditNote));
                    //    }
                    //}
                    return searchResult;
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
                return new List<DTO.PurchasingInvoiceMng.PurchasingInvoiceSearch>();
            }
        }

        public override DTO.PurchasingInvoiceMng.PurchasingInvoice GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try to get data
            try
            {
                using (PurchasingInvoiceMngEntities context = CreateContext())
                {
                    DTO.PurchasingInvoiceMng.PurchasingInvoice dtoItem = new DTO.PurchasingInvoiceMng.PurchasingInvoice();

                    PurchasingInvoiceMng_PurchasingInvoice_View dbItem;
                    dbItem = context.PurchasingInvoiceMng_PurchasingInvoice_View
                        .Include("PurchasingInvoiceMng_PurchasingInvoiceDetail_View")
                        .Include("PurchasingInvoiceMng_PurchasingInvoiceSparepartDetail_View")
                        .Include("PurchasingInvoiceMng_PurchasingInvoiceSampleDetail_View")
                        .FirstOrDefault(o => o.PurchasingInvoiceID == id);

                    dtoItem = converter.DB2DTO_PurchasingInvoice(dbItem);
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
                return new DTO.PurchasingInvoiceMng.PurchasingInvoice();
            }
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            throw new Exception("do not install function");
        }

        public bool DeleteData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                //check permission on invoice
                if (fwFactory.CheckPurchasingInvoicePermission(iRequesterID, id) == 0)
                {
                    throw new Exception("You do not have access permission on this invoice");
                }
                using (PurchasingInvoiceMngEntities context = CreateContext())
                {
                    PurchasingInvoice dbItem = context.PurchasingInvoice.FirstOrDefault(o => o.PurchasingInvoiceID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "invoice not found!";
                        return false;
                    }
                    else
                    {
                        context.PurchasingInvoice.Remove(dbItem);
                        context.SaveChanges();
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

        public override bool UpdateData(int id, ref DTO.PurchasingInvoiceMng.PurchasingInvoice dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public DTO.PurchasingInvoiceMng.SearchSupportList GetSearchSupportData(int iRequesterID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                DAL.Support.DataFactory factory = new Support.DataFactory();
                DTO.PurchasingInvoiceMng.SearchSupportList dtoSupport = new DTO.PurchasingInvoiceMng.SearchSupportList();

                dtoSupport.Seasons = factory.GetSeason().ToList();
               
                //get list supplier by userid
                List<int?> supplierIDs = fwFactory.GetListSupplierByUser(iRequesterID);
                dtoSupport.Suppliers = factory.GetSupplier().ToList().Where(o => supplierIDs.Contains(o.SupplierID)).ToList();
                return dtoSupport;
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
                return new DTO.PurchasingInvoiceMng.SearchSupportList();
            }
        }

        public DTO.PurchasingInvoiceMng.EditSupportList GetEditSupportData()
        {
            DAL.Support.DataFactory factory = new Support.DataFactory();
            Module.Support.DAL.DataFactory module_support_factory = new Module.Support.DAL.DataFactory();
            DTO.PurchasingInvoiceMng.EditSupportList dtoSupport = new DTO.PurchasingInvoiceMng.EditSupportList();
            dtoSupport.Seasons = factory.GetSeason().ToList();
            dtoSupport.PriceDifferenceInvoiceSettings = GetPriceDifferenceInvoiceSetting();
            dtoSupport.FactoryCostTypes = module_support_factory.GetFactoryCostType();
            using (var context = CreateContext())
            {
                dtoSupport.companyDTOs = converter.DB2DTO_Company(context.PurchasingInvoiceMng_Company_View.ToList());
            }
            return dtoSupport;
        }

        public IEnumerable<DTO.PurchasingInvoiceMng.Booking> GetBookings(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            string BLNo = string.Empty;

            if (filters.ContainsKey("BLNo")) BLNo = filters["BLNo"].ToString();
            //try to get data
            try
            {
                using (PurchasingInvoiceMngEntities context = CreateContext())
                {
                    totalRows = context.PurchasingInvoiceMng_function_SearchBooking(iRequesterID, orderBy, orderDirection, BLNo).Count();
                    var result = context.PurchasingInvoiceMng_function_SearchBooking(iRequesterID, orderBy, orderDirection, BLNo);
                    return converter.DB2DTO_Booking(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
                return new List<DTO.PurchasingInvoiceMng.Booking>();
            }
        }

        public IEnumerable<DTO.PurchasingInvoiceMng.LoadingPlanDetail> GetLoadingPlanDetails(int iRequesterID, int bookingID, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            try
            {
                //check permission on booking
                if (fwFactory.CheckBookingPermission(iRequesterID, bookingID) == 0)
                {
                    throw new Exception("You do not have access permission on this booking");
                }
                using (PurchasingInvoiceMngEntities context = CreateContext())
                {
                    var result = context.PurchasingInvoiceMng_LoadingPlanDetail_View.Where(o => o.BookingID == bookingID).Count();
                    return converter.DB2DTO_LoadingPlanDetail(context.PurchasingInvoiceMng_LoadingPlanDetail_View.Where(o => o.BookingID == bookingID).ToList());
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
                return new List<DTO.PurchasingInvoiceMng.LoadingPlanDetail>();
            }
        }

        public IEnumerable<DTO.PurchasingInvoiceMng.LoadingPlanSparepartDetail> GetLoadingPlanSparepartDetails(int iRequesterID, int bookingID, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            try
            {
                //check permission on booking
                if (fwFactory.CheckBookingPermission(iRequesterID, bookingID) == 0)
                {
                    throw new Exception("You do not have access permission on this booking");
                }
                using (PurchasingInvoiceMngEntities context = CreateContext())
                {
                    var result = context.PurchasingInvoiceMng_LoadingPlanSparepartDetail_View.Where(o => o.BookingID == bookingID).Count();
                    return converter.DB2DTO_LoadingPlanSparepartDetail(context.PurchasingInvoiceMng_LoadingPlanSparepartDetail_View.Where(o => o.BookingID == bookingID).ToList());
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
                return new List<DTO.PurchasingInvoiceMng.LoadingPlanSparepartDetail>();
            }
        }

        public DTO.PurchasingInvoiceMng.PurchasingInvoice GetEditData(int iRequesterID, int id, int invoiceType, int bookingID, int supplierID, int parentID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int[] internalCompanyID = new int[] { 1, 2, 3 };
            int officeID = 1;
            if (!internalCompanyID.ToList().Contains(fwFactory.GetCompanyID(iRequesterID).Value)) // 1, 2, 3 = AVF, AVS, AVT
            {
                officeID = 4; // fake the office id equal to 4, backward compatible
            }
            //try to get data
            try
            {
                using (PurchasingInvoiceMngEntities context = CreateContext())
                {
                    DTO.PurchasingInvoiceMng.PurchasingInvoice dtoItem = new DTO.PurchasingInvoiceMng.PurchasingInvoice();

                    // cuong.tran - 03-Jan-2019
                    // Checking Booking is exist in Purchasing Invoice.
                    var purchasingInvoice = context.PurchasingInvoiceMng_PurchasingInvoice_View.FirstOrDefault(o => o.BookingID == bookingID);
                    if (purchasingInvoice != null)
                    {
                        id = purchasingInvoice.PurchasingInvoiceID;
                    }
                    // cuong.tran - 03-Jan-2019

                    if (id > 0)
                    {
                        //check permission on invoice
                        if (fwFactory.CheckPurchasingInvoicePermission(iRequesterID, id) == 0)
                        {
                            throw new Exception("You do not have access permission on this invoice");
                        }
                        //get data
                        PurchasingInvoiceMng_PurchasingInvoice_View dbItem;
                        dbItem = context.PurchasingInvoiceMng_PurchasingInvoice_View
                            .Include("PurchasingInvoiceMng_PurchasingInvoiceDetail_View")
                            .Include("PurchasingInvoiceMng_PurchasingInvoiceSparepartDetail_View")
                            .Include("PurchasingInvoiceMng_PurchasingInvoiceExtraDetail_View")
                            .Include("PurchasingInvoiceMng_PurchasingInvoiceSampleDetail_View")
                            .FirstOrDefault(o => o.PurchasingInvoiceID == id);

                        dtoItem = converter.DB2DTO_PurchasingInvoice(dbItem);

                        //check price permission of user base on officeID
                        if (officeID == 4)
                        {
                            foreach (var item in dtoItem.PurchasingInvoiceDetails)
                            {
                                item.UnitPrice = null;
                                item.Amount = null;
                                item.QuotationSalePrice = null;
                            }
                            foreach (var item in dtoItem.PurchasingInvoiceSparepartDetails)
                            {
                                item.UnitPrice = null;
                                item.Amount = null;
                                item.QuotationSalePrice = null;
                            }
                        }
                    }
                    else
                    {
                        string currentSeason = DALBase.Helper.GetCurrentSeason();
                        if (invoiceType == 1) // 1: Fob Invoice, 2: Other invoice On Supplier, 3 : Other invoice on old invoice (ParentID)
                        {
                            //check permission on booking
                            if (fwFactory.CheckBookingPermission(iRequesterID, bookingID) == 0)
                            {
                                throw new Exception("You do not have access permission on this booking");
                            }
                            //get booking data
                            PurchasingInvoiceMng_Booking_View dbBooking = context.PurchasingInvoiceMng_Booking_View
                                                                                .Include("PurchasingInvoiceMng_LoadingPlanDetail_View")
                                                                                .Include("PurchasingInvoiceMng_LoadingPlanSparepartDetail_View")
                                                                                .Include("PurchasingInvoiceMng_LoadingPlanSampleDetail_View")
                                                                                .FirstOrDefault(o => o.BookingID == bookingID);
                            //Purchasing Info
                            dtoItem.InvoiceType = invoiceType;
                            dtoItem.SupplierID = dbBooking.SupplierID;
                            dtoItem.BookingID = dbBooking.BookingID;
                            dtoItem.CompanyID = dbBooking.IsBuyingOrangePie.HasValue && dbBooking.IsBuyingOrangePie.Value ? 13 : 4;
                            dtoItem.CompanyNM = dbBooking.IsBuyingOrangePie.HasValue && dbBooking.IsBuyingOrangePie.Value ? "Orangepine" : "Eurofar";
                            dtoItem.Season = currentSeason;
                            dtoItem.InvoiceDate = DateTime.Now.ToString("dd/MM/yyyy");

                            dtoItem.BLNo = dbBooking.BLNo;
                            dtoItem.ShipedDate = dbBooking.ShipedDate.Value.ToString("dd/MM/yyyy");
                            dtoItem.SupplierNM = dbBooking.SupplierNM;
                            dtoItem.ForwarderNM = dbBooking.ForwarderNM;
                            dtoItem.Feeder = dbBooking.Feeder;
                            dtoItem.POLName = dbBooking.POLName;
                            dtoItem.POLName = dbBooking.PODName;

                            //Purchasing Detail Info
                            dtoItem.PurchasingInvoiceDetails = new List<DTO.PurchasingInvoiceMng.PurchasingInvoiceDetail>();
                            dtoItem.PurchasingInvoiceSparepartDetails = new List<DTO.PurchasingInvoiceMng.PurchasingInvoiceSparepartDetail>();
                            dtoItem.PurchasingInvoiceSampleDetails = new List<DTO.PurchasingInvoiceMng.PurchasingInvoiceSampleDetail>();
                            DTO.PurchasingInvoiceMng.PurchasingInvoiceDetail dtoProduct;
                            DTO.PurchasingInvoiceMng.PurchasingInvoiceSparepartDetail dtoSparepart;
                            DTO.PurchasingInvoiceMng.PurchasingInvoiceSampleDetail dtoSample;
                            int i = -1;
                            var dbPriceDifference = context.PurchasingInvoiceMng_PriceDifferenceInvoiceSetting_View.Where(o => o.SupplierID == dbBooking.SupplierID && o.Season == currentSeason).FirstOrDefault();
                            foreach (var item in dbBooking.PurchasingInvoiceMng_LoadingPlanDetail_View)
                            {
                                dtoProduct = new DTO.PurchasingInvoiceMng.PurchasingInvoiceDetail();
                                dtoProduct.PurchasingInvoiceDetailID = i;

                                dtoProduct.LoadingPlanDetailID = item.LoadingPlanDetailID;
                                dtoProduct.ClientUD = item.ClientUD;
                                dtoProduct.ProformaInvoiceNo = item.ProformaInvoiceNo;
                                dtoProduct.ContainerNo = item.ContainerNo;
                                dtoProduct.SealNo = item.SealNo;
                                dtoProduct.ContainerTypeNM = item.ContainerTypeNM;
                                dtoProduct.ArticleCode = item.ArticleCode;
                                dtoProduct.Description = item.Description;
                                dtoProduct.PriceStatus = item.PriceStatus;
                                dtoProduct.LoadingPlanUD = item.LoadingPlanUD;
                                dtoProduct.SupplierID = item.SupplierID;
                                dtoProduct.FactoryProformaInvoiceNo = item.FactoryProformaInvoiceNo;

                                dtoProduct.Quantity = item.Quantity;
                                dtoProduct.FactoryPrice = item.QuotationPurchasingPrice;
                                dtoProduct.FactoryAmount = dtoProduct.Quantity * dtoProduct.FactoryPrice;
                                dtoProduct.QuotationPurchasingPrice = item.QuotationPurchasingPrice;
                                dtoProduct.FactoryProformaPrice = item.FactoryProformaPrice;

                                //pemisson for price
                                if (officeID != 4) // 4: factory office
                                {
                                    dtoProduct.UnitPrice = (dbPriceDifference == null ? item.QuotationPurchasingPrice : item.QuotationPurchasingPrice * (1 + dbPriceDifference.Rate));
                                    if (dtoProduct.UnitPrice.HasValue) dtoProduct.UnitPrice = Math.Round(dtoProduct.UnitPrice.Value, 2);
                                    dtoProduct.Amount = dtoProduct.Quantity * dtoProduct.UnitPrice;
                                    dtoProduct.QuotationSalePrice = item.QuotationSalePrice;
                                }

                                dtoItem.PurchasingInvoiceDetails.Add(dtoProduct);
                                i--;
                            }

                            i = -1;
                            foreach (var item in dbBooking.PurchasingInvoiceMng_LoadingPlanSparepartDetail_View)
                            {
                                dtoSparepart = new DTO.PurchasingInvoiceMng.PurchasingInvoiceSparepartDetail();
                                dtoSparepart.PurchasingInvoiceSparepartDetailID = i;

                                dtoSparepart.LoadingPlanSparepartDetailID = item.LoadingPlanSparepartDetailID;
                                dtoSparepart.ClientUD = item.ClientUD;
                                dtoSparepart.ProformaInvoiceNo = item.ProformaInvoiceNo;
                                dtoSparepart.ContainerNo = item.ContainerNo;
                                dtoSparepart.SealNo = item.SealNo;
                                dtoSparepart.ContainerTypeNM = item.ContainerTypeNM;
                                dtoSparepart.ArticleCode = item.ArticleCode;
                                dtoSparepart.Description = item.Description;
                                dtoSparepart.PriceStatus = item.PriceStatus;
                                dtoSparepart.LoadingPlanUD = item.LoadingPlanUD;
                                dtoSparepart.SupplierID = item.SupplierID;
                                dtoSparepart.FactoryProformaInvoiceNo = item.FactoryProformaInvoiceNo;

                                dtoSparepart.Quantity = item.Quantity;
                                dtoSparepart.FactoryPrice = item.QuotationPurchasingPrice;
                                dtoSparepart.FactoryAmount = dtoSparepart.Quantity * dtoSparepart.FactoryPrice;
                                dtoSparepart.QuotationPurchasingPrice = item.QuotationPurchasingPrice;
                                dtoSparepart.FactoryProformaPrice = item.FactoryProformaPrice;

                                //pemisson for price
                                if (officeID != 4) // 4: factory office
                                {
                                    dtoSparepart.UnitPrice = (dbPriceDifference == null ? item.QuotationPurchasingPrice : item.QuotationPurchasingPrice * (1 + dbPriceDifference.Rate));
                                    if (dtoSparepart.UnitPrice.HasValue) dtoSparepart.UnitPrice = Math.Round(dtoSparepart.UnitPrice.Value, 2);
                                    dtoSparepart.Amount = dtoSparepart.Quantity * dtoSparepart.UnitPrice;
                                    dtoSparepart.QuotationSalePrice = item.QuotationSalePrice;
                                }

                                dtoItem.PurchasingInvoiceSparepartDetails.Add(dtoSparepart);
                                i--;
                            }

                            //Sample product
                            i = -1;
                            foreach (var item in dbBooking.PurchasingInvoiceMng_LoadingPlanSampleDetail_View)
                            {
                                dtoSample = new DTO.PurchasingInvoiceMng.PurchasingInvoiceSampleDetail();
                                dtoSample.PurchasingInvoiceSampleDetailID = i;

                                dtoSample.LoadingPlanSampleDetailID = item.LoadingPlanSampleDetailID;
                                dtoSample.ClientUD = item.ClientUD;
                                dtoSample.ProformaInvoiceNo = item.ProformaInvoiceNo;
                                dtoSample.ContainerNo = item.ContainerNo;
                                dtoSample.SealNo = item.SealNo;
                                dtoSample.ContainerTypeNM = item.ContainerTypeNM;
                                dtoSample.ArticleCode = item.ArticleCode;
                                dtoSample.Description = item.Description;
                                dtoSample.PriceStatus = item.PriceStatus;
                                dtoSample.LoadingPlanUD = item.LoadingPlanUD;
                                dtoSample.SupplierID = item.SupplierID;
                                dtoSample.FactoryProformaInvoiceNo = item.FactoryProformaInvoiceNo;

                                dtoSample.Quantity = item.Quantity;
                                dtoSample.FactoryPrice = item.QuotationPurchasingPrice;
                                dtoSample.FactoryAmount = dtoSample.Quantity * dtoSample.FactoryPrice;
                                dtoSample.QuotationPurchasingPrice = item.QuotationPurchasingPrice;
                                dtoSample.FactoryProformaPrice = item.FactoryProformaPrice;

                                //pemisson for price
                                if (officeID != 4) // 4: factory office
                                {
                                    dtoSample.UnitPrice = (dbPriceDifference == null ? item.QuotationPurchasingPrice : item.QuotationPurchasingPrice * (1 + dbPriceDifference.Rate));
                                    if (dtoSample.UnitPrice.HasValue) dtoSample.UnitPrice = Math.Round(dtoSample.UnitPrice.Value, 2);
                                    dtoSample.Amount = dtoSample.Quantity * dtoSample.UnitPrice;
                                    dtoSample.QuotationSalePrice = item.QuotationSalePrice;
                                }

                                dtoItem.PurchasingInvoiceSampleDetails.Add(dtoSample);
                                i--;
                            }
                        }
                        else if (invoiceType == 2) // Other invoice on supplier (SupplierID is not null)
                        {
                            dtoItem.InvoiceType = invoiceType;
                            dtoItem.SupplierID = supplierID;
                            dtoItem.Season = currentSeason;
                            dtoItem.InvoiceDate = DateTime.Now.ToString("dd/MM/yyyy");
                            dtoItem.CompanyID = 4;

                            Module.Support.DAL.DataFactory support_factory = new Module.Support.DAL.DataFactory();
                            dtoItem.SupplierNM = support_factory.GetSupplier(iRequesterID).Where(o => o.SupplierID == supplierID).FirstOrDefault().SupplierNM;

                            //detail info
                            dtoItem.PurchasingInvoiceExtraDetails = new List<DTO.PurchasingInvoiceMng.PurchasingInvoiceExtraDetail>();
                        }
                        else if (invoiceType == 3) // Other invoice on old invoice (ParentID is not null)
                        {
                            //get parent invoice info
                            var dbParentInvoice = context.PurchasingInvoiceMng_PurchasingInvoice_View.Include("PurchasingInvoiceMng_PurchasingInvoiceDetail_View")
                                                                                                     .Include("PurchasingInvoiceMng_PurchasingInvoiceSparepartDetail_View")
                                                                                                     .Include("PurchasingInvoiceMng_PurchasingInvoiceSampleDetail_View")
                                                                                                     .Where(o => o.PurchasingInvoiceID == parentID).FirstOrDefault();
                            if (dbParentInvoice == null)
                            {
                                throw new Exception("Can not find invoice to create extra invoice");
                            }
                            else
                            {
                                //assign invoice ifo
                                dtoItem.InvoiceType = invoiceType;
                                dtoItem.SupplierID = dbParentInvoice.SupplierID;
                                dtoItem.ParentID = dbParentInvoice.PurchasingInvoiceID;
                                dtoItem.Season = currentSeason;
                                dtoItem.InvoiceDate = DateTime.Now.ToString("dd/MM/yyyy");

                                dtoItem.BLNo = dbParentInvoice.BLNo;
                                dtoItem.ShipedDate = dbParentInvoice.ShipedDate.Value.ToString("dd/MM/yyyy");
                                dtoItem.SupplierNM = dbParentInvoice.SupplierNM;
                                dtoItem.ForwarderNM = dbParentInvoice.ForwarderNM;
                                dtoItem.Feeder = dbParentInvoice.Feeder;
                                dtoItem.POLName = dbParentInvoice.POLName;
                                dtoItem.POLName = dbParentInvoice.PODName;

                                //assign invoice detail info
                                dtoItem.PurchasingInvoiceExtraDetails = new List<DTO.PurchasingInvoiceMng.PurchasingInvoiceExtraDetail>();
                                DTO.PurchasingInvoiceMng.PurchasingInvoiceExtraDetail dtoExtraDetail;

                                int i = -1;
                                foreach (var item in dbParentInvoice.PurchasingInvoiceMng_PurchasingInvoiceDetail_View)
                                {
                                    dtoExtraDetail = new DTO.PurchasingInvoiceMng.PurchasingInvoiceExtraDetail();
                                    dtoItem.PurchasingInvoiceExtraDetails.Add(dtoExtraDetail);

                                    dtoExtraDetail.PurchasingInvoiceExtraDetailID = i;
                                    dtoExtraDetail.PurchasingInvoiceDetailID = item.PurchasingInvoiceDetailID;
                                    dtoExtraDetail.Quantity = item.Quantity;
                                    dtoExtraDetail.FactoryPrice = item.FactoryPrice;
                                    dtoExtraDetail.Amount = item.Quantity * item.FactoryPrice;

                                    dtoExtraDetail.ClientUD = item.ClientUD;
                                    dtoExtraDetail.LoadingPlanUD = item.LoadingPlanUD;
                                    dtoExtraDetail.ProformaInvoiceNo = item.ProformaInvoiceNo;
                                    dtoExtraDetail.ContainerNo = item.ContainerNo;
                                    dtoExtraDetail.SealNo = item.SealNo;
                                    dtoExtraDetail.ContainerTypeNM = item.ContainerTypeNM;
                                    dtoExtraDetail.ArticleCode = item.ArticleCode;
                                    dtoExtraDetail.Description = item.Description;
                                    dtoExtraDetail.GoodsType = "PRODUCT";
                                    i--;
                                }

                                foreach (var item in dbParentInvoice.PurchasingInvoiceMng_PurchasingInvoiceSparepartDetail_View)
                                {
                                    dtoExtraDetail = new DTO.PurchasingInvoiceMng.PurchasingInvoiceExtraDetail();
                                    dtoItem.PurchasingInvoiceExtraDetails.Add(dtoExtraDetail);

                                    dtoExtraDetail.PurchasingInvoiceExtraDetailID = i;
                                    dtoExtraDetail.PurchasingInvoiceSparepartDetailID = item.PurchasingInvoiceSparepartDetailID;
                                    dtoExtraDetail.Quantity = item.Quantity;
                                    dtoExtraDetail.FactoryPrice = item.FactoryPrice;
                                    dtoExtraDetail.Amount = item.Quantity * item.FactoryPrice;

                                    dtoExtraDetail.ClientUD = item.ClientUD;
                                    dtoExtraDetail.LoadingPlanUD = item.LoadingPlanUD;
                                    dtoExtraDetail.ProformaInvoiceNo = item.ProformaInvoiceNo;
                                    dtoExtraDetail.ContainerNo = item.ContainerNo;
                                    dtoExtraDetail.SealNo = item.SealNo;
                                    dtoExtraDetail.ContainerTypeNM = item.ContainerTypeNM;
                                    dtoExtraDetail.ArticleCode = item.ArticleCode;
                                    dtoExtraDetail.Description = item.Description;
                                    dtoExtraDetail.GoodsType = "SPAREPART";
                                    i--;
                                }

                                //Sample Product
                                foreach (var item in dbParentInvoice.PurchasingInvoiceMng_PurchasingInvoiceSampleDetail_View)
                                {
                                    dtoExtraDetail = new DTO.PurchasingInvoiceMng.PurchasingInvoiceExtraDetail();
                                    dtoItem.PurchasingInvoiceExtraDetails.Add(dtoExtraDetail);

                                    dtoExtraDetail.PurchasingInvoiceExtraDetailID = i;
                                    dtoExtraDetail.PurchasingInvoiceSampleDetailID = item.PurchasingInvoiceSampleDetailID;
                                    dtoExtraDetail.Quantity = item.Quantity;
                                    dtoExtraDetail.FactoryPrice = item.FactoryPrice;
                                    dtoExtraDetail.Amount = item.Quantity * item.FactoryPrice;

                                    dtoExtraDetail.ClientUD = item.ClientUD;
                                    dtoExtraDetail.LoadingPlanUD = item.LoadingPlanUD;
                                    dtoExtraDetail.ProformaInvoiceNo = item.ProformaInvoiceNo;
                                    dtoExtraDetail.ContainerNo = item.ContainerNo;
                                    dtoExtraDetail.SealNo = item.SealNo;
                                    dtoExtraDetail.ContainerTypeNM = item.ContainerTypeNM;
                                    dtoExtraDetail.ArticleCode = item.ArticleCode;
                                    dtoExtraDetail.Description = item.Description;
                                    dtoExtraDetail.GoodsType = "SAMPLE";
                                    i--;
                                }
                            }
                        }
                    }
                    dtoItem.UserOfficeID = officeID;
                    //dtoItem.UserOfficeID = fwFactory.GetUserOffice(iRequesterID);
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
                return new DTO.PurchasingInvoiceMng.PurchasingInvoice();
            }
        }

        /// <summary>
        /// Invoice confirm by An Viet Thinh. Only user in AVT just can confirm
        /// </summary>
        /// <param name="id"></param>
        /// <param name="SetstatusBy"></param>
        /// <param name="status"></param>
        /// <param name="notification"></param>
        /// <returns></returns>
        public bool SetStatus(int id, int SetstatusBy, bool status, int invoiceType, out Library.DTO.Notification notification)
        {
            string message = status ? "Confirmed Success" : "Un-Confirmed Success";
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = message };
            try
            {
                //check user is in AVT
                Module.Framework.DAL.DataFactory fw_factory = new Module.Framework.DAL.DataFactory();
                int? companyID = fw_factory.GetCompanyID(SetstatusBy);
                if (companyID != 3)
                {
                    //3: An Viet Thinh
                    throw new Exception("You have not permisson to confirm this invoice");
                }

                //check permission on invoice
                if (fwFactory.CheckPurchasingInvoicePermission(SetstatusBy, id) == 0)
                {
                    throw new Exception("You do not have access permission on this invoice");
                }
                using (PurchasingInvoiceMngEntities context = CreateContext())
                {
                    if (invoiceType == 1)
                    {
                        //check booking is confirm
                        var invoice = context.PurchasingInvoiceMng_PurchasingInvoice_View.Where(o => o.PurchasingInvoiceID == id).FirstOrDefault();
                        if (!invoice.IsBookingConfirmed.HasValue || !invoice.IsBookingConfirmed.Value)
                        {
                            throw new Exception("Booking is not confirm yet. You can not confirm invoice");
                        }
                    }

                    //check invoice is payment
                    if (!status)
                    {
                        if (context.PurchasingInvoiceMng_function_CheckInvoiceIsAlreadyPayment(id).FirstOrDefault().Value > 0)
                        {
                            throw new Exception("Invoice is already payment. You can not un-confirm");
                        }
                    }

                    //set status
                    context.PurchasingInvoiceMng_function_SetStatus(id, SetstatusBy, status);
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

        public List<DTO.PurchasingInvoiceMng.PriceDifferenceInvoiceSetting> GetPriceDifferenceInvoiceSetting()
        {
            using (PurchasingInvoiceMngEntities context = CreateContext())
            {
                return converter.DB2DTO_PriceDifferenceInvoiceSetting(context.PurchasingInvoiceMng_PriceDifferenceInvoiceSetting_View.ToList());
            }
        }

        public bool UpdateData(int iRequesterID, int id, ref DTO.PurchasingInvoiceMng.PurchasingInvoice dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (PurchasingInvoiceMngEntities context = CreateContext())
                {
                    PurchasingInvoice dbItem = null;
                    //DocumentMonitoring dbDocumentMonitoring = null;
                    PurchasingInvoiceMng_Booking_View dbBooking = null;
                    if (id == 0)
                    {
                        if (dtoItem.BookingID.HasValue || dtoItem.SupplierID.HasValue || dtoItem.ParentID.HasValue) // create fob invoice base on booking
                        {
                            //check permission on booking
                            if (dtoItem.BookingID.HasValue && fwFactory.CheckBookingPermission(iRequesterID, dtoItem.BookingID.Value) == 0)
                            {
                                throw new Exception("You do not have access permission on this booking");
                            }
                            //check permission on supplier
                            if (dtoItem.SupplierID.HasValue && fwFactory.CheckSupplierPermission(iRequesterID, dtoItem.SupplierID.Value) == 0)
                            {
                                throw new Exception("You do not have access permission on this supplier");
                            }
                            //check permission on purchasing invoice
                            if (dtoItem.ParentID.HasValue && fwFactory.CheckPurchasingInvoicePermission(iRequesterID, dtoItem.ParentID.Value) == 0)
                            {
                                throw new Exception("You do not have access permission on this invoice");
                            }
                        }
                        else
                        {
                            throw new Exception("There are not invoice type, you can not create invoice");
                        }

                        //read data to update
                        dbItem = new PurchasingInvoice();
                        context.PurchasingInvoice.Add(dbItem);

                        //create document monitoring
                        //dbDocumentMonitoring = new DocumentMonitoring();
                        //dbDocumentMonitoring.SendToEUBy = 358;
                        //dbDocumentMonitoring.VNReceivedBy = 358; // 358: Dang Thi Yen Nhu
                        //dbItem.DocumentMonitoring.Add(dbDocumentMonitoring);

                        //get loading plan detail info base on booking to use for update furnindo price in case user create create invoice
                        if (dtoItem.InvoiceType == 1 && dtoItem.BookingID.HasValue)
                        {
                            int bookingID = dtoItem.BookingID.Value;
                            dbBooking = context.PurchasingInvoiceMng_Booking_View.Include("PurchasingInvoiceMng_LoadingPlanDetail_View")
                                                                                 .Include("PurchasingInvoiceMng_LoadingPlanSparepartDetail_View")
                                                                                 .FirstOrDefault(o => o.BookingID == bookingID);
                        }
                    }
                    else
                    {
                        //check permission on invoice
                        if (fwFactory.CheckPurchasingInvoicePermission(iRequesterID, id) == 0)
                        {
                            throw new Exception("You do not have access permission on this invoice");
                        }
                        dbItem = context.PurchasingInvoice.FirstOrDefault(o => o.PurchasingInvoiceID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "invoice not found!";
                        return false;
                    }
                    else
                    {
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoItem.ConcurrencyFlag_String)))
                        {
                            throw new Exception(DALBase.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        if (dtoItem.InvoiceType != 1 && string.IsNullOrEmpty(dtoItem.InvoiceNo))
                        {
                            throw new Exception("You need fill-in invoice no");
                        }

                        //check permission on every purchasing detail row
                        //CheckPermissionOnEveryItem(iRequesterID, dtoItem);

                        //convert dto to db
                        converter.DTO2DB_PurchasingInvoice(iRequesterID, dbBooking, dtoItem, ref dbItem);

                        //calculate amount for invoice with type = 3(extra invoice on parent invoice)
                        if (dbItem.InvoiceType == 3) // 3: extra invoice on parent invoice
                        {
                            foreach (var item in dbItem.PurchasingInvoiceExtraDetail)
                            {
                                item.Amount = item.Quantity * item.FactoryPrice;
                            }
                        }

                        //remove orphan item
                        context.PurchasingInvoiceDetail.Local.Where(o => o.PurchasingInvoice == null).ToList().ForEach(o => context.PurchasingInvoiceDetail.Remove(o));
                        context.PurchasingInvoiceSparepartDetail.Local.Where(o => o.PurchasingInvoice == null).ToList().ForEach(o => context.PurchasingInvoiceSparepartDetail.Remove(o));
                        context.PurchasingInvoiceExtraDetail.Local.Where(o => o.PurchasingInvoice == null).ToList().ForEach(o => context.PurchasingInvoiceExtraDetail.Remove(o));

                       

                        //
                        // generate factory order cache
                        //
                        context.FactoryOrderInfoCacheMng_function_BuildCacheForPurchasingInvoice(dbItem.PurchasingInvoiceID);

                        //Update InvoiceNo
                        if (id == 0 && dbItem.InvoiceType == 1) //auto generea invoice no in case create invoice base on Booking
                        {
                            context.PurchasingInvoiceMng_function_GenerateInvoiceNo(dbItem.PurchasingInvoiceID);
                            context.SaveChanges();
                        }
                       
                        //save data
                        context.SaveChanges();
                        //Get return data
                        dtoItem = GetData(dbItem.PurchasingInvoiceID, out notification);
                        // cuong.tran - 03-Jan-2019

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

        public IEnumerable<DTO.ECommercialInvoiceMng.InvoiceType> GetInvoiceType()
        {
            List<DTO.ECommercialInvoiceMng.InvoiceType> InvoiceTypes = new List<DTO.ECommercialInvoiceMng.InvoiceType>();
            InvoiceTypes.Add(new DTO.ECommercialInvoiceMng.InvoiceType { InvoiceTypeValue = 1, InvoiceTypeText = "FOB-INVOICE" });
            InvoiceTypes.Add(new DTO.ECommercialInvoiceMng.InvoiceType { InvoiceTypeValue = 2, InvoiceTypeText = "EXTRA-INVOICE" });
            return InvoiceTypes;
        }

        public DTO.PurchasingInvoiceMng.InitData GetInitData(int iRequesterID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.PurchasingInvoiceMng.InitData initData = new DTO.PurchasingInvoiceMng.InitData();
            Module.Support.DAL.DataFactory support_factory = new Module.Support.DAL.DataFactory();
            try
            {
                initData.InvoiceTypes = GetInvoiceType();
                initData.Suppliers = support_factory.GetSupplier(iRequesterID);
                return initData;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return new DTO.PurchasingInvoiceMng.InitData();
            }
        }

        public DTO.PurchasingInvoiceMng.SearchFormData GetSearchData(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            string Season = string.Empty;
            string InvoiceNo = string.Empty;
            string ProformaInvoiceNo = string.Empty;
            string ClientUD = string.Empty;
            string ContainerNo = string.Empty;
            string BLNo = string.Empty;
            string PurchasingCreditNoteNo = string.Empty;
            int? SupplierID = null;
            int? CompanyID = null;
            string FactoryInvoiceNo = string.Empty;
            int? ConfirmAll = null;
            string UpdateName = string.Empty;

            if (filters.ContainsKey("Season") && filters["Season"] != null) Season = filters["Season"].ToString();
            if (filters.ContainsKey("InvoiceNo")) InvoiceNo = filters["InvoiceNo"].ToString();
            if (filters.ContainsKey("ProformaInvoiceNo")) ProformaInvoiceNo = filters["ProformaInvoiceNo"].ToString();
            if (filters.ContainsKey("ClientUD")) ClientUD = filters["ClientUD"].ToString();
            if (filters.ContainsKey("ContainerNo")) ContainerNo = filters["ContainerNo"].ToString();
            if (filters.ContainsKey("InvoiceNo")) InvoiceNo = filters["InvoiceNo"].ToString();
            if (filters.ContainsKey("BLNo")) BLNo = filters["BLNo"].ToString();
            if (filters.ContainsKey("PurchasingCreditNoteNo")) PurchasingCreditNoteNo = filters["PurchasingCreditNoteNo"].ToString();
            if (filters.ContainsKey("CompanyID") && filters["CompanyID"] != null && filters["CompanyID"].ToString() != string.Empty) CompanyID = Convert.ToInt32(filters["CompanyID"]);
            if (filters.ContainsKey("SupplierID") && filters["SupplierID"] != null && filters["SupplierID"].ToString() != string.Empty) SupplierID = Convert.ToInt32(filters["SupplierID"]);
            if (filters.ContainsKey("FactoryInvoiceNo")) FactoryInvoiceNo = filters["FactoryInvoiceNo"].ToString();
            if (filters.ContainsKey("ConfirmAll") && filters["ConfirmAll"] != null && filters["ConfirmAll"].ToString() != string.Empty) ConfirmAll = Convert.ToInt32(filters["ConfirmAll"]);
            if (filters.ContainsKey("UpdateName")) UpdateName = filters["UpdateName"].ToString();
            //try to get data
            try
            {
                using (PurchasingInvoiceMngEntities context = CreateContext())
                {
                    DTO.PurchasingInvoiceMng.SearchFormData data = new DTO.PurchasingInvoiceMng.SearchFormData();
                    List<DTO.PurchasingInvoiceMng.PurchasingInvoiceSearch> searchResult = new List<DTO.PurchasingInvoiceMng.PurchasingInvoiceSearch>();
                    List<DTO.PurchasingInvoiceMng.PurchasingInvoiceSearch> searchAllResult = new List<DTO.PurchasingInvoiceMng.PurchasingInvoiceSearch>();

                    var result = context.PurchasingInvoiceMng_function_SearchPurchasingInvoice(iRequesterID, orderBy, orderDirection, Season, InvoiceNo, ProformaInvoiceNo, ClientUD, ContainerNo, BLNo, PurchasingCreditNoteNo, SupplierID, FactoryInvoiceNo, CompanyID, ConfirmAll, UpdateName).ToList();
                    var result1 = result.ToList();
                    var count = 0;
                    decimal? totalAmount = 0;
                    decimal? totalFactoryAmount = 0;
                    foreach (var item in result1)
                    {
                        count = count + 1;
                        totalAmount = totalAmount + item.TotalAmount;
                        totalFactoryAmount = totalFactoryAmount + item.TotalFactoryAmount;

                    }
                    totalRows = count;
                    data.TotalAmount = totalAmount;
                    data.TotalFactoryAmount = totalFactoryAmount;
                    //data.UserOfficeID = fwFactory.GetUserOffice(iRequesterID);
                    data.UserOfficeID = 1;
                    int[] internalCompanyID = new int[] { 1, 2, 3, 13, 75 };
                    if (!internalCompanyID.ToList().Contains(fwFactory.GetCompanyID(iRequesterID).Value)) // 1, 2, 3 = AVF, AVS, AVT
                    {
                        data.UserOfficeID = 4; // fake the office id equal to 4, backward compatible
                    }

                    //var result = context.PurchasingInvoiceMng_function_SearchPurchasingInvoice(iRequesterID, orderBy, orderDirection, Season, InvoiceNo, ProformaInvoiceNo, ClientUD, ContainerNo, BLNo, PurchasingCreditNoteNo, SupplierID, FactoryInvoiceNo, CompanyID, ConfirmAll, UpdateName);
                    searchResult = converter.DB2DTO_PurchasingInvoiceSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());

                    searchAllResult = converter.DB2DTO_PurchasingInvoiceSearch(result1);

                    if (data.UserOfficeID.Value == 4)
                    {
                        data.TotalAmount = null;
                        foreach (var item in searchResult)
                        {
                            item.TotalAmount = null;
                        }
                    }

                    data.Data = searchResult;
                    data.AllData = searchAllResult;
                    return data;
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
                return new DTO.PurchasingInvoiceMng.SearchFormData();
            }
        }

        private void CheckPermissionOnEveryItem(int iRequesterID, DTO.PurchasingInvoiceMng.PurchasingInvoice dtoItem)
        {
            //get list supplier to check item in detail
            List<int?> supplierIDs = fwFactory.GetListSupplierByUser(iRequesterID);
            using (PurchasingInvoiceMngEntities context = CreateContext())
            {
                if (dtoItem.PurchasingInvoiceDetails != null && dtoItem.PurchasingInvoiceDetails.Count() > 0)
                {
                    List<int?> dtoLoadingPlanDetailIDs = dtoItem.PurchasingInvoiceDetails.Where(o => o.PurchasingInvoiceDetailID < 0 && o.Quantity > 0).Select(s => s.LoadingPlanDetailID).ToList();
                    List<int?> dtoPurchasingInvoiceDetailIDs = dtoItem.PurchasingInvoiceDetails.Where(o => o.PurchasingInvoiceDetailID > 0 && o.Quantity > 0).Select(s => s.PurchasingInvoiceDetailID).ToList();

                    //check permission on every item
                    if (dtoLoadingPlanDetailIDs.Count() > 0 && context.PurchasingInvoiceMng_LoadingPlanDetail_View.Where(o => dtoLoadingPlanDetailIDs.Contains(o.LoadingPlanDetailID)).Select(s => s.SupplierID).ToList().Except(supplierIDs).Count() > 0)
                    {
                        throw new Exception("You do not have access permission on these products that you added of invoice");
                    }
                    if (dtoPurchasingInvoiceDetailIDs.Count() > 0 && context.PurchasingInvoiceMng_PurchasingInvoiceDetail_View.Where(o => dtoPurchasingInvoiceDetailIDs.Contains(o.PurchasingInvoiceDetailID)).Select(s => s.SupplierID).ToList().Except(supplierIDs).Count() > 0)
                    {
                        throw new Exception("You do not have access permission on these products of invoice");
                    }

                    //check quotation product price
                    if (dtoLoadingPlanDetailIDs.Count() > 0 && context.PurchasingInvoiceMng_LoadingPlanDetail_View.Where(o => dtoLoadingPlanDetailIDs.Contains(o.LoadingPlanDetailID) && o.PriceStatus != "CONFIRMED").Count() > 0)
                    {
                        throw new Exception("There are some products that price was not confirm. You can not save");
                    }

                    if (dtoPurchasingInvoiceDetailIDs.Count() > 0 && context.PurchasingInvoiceMng_PurchasingInvoiceDetail_View.Where(o => dtoPurchasingInvoiceDetailIDs.Contains(o.PurchasingInvoiceDetailID) && o.PriceStatus != "CONFIRMED").Count() > 0)
                    {
                        throw new Exception("There are some products that price was not confirm. You can not save");
                    }
                }

                //check permission on every purchasing sparepart detail row
                if (dtoItem.PurchasingInvoiceSparepartDetails != null && dtoItem.PurchasingInvoiceSparepartDetails.Count() > 0)
                {
                    List<int?> dtoLoadingPlanSparepartDetailIDs = dtoItem.PurchasingInvoiceSparepartDetails.Where(o => o.PurchasingInvoiceSparepartDetailID < 0 && o.Quantity > 0).Select(s => s.LoadingPlanSparepartDetailID).ToList();
                    List<int?> dtoPurchasingInvoiceSparepartDetailIDs = dtoItem.PurchasingInvoiceSparepartDetails.Where(o => o.PurchasingInvoiceSparepartDetailID > 0 && o.Quantity > 0).Select(s => s.PurchasingInvoiceSparepartDetailID).ToList();

                    //check sparepart item
                    if (dtoLoadingPlanSparepartDetailIDs.Count() > 0 && context.PurchasingInvoiceMng_LoadingPlanSparepartDetail_View.Where(o => dtoLoadingPlanSparepartDetailIDs.Contains(o.LoadingPlanSparepartDetailID)).Select(s => s.SupplierID).ToList().Except(supplierIDs).Count() > 0)
                    {
                        throw new Exception("You do not have access permission on these spareparts that you added of invoice");
                    }
                    if (dtoPurchasingInvoiceSparepartDetailIDs.Count() > 0 && context.PurchasingInvoiceMng_PurchasingInvoiceSparepartDetail_View.Where(o => dtoPurchasingInvoiceSparepartDetailIDs.Contains(o.PurchasingInvoiceSparepartDetailID)).Select(s => s.SupplierID).ToList().Except(supplierIDs).Count() > 0)
                    {
                        throw new Exception("You do not have access permission on these spareparts of invoice");
                    }

                    //check quotation sparepart price
                    if (dtoLoadingPlanSparepartDetailIDs.Count() > 0 && context.PurchasingInvoiceMng_LoadingPlanSparepartDetail_View.Where(o => dtoLoadingPlanSparepartDetailIDs.Contains(o.LoadingPlanSparepartDetailID) && o.PriceStatus != "CONFIRMED").Count() > 0)
                    {
                        throw new Exception("There are some sparepart that price was not confirm. You can not save");
                    }

                    if (dtoPurchasingInvoiceSparepartDetailIDs.Count() > 0 && context.PurchasingInvoiceMng_PurchasingInvoiceSparepartDetail_View.Where(o => dtoPurchasingInvoiceSparepartDetailIDs.Contains(o.PurchasingInvoiceSparepartDetailID) && o.PriceStatus != "CONFIRMED").Count() > 0)
                    {
                        throw new Exception("There are some sparepart that price was not confirm. You can not save");
                    }
                }
            }
        }

        public bool MarkAsExportedToExact(List<int> purchasingInvoiceIDs, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (PurchasingInvoiceMngEntities context = CreateContext())
                {
                    var dbInvoice = context.PurchasingInvoice.Where(o => purchasingInvoiceIDs.Contains(o.PurchasingInvoiceID)).ToList();
                    foreach (var item in dbInvoice)
                    {
                        item.IsExpotedToExact = true;
                    }
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

        public bool MarkAsNotYetExportedToExact(int userId, List<int> purchasingInvoiceIDs, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                if (fwFactory.HasSpecialPermission(userId, Module.Framework.ConstantIdentifier.SPECIAL_PERMISSION_MARK_INVOICE_NOT_YET_EXPORTED))
                {
                    throw new Exception("Your account don't have the permission for mark invoices as not yet exported to Exact!");
                }

                using (PurchasingInvoiceMngEntities context = CreateContext())
                {
                    var dbInvoice = context.PurchasingInvoice.Where(o => purchasingInvoiceIDs.Contains(o.PurchasingInvoiceID)).ToList();
                    foreach (var item in dbInvoice)
                    {
                        item.IsExpotedToExact = false;
                    }
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return false;
            }
        }

        /// <summary>
        /// Invoice confirm by factory.
        /// All price must be confirmed before confirm
        /// </summary>
        /// <param name="setStatusBy"></param>
        /// <param name="purchasingInvoiceID"></param>
        /// <param name="dtoItem"></param>
        /// <param name="isConfirmedPrice"></param>
        /// <param name="notification"></param>
        /// <returns></returns>
        public bool SetConfirmPrice(int setStatusBy, int purchasingInvoiceID, DTO.PurchasingInvoiceMng.PurchasingInvoice dtoItem, bool isConfirmedPrice, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                //check permission on invoice
                if (fwFactory.CheckPurchasingInvoicePermission(setStatusBy, purchasingInvoiceID) == 0)
                {
                    throw new Exception("You do not have access permission on this invoice");
                }

                //update data before set status
                if (isConfirmedPrice)
                {
                    //check factory invoiceNo 
                    if (string.IsNullOrEmpty(dtoItem.FactoryInvoiceNo))
                    {
                        throw new Exception("Please fill-in factory invoice no");
                    }

                    //check invoice date
                    if (string.IsNullOrEmpty(dtoItem.InvoiceDate))
                    {
                        throw new Exception("Please fill-in invoice date");
                    }

                    //save data before confirm
                    this.UpdateData(setStatusBy, purchasingInvoiceID, ref dtoItem, out notification);

                    //check items
                    this.CheckPermissionOnEveryItem(setStatusBy, dtoItem);
                    
                    
                }
                //send notification when user confirm
                
                //set status
                using (PurchasingInvoiceMngEntities context = CreateContext())
                {
                    if (dtoItem.InvoiceType == 1)
                    {
                        //check booking is confirm
                        var invoice = context.PurchasingInvoiceMng_PurchasingInvoice_View.Where(o => o.PurchasingInvoiceID == purchasingInvoiceID).FirstOrDefault();
                        if (!invoice.IsBookingConfirmed.HasValue || !invoice.IsBookingConfirmed.Value)
                        {
                            throw new Exception("Booking is not confirm yet. You can not confirm invoice");
                        }
                    }

                    //set confirm price status
                    PurchasingInvoice dbInvoice = context.PurchasingInvoice.Where(o => o.PurchasingInvoiceID == purchasingInvoiceID).FirstOrDefault();
                    dbInvoice.IsConfirmedPrice = isConfirmedPrice;
                    dbInvoice.ConfirmedPriceBy = setStatusBy;
                    dbInvoice.ConfirmedPriceDate = DateTime.Now;

                    
                    string message = isConfirmedPrice ? "Factory Confirmed Success" : "Factory Un-Confirmed Success";
                    notification.Message = message;
                    //send notification
                    if (isConfirmedPrice == true)
                    {
                        var emailSubject = "Purchasing Invoice " + dtoItem.BLNo + " has been confirmed by: " + dtoItem.SupplierNM;
                        fwFactory.SendEmailNotificationBasedOn("PurchasingInvoiceMng", dtoItem.PurchasingInvoiceID, emailSubject, 1);
                                                
                        // Get information notification with status
                        var groupStatuses = context.FW_NotificationGroupStatus_View.Where(o => o.ModuleUD == "PurchasingInvoiceMng" && o.StatusID == 1).ToList();
                        foreach (var status in groupStatuses)
                        {
                            // Create email body
                            var EmailBody = FrameworkSetting.Setting.FrontendUrl + status.URLLink + "/Edit/" + purchasingInvoiceID.ToString();

                            if (!string.IsNullOrEmpty(status.EmailTemplate))
                            {
                                EmailBody = EmailBody + Environment.NewLine + status.EmailTemplate + purchasingInvoiceID.ToString();
                            }
                            var dbGroupMember = context.FW_NotificationGroupMember_View.Where(o => o.NotificationGroupID == status.NotificationGroupID);
                            foreach (var user in dbGroupMember)
                            {
                                // add to NotificationMessage table
                                NotificationMessage notification1 = new NotificationMessage();
                                notification1.UserID = user.UserID;
                                notification1.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_ACCOUNTING;
                                notification1.NotificationMessageTitle = emailSubject;
                                notification1.NotificationMessageContent = EmailBody;
                                context.NotificationMessage.Add(notification1);
                            }
                        }

                    }

                    //save data
                    context.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                Exception iEx = Library.Helper.GetInnerException(ex);
                notification.Message = iEx.Message;
                return false;
            }
        }
        
    }
}

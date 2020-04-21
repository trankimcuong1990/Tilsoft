using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
namespace DAL.CostInvoiceMng
{
    public class DataFactory : DALBase.FactoryBase<DTO.CostInvoiceMng.CostInvoiceSearchResult, DTO.CostInvoiceMng.CostInvoice>
    {
        private DataConverter converter = new DataConverter();
        private CostInvoiceMngEntities CreateContext()
        {
            return new CostInvoiceMngEntities(DALBase.Helper.CreateEntityConnectionString("CostInvoiceMngModel"));
        }

        public override IEnumerable<DTO.CostInvoiceMng.CostInvoiceSearchResult> GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            string InvoiceNo = string.Empty;
            string ClientUD = string.Empty;
            string ClientNM = string.Empty;
            string BLNo = string.Empty;
            
            if (filters.ContainsKey("InvoiceNo")) InvoiceNo = filters["InvoiceNo"].ToString();
            if (filters.ContainsKey("ClientUD")) ClientUD = filters["ClientUD"].ToString();
            if (filters.ContainsKey("ClientNM")) ClientNM = filters["ClientNM"].ToString();
            if (filters.ContainsKey("BLNo")) BLNo = filters["BLNo"].ToString();
            
            //try to get data
            try
            {
                using (CostInvoiceMngEntities context = CreateContext())
                {
                    totalRows = context.CostInvoiceMng_function_SearchCostInvoice(  orderBy,
                                                                                    orderDirection,
                                                                                    InvoiceNo,
                                                                                    ClientUD,
                                                                                    ClientNM,
                                                                                    BLNo
                                                                                    ).Count();
                    var result = context.CostInvoiceMng_function_SearchCostInvoice( orderBy,
                                                                                    orderDirection,
                                                                                    InvoiceNo,
                                                                                    ClientUD,
                                                                                    ClientNM,
                                                                                    BLNo
                                                                                    );
                    
                    return converter.DB2DTO_CostInvoiceSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
                return new List<DTO.CostInvoiceMng.CostInvoiceSearchResult>();
            }
        }

        public override DTO.CostInvoiceMng.CostInvoice GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            //try to get data
            try
            {
                using (CostInvoiceMngEntities context = CreateContext())
                {
                    CostInvoiceMng_CostInvoice_View dbItem;
                    dbItem = context.CostInvoiceMng_CostInvoice_View
                        .Include("CostInvoiceMng_CostInvoiceDetail_View.CostInvoiceMng_CostInvoiceDetailExtend_View")
                        .FirstOrDefault(o => o.CostInvoiceID == id);
                    return converter.DB2DTO_CostInvoice(dbItem);
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
                return new DTO.CostInvoiceMng.CostInvoice();
            }
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (CostInvoiceMngEntities context = CreateContext())
                {
                    CostInvoice dbItem = context.CostInvoice.FirstOrDefault(o => o.CostInvoiceID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Invoice not found!";
                        return false;
                    }
                    else
                    {
                        context.CostInvoice.Remove(dbItem);
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

        public override bool UpdateData(int id, ref DTO.CostInvoiceMng.CostInvoice dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (CostInvoiceMngEntities context = CreateContext())
                {
                    CostInvoice dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new CostInvoice();
                        context.CostInvoice.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.CostInvoice.FirstOrDefault(o => o.CostInvoiceID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Invoice not found!";
                        return false;
                    }
                    else
                    {
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoItem.ConcurrencyFlag_String)))
                        {
                            throw new Exception(DALBase.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        converter.DTO2DB_CostInvoice(dtoItem, ref dbItem);

                        //remove orphan item
                        context.CostInvoiceDetailExtend.Local.Where(o => o.CostInvoiceDetail == null).ToList().ForEach(o => context.CostInvoiceDetailExtend.Remove(o));
                        context.CostInvoiceDetail.Local.Where(o => o.CostInvoice == null).ToList().ForEach(o => context.CostInvoiceDetail.Remove(o));

                        //save data
                        context.SaveChanges();

                        //reload data
                        dtoItem = GetData(dbItem.CostInvoiceID, out notification);

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

        public DTO.CostInvoiceMng.DataContainer GetDataContainer(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            //try to get data
            try
            {
                using (CostInvoiceMngEntities context = CreateContext())
                {
                    DTO.CostInvoiceMng.DataContainer dtoItem = new DTO.CostInvoiceMng.DataContainer();

                    if (id > 0)
                    {
                        CostInvoiceMng_CostInvoice_View dbItem;
                        dbItem = context.CostInvoiceMng_CostInvoice_View
                                .Include("CostInvoiceMng_CostInvoiceDetail_View.CostInvoiceMng_CostInvoiceDetailExtend_View")
                                .FirstOrDefault(o => o.CostInvoiceID == id);
                        DTO.CostInvoiceMng.CostInvoice CostInvoiceDTOItem = converter.DB2DTO_CostInvoice(dbItem);
                        dtoItem.CostInvoiceData = CostInvoiceDTOItem;
                    }
                    else
                    {
                        dtoItem.CostInvoiceData = new DTO.CostInvoiceMng.CostInvoice();
                        dtoItem.CostInvoiceData.CostInvoiceDetails = new List<DTO.CostInvoiceMng.CostInvoiceDetail>();
                        foreach (DTO.CostInvoiceMng.CostInvoiceDetail item in dtoItem.CostInvoiceData.CostInvoiceDetails)
                        {
                            item.CostInvoiceDetailExtends = new List<DTO.CostInvoiceMng.CostInvoiceDetailExtend>();
                        }
                    }

                    // get support data
                    dtoItem.DeliveryTerms = new DAL.Support.DataFactory().GetDeliveryTerm().ToList();
                    dtoItem.PaymentTerms = new DAL.Support.DataFactory().GetPaymentTerm().ToList();
                    dtoItem.Currency = new DAL.Support.DataFactory().GetCurrency().ToList();
                    dtoItem.CostTypes = GetCostTypes().ToList();

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
                return new DTO.CostInvoiceMng.DataContainer();
            }
        }
       
        public IEnumerable<DTO.CostInvoiceMng.InitInfo> GetInitInfos(out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            //try to get data
            try
            {
                using (CostInvoiceMngEntities context = CreateContext())
                {
                    totalRows = context.CostInvoiceMng_InitInfo_View.Count();
                    var result = context.CostInvoiceMng_InitInfo_View;
                    return converter.DB2DTO_InitInfos(result.ToList());

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
                return new List<DTO.CostInvoiceMng.InitInfo>();
            }
        }

        public IEnumerable<DTO.CostInvoiceMng.InitInfoDetail> GetInitInfoDetails(int eCommercialInvoiceID,int bookingID, int clientID, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            //try to get data
            try
            {
                using (CostInvoiceMngEntities context = CreateContext())
                {
                    totalRows = context.CostInvoiceMng_InitInfoDetail_View.Where(o => o.ECommercialInvoiceID == eCommercialInvoiceID && o.BookingID == bookingID && o.ClientID == clientID).Count();
                    var result = context.CostInvoiceMng_InitInfoDetail_View.Where(o => o.ECommercialInvoiceID == eCommercialInvoiceID && o.BookingID == bookingID && o.ClientID == clientID);
                    return converter.DB2DTO_InitInfoDetails(result.ToList());
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
                return new List<DTO.CostInvoiceMng.InitInfoDetail>();
            }
        }

        public IEnumerable<DTO.CostInvoiceMng.InitInfoDetail> QuickSearchContainer(int eCommercialInvoiceID,int bookingID, int clientID, string containerNo, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (CostInvoiceMngEntities context = CreateContext())
                {
                    var result = context.CostInvoiceMng_function_SearchContainer(eCommercialInvoiceID, bookingID, clientID, containerNo);
                    return converter.DB2DTO_InitInfoDetails(result.ToList());

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
                return new List<DTO.CostInvoiceMng.InitInfoDetail>();
            }
        }

        public DTO.CostInvoiceMng.InvoiceContainerPrintout GetPrintoutData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            //try to get data
            try
            {
                using (CostInvoiceMngEntities context = CreateContext())
                {
                    CostInvoice_ReportView dbItem;
                    dbItem = context.CostInvoiceMng_CostInvoice_ReportView
                        .Include("CostInvoiceDetail_ReportView.CostInvoiceDetailExtend_ReportView")
                        .FirstOrDefault(o => o.CostInvoiceID == id);
                    return converter.DB2DTO_Printout(dbItem);
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
                return new DTO.CostInvoiceMng.InvoiceContainerPrintout();
            }
        }

        private IEnumerable<DTO.CostInvoiceMng.CostType> GetCostTypes()
        {
            List<DTO.CostInvoiceMng.CostType> result = new List<DTO.CostInvoiceMng.CostType>();
            result.Add(new DTO.CostInvoiceMng.CostType() { CostTypeValue = "S", CostTypeText = "SEA FREIGHT" });
            result.Add(new DTO.CostInvoiceMng.CostType() { CostTypeValue = "T", CostTypeText = "TRUCKING" });
            result.Add(new DTO.CostInvoiceMng.CostType() { CostTypeValue = "D", CostTypeText = "DEMURAGE" });
            return result;
        }
    }
}

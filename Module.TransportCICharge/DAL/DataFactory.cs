namespace Module.TransportCICharge.DAL
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Library.DTO;
    using Module.TransportCICharge.DTO;
    using Library;
    using Newtonsoft.Json.Linq;

    internal class DataFactory : Library.Base.DataFactory<DTO.SearchData, DTO.EditData>
    {
        #region ** Variable & Constant **

        protected DataConverter converter = new DataConverter();

        protected Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();

        #endregion

        #region ** Constructors **

        public TransportCIChargeEntities CreateContext()
        {
            return new TransportCIChargeEntities(Helper.CreateEntityConnectionString("DAL.TransportCIChargeModel"));
        }

        #endregion

        #region ** Functions & Methods **

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Notification notification)
        {
            notification = new Notification { Type = NotificationType.Success };

            try
            {
                using (TransportCIChargeEntities context = CreateContext())
                {
                    TransportCICharge dbItem = context.TransportCICharge.FirstOrDefault(o => o.TransportCIChargeID == id);

                    if (dbItem == null)
                        return true;

                    context.TransportCICharge.Remove(dbItem);
                    context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);

                return false;
            }
        }

        public override EditData GetData(int id, out Notification notification)
        {
            notification = new Notification { Type = NotificationType.Success };
            DTO.EditData data = new EditData();
            data.Data = new TransportCIChargeDto();
            data.PaymentTerms = new List<Support.DTO.PaymentTerm>();
            data.ContainerTypes = new List<Support.DTO.ContainerType>();
            data.ChargeTypes = new List<Support.DTO.TypeOfCharge>();
            data.CurrencyTypes = new List<Support.DTO.TypeOfCurrency>();

            try
            {
                if (id > 0)
                {
                    using (var context = CreateContext())
                    {
                        var dbItem = context.TransportCICharge_TransportCICharge_View.SingleOrDefault(o => o.TransportCIChargeID == id);
                        if (dbItem == null)
                        {
                            throw new Exception("Can not found the Transport Cost Forwarder to edit");
                        }

                        data.Data = converter.DB2DTO_TransportCICharge(dbItem);
                    }

                    //Get list container number with BL number
                    Hashtable filters = new Hashtable();
                    filters["BLNo"] = data.Data.BLNo;
                    data.ContainerNrs = GetBookingContainer(filters, out notification);
                }

                data.PaymentTerms = supportFactory.GetPaymentTerm();
                data.ContainerTypes = supportFactory.GetContainerType();
                data.ChargeTypes = supportFactory.GetTypeOfCharges().ToList();
                data.CurrencyTypes = supportFactory.GetTypeOfCurrency();
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override SearchData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            notification = new Notification { Type = NotificationType.Success };

            SearchData data = new SearchData();

            data.Data = new List<TransportCIChargeSearchResultDto>();
            totalRows = 0;

            try
            {
                using (var context = CreateContext())
                {
                    string clientNM = (filters.ContainsKey("ClientNM") && !string.IsNullOrEmpty(filters["ClientNM"].ToString())) ? filters["ClientNM"].ToString().Replace("'", "''") : null;
                    string bookingUD = (filters.ContainsKey("BookingUD") && filters["BookingUD"] != null) ? filters["BookingUD"].ToString().Replace("'", "''") : null;
                    string eurofarInvoiceNumber = (filters.ContainsKey("EurofarInvoiceNr") && filters["EurofarInvoiceNr"] != null) ? filters["EurofarInvoiceNr"].ToString().Replace("'", "''") : null;

                    totalRows = context.TransportCICharge_function_TransportCIChargeSearch(clientNM, bookingUD, eurofarInvoiceNumber, null, null, orderBy, orderDirection).Count();

                    var result = context.TransportCICharge_function_TransportCIChargeSearch(clientNM, bookingUD, eurofarInvoiceNumber, null, null, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_TransportCICharge_SearchView(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
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
            DTO.TransportCIChargeDto dtoEditItem = ((JObject)dtoItem).ToObject<DTO.TransportCIChargeDto>();
            notification = new Notification { Type = NotificationType.Success };

            try
            {
                using (var context = CreateContext())
                {
                    TransportCICharge dbItem;

                    if (id > 0)
                    {
                        dbItem = context.TransportCICharge.FirstOrDefault(o => o.TransportCIChargeID == id);
                    }
                    else
                    {
                        dbItem = new TransportCICharge();

                        context.TransportCICharge.Add(dbItem);

                        dbItem.CreatedBy = userId;
                        dbItem.CreatedDate = DateTime.Now;
                    }

                    if (dbItem == null)
                    {
                        notification.Message = string.Format("TransportCostForwarder [id={0}] not found!", id);
                        return false;
                    }

                    converter.DTO2DB_TransportCICharge(dtoEditItem, ref dbItem);

                    dbItem.UpdatedBy = userId;
                    dbItem.UpdatedDate = DateTime.Now;

                    // remove orphan items
                    context.TransportCIChargeDetail.Local.Where(o => o.TransportCICharge == null).ToList()
                        .ForEach(o => context.TransportCIChargeDetail.Remove(o));

                    context.SaveChanges();

                    dtoItem = GetData(dbItem.TransportCIChargeID, out notification).Data;

                    return true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }
        }

        public ClientBookingList QuickSearchBooking(int userId, Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            ClientBookingList booking = new ClientBookingList();
            booking.Data = new List<ClientBooking>();
            booking.TotalRows = 0;

            var pageSize = 20;
            var pageIndex = 1;
            var sortedBy = string.Empty;
            var sortedDirection = string.Empty;

            if (filters.ContainsKey("pageSize")) pageSize = Convert.ToInt32(filters["pageSize"].ToString());
            if (filters.ContainsKey("pageIndex")) pageIndex = Convert.ToInt32(filters["pageIndex"].ToString());
            if (filters.ContainsKey("sortedBy")) sortedBy = filters["sortedBy"].ToString();
            if (filters.ContainsKey("sortedDirection")) sortedDirection = filters["sortedDirection"].ToString();

            try
            {
                using (var context = CreateContext())
                {
                    string bookingUD = null;
                    string clientID = null;

                    if (filters.ContainsKey("searchQuery") && filters["searchQuery"] != null)
                    {
                        bookingUD = filters["searchQuery"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("clientID") && filters["clientID"] != null)
                    {
                        clientID = filters["clientID"].ToString().Replace("'", "''");
                    }

                    booking.TotalRows = context.SupportMng_function_QuickSearchBookingForClient(bookingUD, /*blNo,*/ clientID, /*userId,*/ sortedBy, sortedDirection).Count();

                    var result = context.SupportMng_function_QuickSearchBookingForClient(bookingUD, /*blNo,*/ clientID, /*userId,*/ sortedBy, sortedDirection);

                    booking.Data = converter.DB2DTO_BookingForClient(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
            }

            return booking;
        }

        public List<DTO.ContainerNr> GetBookingContainer(Hashtable filters, out Notification notification)
        {
            notification = new Notification { Type = NotificationType.Success };

            List<DTO.ContainerNr> containers = new List<DTO.ContainerNr>();

            var sortedBy = string.Empty;
            var sortedDirection = string.Empty;

            try
            {
                using (var context = CreateContext())
                {
                    string blNo = null;

                    if (filters.ContainsKey("BLNo") && filters["BLNo"] != null)
                    {
                        blNo = filters["BLNo"].ToString().Replace("'", "''");
                    }

                    var result = context.Support_function_DropDownContainer(blNo, "ContainerNo", "ASC").ToList();
                    containers = converter.DB2DTO_BookingContainer(result.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
            }

            return containers;
        }

        public DTO.PriceListCICharge GetPricePerUnit(Hashtable filters, out Notification notification)
        {
            DTO.PriceListCICharge priceCICharge = new DTO.PriceListCICharge();
            notification = new Notification { Type = NotificationType.Success };

            try
            {
                using (var context = CreateContext())
                {
                    string chargeTypeID = null;
                    string clientID = null;
                    string polID = null;
                    string containerTypeID = null;
                    string etd = null;

                    if (filters.ContainsKey("typeCharge") && filters["typeCharge"] != null)
                        chargeTypeID = filters["typeCharge"].ToString().Replace("'", "''");
                    if (filters.ContainsKey("clientID") && filters["clientID"] != null)
                        clientID = filters["clientID"].ToString().Replace("'", "''");
                    if (filters.ContainsKey("polID") && filters["polID"] != null)
                        polID = filters["polID"].ToString().Replace("'", "''");
                    if (filters.ContainsKey("typeContainer") && filters["typeContainer"] != null)
                        containerTypeID = filters["typeContainer"].ToString().Replace("'", "''");
                    if (filters.ContainsKey("etd") && filters["etd"] != null)
                        etd = filters["etd"].ToString().Replace("'", "''");

                    var result = context.TransportCICharge_function_GetPricePerUnit(clientID, polID, etd, containerTypeID, chargeTypeID);
                    priceCICharge = converter.DB2DTO_PriceListCICharge(result.SingleOrDefault());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
            }
            return priceCICharge;
        }

        #endregion
    }
}

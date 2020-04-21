using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Library;
using Library.Base;
using Library.DTO;
using Module.Support.DTO;
using Module.TransportCostForwarder.DTO;
using Newtonsoft.Json.Linq;

namespace Module.TransportCostForwarder.DAL
{
    internal class DataFactory : DataFactory<SearchFormData, EditFormData>
    {
        private readonly Support.DAL.DataFactory _supportFactory = new Support.DAL.DataFactory();
        private readonly Framework.DAL.DataFactory _fwFactory = new Framework.DAL.DataFactory();
        private readonly DataConverter _converter = new DataConverter();

        private TransportCostForwarderModelContainer CreateContext()
        {
            return new TransportCostForwarderModelContainer(Helper.CreateEntityConnectionString("DAL.TransportCostForwarderModel"));
        }

        public override SearchFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            notification = new Notification { Type = NotificationType.Success };

            SearchFormData data = new SearchFormData();

            data.Data = new List<TransportCostForwarderSearchResult>();
            totalRows = 0;

            try
            {
                using (var context = CreateContext())
                {
                    string forwarderUD = (filters.ContainsKey("ForwarderUD") && !string.IsNullOrEmpty(filters["ForwarderUD"].ToString())) ? filters["ForwarderUD"].ToString().Replace("'", "''") : null;
                    string bookingUD = (filters.ContainsKey("BookingUD") && filters["BookingUD"] != null) ? filters["BookingUD"].ToString().Replace("'", "''") : null;
                    string transportInvoiceNumber = (filters.ContainsKey("TransportInvoiceNumber") && filters["TransportInvoiceNumber"] != null) ? filters["TransportInvoiceNumber"].ToString().Replace("'", "''") : null;

                    totalRows = context.TransportCostForwarder_function__TransportCostForwarderSearchResult(forwarderUD, bookingUD, transportInvoiceNumber, orderBy, orderDirection).Count();

                    var result = context.TransportCostForwarder_function__TransportCostForwarderSearchResult(forwarderUD, bookingUD, transportInvoiceNumber, orderBy, orderDirection);
                    data.Data = _converter.DB2DTO_TransportCostForwarderSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public EditFormData GetData(int iRequesterID, int id, out Notification notification)
        {
            notification = new Notification { Type = NotificationType.Success };
            var data = new EditFormData
            {
                Data = new DTO.TransportCostForwarder(),
                TypeOfCosts = new List<TypeOfCost>(),
                Containers = new List<BookingContainer>(),
                DropDownCurrency = new List<TypeOfCurrency>()
            };


            try
            {
                if (id > 0)
                {
                    using (var context = CreateContext())
                    {
                        var dbItem = context.TransportCostForwarder_TransportCostForwarder_View.SingleOrDefault(o => o.TransportCostForwarderID == id);
                        if (dbItem == null)
                        {
                            throw new Exception("Can not found the Transport Cost Forwarder to edit");
                        }

                        data.Data = _converter.DB2DTO_TransportCostForwarder(dbItem);
                    }

                    // Get list container number with BL number
                    Hashtable filters = new Hashtable();
                    filters["BLNo"] = data.Data.BLNo;
                    data.Containers = GetBookingContainer(filters, out notification);
                }
                data.TypeOfCosts = _supportFactory.GetTypeOfCosts().ToList();
                data.ContainerTypes = _supportFactory.GetContainerType().ToList();
                data.DropDownCurrency = _supportFactory.GetTypeOfCurrency().ToList();
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override bool DeleteData(int id, out Notification notification)
        {
            notification = new Notification { Type = NotificationType.Success };
            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.TransportCostForwarder.FirstOrDefault(o => o.TransportCostForwarderID == id);
                    if (dbItem == null) return true;

                    context.TransportCostForwarder.Remove(dbItem);
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

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            DTO.TransportCostForwarder dtoEditItem = ((JObject)dtoItem).ToObject<DTO.TransportCostForwarder>();
            notification = new Notification { Type = NotificationType.Success };

            try
            {
                using (var context = CreateContext())
                {
                    TransportCostForwarder dbItem;

                    if (id > 0)
                    {
                        dbItem = context.TransportCostForwarder.FirstOrDefault(o => o.TransportCostForwarderID == id);
                    }
                    else
                    {
                        dbItem = new TransportCostForwarder();

                        context.TransportCostForwarder.Add(dbItem);

                        dbItem.CreatedBy = userId;
                        dbItem.CreatedDate = DateTime.Now;
                    }

                    if (dbItem == null)
                    {
                        notification.Message = string.Format("TransportCostForwarder [id={0}] not found!", id);
                        return false;
                    }

                    _converter.DTO2DB_TransportCostForwarder(dtoEditItem, ref dbItem);

                    dbItem.UpdatedBy = userId;
                    dbItem.UpdatedDate = DateTime.Now;

                    // remove orphan items
                    context.TransportCostForwarderItem.Local.Where(o => o.TransportCostForwarder == null).ToList()
                        .ForEach(o => context.TransportCostForwarderItem.Remove(o));

                    context.SaveChanges();

                    dtoItem = GetData(userId, dbItem.TransportCostForwarderID, out notification).Data;

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

        public ForwarderList QuickSearchForwarder(Hashtable filters, out Notification notification)
        {
            var data = new ForwarderList { TotalRows = 0 };
            notification = new Notification { Type = NotificationType.Success };

            try
            {
                var totalRows = 0;

                data.Data = _supportFactory.QuickSearchForwarder(filters, ref totalRows, out notification).ToList();
                data.TotalRows = totalRows;

                return data;
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);

                return data;
            }
        }

        public override EditFormData GetData(int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public BookingList QuickSearchBooking(int userId, Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            BookingList booking = new BookingList();
            booking.Data = new List<BookingForwarder>();
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
                    string blNo = null;
                    string forwarderID = null;

                    if (filters.ContainsKey("searchQuery") && filters["searchQuery"] != null)
                    {
                        bookingUD = filters["searchQuery"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("forwarderID") && filters["forwarderID"] != null)
                    {
                        forwarderID = filters["forwarderID"].ToString().Replace("'", "''");
                    }

                    booking.TotalRows = context.Support_function_QuickSearchBookingForwarder(bookingUD, /*blNo,*/ forwarderID, /*userId,*/ sortedBy, sortedDirection).Count();
                    var result = context.Support_function_QuickSearchBookingForwarder(bookingUD, /*blNo,*/ forwarderID, /*userId,*/ sortedBy, sortedDirection);
                    booking.Data = _converter.DB2DTO_BookingForwader(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
            }

            return booking;
        }

        public List<BookingContainer> GetBookingContainer(Hashtable filters, out Notification notification)
        {
            notification = new Notification { Type = NotificationType.Success };

            List<BookingContainer> containers = new List<BookingContainer>();

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
                    containers = _converter.DB2DTO_BookingContainer(result.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
            }

            return containers;
        }

        public decimal GetPricePerUnit(Hashtable filters, out Notification notification)
        {
            // Declare price
            decimal price = 0;

            // Update notification
            notification = new Notification { Type = NotificationType.Success };

            try
            {
                using (var context = CreateContext())
                {
                    // Get all value to get price
                    string typeCostID = null;
                    string forwarderID = null;
                    string polID = null;
                    string typeContainerID = null;
                    string etd = null;

                    // Update value
                    if (filters.ContainsKey("TypeCost") && filters["TypeCost"] != null)
                        typeCostID = filters["TypeCost"].ToString().Replace("'", "''");
                    if (filters.ContainsKey("Forwarder") && filters["Forwarder"] != null)
                        forwarderID = filters["Forwarder"].ToString().Replace("'", "''");
                    if (filters.ContainsKey("POL") && filters["POL"] != null)
                        polID = filters["POL"].ToString().Replace("'", "''");
                    if (filters.ContainsKey("TypeContainer") && filters["TypeContainer"] != null)
                        typeContainerID = filters["TypeContainer"].ToString().Replace("'", "''");
                    if (filters.ContainsKey("ETD") && filters["ETD"] != null)
                        etd = filters["ETD"].ToString().Replace("'", "''");

                    // Get price per unit
                    var result = context.TransportCostForwarder_function_GetPrice(etd,forwarderID,polID,typeContainerID,typeCostID).ToList();
                    if (result.Count == 1)
                        price = (result[0].PricePerUnit.HasValue) ? result[0].PricePerUnit.Value : 0;
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
            }

            return price;
        }
    }
}

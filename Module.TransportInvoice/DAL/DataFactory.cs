using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Library.DTO;
using Module.TransportInvoice.DTO;

namespace Module.TransportInvoice.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private DataConverter converter = new DataConverter();
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        
        public DataFactory() { }

        private TransportInvoiceEntities CreateContext()
        {
            return new TransportInvoiceEntities(Library.Helper.CreateEntityConnectionString("DAL.TransportInvoiceModel"));
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new Exception();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (TransportInvoiceEntities context = CreateContext())
                {
                    var dbItem = context.TransportInvoice.Where(o => o.TransportInvoiceID == id).FirstOrDefault();
                    context.TransportInvoice.Remove(dbItem);
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

        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            throw new Exception();
        }

        public override DTO.SearchFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.SearchFormData searchFormData = new DTO.SearchFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            try
            {
                string invoiceNo = null;
	            string blNo = null;
                string forwarderNM = null;
                string currency = null;
                string refNo = null;
                string invoiceStatusText = null;
                string statustorName = null;
                string statusUpdatedDate = null;
                string creatorName = null;
                string createdDate = null;
                string updatorName = null;
                string updatedDate = null;

                if (filters.ContainsKey("invoiceNo") && !string.IsNullOrEmpty(filters["invoiceNo"].ToString()))
                {
                    invoiceNo = filters["invoiceNo"].ToString().Replace("'", "''");
                }

                if (filters.ContainsKey("blNo") && !string.IsNullOrEmpty(filters["blNo"].ToString()))
                {
                    blNo = filters["blNo"].ToString().Replace("'", "''");
                }

                if (filters.ContainsKey("forwarderNM") && !string.IsNullOrEmpty(filters["forwarderNM"].ToString()))
                {
                    forwarderNM = filters["forwarderNM"].ToString().Replace("'", "''");
                }

                if (filters.ContainsKey("currency") && !string.IsNullOrEmpty(filters["currency"].ToString()))
                {
                    currency = filters["currency"].ToString().Replace("'", "''");
                }

                if (filters.ContainsKey("refNo") && !string.IsNullOrEmpty(filters["refNo"].ToString()))
                {
                    refNo = filters["refNo"].ToString().Replace("'", "''");
                }

                if (filters.ContainsKey("invoiceStatusText") && !string.IsNullOrEmpty(filters["invoiceStatusText"].ToString()))
                {
                    invoiceStatusText = filters["invoiceStatusText"].ToString().Replace("'", "''");
                }

                if (filters.ContainsKey("statustorName") && !string.IsNullOrEmpty(filters["statustorName"].ToString()))
                {
                    statustorName = filters["statustorName"].ToString().Replace("'", "''");
                }

                if (filters.ContainsKey("statusUpdatedDate") && !string.IsNullOrEmpty(filters["statusUpdatedDate"].ToString()))
                {
                    statusUpdatedDate = filters["statusUpdatedDate"].ToString().Replace("'", "''");
                }

                if (filters.ContainsKey("creatorName") && !string.IsNullOrEmpty(filters["creatorName"].ToString()))
                {
                    creatorName = filters["creatorName"].ToString().Replace("'", "''");
                }

                if (filters.ContainsKey("createdDate") && !string.IsNullOrEmpty(filters["createdDate"].ToString()))
                {
                    createdDate = filters["createdDate"].ToString().Replace("'", "''");
                }

                if (filters.ContainsKey("updatorName") && !string.IsNullOrEmpty(filters["updatorName"].ToString()))
                {
                    updatorName = filters["updatorName"].ToString().Replace("'", "''");
                }

                if (filters.ContainsKey("updatedDate") && !string.IsNullOrEmpty(filters["updatedDate"].ToString()))
                {
                    updatedDate = filters["updatedDate"].ToString().Replace("'", "''");
                }
                using (TransportInvoiceEntities context = CreateContext())
                {
                    totalRows = context.TransportInvoiceMng_function_SearchTransportInvoice(orderBy, orderDirection,invoiceNo,blNo,forwarderNM,currency,refNo,invoiceStatusText,statustorName,statusUpdatedDate,creatorName,createdDate,updatorName,updatedDate).Count();
                    var result = context.TransportInvoiceMng_function_SearchTransportInvoice(orderBy, orderDirection, invoiceNo, blNo, forwarderNM, currency, refNo, invoiceStatusText, statustorName, statusUpdatedDate, creatorName, createdDate, updatorName, updatedDate);
                    searchFormData.Data = converter.DB2DTO_TransportInvoiceSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
                searchFormData.Seasons = supportFactory.GetSeason();
                return searchFormData;
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
                return searchFormData;
            }
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.TransportInvoiceDTO dtoTransportInvoice = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.TransportInvoiceDTO>();
            try
            {
                using (TransportInvoiceEntities context = CreateContext())
                {
                    TransportInvoice dbItem = null;
                    if (id > 0)
                    {                        
                        dbItem = context.TransportInvoice.Where(o => o.TransportInvoiceID == id).FirstOrDefault();
                        dbItem.UpdatedBy = userId;
                        dbItem.UpdatedDate = DateTime.Now;
                    }
                    else
                    {
                        dbItem = new TransportInvoice();
                        dbItem.CreatedBy = userId;
                        dbItem.CreatedDate = DateTime.Now;
                        context.TransportInvoice.Add(dbItem);
                    }
                    if (dbItem == null)
                    {
                        notification.Message = "data not found!";
                        return false;
                    }
                    else
                    {
                        //convert dto to db
                        converter.DTO2DB_TransportInvoice(dtoTransportInvoice, ref dbItem);
                        //remove orphan
                        context.TransportInvoiceContainerDetail.Local.Where(o => o.TransportInvoiceDetail == null).ToList().ForEach(o => context.TransportInvoiceContainerDetail.Remove(o));
                        context.TransportInvoiceDetail.Local.Where(o => o.TransportInvoice == null).ToList().ForEach(o => context.TransportInvoiceDetail.Remove(o));
                        //save data
                        context.SaveChanges();
                        //get return data
                        dtoItem = GetData(userId, dbItem.TransportInvoiceID, dbItem.BookingID, out notification).Data;
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

        public EditFormData GetData(int userId, int id, int? bookingID, out Notification notification)
        {
            DTO.EditFormData editFormData = new DTO.EditFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (TransportInvoiceEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        TransportInvoiceMng_TransportInvoice_View dbItem;
                        dbItem = context.TransportInvoiceMng_TransportInvoice_View
                            .Include("TransportInvoiceMng_TransportInvoiceDetail_View.TransportInvoiceMng_TransportInvoiceContainerDetail_View").FirstOrDefault(o => o.TransportInvoiceID == id);
                        editFormData.Data = converter.DB2DTO_TransportInvoice(dbItem);
                        editFormData.LoadingPlanDTOs = this.GetLoadingPlan(editFormData.Data.BookingID, out notification);
                        editFormData.InitCostItems = AutoMapper.Mapper.Map<List<TransportInvoiceMng_InitCostItem_View>, List<DTO.InitCostItemDTO>>(context.TransportInvoiceMng_action_GetInitCostItem(editFormData.Data.BookingID).ToList());
                    }
                    else
                    {
                        editFormData.Data = new DTO.TransportInvoiceDTO();
                        editFormData.Data.InvoiceStatusID = 1;
                        editFormData.Data.InvoiceStatusText = "PENDING";
                        editFormData.Data.TransportInvoiceDetailDTOs = new List<TransportInvoiceDetailDTO>();
                        editFormData.LoadingPlanDTOs = this.GetLoadingPlan(bookingID, out notification);
                        editFormData.InitCostItems = AutoMapper.Mapper.Map<List<TransportInvoiceMng_InitCostItem_View>, List<DTO.InitCostItemDTO>>(context.TransportInvoiceMng_action_GetInitCostItem(bookingID).ToList());

                        //init cost item
                        //TransportInvoiceDetailDTO costDetailDTO;
                        //TransportInvoiceContainerDetailDTO containerDetailDTO;
                        //int i = -1;
                        //int j = -1;
                        //var initCostItem = context.TransportInvoiceMng_action_GetInitCostItem(bookingID).ToList();
                        //foreach (var item in initCostItem)
                        //{
                        //    costDetailDTO = new TransportInvoiceDetailDTO();
                        //    editFormData.Data.TransportInvoiceDetailDTOs.Add(costDetailDTO);

                        //    costDetailDTO.TransportInvoiceDetailID = i;
                        //    costDetailDTO.TransportCostItemID = item.TransportCostItemID;
                        //    costDetailDTO.Currency = item.Currency;
                        //    //costDetailDTO.OfferPrice = item.OfferPrice;
                        //    costDetailDTO.TransportCostChargeTypeID = item.TransportCostChargeTypeID;
                        //    costDetailDTO.TransportInvoiceContainerDetailDTOs = new List<TransportInvoiceContainerDetailDTO>();

                        //    if (item.TransportCostChargeTypeID == 2) //charge per container
                        //    {
                        //        foreach (var loadingPlanItem in editFormData.LoadingPlanDTOs)
                        //        {
                        //            containerDetailDTO = new TransportInvoiceContainerDetailDTO();
                        //            costDetailDTO.TransportInvoiceContainerDetailDTOs.Add(containerDetailDTO);

                        //            containerDetailDTO.TransportInvoiceContainerDetailID = j;
                        //            containerDetailDTO.LoadingPlanID = loadingPlanItem.LoadingPlanID;
                        //            containerDetailDTO.ContainerNo = loadingPlanItem.ContainerNo;
                        //            containerDetailDTO.ContainerTypeNM = loadingPlanItem.ContainerTypeNM;
                        //            switch (loadingPlanItem.ContainerTypeID)
                        //            {
                        //                case 1:
                        //                    containerDetailDTO.OfferPrice = item.Cost20DC;
                        //                    containerDetailDTO.Amount = item.Cost20DC;
                        //                    break;
                        //                case 2:
                        //                    containerDetailDTO.OfferPrice = item.Cost40DC;
                        //                    containerDetailDTO.Amount = item.Cost40DC;
                        //                    break;
                        //                case 3:
                        //                    containerDetailDTO.OfferPrice = item.Cost40HC;
                        //                    containerDetailDTO.Amount = item.Cost40HC;
                        //                    break;
                        //            }
                        //            j--;
                        //        }
                        //    }
                        //    else if (item.TransportCostChargeTypeID == 1)//charge per BL
                        //    {
                        //        costDetailDTO.OfferPrice = item.Cost40HC;
                        //    }

                        //    i--;
                        //}

                    }
                    editFormData.TransportCostItemDTOs = this.GetTransportCostItem(out notification);
                    editFormData.Currencies = supportFactory.GetCurrency();
                    editFormData.TransportCostTypes = supportFactory.GetTransportCostType();
                    editFormData.TransportCostChargeTypes = supportFactory.GetTransportCostChargeType();
                    return editFormData;
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
                return editFormData;
            }
        }

        public List<DTO.LoadingPlanDTO> GetLoadingPlan(int? bookingID, out Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (TransportInvoiceEntities context = CreateContext())
                {
                    var x = context.TransportInvoiceMng_LoadingPlan_View.Where(o =>o.BookingID==bookingID).ToList();
                    return AutoMapper.Mapper.Map<List<TransportInvoiceMng_LoadingPlan_View>, List<DTO.LoadingPlanDTO>>(x);
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
                return new List<LoadingPlanDTO>();
            }
        }

        public List<DTO.TransportCostItemDTO> GetTransportCostItem(out Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (TransportInvoiceEntities context = CreateContext())
                {
                    var x = context.TransportInvoiceMng_TransportCostItem_View.ToList();
                    return  AutoMapper.Mapper.Map<List<TransportInvoiceMng_TransportCostItem_View>, List<DTO.TransportCostItemDTO>>(x);
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
                return new List<TransportCostItemDTO>();
            }
        }

        public List<DTO.BookingDTO> GetBooking(Hashtable filters, out Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (TransportInvoiceEntities context = CreateContext())
                {
                    string searchQuery = filters["searchQuery"].ToString();
                    string season = filters["season"].ToString();
                    var x = context.TransportInvoiceMng_Booking_View.Where(o=>o.Season==season && o.BLNo.Contains(searchQuery)).ToList();
                    return AutoMapper.Mapper.Map<List<TransportInvoiceMng_Booking_View>, List<DTO.BookingDTO>>(x);
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
                return new List<BookingDTO>();
            }
        }

        public bool SetInvoiceStatus(int userID,int statusID, int transportInvoiceID, out Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success, Message="Set status success"};
            try
            {
                using (TransportInvoiceEntities context = CreateContext())
                {
                    if (transportInvoiceID <= 0)
                    {
                        throw new Exception("You have to save data before set status");
                    }
                    var x = context.TransportInvoice.Where(o => o.TransportInvoiceID == transportInvoiceID).FirstOrDefault();
                    x.InvoiceStatusID = statusID;
                    x.StatusUpdatedBy = userID;
                    x.StatusUpdatedDate = DateTime.Now;
                    context.SaveChanges();
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
    }
}

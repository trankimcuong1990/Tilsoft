using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OfferSeasonQuotationRequestMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Module.Support.DAL.DataFactory supportFactory = new Module.Support.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private OfferSeasonQuotationRequestMngEntities CreateContext()
        {
            return new OfferSeasonQuotationRequestMngEntities(Library.Helper.CreateEntityConnectionString("DAL.OfferSeasonQuotationRequestMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.OfferSeasonQuotationRequestSearchResultDTO>();
            totalRows = 0;

            string ClientUD = null;
            string Season = null;
            string Description = null;
            string OfferSeasonUD = null;
            bool? HasFactoryQuotation = null;
            int? DeltaRate = null;

            if (filters.ContainsKey("ClientUD") && !string.IsNullOrEmpty(filters["ClientUD"].ToString()))
            {
                ClientUD = filters["ClientUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("Season") && filters["Season"] != null && !string.IsNullOrEmpty(filters["Season"].ToString()))
            {
                Season = filters["Season"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("Description") && !string.IsNullOrEmpty(filters["Description"].ToString()))
            {
                Description = filters["Description"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("OfferSeasonUD") && !string.IsNullOrEmpty(filters["OfferSeasonUD"].ToString()))
            {
                OfferSeasonUD = filters["OfferSeasonUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("HasFactoryQuotation") && filters["HasFactoryQuotation"] != null && !string.IsNullOrEmpty(filters["HasFactoryQuotation"].ToString()))
            {
                HasFactoryQuotation = Library.Helper.ConvertStringToBool(filters["HasFactoryQuotation"].ToString());
            }
            if (filters.ContainsKey("DeltaRate") && filters["DeltaRate"] != null && !string.IsNullOrEmpty(filters["DeltaRate"].ToString()))
            {
                DeltaRate = Library.Helper.ConvertStringToInt(filters["DeltaRate"].ToString());
            }
            //DeltaRate
            //try to get data
            try
            {
                using (OfferSeasonQuotationRequestMngEntities context = CreateContext())
                {
                    totalRows = context.OfferSeasonQuotatonRequestMng_function_SearchQuotationRequest(ClientUD, Season, Description, OfferSeasonUD, HasFactoryQuotation, DeltaRate, orderBy, orderDirection).Count();
                    var result = context.OfferSeasonQuotatonRequestMng_function_SearchQuotationRequest(ClientUD, Season, Description, OfferSeasonUD, HasFactoryQuotation, DeltaRate, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_OfferSeasonQuotationRequestSearchResult(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());

                    var IDs = data.Data.Select(o => o.OfferSeasonQuotationRequestID).ToList();
                    data.FactoryQuotationSearchResultDTOs = converter.DB2DTO_FactoryQuotationSearchResult(context.OfferSeasonQuotatonRequestMng_FactoryQuotationSearchResult_View.Where(o => IDs.Contains(o.OfferSeasonQuotationRequestID.Value)).ToList());

                    var DetailIDs = data.FactoryQuotationSearchResultDTOs.Select(o => o.OfferSeasonQuotationRequestID).ToList();
                    data.PreferedFactoryDTOs = converter.DB2DTO_PreferedFactory(context.OfferSeasonQuotatonRequestMng_PreferedFactory_View.Where(o => DetailIDs.Contains(o.OfferSeasonQuotationRequestID)).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }

            return data;
        }

        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //DTO.EditFormData data = new DTO.EditFormData();
            //data.Data = new List<DTO.OfferSeasonQuotationRequestDetailDTO>();

            ////try to get data
            //try
            //{
            //    using (OfferSeasonQuotationRequestMngEntities context = CreateContext())
            //    {
            //        data.Data = converter.DB2DTO_OfferSeasonQuotationRequestDetail(context.OfferSeasonQuotatonRequestMng_OfferSeasonQuotationRequestDetail_View.Where(o => o.OfferSeasonQuotationRequestID == id).ToList());
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification.Type = Library.DTO.NotificationType.Error;
            //    notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            //}

            //return data;
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            List<int> dtoFactories = ((Newtonsoft.Json.Linq.JArray)dtoItem).ToObject<List<int>>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (OfferSeasonQuotationRequestMngEntities context = CreateContext())
                {
                    OfferSeasonQuotationRequest dbItem = null;
                    if (id == 0)
                    {
                        throw new Exception("Invalid request!");
                    }
                    else
                    {
                        dbItem = context.OfferSeasonQuotationRequest.FirstOrDefault(o => o.OfferSeasonQuotationRequestID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Request not found!";
                        return false;
                    }
                    else
                    {
                        //converter.DTO2DB(dtoDetails, ref dbItem, userId);
                        foreach (int factoryId in dtoFactories)
                        {
                            dbItem.OfferSeasonQuotationRequestDetail.Add(new OfferSeasonQuotationRequestDetail { UpdatedBy = userId, UpdatedDate = DateTime.Now, FactoryID = factoryId });                            
                        }
                        context.SaveChanges();
                        context.OfferSeasonQuotatonRequestMng_function_InsertIntoQuotationDetail(id);
                        context.SaveChanges();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
                return false;
            }
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();

            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {
            //        SampleTechnicalDrawing dbItem = context.SampleTechnicalDrawing.FirstOrDefault(o => o.SampleTechnicalDrawingID == id);
            //        if (dbItem == null)
            //        {
            //            notification.Message = "Sample Technical Drawing not found!";
            //            return false;
            //        }
            //        else
            //        {
            //            dbItem.IsConfirmed = true;
            //            dbItem.ConfirmedBy = userId;
            //            dbItem.ConfirmedDate = DateTime.Now;
            //            context.SaveChanges();
            //            return true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
            //    return false;
            //}
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();

            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {
            //        SampleTechnicalDrawing dbItem = context.SampleTechnicalDrawing.FirstOrDefault(o => o.SampleTechnicalDrawingID == id);
            //        if (dbItem == null)
            //        {
            //            notification.Message = "Sample Technical Drawing not found!";
            //            return false;
            //        }
            //        else
            //        {
            //            dbItem.IsConfirmed = false;
            //            dbItem.ConfirmedBy = null;
            //            dbItem.ConfirmedDate = null;
            //            context.SaveChanges();
            //            return true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
            //    return false;
            //}
        }

        //
        // CUSTOM FUNCTION HERE
        //

        public DTO.SupportFormData GetInitData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            DTO.SupportFormData data = new DTO.SupportFormData();
            try
            {
                using (OfferSeasonQuotationRequestMngEntities context = CreateContext())
                {
                    data.FactoryDTOs = converter.DB2DTO_Factory(context.OfferSeasonQuotatonRequestMng_Factory_View.ToList());
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

using Library.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Library;

namespace Module.OfferSeasonMng.DAL
{
    internal partial class DataFactory : Library.Base.DataFactory2<DTO.SearchFormData, DTO.EditFormData>
    {
        private Module.Support.DAL.DataFactory supportFactory = new Module.Support.DAL.DataFactory();
        Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();
        private OfferSeasonMngEntities CreateContext()
        {
            return new OfferSeasonMngEntities(Library.Helper.CreateEntityConnectionString("DAL.OfferSeasonMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.SearchFormData searchFormData = new DTO.SearchFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            try
            {
                string season = null;
                string clientUD = null;
                string clientNM = null;
                int? offerSeasonTypeID = null;
                string offerSeasonUD = null;
                int? saleID = null;

                if (filters.ContainsKey("season") && filters["season"] != null && !string.IsNullOrEmpty(filters["season"].ToString()))
                {
                    season = filters["season"].ToString().Replace("'", "''");
                }

                if (filters.ContainsKey("clientUD") && !string.IsNullOrEmpty(filters["clientUD"].ToString()))
                {
                    clientUD = filters["clientUD"].ToString().Replace("'", "''");
                }

                if (filters.ContainsKey("clientNM") && !string.IsNullOrEmpty(filters["clientNM"].ToString()))
                {
                    clientNM = filters["clientNM"].ToString().Replace("'", "''");
                }

                if (filters.ContainsKey("offerSeasonTypeID") && filters["offerSeasonTypeID"] != null)
                {
                    offerSeasonTypeID = Convert.ToInt32(filters["offerSeasonTypeID"]);
                }

                if (filters.ContainsKey("offerSeasonUD") && !string.IsNullOrEmpty(filters["offerSeasonUD"].ToString()))
                {
                    offerSeasonUD = filters["offerSeasonUD"].ToString().Replace("'", "''");
                }

                if (filters.ContainsKey("saleID") && filters["saleID"] != null)
                {
                    saleID = Convert.ToInt32(filters["saleID"]);
                }

                using (OfferSeasonMngEntities context = CreateContext())
                {
                    totalRows = context.OfferSeasonMng_function_SearchOfferSeason(orderBy, orderDirection, season, clientUD, clientNM, offerSeasonTypeID, offerSeasonUD, saleID).Count();
                    var result = context.OfferSeasonMng_function_SearchOfferSeason(orderBy, orderDirection, season, clientUD, clientNM, offerSeasonTypeID, offerSeasonUD, saleID);
                    searchFormData.Data = converter.DB2DTO_Search(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());

                    //get support list
                    searchFormData.OfferSeasonTypeDTOs = this.GetOfferSeasonType(out notification);
                    searchFormData.AccManagerDTOs = AutoMapper.Mapper.Map<List<OfferSeasonMng_ActiveSales_View>, List<DTO.AccManagerDTO>>(context.OfferSeasonMng_ActiveSales_View.ToList());
                                     
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return searchFormData;
        }
        public override DTO.EditFormData GetData(int userId, int id, Hashtable param, out Library.DTO.Notification notification)
        {            
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData editFormData = new DTO.EditFormData();
            Module.Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
            
            try
            {
                using (OfferSeasonMngEntities context = CreateContext())
                {
                    //auto update item status to 3 (Quotation has approved  price)
                    context.OfferSeasonMng_function_QuotationApprovedPrice(id);

                    //get data
                    if (id > 0)
                    {
                        //get offer season
                        var dbItem = context.OfferSeasonMng_OfferSeason_View.Where(o => o.OfferSeasonID == id).FirstOrDefault();
                        editFormData.OfferSeasonDTO = converter.DB2DTO_OfferSeason(dbItem);

                        //get offer season detail
                        var dbOfferSeasonDetail = context.OfferSeasonMng_OfferSeasonDetail_View.Where(o => o.OfferSeasonID == id);
                        editFormData.OfferSeasonDetailDTOs = converter.DB2DTO_OfferSeasonDetail(dbOfferSeasonDetail.ToList());                        
                    }
                    else
                    {
                        int offerSeasonTypeID = Convert.ToInt32(param["offerSeasonTypeID"]);
                        if (offerSeasonTypeID > 0)
                        {
                            editFormData.OfferSeasonDTO.OfferSeasonTypeID = offerSeasonTypeID;
                            editFormData.OfferSeasonDTO.OfferSeasonTypeNM = this.GetOfferSeasonType(out notification).Where(o => o.OfferSeasonTypeID == offerSeasonTypeID).FirstOrDefault().OfferSeasonTypeNM;

                            switch (offerSeasonTypeID) {
                                case 2:
                                case 4:
                                case 6:
                                    editFormData.OfferSeasonDTO.Currency = "USD";
                                    break;
                                case 3:
                                case 5:
                                case 7:
                                case 1:
                                    editFormData.OfferSeasonDTO.Currency = "EUR";
                                    break;
                            }
                        }
                        //initialize                        
                        editFormData.OfferSeasonDTO.Season = Library.Helper.GetCurrentSeason();                  
                    }

                    //
                    //support list
                    //
                    switch (editFormData.OfferSeasonDTO.OfferSeasonTypeID)
                    {
                        case 2:
                        case 3:
                        case 6:
                        case 7:
                            //esimated cost by client
                            var estimateCost = context.OfferSeasonMng_ClientEstimatedAdditionalCost_View.Where(o => o.ClientID == editFormData.OfferSeasonDTO.ClientID);
                            editFormData.ClientEstimatedAdditionalCostDTOs = AutoMapper.Mapper.Map<List<OfferSeasonMng_ClientEstimatedAdditionalCost_View>, List<DTO.ClientEstimatedAdditionalCostDTO>>(estimateCost.ToList());
                            
                            //planning purchasing price source
                            editFormData.PlaningPurchasingPriceSourceDTOs = AutoMapper.Mapper.Map<List<SupportMng_PlaningPurchasingPriceSource_View>, List<DTO.PlaningPurchasingPriceSourceDTO>>(context.SupportMng_PlaningPurchasingPriceSource_View.ToList());

                            //factory
                            editFormData.FactoryDTOs = AutoMapper.Mapper.Map<List<OfferSeasonMng_Factory_View>, List<DTO.FactoryDTO>>(context.OfferSeasonMng_function_GetFactory(userId).ToList());

                            //offerseason status
                            editFormData.OfferSeasonItemStatusDTOs = AutoMapper.Mapper.Map<List<SupportMng_OfferSeasonItemStatus_View>, List<DTO.OfferSeasonItemStatusDTO>>(context.SupportMng_OfferSeasonItemStatus_View.ToList());

                            //product element
                            editFormData.ProductElementDTOs = AutoMapper.Mapper.Map<List<SupportMng_ProductElement_View>, List<DTO.ProductElementDTO>>(context.SupportMng_ProductElement_View.ToList());
                            break;                        
                    }
                    
                    editFormData.Seasons = supportFactory.GetSeason();
                    editFormData.VAT = this.GetVAT();
                    editFormData.Currencies = this.GetCurrency();
                    editFormData.MasterSettingDTO = AutoMapper.Mapper.Map<OfferSeasonMng_function_GetMasterSetting_Result, DTO.MasterSettingDTO>(context.OfferSeasonMng_function_GetMasterSetting(editFormData.OfferSeasonDTO.Season).FirstOrDefault());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return editFormData;
        }
        public override bool DeleteData(int userID, int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }
        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.OfferSeasonDTO dtoOfferSeason = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.OfferSeasonDTO>();
            try
            {
                using (OfferSeasonMngEntities context = CreateContext())
                {
                    OfferSeason dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new OfferSeason();
                        context.OfferSeason.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.OfferSeason.Where(o => o.OfferSeasonID == id).FirstOrDefault();
                    }
                    if (dbItem == null)
                    {
                        notification.Message = "data not found!";
                        return false;
                    }
                    else
                    {
                        if (!dtoOfferSeason.OfferSeasonTypeID.HasValue)
                        {
                            throw new Exception("Please select type of offer");
                        }
                        if(dtoOfferSeason.OfferSeasonID == 0)
                        {
                            dtoOfferSeason.OfferSeasonUD = context.OfferSeasonMng_function_GenerateOfferSeasonCode(dtoOfferSeason.OfferSeasonID, dtoOfferSeason.Season, dtoOfferSeason.ClientID).FirstOrDefault();
                        }

                        //convert to db
                        converter.DTO2DB_OfferSeason(userId, dtoOfferSeason, ref dbItem);

                        //save data
                        context.SaveChanges();

                        //get return data
                        dtoItem = GetData(userId, dbItem.OfferSeasonID, null, out notification).OfferSeasonDTO;
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
        }
        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }
        public override object GetInitData(int userId, out Notification notification)
        {
            throw new NotImplementedException();
        }
        public override object GetSearchFilter(int userId, out Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}

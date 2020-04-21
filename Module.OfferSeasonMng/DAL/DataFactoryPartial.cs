using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Module.OfferSeasonMng.DAL
{
    internal partial class DataFactory
    {
        public List<DTO.OfferSeasonTypeDTO> GetOfferSeasonType(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.OfferSeasonTypeDTO> data = new List<DTO.OfferSeasonTypeDTO>();
            try
            {
                using (OfferSeasonMngEntities context = CreateContext())
                {
                    var item = context.SupportMng_OfferSeasonType_View.ToList();
                    data = AutoMapper.Mapper.Map<List<SupportMng_OfferSeasonType_View>, List<DTO.OfferSeasonTypeDTO>>(item);
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return data;
        }

        public DTO.OfferSeasonDetailDTO GetOfferSeasonDetail(int offerSeasonDetailID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.OfferSeasonDetailDTO dtoItem = new DTO.OfferSeasonDetailDTO();
            try
            {
                using (OfferSeasonMngEntities context = CreateContext())
                {
                    if (offerSeasonDetailID > 0)
                    {
                        var dbItem = context.OfferSeasonMng_OfferSeasonDetail_View.Where(o => o.OfferSeasonDetailID == offerSeasonDetailID).FirstOrDefault();
                        dtoItem = AutoMapper.Mapper.Map<OfferSeasonMng_OfferSeasonDetail_View, DTO.OfferSeasonDetailDTO>(dbItem);
                    }                    
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return dtoItem;
        }

        public List<DTO.OfferSeasonDetailDTO> UpdateOfferSeasonDetail2(int userId, int offerSeasonID, object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.OfferSeasonDetailDTO> dtoOfferSeasonDetails = ((Newtonsoft.Json.Linq.JArray)dtoItem).ToObject<List<DTO.OfferSeasonDetailDTO>>();
            List<DTO.OfferSeasonDetailDTO> updatedReturnData = new List<DTO.OfferSeasonDetailDTO>();
            try
            {
                using (OfferSeasonMngEntities context = CreateContext())
                {
                    foreach (var item in dtoOfferSeasonDetails)
                    {
                        DTO.OfferSeasonDetailDTO detailDTO = this.UpdateOfferSeasonDetail(userId, offerSeasonID, item.OfferSeasonDetailID, Newtonsoft.Json.Linq.JObject.FromObject(item), out notification);
                        detailDTO.RowID = item.RowID; //asign rowID after get from db, so we can update back to frontend without reload page
                        if (notification.Type != Library.DTO.NotificationType.Error)
                        {
                            updatedReturnData.Add(detailDTO);
                        }
                    }                                      
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);                
            }
            return updatedReturnData;
        }

        public DTO.OfferSeasonDetailDTO UpdateOfferSeasonDetail(int userId, int offerSeasonID, int offerSeasonDetailID, object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.OfferSeasonDetailDTO dtoOfferSeasonDetail = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.OfferSeasonDetailDTO>();
            try
            {
                using (OfferSeasonMngEntities context = CreateContext())
                {
                    //find offer season to attach offerSeasonDetail
                    OfferSeason dbOfferSeason = context.OfferSeason.Where(o => o.OfferSeasonID == offerSeasonID).FirstOrDefault();
                    if (dbOfferSeason == null)
                    {
                        throw new Exception("Could not find offer season");
                    }

                    //offer seasons detail
                    OfferSeasonDetail dbItem = null;
                    bool isAllowEditProperties = true;
                    if (offerSeasonDetailID > 0)
                    {                       
                        //get item to update
                        dbItem = context.OfferSeasonDetail.Where(o => o.OfferSeasonDetailID == offerSeasonDetailID).FirstOrDefault();

                        //check item is in factory order so we can allow edit property of item
                        bool isInFactoryOrder = context.OfferSeasonMng_function_CheckOfferItemIsInFactoryOrder(offerSeasonDetailID).FirstOrDefault().Value > 0;
                        isAllowEditProperties = !isInFactoryOrder;

                        //get admin permission
                        Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
                        if (fwFactory.HasSpecialPermission(userId, Module.Framework.ConstantIdentifier.SPECIAL_PERMISSION_CHANGE_OFFER_ITEM_OPTION))
                        {
                            isAllowEditProperties = true;
                        }
                    }
                    else
                    {
                        //create new OfferSeasonDetail
                        dbItem = new OfferSeasonDetail();
                        dbOfferSeason.OfferSeasonDetail.Add(dbItem);
                    }
                    if (dbItem == null)
                    {
                        throw new Exception("data not found offer season item!");                  
                    }
                    else
                    {                        
                        //convert dto 2 db
                        converter.DTO2DB_OfferSeasonDetail(userId, dbOfferSeason.OfferSeasonTypeID, offerSeasonDetailID, dtoOfferSeasonDetail, isAllowEditProperties, ref dbItem);
                        if (dbItem.IsPlaningPurchasingPriceSelected.HasValue && dbItem.IsPlaningPurchasingPriceSelected.Value && dbItem.PlaningPurchasingPriceSelectedBy == null)
                        {
                            dbItem.PlaningPurchasingPriceSelectedBy = userId;
                            dbItem.PlaningPurchasingPriceSelectedDate = DateTime.Now;
                        }

                        //save data
                        context.SaveChanges();
                                              
                        //auto make quotation request to quotation
                        //context.OfferSeasonQuotatonRequestMng_function_AddOfferSeasonItemToQuotation(dbItem.OfferSeasonDetailID, userId);
                        // disabled, using trigger instead

                        //auto reset quotation status if change property of item
                        if (dtoOfferSeasonDetail.IsChangedProperties.HasValue && dtoOfferSeasonDetail.IsChangedProperties.Value && isAllowEditProperties)
                        {
                            context.OfferSeasonMng_function_ResetStatusOfQuotation(dbItem.OfferSeasonDetailID);
                        }

                        //get return data
                        dtoOfferSeasonDetail = GetOfferSeasonDetail(dbItem.OfferSeasonDetailID, out notification);

                        return dtoOfferSeasonDetail;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
                return null;
            }            
        }
       
        public DTO.ModelSearchFormData SearchModel(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.ModelSearchFormData data = new DTO.ModelSearchFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            try
            {
                string modelUD = null;
                string modelNM = null;
                string season = null;
                string rangeName = null;
                string collection = null;

                if (filters.ContainsKey("modelUD") && !string.IsNullOrEmpty(filters["modelUD"].ToString()))
                {
                    modelUD = filters["modelUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("modelNM") && !string.IsNullOrEmpty(filters["modelNM"].ToString()))
                {
                    modelNM = filters["modelNM"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("season") && !string.IsNullOrEmpty(filters["season"].ToString()))
                {
                    season = filters["season"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("rangeName") && !string.IsNullOrEmpty(filters["rangeName"].ToString()))
                {
                    rangeName = filters["rangeName"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("collection") && !string.IsNullOrEmpty(filters["collection"].ToString()))
                {
                    collection = filters["collection"].ToString().Replace("'", "''");
                }
                using (OfferSeasonMngEntities context = CreateContext())
                {
                    totalRows = context.OfferSeasonMng_function_SearchModel(orderBy, orderDirection, modelUD, modelNM, season, rangeName, collection).Count();
                    var result = context.OfferSeasonMng_function_SearchModel(orderBy, orderDirection, modelUD, modelNM, season, rangeName, collection);
                    data.Data = converter.DB2DTO_SearchModel(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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

        public DTO.ProductSearchFormData SearchProduct(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.ProductSearchFormData data = new DTO.ProductSearchFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            try
            {
                string modelUD = null;
                string modelNM = null;
                string season = null;
                string articleCode = null;
                string description = null;

                if (filters.ContainsKey("modelUD") && !string.IsNullOrEmpty(filters["modelUD"].ToString()))
                {
                    modelUD = filters["modelUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("modelNM") && !string.IsNullOrEmpty(filters["modelNM"].ToString()))
                {
                    modelNM = filters["modelNM"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("season") && !string.IsNullOrEmpty(filters["season"].ToString()))
                {
                    season = filters["season"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("articleCode") && !string.IsNullOrEmpty(filters["articleCode"].ToString()))
                {
                    articleCode = filters["articleCode"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("description") && !string.IsNullOrEmpty(filters["description"].ToString()))
                {
                    description = filters["description"].ToString().Replace("'", "''");
                }
                using (OfferSeasonMngEntities context = CreateContext())
                {
                    totalRows = context.OfferSeasonMng_function_SearchProduct(orderBy, orderDirection, modelUD, modelNM, season, articleCode, description).Count();
                    var result = context.OfferSeasonMng_function_SearchProduct(orderBy, orderDirection, modelUD, modelNM, season, articleCode, description);
                    data.Data = converter.DB2DTO_SearchProduct(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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

        public DTO.SparepartSearchFormData SearchSparepart(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.SparepartSearchFormData data = new DTO.SparepartSearchFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            try
            {
                string modelUD = null;
                string modelNM = null;
                string season = null;
                string articleCode = null;
                string description = null;

                if (filters.ContainsKey("modelUD") && !string.IsNullOrEmpty(filters["modelUD"].ToString()))
                {
                    modelUD = filters["modelUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("modelNM") && !string.IsNullOrEmpty(filters["modelNM"].ToString()))
                {
                    modelNM = filters["modelNM"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("season") && !string.IsNullOrEmpty(filters["season"].ToString()))
                {
                    season = filters["season"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("articleCode") && !string.IsNullOrEmpty(filters["articleCode"].ToString()))
                {
                    articleCode = filters["articleCode"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("description") && !string.IsNullOrEmpty(filters["description"].ToString()))
                {
                    description = filters["description"].ToString().Replace("'", "''");
                }
                using (OfferSeasonMngEntities context = CreateContext())
                {
                    totalRows = context.OfferSeasonMng_function_SearchSparepart(orderBy, orderDirection, modelUD, modelNM, season, articleCode, description).Count();
                    var result = context.OfferSeasonMng_function_SearchSparepart(orderBy, orderDirection, modelUD, modelNM, season, articleCode, description);
                    data.Data = converter.DB2DTO_SearchSparepart(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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

        public DTO.OfferItemProperiesDTO GetOfferItemProperties(System.Collections.Hashtable param ,out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.OfferItemProperiesDTO data = new DTO.OfferItemProperiesDTO();
            try
            {
                int? ModelID= Convert.ToInt32(param["modelID"]);
                int? MaterialID= Convert.ToInt32(param["materialID"]);
                int? MaterialTypeID= Convert.ToInt32(param["materialTypeID"]);
                int? MaterialColorID= Convert.ToInt32(param["materialColorID"]);
                int? FrameMaterialID= Convert.ToInt32(param["frameMaterialID"]);
                int? FrameMaterialColorID= Convert.ToInt32(param["frameMaterialColorID"]);
                int? CushionColorID= Convert.ToInt32(param["cushionColorID"]);
                int? SubMaterialID= Convert.ToInt32(param["subMaterialID"]);
                int? SubMaterialColorID= Convert.ToInt32(param["subMaterialColorID"]);
                int? BackCushionID= Convert.ToInt32(param["backCushionID"]);
                int? SeatCushionID= Convert.ToInt32(param["seatCushionID"]);
                int? FSCTypeID= Convert.ToInt32(param["fscTypeID"]);
                int? FSCPercent= Convert.ToInt32(param["fscPercent"]);
                using (OfferSeasonMngEntities context = CreateContext())
                {
                    var itemProperties = context.OfferSeasonMng_function_GetOfferItemProperies(ModelID, MaterialID, MaterialTypeID, MaterialColorID, FrameMaterialID, FrameMaterialColorID, CushionColorID, SubMaterialID, SubMaterialColorID, BackCushionID, SeatCushionID, FSCTypeID, FSCPercent).FirstOrDefault();
                    data = AutoMapper.Mapper.Map<OfferSeasonMng_OfferItemProperies_View, DTO.OfferItemProperiesDTO>(itemProperties);                    
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return data;
        }

        public DTO.OfferItemDefaultPropertiesDTO GetOfferItemDefaultProperties(int? modelID, string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.OfferItemDefaultPropertiesDTO data = new DTO.OfferItemDefaultPropertiesDTO();
            try
            {
                using (OfferSeasonMngEntities context = CreateContext())
                {
                    var item = context.OfferSeasonMng_function_GetOfferItemDefaultProperties(modelID, season).FirstOrDefault();
                    data = AutoMapper.Mapper.Map<OfferSeasonMng_function_GetOfferItemDefaultProperties_Result, DTO.OfferItemDefaultPropertiesDTO>(item);
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return data;
        }

        public List<DTO.ClientDTO> SearchClient(string searchQuery, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.ClientDTO> data = new List<DTO.ClientDTO>();
            try
            {
                using (OfferSeasonMngEntities context = CreateContext())
                {
                    var clients = context.OfferSeasonMng_Client_View.Where(o => o.ClientUD.Contains(searchQuery) || o.ClientNM.Contains(searchQuery));
                    data = AutoMapper.Mapper.Map<List<OfferSeasonMng_Client_View>, List<DTO.ClientDTO>>(clients.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return data;
        }

        private List<object> GetVAT()
        {
            List<object> vat = new List<object>();
            vat.Add(new { percent= 0, name = "0%"});
            vat.Add(new { percent = 21, name = "21%" });
            return vat;
        }

        private List<object> GetCurrency()
        {
            List<object> currency = new List<object>();
            currency.Add(new { id = "USD", name = "USD" });
            currency.Add(new { id = "EUR", name = "EUR" });
            return currency;
        }

        public List<DTO.ModelSparepartDTO> SearchModelSparepart(int modelID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.ModelSparepartDTO> data = new List<DTO.ModelSparepartDTO>();
            try
            {
                using (OfferSeasonMngEntities context = CreateContext())
                {
                    var modelSparepart = context.OfferSeasonMng_ModelSparepart_View.Where(o =>o.ModelID==modelID);
                    data = AutoMapper.Mapper.Map<List<OfferSeasonMng_ModelSparepart_View>,List<DTO.ModelSparepartDTO>>(modelSparepart.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return data;
        }

        public DTO.PlaningPurchasingPriceData GetPlanningPurchasingPrice(System.Collections.Hashtable param, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.PlaningPurchasingPriceData data = new DTO.PlaningPurchasingPriceData();
            data.PlanningPurchasingPriceDTOs = new List<DTO.PlanningPurchasingPriceDTO>();
            data.FactoryPendingPriceDTOs = new List<DTO.FactoryPendingPriceDTO>();
            try
            {
                int? ClientID = Library.Helper.ConvertStringToInt(param["clientID"].ToString());
                int? ModelID = Library.Helper.ConvertStringToInt(param["modelID"].ToString());
                int? MaterialID = Library.Helper.ConvertStringToInt(param["materialID"].ToString());
                int? MaterialTypeID = Library.Helper.ConvertStringToInt(param["materialTypeID"].ToString());
                int? MaterialColorID = Library.Helper.ConvertStringToInt(param["materialColorID"].ToString());
                int? FrameMaterialID = Library.Helper.ConvertStringToInt(param["frameMaterialID"].ToString());
                int? FrameMaterialColorID = Library.Helper.ConvertStringToInt(param["frameMaterialColorID"].ToString());
                int? SubMaterialID = Library.Helper.ConvertStringToInt(param["subMaterialID"].ToString());
                int? SubMaterialColorID = Library.Helper.ConvertStringToInt(param["subMaterialColorID"].ToString());
                int? BackCushionID = Library.Helper.ConvertStringToInt(param["backCushionID"].ToString());
                int? SeatCushionID = Library.Helper.ConvertStringToInt(param["seatCushionID"].ToString());
                int? CushionColorID = Library.Helper.ConvertStringToInt(param["cushionColorID"].ToString());
                int? FSCTypeID = Library.Helper.ConvertStringToInt(param["fscTypeID"].ToString());
                int? FSCPercent = Library.Helper.ConvertStringToInt(param["fscPercentID"].ToString());
                int? PackagingMethodID = param["packagingMethodID"] != null ? Library.Helper.ConvertStringToInt(param["packagingMethodID"].ToString()) : null;
                int? ClientSpecialPackagingMethodID = param["clientSpecialPackagingMethodID"] != null ? Library.Helper.ConvertStringToInt(param["clientSpecialPackagingMethodID"].ToString()) : null;
                string Season = param["season"].ToString();
                int? OfferSeasonDetailID = param["offerSeasonDetailID"] != null ? Library.Helper.ConvertStringToInt(param["offerSeasonDetailID"].ToString()) : null;

                using (OfferSeasonMngEntities context = CreateContext())
                {
                    var priceItem = context.OfferSeasonMng_function_GetPlaningPurchasingPrice(ClientID, ModelID, MaterialID, MaterialTypeID, MaterialColorID, FrameMaterialID, FrameMaterialColorID, SubMaterialID, SubMaterialColorID, BackCushionID, SeatCushionID, CushionColorID, FSCTypeID, FSCPercent, PackagingMethodID, ClientSpecialPackagingMethodID, Season, OfferSeasonDetailID).ToList();
                    if (OfferSeasonDetailID.HasValue)
                    {
                        var pendingPrices = context.OfferSeasonMng_function_GetItemPendingPrice(OfferSeasonDetailID.Value).ToList();
                        data.FactoryPendingPriceDTOs = AutoMapper.Mapper.Map<List<OfferSeasonMng_function_GetItemPendingPrice_Result>, List<DTO.FactoryPendingPriceDTO>>(pendingPrices);
                    }
                    data.PlanningPurchasingPriceDTOs = AutoMapper.Mapper.Map<List<OfferSeasonMng_PlanningPurchasingPrice_View>, List<DTO.PlanningPurchasingPriceDTO>>(priceItem);
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return data;
        }

        public bool DeleteOfferSeasonDetail(int offerSeasonDetailID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.OfferSeasonDetailDTO dtoItem = new DTO.OfferSeasonDetailDTO();
            try
            {
                using (OfferSeasonMngEntities context = CreateContext())
                {
                    if (offerSeasonDetailID > 0)
                    {
                        context.OfferSeasonMng_function_DeleteOfferSeasonDetail(offerSeasonDetailID);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return false;
        }

        public string ExportOfferSeasonDetailToExcel(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            OfferSeasonDataObject ds = new OfferSeasonDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("OfferSeasonMng_function_ExportDetailToExcel", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@OfferSeasonID", id);
                adap.Fill(ds.OfferSeasonMng_function_ExportDetailToExcel);
                return Library.Helper.CreateReportFileWithEPPlus(ds, "OfferSeasonDetail", new List<string> { "OfferSeasonMng_function_ExportDetailToExcel" });
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
                return string.Empty;
            }
        }

        public List<DTO.SalePriceTable> GetSalePriceTable(System.Collections.Hashtable param, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.SalePriceTable> data = new List<DTO.SalePriceTable>();
            try
            {
                int? ClientID = Library.Helper.ConvertStringToInt(param["clientID"].ToString());
                int? ModelID = Library.Helper.ConvertStringToInt(param["modelID"].ToString());
                int? MaterialID = Library.Helper.ConvertStringToInt(param["materialID"].ToString());
                int? MaterialTypeID = Library.Helper.ConvertStringToInt(param["materialTypeID"].ToString());
                int? MaterialColorID = Library.Helper.ConvertStringToInt(param["materialColorID"].ToString());
                int? FrameMaterialID = Library.Helper.ConvertStringToInt(param["frameMaterialID"].ToString());
                int? FrameMaterialColorID = Library.Helper.ConvertStringToInt(param["frameMaterialColorID"].ToString());
                int? SubMaterialID = Library.Helper.ConvertStringToInt(param["subMaterialID"].ToString());
                int? SubMaterialColorID = Library.Helper.ConvertStringToInt(param["subMaterialColorID"].ToString());
                int? BackCushionID = Library.Helper.ConvertStringToInt(param["backCushionID"].ToString());
                int? SeatCushionID = Library.Helper.ConvertStringToInt(param["seatCushionID"].ToString());
                int? CushionColorID = Library.Helper.ConvertStringToInt(param["cushionColorID"].ToString());
                int? FSCTypeID = Library.Helper.ConvertStringToInt(param["fscTypeID"].ToString());
                int? FSCPercent = Library.Helper.ConvertStringToInt(param["fscPercentID"].ToString());
                int? PackagingMethodID = param["packagingMethodID"] != null ? Library.Helper.ConvertStringToInt(param["packagingMethodID"].ToString()) : null;
                int? ClientSpecialPackagingMethodID = param["clientSpecialPackagingMethodID"] != null ? Library.Helper.ConvertStringToInt(param["clientSpecialPackagingMethodID"].ToString()) : null;
                string Season = param["season"].ToString();
                string Currency = param["currency"].ToString();

                using (OfferSeasonMngEntities context = CreateContext())
                {
                    var priceItem = context.OfferSeasonMng_function_GetSalePrice(ClientID, ModelID, MaterialID, MaterialTypeID, MaterialColorID, FrameMaterialID, FrameMaterialColorID, SubMaterialID, SubMaterialColorID, BackCushionID, SeatCushionID, CushionColorID, FSCTypeID, FSCPercent, PackagingMethodID, ClientSpecialPackagingMethodID, Season, Currency).ToList();
                    data = AutoMapper.Mapper.Map<List<OfferSeasonMng_function_GetSalePrice_Result>, List<DTO.SalePriceTable>>(priceItem);
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return data;
        }

        public List<DTO.SalePriceTableLastSeason> GetSalePriceTableLastSeason(int offerSeasonID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.SalePriceTableLastSeason> data = new List<DTO.SalePriceTableLastSeason>();
            try
            {
                using (OfferSeasonMngEntities context = CreateContext())
                {
                    var priceItem = context.OfferSeasonMng_function_GetSalePriceLastSeason(offerSeasonID).ToList();
                    data = AutoMapper.Mapper.Map<List<OfferSeasonMng_function_GetSalePriceLastSeason_Result>, List<DTO.SalePriceTableLastSeason>>(priceItem);
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return data;
        }

        public List<DTO.OfferLineDTO> GetOfferLineByOfferSeasonDetail(int offerSeasonDetailID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.OfferLineDTO> data = new List<DTO.OfferLineDTO>();
            try
            {
                using (OfferSeasonMngEntities context = CreateContext())
                {
                    var dbOfferLine = context.OfferSeasonMng_OfferLine_View.Where(o => o.OfferSeasonDetailID == offerSeasonDetailID).ToList();
                    data = AutoMapper.Mapper.Map<List<OfferSeasonMng_OfferLine_View>, List<DTO.OfferLineDTO>>(dbOfferLine);
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return data;
        }

        public bool AdminUpdateSalePrice(int offerSeasonDetailID, decimal? salePrice, int updatedBy, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (OfferSeasonMngEntities context = CreateContext())
                {
                    context.OfferSeasonMng_function_AdminUpdateSalePrice(offerSeasonDetailID, salePrice, updatedBy);
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return false;
        }

        public List<DTO.ImageGalleryDTO> GetImageGallery(int offerSeasonID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (OfferSeasonMngEntities context = CreateContext())
                {
                    var data = context.OfferSeasonMng_function_GetImageGallery(offerSeasonID).ToList();
                    return AutoMapper.Mapper.Map<List<OfferSeasonMng_function_GetImageGallery_Result>, List<DTO.ImageGalleryDTO>>(data);
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return new List<DTO.ImageGalleryDTO>();
            }            
        }

        public DTO.SampleOfferSeasonDTO CreateOfferSeasonSample(int? offerSeasonDetailID, int? clientID, string season, int? userID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (OfferSeasonMngEntities context = CreateContext())
                {
                    var data = context.OfferSeasonMng_function_CreateOfferSeasonSample(offerSeasonDetailID, clientID, season, userID).FirstOrDefault();
                    return AutoMapper.Mapper.Map<OfferSeasonMng_function_CreateOfferSeasonSample_Result, DTO.SampleOfferSeasonDTO>(data);
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return new DTO.SampleOfferSeasonDTO();
            }
        }

        public List<DTO.RelatedFactoryOrderDetailDTO> GetRelatedFactoryOrderDetail(int offerSeasonID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (OfferSeasonMngEntities context = CreateContext())
                {
                    var data = context.OfferSeasonMng_function_GetRelatedFactoryOrderDetail(offerSeasonID).ToList();
                    return AutoMapper.Mapper.Map<List<OfferSeasonMng_function_GetRelatedFactoryOrderDetail_Result>, List<DTO.RelatedFactoryOrderDetailDTO>>(data);
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return new List<DTO.RelatedFactoryOrderDetailDTO>();
            }
        }

        public List<DTO.PurchasingPriceLastYearDTO> GetPurchasingPriceLastYear(int offerSeasonID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (OfferSeasonMngEntities context = CreateContext())
                {
                    var data = context.OfferSeasonMng_function_GetCurrentSupplier(offerSeasonID).ToList();
                    return AutoMapper.Mapper.Map<List<OfferSeasonMng_function_GetCurrentSupplier_Result>, List<DTO.PurchasingPriceLastYearDTO>>(data);
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return new List<DTO.PurchasingPriceLastYearDTO>();
            }
        }
    }
}

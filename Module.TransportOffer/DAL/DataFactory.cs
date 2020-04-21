using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Library.DTO;
using Module.TransportOffer.DTO;

namespace Module.TransportOffer.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private DataConverter converter = new DataConverter();
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        
        public DataFactory() { }

        private TransportOfferEntities CreateContext()
        {
            return new TransportOfferEntities(Library.Helper.CreateEntityConnectionString("DAL.TransportOfferModel"));
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
                using (TransportOfferEntities context = CreateContext())
                {
                    var dbItem = context.TransportOffer.Where(o => o.TransportOfferID == id).FirstOrDefault();
                    context.TransportOffer.Remove(dbItem);
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
                string forwarderNM = null;
                string validFrom = null;
                string validTo = null;
                string description = null;
                string updatedDate = null;
                string updatorName = null;
                int? forwarderID = null;
                if (filters.ContainsKey("forwarderNM") && !string.IsNullOrEmpty(filters["forwarderNM"].ToString()))
                {
                    forwarderNM = filters["forwarderNM"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("validFrom") && !string.IsNullOrEmpty(filters["validFrom"].ToString()))
                {
                    validFrom = filters["validFrom"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("validTo") && !string.IsNullOrEmpty(filters["validTo"].ToString()))
                {
                    validTo = filters["validTo"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("description") && !string.IsNullOrEmpty(filters["description"].ToString()))
                {
                    description = filters["description"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("updatorName") && !string.IsNullOrEmpty(filters["updatorName"].ToString()))
                {
                    updatorName = filters["updatorName"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("updatedDate") && !string.IsNullOrEmpty(filters["updatedDate"].ToString()))
                {
                    updatedDate = filters["updatedDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("forwarderID") && filters["forwarderID"]!=null)
                {
                    forwarderID = Convert.ToInt32(filters["forwarderID"]);
                }
                using (TransportOfferEntities context = CreateContext())
                {
                    totalRows = context.TransportOfferMng_function_SearchTransportOffer(orderBy, orderDirection, forwarderID, forwarderNM,validFrom,validTo,description,updatedDate,updatorName).Count();
                    var result = context.TransportOfferMng_function_SearchTransportOffer(orderBy, orderDirection, forwarderID, forwarderNM, validFrom, validTo, description, updatedDate, updatorName);
                    searchFormData.Data = converter.DB2DTO_TransportOfferSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
                searchFormData.Forwarders = supportFactory.GetForwarder();
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
            DTO.TransportOfferDTO dtoTransportOffer = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.TransportOfferDTO>();
            try
            {
                using (TransportOfferEntities context = CreateContext())
                {
                    TransportOffer dbItem = null;
                    if (id > 0)
                    {                        
                        dbItem = context.TransportOffer.Where(o => o.TransportOfferID == id).FirstOrDefault();
                    }
                    else
                    {
                        dbItem = new TransportOffer();
                        context.TransportOffer.Add(dbItem);
                    }
                    if (dbItem == null)
                    {
                        notification.Message = "data not found!";
                        return false;
                    }
                    else
                    {
                        Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
                        string tempFolder = FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\";
                        if (dtoTransportOffer.File_HasChange.HasValue && dtoTransportOffer.File_HasChange.Value)
                        {
                            dtoTransportOffer.FileUD = fwFactory.CreateFilePointer(tempFolder, dtoTransportOffer.File_NewFile, dtoTransportOffer.FileUD, dtoTransportOffer.FriendlyName);
                        }
                        //convert dto to db
                        converter.DTO2DB_TransportOffer(dtoTransportOffer, ref dbItem);
                        dbItem.UpdatedBy = userId;
                        //remove orphan
                        context.TransportOfferCostDetail.Local.Where(o => o.TransportOffer == null).ToList().ForEach(o => context.TransportOfferCostDetail.Remove(o));
                        context.TransportOfferConditionDetail.Local.Where(o => o.TransportOffer == null).ToList().ForEach(o => context.TransportOfferConditionDetail.Remove(o));
                        //save data
                        context.SaveChanges();
                        //get return data
                        dtoItem = GetData(userId, dbItem.TransportOfferID, out notification).Data;
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

        public EditFormData GetData(int userId, int id, out Notification notification)
        {
            DTO.EditFormData editFormData = new DTO.EditFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (TransportOfferEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        TransportOfferMng_TransportOffer_View dbItem;
                        dbItem = context.TransportOfferMng_TransportOffer_View
                            .Include("TransportOfferMng_TransportOfferCostDetail_View")
                            .Include("TransportOfferMng_TransportOfferConditionDetail_View").FirstOrDefault(o => o.TransportOfferID == id);
                        editFormData.Data = converter.DB2DTO_TransportOffer(dbItem);
                    }
                    else
                    {
                        editFormData.Data = new DTO.TransportOfferDTO();
                        editFormData.Data.TransportOfferConditionDetailDTOs = new List<TransportOfferConditionDetailDTO>();
                        editFormData.Data.TransportOfferCostDetailDTOs = new List<TransportOfferCostDetailDTO>();
                        
                        DTO.TransportOfferCostDetailDTO costDetail;
                        int costDetailID = -1;
                        foreach (var costItem in context.TransportOfferMng_TransportCostItem_View.OrderBy(o =>o.TransportCostTypeID).Where(o => o.IsDefault.HasValue && o.IsDefault.Value).ToList())
                        {
                            if (costItem.TransportCostTypeID.HasValue && costItem.TransportCostTypeID.Value == 1)
                            {
                                foreach (var pol in context.POL.Where(o => o.IsIncludedInTransportOffer.HasValue && o.IsIncludedInTransportOffer.Value).ToList())
                                {
                                    foreach (var pod in context.POD.Where(o => o.IsIncludedInTransportOffer.HasValue && o.IsIncludedInTransportOffer.Value))
                                    {
                                        costDetail = new TransportOfferCostDetailDTO();
                                        costDetail.TransportOfferCostDetailID = costDetailID;
                                        costDetail.TransportCostItemID = costItem.TransportCostItemID;
                                        costDetail.Currency = costItem.Currency;
                                        costDetail.POLID = pol.PoLID;
                                        costDetail.PODID = pod.PoDID;
                                        editFormData.Data.TransportOfferCostDetailDTOs.Add(costDetail);
                                        costDetailID--;
                                    }
                                }
                            }
                            else if (costItem.TransportCostTypeID.HasValue && costItem.TransportCostTypeID.Value == 2)
                            {
                                foreach (var pod in context.POD.Where(o => o.IsIncludedInTransportOffer.HasValue && o.IsIncludedInTransportOffer.Value))
                                {
                                    costDetail = new TransportOfferCostDetailDTO();
                                    costDetail.TransportOfferCostDetailID = costDetailID;
                                    costDetail.TransportCostItemID = costItem.TransportCostItemID;
                                    costDetail.Currency = costItem.Currency;
                                    costDetail.PODID = pod.PoDID;
                                    editFormData.Data.TransportOfferCostDetailDTOs.Add(costDetail);
                                    costDetailID--;
                                }
                            }
                            
                        }

                    }
                    editFormData.Forwarders = supportFactory.GetForwarder();
                    editFormData.Currencies = supportFactory.GetCurrency();                    
                    editFormData.TransportCostItemDTOs = this.GetTransportCostItem(out notification);
                    editFormData.TransportConditionItemDTOs = this.GetTransportConditionItem(out notification);
                    editFormData.TransportCostTypes = supportFactory.GetTransportCostType();
                    editFormData.TransportCostChargeTypes = supportFactory.GetTransportCostChargeType();
                    //POD, POL
                    List<Module.Support.DTO.POD> PODs = new List<Support.DTO.POD>();
                    PODs.Add(new Support.DTO.POD() { PoDID=-1, PoDName="<<--All-->>"});
                    PODs.AddRange(supportFactory.GetPOD());
                    editFormData.PODs = PODs;
                    //POL
                    List<Module.Support.DTO.POL> POLs = new List<Support.DTO.POL>();
                    POLs.Add(new Support.DTO.POL() { PoLID=-1, PoLname = "<<--All-->>"});
                    POLs.AddRange(supportFactory.GetPOL());
                    editFormData.POLs = POLs;
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

        public List<DTO.TransportCostItemDTO> GetTransportCostItem(out Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (TransportOfferEntities context = CreateContext())
                {
                    var x = context.TransportOfferMng_TransportCostItem_View.ToList();
                    return  AutoMapper.Mapper.Map<List<TransportOfferMng_TransportCostItem_View>, List<DTO.TransportCostItemDTO>>(x);
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

        public List<DTO.TransportConditionItemDTO> GetTransportConditionItem(out Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (TransportOfferEntities context = CreateContext())
                {
                    var x = context.TransportOfferMng_TransportConditionItem_View.ToList();
                    return AutoMapper.Mapper.Map<List<TransportOfferMng_TransportConditionItem_View>, List<DTO.TransportConditionItemDTO>>(x);
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
                return new List<TransportConditionItemDTO>();
            }
        }

        public DTO.TransportCostItemDTO GetTransportCostItem(int id, out Library.DTO.Notification notification)
        {
            DTO.TransportCostItemDTO dtoItem;
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (TransportOfferEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        TransportOfferMng_TransportCostItem_View dbItem;
                        dbItem = context.TransportOfferMng_TransportCostItem_View.Where(o => o.TransportCostItemID == id).FirstOrDefault(); ;
                        dtoItem = AutoMapper.Mapper.Map<TransportOfferMng_TransportCostItem_View, DTO.TransportCostItemDTO>(dbItem);
                    }
                    else
                    {
                        dtoItem = new TransportCostItemDTO();
                        dtoItem.Currency = "USD";
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
                return new TransportCostItemDTO(); ;
            }
        }

        public DTO.TransportConditionItemDTO GetTransportConditionItem(int id, out Library.DTO.Notification notification)
        {
            DTO.TransportConditionItemDTO dtoItem;
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (TransportOfferEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        TransportOfferMng_TransportConditionItem_View dbItem;
                        dbItem = context.TransportOfferMng_TransportConditionItem_View.Where(o => o.TransportConditionItemID == id).FirstOrDefault(); ;
                        dtoItem = AutoMapper.Mapper.Map<TransportOfferMng_TransportConditionItem_View, DTO.TransportConditionItemDTO>(dbItem);
                    }
                    else
                    {
                        dtoItem = new TransportConditionItemDTO();
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
                return new TransportConditionItemDTO();
            }
        }

        public DTO.TransportCostItemDTO UpdateTransportCostItem(int userId,int id, ref object dtoObjectItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.TransportCostItemDTO dtoItem = ((Newtonsoft.Json.Linq.JObject)dtoObjectItem).ToObject<DTO.TransportCostItemDTO>();
            try
            {
                using (TransportOfferEntities context = CreateContext())
                {
                    TransportCostItem dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new TransportCostItem();
                        context.TransportCostItem.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.TransportCostItem.Where(o => o.TransportCostItemID == id).FirstOrDefault();
                    }
                    if (dbItem == null)
                    {
                        throw new Exception("data not found!");
                    }
                    else
                    {
                        AutoMapper.Mapper.Map<DTO.TransportCostItemDTO, TransportCostItem>(dtoItem, dbItem);
                        dbItem.UpdatedBy = userId;
                        dbItem.UpdatedDate = DateTime.Now;
                        //apply save data
                        context.SaveChanges();
                        //get return data
                        dtoItem = GetTransportCostItem(dbItem.TransportCostItemID, out notification);
                        return dtoItem;
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
                return new TransportCostItemDTO();
            }
        }

        public DTO.TransportConditionItemDTO UpdateTransportConditionItem(int userId, int id, ref object dtoObjectItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.TransportConditionItemDTO dtoItem = ((Newtonsoft.Json.Linq.JObject)dtoObjectItem).ToObject<DTO.TransportConditionItemDTO>();
            try
            {
                using (TransportOfferEntities context = CreateContext())
                {
                    TransportConditionItem dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new TransportConditionItem();
                        context.TransportConditionItem.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.TransportConditionItem.Where(o => o.TransportConditionItemID == id).FirstOrDefault();
                    }
                    if (dbItem == null)
                    {
                        throw new Exception("data not found!");
                    }
                    else
                    {
                        AutoMapper.Mapper.Map<DTO.TransportConditionItemDTO, TransportConditionItem>(dtoItem, dbItem);
                        dbItem.UpdatedBy = userId;
                        dbItem.UpdatedDate = DateTime.Now;
                        //apply save data
                        context.SaveChanges();
                        //get return data
                        dtoItem = GetTransportConditionItem(dbItem.TransportConditionItemID, out notification);
                        return dtoItem;
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
                return new TransportConditionItemDTO();
            }
        }

        public string GetReportTransportOfferOverview(string validFrom, string validTo, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();
            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("TransportOfferMng_function_GetReportTransportOffer", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@ValidFrom", validFrom);
                adap.SelectCommand.Parameters.AddWithValue("@ValidTo", validTo);

                adap.TableMappings.Add("Table", "TransportOffer");
                adap.TableMappings.Add("Table1", "TransportOfferCostDetail");
                adap.TableMappings.Add("Table2", "LeftItem");
                adap.TableMappings.Add("Table3", "TransportCostTypeGroup");
                adap.TableMappings.Add("Table4", "TransportOfferConditionDetail");
                adap.TableMappings.Add("Table5", "LeftItem_Condition");
                adap.Fill(ds);
                
                return Library.Helper.CreateReportFileWithEPPlus2(ds, "TransportOfferOverview");
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return string.Empty;
            }
        }

        public bool DeleteTransportCostItem(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();
            try
            {
                using (TransportOfferEntities context = CreateContext())
                {
                    var x = context.TransportCostItem.Where(o => o.TransportCostItemID == id).FirstOrDefault();
                    context.TransportCostItem.Remove(x);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return false;
            }
        }

        public bool DeleteTransportConditionItem(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();
            try
            {
                using (TransportOfferEntities context = CreateContext())
                {
                    var x = context.TransportConditionItem.Where(o => o.TransportConditionItemID == id).FirstOrDefault();
                    context.TransportConditionItem.Remove(x);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Library.DTO;
using Module.ClientOfferMng.DTO;

namespace Module.ClientOfferMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private DataConverter converter = new DataConverter();
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        
        public DataFactory() { }

        private ClientOfferMngEntities CreateContext()
        {
            return new ClientOfferMngEntities(Library.Helper.CreateEntityConnectionString("DAL.ClientOfferMngModel"));
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
                using (ClientOfferMngEntities context = CreateContext())
                {
                    var dbItem = context.ClientOffer.Where(o => o.ClientOfferID == id).FirstOrDefault();
                    context.ClientOffer.Remove(dbItem);
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
                string clientNM = null;
                string validFrom = null;
                string validTo = null;
                string description = null;
                string updatedDate = null;
                string updatorName = null;
                int? clientID = null;
                if (filters.ContainsKey("clientNM") && !string.IsNullOrEmpty(filters["clientNM"].ToString()))
                {
                    clientNM = filters["clientNM"].ToString().Replace("'", "''");
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
                if (filters.ContainsKey("clientID") && filters["clientID"] !=null)
                {
                    clientID = Convert.ToInt32(filters["clientID"]);
                }
                using (ClientOfferMngEntities context = CreateContext())
                {
                    totalRows = context.ClientOfferMng_function_SearchClientOffer(orderBy, orderDirection, clientID, clientNM, validFrom,validTo,description,updatedDate,updatorName).Count();
                    var result = context.ClientOfferMng_function_SearchClientOffer(orderBy, orderDirection, clientID, clientNM, validFrom, validTo, description, updatedDate, updatorName);
                    searchFormData.Data = converter.DB2DTO_ClientOfferSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
                searchFormData.Clients = supportFactory.GetClient();
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
            DTO.ClientOffer dtoClientOffer = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.ClientOffer>();
            try
            {
                using (ClientOfferMngEntities context = CreateContext())
                {
                    ClientOffer dbItem = null;
                    if (id > 0)
                    {                        
                        dbItem = context.ClientOffer.Where(o => o.ClientOfferID == id).FirstOrDefault();
                    }
                    else
                    {
                        dbItem = new ClientOffer();
                        context.ClientOffer.Add(dbItem);
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
                        if (dtoClientOffer.File_HasChange.HasValue && dtoClientOffer.File_HasChange.Value)
                        {
                            dtoClientOffer.FileUD = fwFactory.CreateFilePointer(tempFolder, dtoClientOffer.File_NewFile, dtoClientOffer.FileUD, dtoClientOffer.FriendlyName);
                        }
                        //convert dto to db
                        converter.DTO2DB_ClientOffer(dtoClientOffer, ref dbItem);
                        dbItem.UpdatedBy = userId;
                        //remove orphan
                        context.ClientOfferCostDetail.Local.Where(o => o.ClientOffer == null).ToList().ForEach(o => context.ClientOfferCostDetail.Remove(o));
                        context.ClientOfferConditionDetail.Local.Where(o => o.ClientOffer == null).ToList().ForEach(o => context.ClientOfferConditionDetail.Remove(o));
                        context.ClientOfferAdditionalDetail.Local.Where(o => o.ClientOffer == null).ToList().ForEach(o => context.ClientOfferAdditionalDetail.Remove(o));
                        //save data
                        context.SaveChanges();
                        //get return data
                        dtoItem = GetData(userId, dbItem.ClientOfferID, out notification).Data;
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
                using (ClientOfferMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        ClientOfferMng_ClientOffer_View dbItem;
                        dbItem = context.ClientOfferMng_ClientOffer_View
                            .Include("ClientOfferMng_ClientOfferCostDetail_View")
                            .Include("ClientOfferMng_ClientOfferAdditionalDetail_View")
                            .Include("ClientOfferMng_ClientOfferConditionDetail_View").FirstOrDefault(o => o.ClientOfferID == id);
                        editFormData.Data = converter.DB2DTO_ClientOffer(dbItem);
                    }
                    else
                    {
                        editFormData.Data = new DTO.ClientOffer();
                        editFormData.Data.ClientOfferConditionDetailDTOs = new List<DTO.ClientOfferConditionDetailDTO>();
                        editFormData.Data.ClientOfferCostDetailDTOs = new List<DTO.ClientOfferCostDetailDTO>();
                        editFormData.Data.ClientOfferAdditionalDetailDTOs = new List<DTO.ClientOfferAdditionalDetailDTO>();

                        //DTO.ClientOfferCostDetailDTO costDetail;
                        //int costDetailID = -1;
                        //foreach (var costItem in context.ClientOfferMng_ClientCostItem_View.OrderBy(o => o.ClientCostTypeID).Where(o => o.IsDefault.HasValue && o.IsDefault.Value).ToList())
                        //{
                        //    if (costItem.ClientCostTypeID.HasValue && costItem.ClientCostTypeID.Value == 1)
                        //    {
                        //        foreach (var pol in context.POL.Where(o => o.IsIncludedInTransportOffer.HasValue && o.IsIncludedInTransportOffer.Value).ToList())
                        //        {
                        //            foreach (var pod in context.POD.Where(o => o.IsIncludedInTransportOffer.HasValue && o.IsIncludedInTransportOffer.Value))
                        //            {
                        //                costDetail = new ClientOfferCostDetailDTO();
                        //                costDetail.ClientOfferCostDetailID = costDetailID;
                        //                costDetail.ClientCostItemID = costItem.ClientCostItemID;
                        //                costDetail.Currency = costItem.Currency;
                        //                costDetail.POLID = pol.PoLID;
                        //                costDetail.PODID = pod.PoDID;
                        //                editFormData.Data.ClientOfferCostDetailDTOs.Add(costDetail);
                        //                costDetailID--;
                        //            }
                        //        }
                        //    }
                        //    else if (costItem.ClientCostTypeID.HasValue && costItem.ClientCostTypeID.Value == 2)
                        //    {
                        //        foreach (var pod in context.POD.Where(o => o.IsIncludedInTransportOffer.HasValue && o.IsIncludedInTransportOffer.Value))
                        //        {
                        //            costDetail = new ClientOfferCostDetailDTO();
                        //            costDetail.ClientOfferCostDetailID = costDetailID;
                        //            costDetail.ClientCostItemID = costItem.ClientCostItemID;
                        //            costDetail.Currency = costItem.Currency;
                        //            costDetail.PODID = pod.PoDID;
                        //            editFormData.Data.ClientOfferCostDetailDTOs.Add(costDetail);
                        //            costDetailID--;
                        //        }
                        //    }

                        //}

                    }
                    editFormData.Clients = supportFactory.GetClient();
                    editFormData.Currencies = supportFactory.GetCurrency2();
                    editFormData.ClientCostItemDTOs = this.GetClienttCostItem(out notification);
                    editFormData.ClientConditionItemDTOs = this.GetClientConditionItem(out notification);
                    editFormData.ClientAdditionalItemDTOs = this.GetClientAdditionalItem(out notification);
                    editFormData.ClientCostTypes = supportFactory.GetTransportCostType();
                    editFormData.ClientCostChargeTypes = supportFactory.GetTransportCostChargeType();
                    editFormData.PaymentTerms = supportFactory.GetPaymentTerm();
                    //POD, POL
                    List<Module.Support.DTO.POD> PODs = new List<Support.DTO.POD>();
                    PODs.Add(new Support.DTO.POD() { PoDID = -1, PoDName = "<<--All-->>" });
                    PODs.AddRange(supportFactory.GetPOD());
                    editFormData.PODs = PODs;
                    //POL
                    List<Module.Support.DTO.POL> POLs = new List<Support.DTO.POL>();
                    POLs.Add(new Support.DTO.POL() { PoLID = -1, PoLname = "<<--All-->>" });
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

        public List<DTO.ClientCostItemDTO> GetClienttCostItem(out Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ClientOfferMngEntities context = CreateContext())
                {
                    var x = context.ClientOfferMng_ClientCostItem_View.ToList();
                    return AutoMapper.Mapper.Map<List<ClientOfferMng_ClientCostItem_View>, List<DTO.ClientCostItemDTO>>(x);
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
                return new List<ClientCostItemDTO>();
            }
        }

        public List<DTO.ClientConditionItemDTO> GetClientConditionItem(out Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ClientOfferMngEntities context = CreateContext())
                {
                    var x = context.ClientOfferMng_ClientConditionItem_View.ToList();
                    return AutoMapper.Mapper.Map<List<ClientOfferMng_ClientConditionItem_View>, List<DTO.ClientConditionItemDTO>>(x);
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
                return new List<ClientConditionItemDTO>();
            }
        }

        public List<DTO.ClientAdditionalItemDTO> GetClientAdditionalItem(out Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ClientOfferMngEntities context = CreateContext())
                {
                    var x = context.ClientOfferMng_ClientAdditionalItem_View.ToList();
                    return AutoMapper.Mapper.Map<List<ClientOfferMng_ClientAdditionalItem_View>, List<DTO.ClientAdditionalItemDTO>>(x);
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
                return new List<ClientAdditionalItemDTO>();
            }
        }

        public DTO.ClientCostItemDTO GetClientCostItem(int id, out Library.DTO.Notification notification)
        {
            DTO.ClientCostItemDTO dtoItem;
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ClientOfferMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        ClientOfferMng_ClientCostItem_View dbItem;
                        dbItem = context.ClientOfferMng_ClientCostItem_View.Where(o => o.ClientCostItemID == id).FirstOrDefault(); ;
                        dtoItem = AutoMapper.Mapper.Map<ClientOfferMng_ClientCostItem_View, DTO.ClientCostItemDTO>(dbItem);
                    }
                    else
                    {
                        dtoItem = new ClientCostItemDTO();
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
                return new ClientCostItemDTO(); ;
            }
        }

        public DTO.ClientConditionItemDTO GetClientConditionItem(int id, out Library.DTO.Notification notification)
        {
            DTO.ClientConditionItemDTO dtoItem;
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ClientOfferMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        ClientOfferMng_ClientConditionItem_View dbItem;
                        dbItem = context.ClientOfferMng_ClientConditionItem_View.Where(o => o.ClientConditionItemID == id).FirstOrDefault(); ;
                        dtoItem = AutoMapper.Mapper.Map<ClientOfferMng_ClientConditionItem_View, DTO.ClientConditionItemDTO>(dbItem);
                    }
                    else
                    {
                        dtoItem = new ClientConditionItemDTO();
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
                return new ClientConditionItemDTO();
            }
        }

        public DTO.ClientAdditionalItemDTO GetClientAdditionalItem(int id, out Library.DTO.Notification notification)
        {
            DTO.ClientAdditionalItemDTO dtoItem;
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ClientOfferMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        ClientOfferMng_ClientAdditionalItem_View dbItem;
                        dbItem = context.ClientOfferMng_ClientAdditionalItem_View.Where(o => o.ClientAdditionalItemID == id).FirstOrDefault(); ;
                        dtoItem = AutoMapper.Mapper.Map<ClientOfferMng_ClientAdditionalItem_View, DTO.ClientAdditionalItemDTO>(dbItem);
                    }
                    else
                    {
                        dtoItem = new ClientAdditionalItemDTO();
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
                return new ClientAdditionalItemDTO();
            }
        }

        public DTO.ClientCostItemDTO UpdateClientCostItem(int userId, int id, ref object dtoObjectItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ClientCostItemDTO dtoItem = ((Newtonsoft.Json.Linq.JObject)dtoObjectItem).ToObject<DTO.ClientCostItemDTO>();
            try
            {
                using (ClientOfferMngEntities context = CreateContext())
                {
                    ClientCostItem dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new ClientCostItem();
                        context.ClientCostItem.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.ClientCostItem.Where(o => o.ClientCostItemID == id).FirstOrDefault();
                    }
                    if (dbItem == null)
                    {
                        throw new Exception("data not found!");
                    }
                    else
                    {
                        AutoMapper.Mapper.Map<DTO.ClientCostItemDTO, ClientCostItem>(dtoItem, dbItem);
                        dbItem.UpdatedBy = userId;
                        dbItem.UpdatedDate = DateTime.Now;
                        //apply save data
                        context.SaveChanges();
                        //get return data
                        dtoItem = GetClientCostItem(dbItem.ClientCostItemID, out notification);
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
                return new ClientCostItemDTO();
            }
        }

        public DTO.ClientConditionItemDTO UpdateClientConditionItem(int userId, int id, ref object dtoObjectItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ClientConditionItemDTO dtoItem = ((Newtonsoft.Json.Linq.JObject)dtoObjectItem).ToObject<DTO.ClientConditionItemDTO>();
            try
            {
                using (ClientOfferMngEntities context = CreateContext())
                {
                    ClientConditionItem dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new ClientConditionItem();
                        context.ClientConditionItem.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.ClientConditionItem.Where(o => o.ClientConditionItemID == id).FirstOrDefault();
                    }
                    if (dbItem == null)
                    {
                        throw new Exception("data not found!");
                    }
                    else
                    {
                        AutoMapper.Mapper.Map<DTO.ClientConditionItemDTO, ClientConditionItem>(dtoItem, dbItem);
                        dbItem.UpdatedBy = userId;
                        dbItem.UpdatedDate = DateTime.Now;
                        //apply save data
                        context.SaveChanges();
                        //get return data
                        dtoItem = GetClientConditionItem(dbItem.ClientConditionItemID, out notification);
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
                return new ClientConditionItemDTO();
            }
        }


        public string GetReportClientOfferOverview(string validFrom, string validTo, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject2 ds = new ReportDataObject2();
            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("ClientOfferMng_function_GetReportClientOffer", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@ValidFrom", validFrom);
                adap.SelectCommand.Parameters.AddWithValue("@ValidTo", validTo);

                adap.TableMappings.Add("Table", "ClientOffer");
                adap.TableMappings.Add("Table1", "ClientOfferCostDetail");
                adap.TableMappings.Add("Table2", "LeftItem");
                adap.TableMappings.Add("Table3", "ClientCostTypeGroup");
                adap.TableMappings.Add("Table4", "ClientOfferAdditionalDetail");
                adap.TableMappings.Add("Table5", "LeftItem_Additional");
                adap.TableMappings.Add("Table6", "ClientOfferConditionDetail");
                adap.TableMappings.Add("Table7", "LeftItem_Condition");
                adap.Fill(ds);

                return Library.Helper.CreateReportFileWithEPPlus2(ds, "ClientTransportOfferOverview");
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

        public bool DeleteClientCostItem(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ClientOfferMngEntities context = CreateContext())
                {
                    var x = context.ClientCostItem.Where(o => o.ClientCostItemID == id).FirstOrDefault();
                    context.ClientCostItem.Remove(x);
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

        public bool DeleteClientConditionItem(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ClientOfferMngEntities context = CreateContext())
                {
                    var x = context.ClientConditionItem.Where(o => o.ClientConditionItemID == id).FirstOrDefault();
                    context.ClientConditionItem.Remove(x);
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

        public string ClientOffer_Printout(int id, int printOptionValue, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = NotificationType.Success;

            string fileName = "";
            ClientOfferObject ds = new ClientOfferObject();

            try
            {
                System.Data.SqlClient.SqlDataAdapter adap = new System.Data.SqlClient.SqlDataAdapter();
                adap.SelectCommand = new System.Data.SqlClient.SqlCommand("ClientOfferMng_function_Printout", new System.Data.SqlClient.SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@ClientOfferID", id);
                adap.TableMappings.Add("Table", "ForwarderMng_Offer_View");
                adap.TableMappings.Add("Table1", "ForwarderMng_CostItem_View");
                adap.TableMappings.Add("Table2", "ForwarderMng_Condition_View");
                adap.TableMappings.Add("Table3", "ForwarderMng_AddCondition_View");
                adap.Fill(ds);

                ds.AcceptChanges();
                if(printOptionValue == 1)
                    fileName = Library.Helper.CreateReportFileWithEPPlus2(ds, "ClientOfferPrint_NL");
                else
                    fileName = Library.Helper.CreateReportFileWithEPPlus2(ds, "ClientOfferPrint");
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;

                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
            }
            return fileName;
        }
    }
}

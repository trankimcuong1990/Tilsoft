using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
namespace Module.FactoryMaterialOrderNorm.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private DataConverter converter = new DataConverter();

        private FactoryMaterialOrderNormEntities CreateContext()
        {
            return new FactoryMaterialOrderNormEntities(Library.Helper.CreateEntityConnectionString("DAL.FactoryMaterialOrderNormModel"));
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FactoryMaterialOrderNormEntities context = CreateContext())
                {
                    var dbItem = context.FactoryMaterialOrderNorm.Where(o => o.FactoryMaterialOrderNormID == id).FirstOrDefault();
                    foreach (var item in dbItem.FactoryMaterialOrderNormDetail.ToArray())
                    {
                        context.FactoryMaterialOrderNormDetail.Remove(item);
                    }
                    context.FactoryMaterialOrderNorm.Remove(dbItem);
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
            DTO.EditFormData editFormData = new DTO.EditFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FactoryMaterialOrderNormEntities context = CreateContext())
                {
                    FactoryMaterialOrderNormMng_FactoryMaterialOrderNorm_View dbItem;
                    dbItem = context.FactoryMaterialOrderNormMng_FactoryMaterialOrderNorm_View.FirstOrDefault(o => o.FactoryMaterialOrderNormID == id);
                    editFormData.Data = converter.DB2DTO_FactoryMaterialOrderNorm(dbItem);
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

        public override DTO.SearchFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.SearchFormData searchFormData = new DTO.SearchFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            string proformaInvoiceNo = string.Empty;
            string clientUD = string.Empty;
            string articleCode = string.Empty;
            string description = string.Empty;

            if (filters.ContainsKey("proformaInvoiceNo")) proformaInvoiceNo = filters["proformaInvoiceNo"].ToString();
            if (filters.ContainsKey("clientUD")) clientUD = filters["clientUD"].ToString();
            if (filters.ContainsKey("articleCode")) articleCode = filters["articleCode"].ToString();
            if (filters.ContainsKey("description")) description = filters["description"].ToString();

            try
            {
                using (FactoryMaterialOrderNormEntities context = CreateContext())
                {
                    totalRows = context.FactoryMaterialOrderNormMng_function_SearchFactoryMaterialOrderNorm(orderBy, orderDirection, proformaInvoiceNo, clientUD, articleCode, description).Count();
                    var result = context.FactoryMaterialOrderNormMng_function_SearchFactoryMaterialOrderNorm(orderBy, orderDirection, proformaInvoiceNo, clientUD, articleCode, description);
                    searchFormData.Data = converter.DB2DTO_FactoryMaterialOrderNormSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
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
            DTO.FactoryMaterialOrderNorm dtoFactoryMaterialOrderNorm = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.FactoryMaterialOrderNorm>();
            try
            {
                using (FactoryMaterialOrderNormEntities context = CreateContext())
                {
                    FactoryMaterialOrderNorm dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new FactoryMaterialOrderNorm();
                        context.FactoryMaterialOrderNorm.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.FactoryMaterialOrderNorm.Where(o => o.FactoryMaterialOrderNormID == id).FirstOrDefault();
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "data not found!";
                        return false;
                    }
                    else
                    {
                        //convert dto to db
                        converter.DTO2DB_FactoryMaterialOrderNorm(dtoFactoryMaterialOrderNorm, ref dbItem);
                        //remove orphan item
                        context.FactoryMaterialOrderNormDetail.Local.Where(o => o.FactoryMaterialOrderNorm == null).ToList().ForEach(o => context.FactoryMaterialOrderNormDetail.Remove(o));
                        //save data
                        context.SaveChanges();
                        //get return data
                        dtoItem = GetData(dbItem.FactoryMaterialOrderNormID, out notification).Data;
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

        public DTO.SearchFormClientData GetListClient(Hashtable filters, out Library.DTO.Notification notification)
        {
            DTO.SearchFormClientData Data = new DTO.SearchFormClientData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //Module.Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();

            //search info
            string clientUD = string.Empty;
            string articleCode = string.Empty;
            string description = string.Empty;

            
            if (filters.ContainsKey("clientUD")) clientUD = filters["clientUD"].ToString();
            if (filters.ContainsKey("articleCode")) articleCode = filters["articleCode"].ToString();
            if (filters.ContainsKey("description")) description = filters["description"].ToString();

            //pager info
            int pageSize = 20;
            int pageIndex = 1;
            string sortedBy = string.Empty;
            string sortedDirection = string.Empty;

            if (filters.ContainsKey("pageSize")) pageSize = Convert.ToInt32(filters["pageSize"].ToString());
            if (filters.ContainsKey("pageIndex")) pageIndex = Convert.ToInt32(filters["pageIndex"].ToString());
            if (filters.ContainsKey("sortedBy")) sortedBy = filters["sortedBy"].ToString();
            if (filters.ContainsKey("sortedDirection")) sortedDirection = filters["sortedDirection"].ToString();

            try
            {
                //Data.Seasons = supportFactory.GetSeason();
                using (FactoryMaterialOrderNormEntities context = CreateContext())
                {
                    //get clients
                    Data.TotalRows = context.FactoryMaterialOrderNormMng_function_SearchClient(sortedBy, sortedDirection, clientUD, articleCode, description).Count();
                    var result = context.FactoryMaterialOrderNormMng_function_SearchClient(sortedBy, sortedDirection, clientUD, articleCode, description);
                    Data.Data = converter.DB2DTO_Client(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());

                    //get client products
                    List<int?> clientIDs = Data.Data.Select(o => o.ClientID).ToList();
                    List<DTO.ClientProduct> clientProducts = new List<DTO.ClientProduct>();
                    clientProducts = converter.DB2DTO_ClientProduct(context.FactoryMaterialOrderNormMng_ClientProduct_View.Where(o =>
                            clientIDs.Contains(o.ClientID)
                            && o.ArticleCode.Contains(articleCode != "" ? articleCode : o.ArticleCode)
                            && o.Description.Contains(description != "" ? description : o.Description)
                            ).ToList());


                    //get material standard norm
                    List<int?> productIDs = clientProducts.Select(o => o.ProductID).ToList();
                    List<DTO.DefaultNorm> defaultNorms = converter.DB2DTO_DefaultNorm(context.FactoryMaterialOrderNormMng_DefaultNorm_View.Where(o => productIDs.Contains(o.ProductID)).ToList());

                    foreach (var item in Data.Data)
                    {
                        //item.Season = Library.Helper.GetCurrentSeason();
                        //get product
                        item.ClientProducts = converter.DTO2DTO_ClientProduct(clientProducts.Where(o =>o.ClientID == item.ClientID).ToList());
                        //get material
                        foreach (var product in item.ClientProducts)
                        {
                            product.DefaultNorms = converter.DTO2DTO_DefaultNorm(defaultNorms.Where(o =>o.ProductID == product.ProductID).ToList());
                        }
                    }
                }
                return Data;
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
                return Data;
            }
        }

        public DTO.EditFormData GetEditFormData(int id,int? clientID, int? productID, out Library.DTO.Notification notification)
        {
            DTO.EditFormData editFormData = new DTO.EditFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            Module.Support.DAL.DataFactory support_factory = new Support.DAL.DataFactory();
            try
            {
                using (FactoryMaterialOrderNormEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        FactoryMaterialOrderNormMng_FactoryMaterialOrderNorm_View dbItem;
                        dbItem = context.FactoryMaterialOrderNormMng_FactoryMaterialOrderNorm_View.FirstOrDefault(o => o.FactoryMaterialOrderNormID == id);
                        editFormData.Data = converter.DB2DTO_FactoryMaterialOrderNorm(dbItem);
                    }
                    else
                    {
                        //get info client and product
                        var client = context.FactoryMaterialOrderNormMng_Client_View.Where(o => o.ClientID == clientID).FirstOrDefault();
                        var clientProduct = context.FactoryMaterialOrderNormMng_ClientProduct_View.Where(o => o.ClientID == clientID && o.ProductID == productID).FirstOrDefault();
                        var defaultNorm = context.FactoryMaterialOrderNormMng_DefaultNorm_View.Where(o => o.ProductID == productID).ToList();
                        
                        //init edit form info
                        editFormData.Data = new DTO.FactoryMaterialOrderNorm();
                        editFormData.Data.Season = Library.Helper.GetCurrentSeason();
                        editFormData.Data.ClientID = clientID;
                        editFormData.Data.ProductID = productID;
                        editFormData.Data.ClientUD = client.ClientUD;
                        editFormData.Data.ArticleCode = clientProduct.ArticleCode;
                        editFormData.Data.Description = clientProduct.Description;

                        //init default material norm
                        editFormData.Data.FactoryMaterialOrderNormDetails = new List<DTO.FactoryMaterialOrderNormDetail>();
                        DTO.FactoryMaterialOrderNormDetail orderMaterialNorm;

                        int i = -1;
                        foreach (var item in defaultNorm)
                        {
                            orderMaterialNorm = new DTO.FactoryMaterialOrderNormDetail();
                            orderMaterialNorm.FactoryMaterialOrderNormDetailID = i;
                            orderMaterialNorm.FactoryMaterialID = item.FactoryMaterialID;
                            orderMaterialNorm.UnitID = item.UnitID;
                            orderMaterialNorm.NormValue = item.NormValue;
                            orderMaterialNorm.FactoryGoodsProcedureID = item.FactoryGoodsProcedureID;
                            orderMaterialNorm.FactoryMaterialUD = item.FactoryMaterialUD;
                            orderMaterialNorm.FactoryMaterialNM = item.FactoryMaterialNM;
                            editFormData.Data.FactoryMaterialOrderNormDetails.Add(orderMaterialNorm);

                            i--;
                        }
                    }
                    //get support list
                    editFormData.Units = support_factory.GetUnit(1);
                    editFormData.Seasons = support_factory.GetSeason();
                    editFormData.FactoryGoodsProcedures = support_factory.GetFactoryGoodsProcedure();
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

        public bool CreateOrderNorm(object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            return false;
            //DTO.SaleOrder dtoOrder = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.SaleOrder>();
            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Create norm for order : " + dtoOrder.ProformaInvoiceNo + ", client: " + dtoOrder.ClientUD + " success" };
            //FactoryMaterialOrderNorm dbItem;
            //FactoryMaterialOrderNormDetail dbItemDetail;
            //try
            //{
            //    using (FactoryMaterialOrderNormEntities context = CreateContext())
            //    {
            //        foreach (var product in dtoOrder.SaleOrderDetails)
            //        {
            //            dbItem = new FactoryMaterialOrderNorm();
            //            dbItem.SaleOrderID = dtoOrder.SaleOrderID;
            //            dbItem.ProductID = product.ProductID;
            //            context.FactoryMaterialOrderNorm.Add(dbItem);

            //            if (product.DefaultNorms.Count() > 0)
            //            {
            //                foreach (var material in product.DefaultNorms)
            //                {
            //                    dbItemDetail = new FactoryMaterialOrderNormDetail();
            //                    dbItemDetail.FactoryMaterialID = material.FactoryMaterialID;
            //                    dbItemDetail.UnitID = material.UnitID;
            //                    dbItemDetail.NormValue = material.NormValue;
            //                    dbItem.FactoryMaterialOrderNormDetail.Add(dbItemDetail);
            //                }
            //            }
            //            else
            //            {
            //                throw new Exception("Product " + product.ArticleCode + "(" + product.Description + ")" + " does not create default norm. You must create default norm before create order norm");
            //            }
            //        }
            //        context.SaveChanges();
            //        return true;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification.Type = Library.DTO.NotificationType.Error;
            //    notification.Message = ex.Message;
            //    notification.DetailMessage.Add(ex.Message);
            //    if (ex.GetBaseException() != null)
            //    {
            //        notification.DetailMessage.Add(ex.GetBaseException().Message);
            //    }
            //    return false;
            //}
        }
    }
}

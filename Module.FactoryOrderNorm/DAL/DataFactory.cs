using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
namespace Module.FactoryOrderNorm.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private DataConverter converter = new DataConverter();

        private FactoryOrderNormEntities CreateContext()
        {
            return new FactoryOrderNormEntities(Library.Helper.CreateEntityConnectionString("DAL.FactoryOrderNormModel"));
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
                using (FactoryOrderNormEntities context = CreateContext())
                {
                    var dbItem = context.FactoryOrderNorm.Where(o => o.FactoryOrderNormID == id).FirstOrDefault();
                    //foreach (var item in dbItem.FactoryFinishedProductOrderNorm.ToArray())
                    //{
                    //    foreach (var mItem in item.FactoryMaterialOrderNorm.ToArray())
                    //    {
                    //        context.FactoryMaterialOrderNorm.Remove(mItem);
                    //    }
                    //    context.FactoryFinishedProductOrderNorm.Remove(item);
                    //}
                    context.FactoryOrderNorm.Remove(dbItem);
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
            Module.Support.DAL.DataFactory support_factory = new Support.DAL.DataFactory();
            try
            {
                using (FactoryOrderNormEntities context = CreateContext())
                {
                    FactoryOrderNormMng_FactoryOrderNorm_View dbItem;
                    dbItem = context.FactoryOrderNormMng_FactoryOrderNorm_View.FirstOrDefault(o => o.FactoryOrderNormID == id);
                    editFormData.Data = converter.DB2DTO_FactoryOrderNorm(dbItem);

                    //support data
                    //editFormData.Units = support_factory.GetUnit(1);
                    editFormData.Seasons = support_factory.GetSeason();
                    editFormData.FactoryGoodsProcedures = support_factory.GetFactoryGoodsProcedure();
                    editFormData.MaterialGroupTypes = support_factory.GetMaterialGroupType();
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

            string articleCode = string.Empty;
            string description = string.Empty;
            string clientUD = string.Empty;
            string season = string.Empty;
            if (filters.ContainsKey("articleCode")) articleCode = filters["articleCode"].ToString();
            if (filters.ContainsKey("description")) description = filters["description"].ToString();
            if (filters.ContainsKey("clientUD")) clientUD = filters["clientUD"].ToString();
            if (filters.ContainsKey("season")) season = filters["season"].ToString();

            try
            {
                using (FactoryOrderNormEntities context = CreateContext())
                {
                    totalRows = context.FactoryOrderNormMng_function_SearchFactoryOrderNorm(orderBy, orderDirection,articleCode,description,clientUD,season).Count();
                    var result = context.FactoryOrderNormMng_function_SearchFactoryOrderNorm(orderBy, orderDirection, articleCode, description, clientUD, season);
                    searchFormData.Data = converter.DB2DTO_FactoryOrderNormSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
            DTO.FactoryOrderNorm dtoFactoryOrderNorm = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.FactoryOrderNorm>();
            try
            {
                using (FactoryOrderNormEntities context = CreateContext())
                {
                    FactoryOrderNorm dbItem = null;
                    if (id > 0)
                    {
                        dbItem = context.FactoryOrderNorm.Where(o => o.FactoryOrderNormID == id).FirstOrDefault();
                    }
                    else
                    {
                        dbItem = new FactoryOrderNorm();
                        context.FactoryOrderNorm.Add(dbItem);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "data not found!";
                        return false;
                    }
                    else
                    {
                        //convert dto to db
                        converter.DTO2DB_FactoryOrderNorm(dtoFactoryOrderNorm, ref dbItem);
                        //remove orphan item
                        context.FactoryMaterialOrderNorm.Local.Where(o => o.FactoryFinishedProductOrderNorm == null).ToList().ForEach(o => context.FactoryMaterialOrderNorm.Remove(o));
                        context.FactoryFinishedProductOrderNorm.Local.Where(o => o.FactoryOrderNorm == null).ToList().ForEach(o => context.FactoryFinishedProductOrderNorm.Remove(o));
                        //save data
                        context.SaveChanges();
                        //get return data
                        dtoItem = GetData(dbItem.FactoryOrderNormID, out notification).Data;
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

        public int CreateNewFactoryFinishedProduct(string code, string name, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message ="Create component success" };
            try
            {
                if (string.IsNullOrEmpty(code))
                {
                    throw new Exception("You have to fill-in component code");
                }
                if (string.IsNullOrEmpty(name))
                {
                    throw new Exception("You have to fill-in component name");
                }
                using (FactoryOrderNormEntities context = CreateContext())
                {
                    FactoryFinishedProduct dbFinishedProduct = new FactoryFinishedProduct();
                    dbFinishedProduct.FactoryFinishedProductUD = code;
                    dbFinishedProduct.FactoryFinishedProductNM = name;
                    context.FactoryFinishedProduct.Add(dbFinishedProduct);
                    context.SaveChanges();
                    return dbFinishedProduct.FactoryFinishedProductID;
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
                return -1;
            }
        }

        public DTO.ClientSearchFormData GetListClient(Hashtable filters, out Library.DTO.Notification notification)
        {
            DTO.ClientSearchFormData Data = new DTO.ClientSearchFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            Module.Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();

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
                Data.Seasons = supportFactory.GetSeason();
                using (FactoryOrderNormEntities context = CreateContext())
                {
                    //get clients
                    Data.TotalRows = context.FactoryOrderNormMng_function_SearchClient(sortedBy, sortedDirection, clientUD, articleCode, description).Count();
                    var result = context.FactoryOrderNormMng_function_SearchClient(sortedBy, sortedDirection, clientUD, articleCode, description);
                    Data.Data = converter.DB2DTO_Client(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());

                    //get client products
                    List<int?> clientIDs = Data.Data.Select(o => o.ClientID).ToList();
                    List<DTO.ClientProduct> clientProducts = converter.DB2DTO_ClientProduct(context.FactoryOrderNormMng_ClientProduct_View.Where(o =>
                            clientIDs.Contains(o.ClientID)
                            && o.ArticleCode.Contains(articleCode != "" ? articleCode : o.ArticleCode)
                            && o.Description.Contains(description != "" ? description : o.Description)
                            ).ToList());


                    //get material standard norm
                    List<int?> productIDs = clientProducts.Select(o => o.ProductID).ToList();
                    foreach (var item in Data.Data)
                    {
                        item.ClientProducts = converter.DTO2DTO_ClientProduct(clientProducts.Where(o => o.ClientID == item.ClientID).ToList());
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

        //public DTO.EditFormData GetEditFormData(int id, int? clientID, int? productID, out Library.DTO.Notification notification)
        //{
        //    DTO.EditFormData editFormData = new DTO.EditFormData();
        //    notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
        //    Module.Support.DAL.DataFactory support_factory = new Support.DAL.DataFactory();
        //    try
        //    {
        //        using (FactoryOrderNormEntities context = CreateContext())
        //        {
        //            if (id > 0)
        //            {
        //                FactoryOrderNormMng_FactoryOrderNorm_View dbItem;
        //                dbItem = context.FactoryOrderNormMng_FactoryOrderNorm_View.FirstOrDefault(o => o.FactoryOrderNormID == id);
        //                editFormData.Data = converter.DB2DTO_FactoryOrderNorm(dbItem);
        //            }
        //            else
        //            {
        //                //get info client and product
        //                var client = context.FactoryOrderNormMng_Client_View.Where(o => o.ClientID == clientID).FirstOrDefault();
        //                var clientProduct = context.FactoryOrderNormMng_ClientProduct_View.Where(o => o.ClientID == clientID && o.ProductID == productID).FirstOrDefault();
                        
        //                var product = context.FactoryOrderNormMng_Product_View.Where(o => o.ProductID == productID).FirstOrDefault();
        //                int? modelID = product.ModelID;
        //                var defaultNorm = context.FactoryOrderNormMng_FactoryFinishedProductNorm_View.Where(o => o.ModelID == modelID).ToList();

        //                //init edit form info
        //                editFormData.Data = new DTO.FactoryOrderNorm();
        //                editFormData.Data.Season = Library.Helper.GetCurrentSeason();
        //                editFormData.Data.ClientID = clientID;
        //                editFormData.Data.ProductID = productID;
        //                editFormData.Data.ClientUD = client.ClientUD;
        //                editFormData.Data.ArticleCode = clientProduct.ArticleCode;
        //                editFormData.Data.Description = clientProduct.Description;
        //                editFormData.Data.ModelUD = product.ModelUD;
        //                editFormData.Data.ModelNM = product.ModelNM;

        //                //init default material norm
        //                editFormData.Data.FactoryFinishedProductOrderNorms = new List<DTO.FactoryFinishedProductOrderNorm>();
        //                DTO.FactoryFinishedProductOrderNorm finishedProductNorm;
        //                DTO.FactoryMaterialOrderNorm materialNorm;

        //                int i = -1;
        //                foreach (var item in defaultNorm)
        //                {
        //                    finishedProductNorm = new DTO.FactoryFinishedProductOrderNorm();
        //                    finishedProductNorm.FactoryMaterialOrderNorms = new List<DTO.FactoryMaterialOrderNorm>();

        //                    finishedProductNorm.FactoryFinishedProductID = item.FactoryFinishedProductID;
        //                    finishedProductNorm.NormValue = item.NormValue;
        //                    finishedProductNorm.UnitID = item.UnitID;
        //                    finishedProductNorm.FactoryFinishedProductUD = item.FactoryFinishedProductUD;
        //                    finishedProductNorm.FactoryFinishedProductNM = item.FactoryFinishedProductNM;
        //                    finishedProductNorm.IsEditing = false;

        //                    foreach (var mItem in item.FactoryOrderNormMng_FactoryMaterialNorm_View)
        //                    {
        //                        materialNorm = new DTO.FactoryMaterialOrderNorm();
        //                        materialNorm.FactoryMaterialID = mItem.FactoryMaterialID;
        //                        materialNorm.NormValue = mItem.NormValue;
        //                        materialNorm.UnitID = mItem.UnitID;
        //                        materialNorm.FactoryMaterialUD = mItem.FactoryMaterialUD;
        //                        materialNorm.FactoryMaterialNM = mItem.FactoryMaterialNM;
        //                        materialNorm.UnitNM = mItem.UnitNM;
        //                        materialNorm.IsEditing = true;
        //                        finishedProductNorm.FactoryMaterialOrderNorms.Add(materialNorm);
        //                    }
        //                    editFormData.Data.FactoryFinishedProductOrderNorms.Add(finishedProductNorm);
        //                    i--;
        //                }
        //            }
        //            //get support list
        //            editFormData.Units = support_factory.GetUnit(1);
        //            editFormData.Seasons = support_factory.GetSeason();
        //            //editFormData.FactoryGoodsProcedures = support_factory.GetFactoryGoodsProcedure();
        //            return editFormData;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification.Type = Library.DTO.NotificationType.Error;
        //        notification.Message = ex.Message;
        //        notification.DetailMessage.Add(ex.Message);
        //        if (ex.GetBaseException() != null)
        //        {
        //            notification.DetailMessage.Add(ex.GetBaseException().Message);
        //        }
        //        return editFormData;
        //    }
        //}

        public int CreateOrderNorm(string season, int? clientID, int? productID, out Library.DTO.Notification notification)
        {
            DTO.EditFormData editFormData = new DTO.EditFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Create BOM Of Order success"};
            try
            {
                if (string.IsNullOrEmpty(season) || season == "null")
                {
                    throw new Exception("Season is empty. You have to select season");
                }
                if (!clientID.HasValue)
                {
                    throw new Exception("Client is empty. You have to select client");
                }
                if (!productID.HasValue)
                {
                    throw new Exception("Product is empty. You have to select product");
                }
                using (FactoryOrderNormEntities context = CreateContext())
                {
                    var product = context.FactoryOrderNormMng_Product_View.Where(o => o.ProductID == productID).FirstOrDefault();
                    int? modelID = product.ModelID;
                    var defaultNorm = context.FactoryOrderNormMng_FactoryFinishedProductNorm_View.Where(o => o.ModelID == modelID).ToList();

                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            //db
                            FactoryOrderNorm orderNorm = new FactoryOrderNorm();
                            context.FactoryOrderNorm.Add(orderNorm);

                            //init edit form info
                            orderNorm.Season = Library.Helper.GetCurrentSeason();
                            orderNorm.ClientID = clientID;
                            orderNorm.ProductID = productID;

                            //init default material norm
                            FactoryFinishedProductOrderNorm finishedProductNorm;
                            FactoryMaterialOrderNorm materialNorm;
                            FactoryFinishedProductOrderNormFactoryGoodsProcedure goodsProcedure;

                            List<DTO.TempComponentNorm> tempNormIds = new List<DTO.TempComponentNorm>();
                            foreach (var item in defaultNorm.Where(o =>o.ParentNormID == null))
                            {
                                finishedProductNorm = new FactoryFinishedProductOrderNorm();
                                orderNorm.FactoryFinishedProductOrderNorm.Add(finishedProductNorm);

                                finishedProductNorm.FactoryFinishedProductID = item.FactoryFinishedProductID;
                                finishedProductNorm.NormValue = item.NormValue;
                                finishedProductNorm.UnitID = item.UnitID;
                                finishedProductNorm.MaterialGroupTypeID = item.MaterialGroupTypeID;

                                foreach (var mItem in item.FactoryOrderNormMng_FactoryMaterialNorm_View)
                                {
                                    materialNorm = new FactoryMaterialOrderNorm();
                                    finishedProductNorm.FactoryMaterialOrderNorm.Add(materialNorm);

                                    materialNorm.FactoryMaterialID = mItem.FactoryMaterialID;
                                    materialNorm.NormValue = mItem.NormValue;
                                    materialNorm.UnitID = mItem.UnitID;
                                }

                                foreach (var pItem in item.FactoryOrderNormMng_FactoryFinishedProductNormFactoryGoodsProcedure_View)
                                {
                                    goodsProcedure = new FactoryFinishedProductOrderNormFactoryGoodsProcedure();
                                    finishedProductNorm.FactoryFinishedProductOrderNormFactoryGoodsProcedure.Add(goodsProcedure);

                                    goodsProcedure.FactoryGoodsProcedureID = pItem.FactoryGoodsProcedureID;
                                    goodsProcedure.IsDefault = pItem.IsDefault;
                                }

                                context.SaveChanges();
                                tempNormIds.Add(new DTO.TempComponentNorm { FinishedProductNormID = item.FactoryFinishedProductNormID, FinishedProductOrderNormID = finishedProductNorm.FactoryFinishedProductOrderNormID});
                            }

                            foreach (var item in defaultNorm.Where(o => o.ParentNormID != null))
                            {
                                finishedProductNorm = new FactoryFinishedProductOrderNorm();
                                orderNorm.FactoryFinishedProductOrderNorm.Add(finishedProductNorm);

                                finishedProductNorm.FactoryFinishedProductID = item.FactoryFinishedProductID;
                                finishedProductNorm.NormValue = item.NormValue;
                                finishedProductNorm.UnitID = item.UnitID;
                                finishedProductNorm.ParentNormID = tempNormIds.Where(o => o.FinishedProductNormID == item.ParentNormID).FirstOrDefault().FinishedProductOrderNormID;
                                finishedProductNorm.MaterialGroupTypeID = item.MaterialGroupTypeID;

                                foreach (var mItem in item.FactoryOrderNormMng_FactoryMaterialNorm_View)
                                {
                                    materialNorm = new FactoryMaterialOrderNorm();
                                    finishedProductNorm.FactoryMaterialOrderNorm.Add(materialNorm);

                                    materialNorm.FactoryMaterialID = mItem.FactoryMaterialID;
                                    materialNorm.NormValue = mItem.NormValue;
                                    materialNorm.UnitID = mItem.UnitID;
                                }

                                foreach (var pItem in item.FactoryOrderNormMng_FactoryFinishedProductNormFactoryGoodsProcedure_View)
                                {
                                    goodsProcedure = new FactoryFinishedProductOrderNormFactoryGoodsProcedure();
                                    finishedProductNorm.FactoryFinishedProductOrderNormFactoryGoodsProcedure.Add(goodsProcedure);

                                    goodsProcedure.FactoryGoodsProcedureID = pItem.FactoryGoodsProcedureID;
                                    goodsProcedure.IsDefault = pItem.IsDefault;
                                }
                            }
                            context.SaveChanges();
                            dbContextTransaction.Commit();
                            return orderNorm.FactoryOrderNormID;
                        }
                        catch (Exception ex)
                        {
                            dbContextTransaction.Rollback();
                            throw new Exception(ex.ToString());
                        }
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
                return -1;
            }
        }

        public int CreateComponentNorm(int factoryOrderNormID, DTO.FactoryFinishedProductOrderNorm dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Update component norm success" };
            try
            {
                if (!dtoItem.FactoryFinishedProductID.HasValue)
                {
                    throw new Exception("You have to select component");
                }
                if (!dtoItem.NormValue.HasValue)
                {
                    throw new Exception("You have to fill-in norm value");
                }
                if (dtoItem.FactoryFinishedProductOrderNormFactoryGoodsProcedures == null || dtoItem.FactoryFinishedProductOrderNormFactoryGoodsProcedures.Count()==0)
                {
                    throw new Exception("You have to fill-in procedure");
                }
                if (dtoItem.FactoryFinishedProductOrderNormFactoryGoodsProcedures.Count() > 0)
                {
                    if (dtoItem.FactoryFinishedProductOrderNormFactoryGoodsProcedures.Where(o => o.IsDefault.HasValue && o.IsDefault.Value).Count() == 0)
                    {
                        throw new Exception("You need select dedault procedure");
                    }
                    if (dtoItem.FactoryFinishedProductOrderNormFactoryGoodsProcedures.Where(o => o.IsDefault.HasValue && o.IsDefault.Value).Count() > 1)
                    {
                        throw new Exception("Only one procedure is default");
                    }
                }
                using (FactoryOrderNormEntities context = CreateContext())
                {
                    FactoryFinishedProductOrderNorm dbItem;
                    FactoryFinishedProductOrderNormFactoryGoodsProcedure dbGoodsProcedure;
                    if (dtoItem.FactoryFinishedProductOrderNormID > 0)
                    {
                        dbItem = context.FactoryFinishedProductOrderNorm.Where(o => o.FactoryFinishedProductOrderNormID == dtoItem.FactoryFinishedProductOrderNormID).FirstOrDefault();

                        //goods procedure opreration
                        foreach (var item in dbItem.FactoryFinishedProductOrderNormFactoryGoodsProcedure.ToArray())
                        {
                            if (!dtoItem.FactoryFinishedProductOrderNormFactoryGoodsProcedures.Select(s => s.FactoryFinishedProductOrderNormFactoryGoodsProcedureID).Contains(item.FactoryFinishedProductOrderNormFactoryGoodsProcedureID))
                            {
                                dbItem.FactoryFinishedProductOrderNormFactoryGoodsProcedure.Remove(item);
                            }
                        }

                        foreach (var item in dtoItem.FactoryFinishedProductOrderNormFactoryGoodsProcedures)
                        {
                            if (item.FactoryFinishedProductOrderNormFactoryGoodsProcedureID > 0)
                            {
                                dbGoodsProcedure = dbItem.FactoryFinishedProductOrderNormFactoryGoodsProcedure.Where(o => o.FactoryFinishedProductOrderNormFactoryGoodsProcedureID == item.FactoryFinishedProductOrderNormFactoryGoodsProcedureID).FirstOrDefault();
                            }
                            else
                            {
                                dbGoodsProcedure = new FactoryFinishedProductOrderNormFactoryGoodsProcedure();
                                dbItem.FactoryFinishedProductOrderNormFactoryGoodsProcedure.Add(dbGoodsProcedure);
                            }
                            if (dbGoodsProcedure != null)
                            {
                                dbGoodsProcedure.FactoryGoodsProcedureID = item.FactoryGoodsProcedureID;
                                dbGoodsProcedure.IsDefault = item.IsDefault;
                            }
                        }
                    }
                    else
                    {
                        dbItem = new FactoryFinishedProductOrderNorm();
                        context.FactoryFinishedProductOrderNorm.Add(dbItem);
                        dbItem.FactoryOrderNormID = factoryOrderNormID;

                        if (dtoItem.FactoryFinishedProductOrderNormFactoryGoodsProcedures != null)
                        {
                            foreach (var item in dtoItem.FactoryFinishedProductOrderNormFactoryGoodsProcedures)
                            {
                                dbGoodsProcedure = new FactoryFinishedProductOrderNormFactoryGoodsProcedure();
                                dbItem.FactoryFinishedProductOrderNormFactoryGoodsProcedure.Add(dbGoodsProcedure);
                                dbGoodsProcedure.FactoryGoodsProcedureID = item.FactoryGoodsProcedureID;
                                dbGoodsProcedure.IsDefault = item.IsDefault;
                            }
                        }
                    }
                    if (dbItem != null)
                    {
                        dbItem.FactoryFinishedProductID = dtoItem.FactoryFinishedProductID;
                        dbItem.NormValue = dtoItem.NormValue;
                        dbItem.MaterialGroupTypeID = dtoItem.MaterialGroupTypeID;
                    }
                    else
                    {
                        throw new Exception("Could not find data. You have to save data before update component norm");
                    }
                    //remove orphan item
                    context.FactoryFinishedProductOrderNormFactoryGoodsProcedure.Local.Where(o => o.FactoryFinishedProductOrderNorm == null).ToList().ForEach(o => context.FactoryFinishedProductOrderNormFactoryGoodsProcedure.Remove(o));
                    context.SaveChanges();
                    return dbItem.FactoryFinishedProductOrderNormID;
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
                return -1;
            }
        }

        public int CreateSubComponentNorm(int parentFactoryFinishedProductNormID, DTO.FactoryFinishedProductOrderNorm dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Update sub component norm success" };
            try
            {
                if (!dtoItem.FactoryFinishedProductID.HasValue)
                {
                    throw new Exception("You have to select component");
                }
                if (!dtoItem.NormValue.HasValue)
                {
                    throw new Exception("You have to fill-in norm value");
                }
                if (dtoItem.FactoryFinishedProductOrderNormFactoryGoodsProcedures == null || dtoItem.FactoryFinishedProductOrderNormFactoryGoodsProcedures.Count() == 0)
                {
                    throw new Exception("You have to fill-in procedure");
                }
                if (dtoItem.FactoryFinishedProductOrderNormFactoryGoodsProcedures.Count() > 0)
                {
                    if (dtoItem.FactoryFinishedProductOrderNormFactoryGoodsProcedures.Where(o => o.IsDefault.HasValue && o.IsDefault.Value).Count() == 0)
                    {
                        throw new Exception("You need select dedault procedure");
                    }
                    if (dtoItem.FactoryFinishedProductOrderNormFactoryGoodsProcedures.Where(o => o.IsDefault.HasValue && o.IsDefault.Value).Count() > 1)
                    {
                        throw new Exception("Only one procedure is default");
                    }
                }
                using (FactoryOrderNormEntities context = CreateContext())
                {
                    FactoryFinishedProductOrderNorm dbItem;
                    FactoryFinishedProductOrderNormFactoryGoodsProcedure dbGoodsProcedure;
                    if (dtoItem.FactoryFinishedProductOrderNormID > 0)
                    {
                        dbItem = context.FactoryFinishedProductOrderNorm.Where(o => o.FactoryFinishedProductOrderNormID == dtoItem.FactoryFinishedProductOrderNormID).FirstOrDefault();

                        //goods procedure opreration
                        foreach (var item in dbItem.FactoryFinishedProductOrderNormFactoryGoodsProcedure.ToArray())
                        {
                            if (!dtoItem.FactoryFinishedProductOrderNormFactoryGoodsProcedures.Select(s => s.FactoryFinishedProductOrderNormFactoryGoodsProcedureID).Contains(item.FactoryFinishedProductOrderNormFactoryGoodsProcedureID))
                            {
                                dbItem.FactoryFinishedProductOrderNormFactoryGoodsProcedure.Remove(item);
                            }
                        }

                        foreach (var item in dtoItem.FactoryFinishedProductOrderNormFactoryGoodsProcedures)
                        {
                            if (item.FactoryFinishedProductOrderNormFactoryGoodsProcedureID > 0)
                            {
                                dbGoodsProcedure = dbItem.FactoryFinishedProductOrderNormFactoryGoodsProcedure.Where(o => o.FactoryFinishedProductOrderNormFactoryGoodsProcedureID == item.FactoryFinishedProductOrderNormFactoryGoodsProcedureID).FirstOrDefault();
                            }
                            else
                            {
                                dbGoodsProcedure = new FactoryFinishedProductOrderNormFactoryGoodsProcedure();
                                dbItem.FactoryFinishedProductOrderNormFactoryGoodsProcedure.Add(dbGoodsProcedure);
                            }
                            if (dbGoodsProcedure != null)
                            {
                                dbGoodsProcedure.FactoryGoodsProcedureID = item.FactoryGoodsProcedureID;
                                dbGoodsProcedure.IsDefault = item.IsDefault;
                            }
                        }
                    }
                    else
                    {
                        var parentComponent = context.FactoryFinishedProductOrderNorm.FirstOrDefault(o => o.FactoryFinishedProductOrderNormID == parentFactoryFinishedProductNormID);
                        dbItem = new FactoryFinishedProductOrderNorm();
                        context.FactoryFinishedProductOrderNorm.Add(dbItem);
                        dbItem.FactoryOrderNormID = parentComponent.FactoryOrderNormID;
                        dbItem.ParentNormID = parentFactoryFinishedProductNormID;

                        if (dtoItem.FactoryFinishedProductOrderNormFactoryGoodsProcedures != null)
                        {
                            foreach (var item in dtoItem.FactoryFinishedProductOrderNormFactoryGoodsProcedures)
                            {
                                dbGoodsProcedure = new FactoryFinishedProductOrderNormFactoryGoodsProcedure();
                                dbItem.FactoryFinishedProductOrderNormFactoryGoodsProcedure.Add(dbGoodsProcedure);
                                dbGoodsProcedure.FactoryGoodsProcedureID = item.FactoryGoodsProcedureID;
                                dbGoodsProcedure.IsDefault = item.IsDefault;
                            }
                        }
                    }

                    if (dbItem != null)
                    {
                        dbItem.FactoryFinishedProductID = dtoItem.FactoryFinishedProductID;
                        dbItem.NormValue = dtoItem.NormValue;
                        dbItem.MaterialGroupTypeID = dtoItem.MaterialGroupTypeID;
                    }
                    else
                    {
                        throw new Exception("Could not find data. You have to save data before update sub component norm");
                    }
                    context.FactoryFinishedProductOrderNormFactoryGoodsProcedure.Local.Where(o => o.FactoryFinishedProductOrderNorm == null).ToList().ForEach(o => context.FactoryFinishedProductOrderNormFactoryGoodsProcedure.Remove(o));
                    context.SaveChanges();
                    return dbItem.FactoryFinishedProductOrderNormID;
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
                return -1;
            }
        }

        public int CreateFactoryMaterialNorm(int factoryFinishedProductNormID, DTO.FactoryMaterialOrderNorm dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Update material norm success" };
            try
            {
                if (!dtoItem.FactoryMaterialID.HasValue)
                {
                    throw new Exception("You have to select material");
                }
                if (!dtoItem.NormValue.HasValue)
                {
                    throw new Exception("You have to fill-in norm value");
                }
                using (FactoryOrderNormEntities context = CreateContext())
                {
                    FactoryMaterialOrderNorm dbItem;
                    if (dtoItem.FactoryMaterialOrderNormID > 0)
                    {
                        dbItem = context.FactoryMaterialOrderNorm.Where(o => o.FactoryMaterialOrderNormID == dtoItem.FactoryMaterialOrderNormID).FirstOrDefault();
                    }
                    else
                    {
                        dbItem = new FactoryMaterialOrderNorm();
                        dbItem.FactoryFinishedProductOrderNorm = context.FactoryFinishedProductOrderNorm.FirstOrDefault(o => o.FactoryFinishedProductOrderNormID == factoryFinishedProductNormID);
                        context.FactoryMaterialOrderNorm.Add(dbItem);
                    }
                    if (dbItem != null)
                    {
                        dbItem.FactoryMaterialID = dtoItem.FactoryMaterialID;
                        dbItem.NormValue = dtoItem.NormValue;
                        dbItem.UnitID = dtoItem.UnitID;
                    }
                    else
                    {
                        throw new Exception("Could not find data. You have to save data before update material norm");
                    }
                    context.SaveChanges();
                    return dbItem.FactoryMaterialOrderNormID;
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
                return -1;
            }
        }

        public int DeleteFinishedProductNorm(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success, Message = "Delete success" };
            try
            {
                using (FactoryOrderNormEntities context = CreateContext())
                {
                    var dbSub = context.FactoryFinishedProductOrderNorm.Where(o => o.ParentNormID == id);
                    if (dbSub != null && dbSub.Count() > 0)
                    {
                        throw new Exception("You have to delete sub component first");
                    }
                    var dbItem = context.FactoryFinishedProductOrderNorm.FirstOrDefault(o => o.FactoryFinishedProductOrderNormID == id);

                    foreach (var cItem in dbItem.FactoryFinishedProductOrderNormFactoryGoodsProcedure.ToArray())
                    {
                        dbItem.FactoryFinishedProductOrderNormFactoryGoodsProcedure.Remove(cItem);
                    }
                    context.FactoryFinishedProductOrderNormFactoryGoodsProcedure.Local.Where(o => o.FactoryFinishedProductOrderNorm == null).ToList().ForEach(o => context.FactoryFinishedProductOrderNormFactoryGoodsProcedure.Remove(o));
                    context.FactoryFinishedProductOrderNorm.Remove(dbItem);
                    context.SaveChanges();
                }
                return id;
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
                return -1;
            }
        }
        
        public int DeleteMaterialNorm(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success, Message = "Delete success" };
            try
            {
                using (FactoryOrderNormEntities context = CreateContext())
                {
                    var dbItem = context.FactoryMaterialOrderNorm.FirstOrDefault(o => o.FactoryMaterialOrderNormID == id);
                    context.FactoryMaterialOrderNorm.Remove(dbItem);
                    context.SaveChanges();
                }
                return id;
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
                return -1;
            }
        }
    }
}

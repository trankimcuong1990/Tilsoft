using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
namespace Module.FactoryNorm.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private DataConverter converter = new DataConverter();

        private FactoryNormEntities CreateContext()
        {
            return new FactoryNormEntities(Library.Helper.CreateEntityConnectionString("DAL.FactoryNormModel"));
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
                using (FactoryNormEntities context = CreateContext())
                {
                    var dbItem = context.FactoryNorm.Where(o => o.FactoryNormID == id).FirstOrDefault();
                    foreach (var item in dbItem.FactoryFinishedProductNorm.ToArray())
                    {
                        foreach (var mItem in item.FactoryMaterialNorm)
                        {
                            context.FactoryMaterialNorm.Remove(mItem);
                        }
                        context.FactoryFinishedProductNorm.Remove(item);
                    }
                    context.FactoryNorm.Remove(dbItem);
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
                using (FactoryNormEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        FactoryNormMng_FactoryNorm_View dbItem;
                        dbItem = context.FactoryNormMng_FactoryNorm_View.FirstOrDefault(o => o.FactoryNormID == id);
                        editFormData.Data = converter.DB2DTO_FactoryNorm(dbItem);
                    }
                    else
                    {
                        editFormData.Data = new DTO.FactoryNorm();
                        editFormData.Data.FactoryFinishedProductNorms = new List<DTO.FactoryFinishedProductNorm>();
                        foreach (var item in editFormData.Data.FactoryFinishedProductNorms)
                        {
                            item.FactoryMaterialNorms = new List<DTO.FactoryMaterialNorm>();
                        }
                    }
                    //get support list
                    //editFormData.Units = support_factory.GetUnit(1);
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

            string modelUD = string.Empty;
            string modelNM = string.Empty;
            if (filters.ContainsKey("modelUD")) modelUD = filters["modelUD"].ToString();
            if (filters.ContainsKey("modelNM")) modelNM = filters["modelNM"].ToString();

            try
            {
                using (FactoryNormEntities context = CreateContext())
                {
                    totalRows = context.FactoryNormMng_function_SearchFactoryNorm(orderBy, orderDirection, modelUD, modelNM).Count();
                    var result = context.FactoryNormMng_function_SearchFactoryNorm(orderBy, orderDirection, modelUD, modelNM);
                    searchFormData.Data = converter.DB2DTO_FactoryNormSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
            DTO.FactoryNorm dtoFactoryNorm = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.FactoryNorm>();
            try
            {
                using (FactoryNormEntities context = CreateContext())
                {
                    FactoryNorm dbItem = null;
                    if (id > 0)
                    {
                        dbItem = context.FactoryNorm.Where(o => o.FactoryNormID == id).FirstOrDefault();
                    }
                    else
                    {
                        dbItem = new FactoryNorm();
                        context.FactoryNorm.Add(dbItem);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "data not found!";
                        return false;
                    }
                    else
                    {
                        //convert dto to db
                        converter.DTO2DB_FactoryNorm(dtoFactoryNorm, ref dbItem);
                        //remove orphan item
                        context.FactoryMaterialNorm.Local.Where(o => o.FactoryFinishedProductNorm == null).ToList().ForEach(o => context.FactoryMaterialNorm.Remove(o));
                        context.FactoryFinishedProductNorm.Local.Where(o => o.FactoryNorm == null).ToList().ForEach(o => context.FactoryFinishedProductNorm.Remove(o));
                        //save data
                        context.SaveChanges();
                        //get return data
                        dtoItem = GetData(dbItem.FactoryNormID, out notification).Data;
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
                using (FactoryNormEntities context = CreateContext())
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

        public int CreateComponentNorm(int factoryNormID, DTO.FactoryFinishedProductNorm dtoItem, out Library.DTO.Notification notification)
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
                if (dtoItem.FactoryFinishedProductNormFactoryGoodsProcedures == null || dtoItem.FactoryFinishedProductNormFactoryGoodsProcedures.Count() == 0)
                {
                    throw new Exception("You have to fill-in procedure");
                }
                if (dtoItem.FactoryFinishedProductNormFactoryGoodsProcedures.Count() > 0)
                {
                    if (dtoItem.FactoryFinishedProductNormFactoryGoodsProcedures.Where(o => o.IsDefault.HasValue && o.IsDefault.Value).Count() == 0)
                    {
                        throw new Exception("You need select dedault procedure");
                    }
                    if (dtoItem.FactoryFinishedProductNormFactoryGoodsProcedures.Where(o => o.IsDefault.HasValue && o.IsDefault.Value).Count() > 1)
                    {
                        throw new Exception("Only one procedure is default");
                    }
                }
                using (FactoryNormEntities context = CreateContext())
                {
                    FactoryFinishedProductNorm dbItem;
                    FactoryFinishedProductNormFactoryGoodsProcedure dbGoodsProcedure;
                    if (dtoItem.FactoryFinishedProductNormID > 0)
                    {
                        dbItem = context.FactoryFinishedProductNorm.Where(o => o.FactoryFinishedProductNormID == dtoItem.FactoryFinishedProductNormID).FirstOrDefault();

                        //goods procedure opreration
                        foreach (var item in dbItem.FactoryFinishedProductNormFactoryGoodsProcedure.ToArray())
                        {
                            if (!dtoItem.FactoryFinishedProductNormFactoryGoodsProcedures.Select(s => s.FactoryFinishedProductNormFactoryGoodsProcedureID).Contains(item.FactoryFinishedProductNormFactoryGoodsProcedureID))
                            {
                                dbItem.FactoryFinishedProductNormFactoryGoodsProcedure.Remove(item);
                            }
                        }

                        foreach (var item in dtoItem.FactoryFinishedProductNormFactoryGoodsProcedures)
                        {
                            if (item.FactoryFinishedProductNormFactoryGoodsProcedureID > 0)
                            {
                                dbGoodsProcedure = dbItem.FactoryFinishedProductNormFactoryGoodsProcedure.Where(o => o.FactoryFinishedProductNormFactoryGoodsProcedureID == item.FactoryFinishedProductNormFactoryGoodsProcedureID).FirstOrDefault();
                            }
                            else
                            {
                                dbGoodsProcedure = new FactoryFinishedProductNormFactoryGoodsProcedure();
                                dbItem.FactoryFinishedProductNormFactoryGoodsProcedure.Add(dbGoodsProcedure);
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
                        dbItem = new FactoryFinishedProductNorm();
                        context.FactoryFinishedProductNorm.Add(dbItem);
                        dbItem.FactoryNormID = factoryNormID;

                        if (dtoItem.FactoryFinishedProductNormFactoryGoodsProcedures != null)
                        {
                            foreach (var item in dtoItem.FactoryFinishedProductNormFactoryGoodsProcedures)
                            {
                                dbGoodsProcedure = new FactoryFinishedProductNormFactoryGoodsProcedure();
                                dbItem.FactoryFinishedProductNormFactoryGoodsProcedure.Add(dbGoodsProcedure);
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
                    context.FactoryFinishedProductNormFactoryGoodsProcedure.Local.Where(o => o.FactoryFinishedProductNorm == null).ToList().ForEach(o => context.FactoryFinishedProductNormFactoryGoodsProcedure.Remove(o));
                    context.SaveChanges();
                    return dbItem.FactoryFinishedProductNormID;
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

        public int CreateSubComponentNorm(int parentFactoryFinishedProductNormID, DTO.FactoryFinishedProductNorm dtoItem, out Library.DTO.Notification notification)
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
                if (dtoItem.FactoryFinishedProductNormFactoryGoodsProcedures == null || dtoItem.FactoryFinishedProductNormFactoryGoodsProcedures.Count() == 0)
                {
                    throw new Exception("You have to fill-in procedure");
                }
                if (dtoItem.FactoryFinishedProductNormFactoryGoodsProcedures.Count() > 0)
                {
                    if (dtoItem.FactoryFinishedProductNormFactoryGoodsProcedures.Where(o => o.IsDefault.HasValue && o.IsDefault.Value).Count() == 0)
                    {
                        throw new Exception("You need select dedault procedure");
                    }
                    if (dtoItem.FactoryFinishedProductNormFactoryGoodsProcedures.Where(o => o.IsDefault.HasValue && o.IsDefault.Value).Count() > 1)
                    {
                        throw new Exception("Only one procedure is default");
                    }
                }
                using (FactoryNormEntities context = CreateContext())
                {
                    FactoryFinishedProductNorm dbItem;
                    FactoryFinishedProductNormFactoryGoodsProcedure dbGoodsProcedure;
                    if (dtoItem.FactoryFinishedProductNormID > 0)
                    {
                        dbItem = context.FactoryFinishedProductNorm.Where(o => o.FactoryFinishedProductNormID == dtoItem.FactoryFinishedProductNormID).FirstOrDefault();

                        //goods procedure opreration
                        foreach (var item in dbItem.FactoryFinishedProductNormFactoryGoodsProcedure.ToArray())
                        {
                            if (!dtoItem.FactoryFinishedProductNormFactoryGoodsProcedures.Select(s => s.FactoryFinishedProductNormFactoryGoodsProcedureID).Contains(item.FactoryFinishedProductNormFactoryGoodsProcedureID))
                            {
                                dbItem.FactoryFinishedProductNormFactoryGoodsProcedure.Remove(item);
                            }
                        }

                        foreach (var item in dtoItem.FactoryFinishedProductNormFactoryGoodsProcedures)
                        {
                            if (item.FactoryFinishedProductNormFactoryGoodsProcedureID > 0)
                            {
                                dbGoodsProcedure = dbItem.FactoryFinishedProductNormFactoryGoodsProcedure.Where(o => o.FactoryFinishedProductNormFactoryGoodsProcedureID == item.FactoryFinishedProductNormFactoryGoodsProcedureID).FirstOrDefault();
                            }
                            else
                            {
                                dbGoodsProcedure = new FactoryFinishedProductNormFactoryGoodsProcedure();
                                dbItem.FactoryFinishedProductNormFactoryGoodsProcedure.Add(dbGoodsProcedure);
                            }
                            if (dbGoodsProcedure != null)
                            {
                                dbGoodsProcedure.FactoryGoodsProcedureID = item.FactoryGoodsProcedureID;
                                dbGoodsProcedure.IsDefault = item.IsDefault;
                            }
                        }
                    }
                    else {
                        var parentComponent = context.FactoryFinishedProductNorm.FirstOrDefault(o => o.FactoryFinishedProductNormID == parentFactoryFinishedProductNormID);
                        dbItem = new FactoryFinishedProductNorm();
                        context.FactoryFinishedProductNorm.Add(dbItem);
                        dbItem.FactoryNormID = parentComponent.FactoryNormID;
                        dbItem.ParentNormID = parentFactoryFinishedProductNormID;

                        if (dtoItem.FactoryFinishedProductNormFactoryGoodsProcedures != null)
                        {
                            foreach (var item in dtoItem.FactoryFinishedProductNormFactoryGoodsProcedures)
                            {
                                dbGoodsProcedure = new FactoryFinishedProductNormFactoryGoodsProcedure();
                                dbItem.FactoryFinishedProductNormFactoryGoodsProcedure.Add(dbGoodsProcedure);
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
                    context.FactoryFinishedProductNormFactoryGoodsProcedure.Local.Where(o => o.FactoryFinishedProductNorm == null).ToList().ForEach(o => context.FactoryFinishedProductNormFactoryGoodsProcedure.Remove(o));
                    context.SaveChanges();
                    return dbItem.FactoryFinishedProductNormID;
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

        public int CreateFactoryMaterialNorm(int factoryFinishedProductNormID, DTO.FactoryMaterialNorm dtoItem, out Library.DTO.Notification notification)
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
                using (FactoryNormEntities context = CreateContext())
                {
                    FactoryMaterialNorm dbItem;
                    if (dtoItem.FactoryMaterialNormID > 0)
                    {
                        dbItem = context.FactoryMaterialNorm.Where(o => o.FactoryMaterialNormID == dtoItem.FactoryMaterialNormID).FirstOrDefault();
                    }
                    else {
                        dbItem = new FactoryMaterialNorm();
                        dbItem.FactoryFinishedProductNorm = context.FactoryFinishedProductNorm.FirstOrDefault(o => o.FactoryFinishedProductNormID == factoryFinishedProductNormID);
                        context.FactoryMaterialNorm.Add(dbItem);
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
                    return dbItem.FactoryMaterialNormID;
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
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success, Message="Delete success" };
            try
            {
                using (FactoryNormEntities context = CreateContext())
                {
                    var dbSub = context.FactoryFinishedProductNorm.Where(o => o.ParentNormID == id);
                    if (dbSub != null && dbSub.Count() > 0)
                    {
                        throw new Exception("You have to delete sub component first");
                    }
                    var dbItem = context.FactoryFinishedProductNorm.FirstOrDefault(o =>o.FactoryFinishedProductNormID == id);
                    foreach (var cItem in dbItem.FactoryFinishedProductNormFactoryGoodsProcedure.ToArray())
                    {
                        dbItem.FactoryFinishedProductNormFactoryGoodsProcedure.Remove(cItem);
                    }
                    context.FactoryFinishedProductNormFactoryGoodsProcedure.Local.Where(o => o.FactoryFinishedProductNorm == null).ToList().ForEach(o => context.FactoryFinishedProductNormFactoryGoodsProcedure.Remove(o));
                    context.FactoryFinishedProductNorm.Remove(dbItem);
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
                using (FactoryNormEntities context = CreateContext())
                {
                    var dbItem = context.FactoryMaterialNorm.FirstOrDefault(o => o.FactoryMaterialNormID == id);
                    context.FactoryMaterialNorm.Remove(dbItem);
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

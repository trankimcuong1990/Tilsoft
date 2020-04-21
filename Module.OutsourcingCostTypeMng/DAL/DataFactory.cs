using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;
using Module.OutsourcingCostTypeMng.DTO;

namespace Module.OutsourcingCostTypeMng.DAL
{
    public class DataFactory
    {
        private DataConverter converter = new DataConverter();
        public DataFactory() { }
        private OutsourcingCostTypeMngEntities CreateContext()
        {
            return new OutsourcingCostTypeMngEntities(Library.Helper.CreateEntityConnectionString("DAL.OutsourcingCostTypeMngModel"));
        }
       
        public bool DeleteData(int id, out Notification notification)
        {
            notification = new Notification { Type = NotificationType.Success };
            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.OutsourcingCostType.Where(o => o.OutsourcingCostTypeID == id).FirstOrDefault();
                    context.OutsourcingCostType.Remove(dbItem);
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

        public EditFormData GetData(int id, out Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new DTO.OutsourcingCostTypeDTO();
            data.ProductionItemGroups = new List<ProductionItemGroup>();
            //try to get data
            try
            {
                using (OutsourcingCostTypeMngEntities context = CreateContext())
                {
                    if (id != 0)
                    {
                        data.Data = converter.DB2DTO_OutSourcingCostType(context.OutsourcingCostTypeMng_OutSourcingCostTypeMng_View.FirstOrDefault(o => o.OutsourcingCostTypeID == id));
                    }
                    data.ProductionItemGroups = converter.DB2DTO_GetProductionItemGroup(context.OutsourcingCostTypeMng_ProductionItemGroup_View.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public SearchFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            DTO.SearchFormData searchFormData = new DTO.SearchFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            try
            {
                string outSourcingCostTypeUD = "";
                string outSourcingCostTypeNM = "";
                string outSourcingCostTypeNMEN = "";
                int? productionItemGroupID = null;


                if (filters.ContainsKey("outsourcingCostTypeUD") && !string.IsNullOrEmpty(filters["outsourcingCostTypeUD"].ToString()))
                {
                    outSourcingCostTypeUD = filters["outsourcingCostTypeUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("outsourcingCostTypeNM") && !string.IsNullOrEmpty(filters["outsourcingCostTypeNM"].ToString()))
                {
                    outSourcingCostTypeNM = filters["outsourcingCostTypeNM"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("outsourcingCostTypeNMEN") && !string.IsNullOrEmpty(filters["outsourcingCostTypeNMEN"].ToString()))
                {
                    outSourcingCostTypeNMEN = filters["outsourcingCostTypeNMEN"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("productionItemGroupID") && !string.IsNullOrEmpty(filters["productionItemGroupID"].ToString()))
                {
                    productionItemGroupID = Convert.ToInt32(filters["productionItemGroupID"]);
                }

                using (OutsourcingCostTypeMngEntities context = CreateContext())
                {
                    totalRows = context.OutsourcingCostTypeMng_function_SearchOutSourcingCostTypeMng(outSourcingCostTypeUD, outSourcingCostTypeNM, outSourcingCostTypeNMEN, productionItemGroupID, orderBy, orderDirection).Count();
                    var result = context.OutsourcingCostTypeMng_function_SearchOutSourcingCostTypeMng(outSourcingCostTypeUD, outSourcingCostTypeNM, outSourcingCostTypeNMEN, productionItemGroupID, orderBy, orderDirection);
                    searchFormData.Data = converter.DB2DTO_OutSourcingCostTypeSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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

        public DTO.InitData GetInitData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.InitData data = new DTO.InitData();
            data.ProductionItemGroups = new List<ProductionItemGroup>();
            try
            {
                using (OutsourcingCostTypeMngEntities context = CreateContext())
                {
                    data.ProductionItemGroups = converter.DB2DTO_GetProductionItemGroup(context.OutsourcingCostTypeMng_ProductionItemGroup_View.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }
            return data;
        }

        public bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.OutsourcingCostTypeDTO dtoOutSourcingCostType = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.OutsourcingCostTypeDTO>();
            try
            {
                ////get companyID
                //Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                //int? companyID = fw_factory.GetCompanyID(userId);
                using (OutsourcingCostTypeMngEntities context = CreateContext())
                {
                    OutsourcingCostType dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new OutsourcingCostType();
                        context.OutsourcingCostType.Add(dbItem);
                        var listOutsourcingCostType = context.OutsourcingCostTypeMng_OutSourcingCostTypeMng_SearchView.ToList();
                        foreach (var item in listOutsourcingCostType)
                        {
                            if (dtoOutSourcingCostType.OutsourcingCostTypeUD == item.OutsourcingCostTypeUD)
                            {
                                notification.Message = "This code already exists. Please input another code!!";
                                notification.Type = Library.DTO.NotificationType.Error;
                                return false;
                            }
                        }
                    }
                    else
                    {
                        dbItem = context.OutsourcingCostType.Where(o => o.OutsourcingCostTypeID == id).FirstOrDefault();
                    }
                    if (dbItem == null)
                    {
                        notification.Message = "data not found!";
                        return false;
                    }
                    else
                    {
                        //
                        //convert dto to db
                        converter.DTO2DB_OutSourcingCostType(dtoOutSourcingCostType, ref dbItem);
                        //dbItem.CompanyID = companyID;
                        dbItem.UpdatedBy = userId;
                        dbItem.UpdatedDate = DateTime.Now;

                        //save data
                        context.SaveChanges();
                        //get return data
                        dtoItem = GetData(dbItem.OutsourcingCostTypeID, out notification).Data;
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
    }
}

using Library.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OutsourcingCostMng.DAL
{
    public class DataFactory
    {
        private DataConverter converter = new DataConverter();
        public DataFactory() { }

        private OutsourcingCostMngsEntities CreateContext()
        {
            return new OutsourcingCostMngsEntities(Library.Helper.CreateEntityConnectionString("DAL.OutsourcingCostMngsModel"));
        }

        public DTO.SearchFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.OutsourcingCostSearchDTO>();
            data.WorkCenters = new List<DTO.WorkCenterDTO>();
            totalRows = 0;

            string OutsourcingCostUD = null;
            string OutsourcingCostNM = null;
            string OutsourcingCostNMEN = null;
            int? WorkCenterID = null;
            bool? IsAdditionalCost = null;
            if (filters.ContainsKey("OutsourcingCostUD") && !string.IsNullOrEmpty(filters["OutsourcingCostUD"].ToString()))
            {
                OutsourcingCostUD = filters["OutsourcingCostUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("OutsourcingCostNM") && !string.IsNullOrEmpty(filters["OutsourcingCostNM"].ToString()))
            {
                OutsourcingCostNM = filters["OutsourcingCostNM"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("OutsourcingCostNMEN") && !string.IsNullOrEmpty(filters["OutsourcingCostNMEN"].ToString()))
            {
                OutsourcingCostNMEN = filters["OutsourcingCostNMEN"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("WorkCenterID") && !string.IsNullOrEmpty(filters["WorkCenterID"].ToString()))
            {
                WorkCenterID = Convert.ToInt32(filters["WorkCenterID"]);
            }
            if (filters.ContainsKey("IsAdditionalCost") && !string.IsNullOrEmpty(filters["IsAdditionalCost"].ToString()))
            {
                IsAdditionalCost = (filters["IsAdditionalCost"].ToString() == "true") ? true : false;
            }

            //try to get data
            try
            {
                using (var context = CreateContext())
                {
                    totalRows = context.OutsourcingCostMng_function_SearchOutsourceCost(OutsourcingCostUD, OutsourcingCostNM, OutsourcingCostNMEN, WorkCenterID, IsAdditionalCost, orderBy, orderDirection).Count();
                    var result = context.OutsourcingCostMng_function_SearchOutsourceCost(OutsourcingCostUD, OutsourcingCostNM, OutsourcingCostNMEN, WorkCenterID, IsAdditionalCost, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_OutsourcingCostSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
                using (var context = CreateContext())
                {
                    data.WorkCenters = converter.DB2DTO_GetWorkCenter(context.OutsourcingCostMng_WorkCenter_View.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public DTO.EditFormData GetData(int id, out Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new DTO.OutsourcingCostDTO();
            data.WorkCenters = new List<DTO.WorkCenterDTO>();
            //try to get data
            try
            {
                using (var context = CreateContext())
                {
                    if (id != 0)
                    {
                        data.Data = converter.DB2DTO_OutSourcingCost(context.OutsourcingCostMng_OutsourcingCost_View.FirstOrDefault(o => o.OutsourcingCostID == id));
                    }

                }

                using (var context = CreateContext())
                {
                    data.WorkCenters = converter.DB2DTO_GetWorkCenter(context.OutsourcingCostMng_WorkCenter_View.ToList());
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
            DTO.OutsourcingCostDTO dtoOutsourcingCost = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.OutsourcingCostDTO>();
            try
            {
                ////get companyID
                //Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                //int? companyID = fw_factory.GetCompanyID(userId);
                using (var context = CreateContext())
                {
                    OutsourcingCost dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new OutsourcingCost();
                        context.OutsourcingCost.Add(dbItem);
                        var listOutsourcingCost = context.OutsourcingCostMng_OutsourcingCost_SearchView.ToList();
                        foreach (var item in listOutsourcingCost)
                        {
                            if(dtoOutsourcingCost.OutsourcingCostUD == item.OutsourcingCostUD)
                            {
                                notification.Message = "This code already exists. Please input another code!!";
                                notification.Type = Library.DTO.NotificationType.Error;
                                return false;
                            }
                        }
                    }
                    else
                    {
                        dbItem = context.OutsourcingCost.Where(o => o.OutsourcingCostID == id).FirstOrDefault();
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
                        converter.DTO2DB_OutSourcingCost(dtoOutsourcingCost, ref dbItem);
                        //dbItem.CompanyID = companyID;
                        dbItem.UpdatedBy = userId;
                        dbItem.UpdatedDate = DateTime.Now;

                        //save data
                        context.SaveChanges();
                        //get return data
                        dtoItem = GetData(dbItem.OutsourcingCostID, out notification).Data;
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

        public bool Delete(int id, out Notification notification)
        {
            notification = new Notification { Type = NotificationType.Success };
            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.OutsourcingCost.Where(o => o.OutsourcingCostID == id).FirstOrDefault();
                    context.OutsourcingCost.Remove(dbItem);
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
    }
}

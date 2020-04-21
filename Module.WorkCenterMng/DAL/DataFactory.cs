using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;
using Module.WorkCenterMng.DTO;

namespace Module.WorkCenterMng.DAL
{
    public class DataFactory : Library.Base.DataFactory<SearchForm, EditForm>
    {
        private DAL.DataConverter converter = new DataConverter();

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Notification notification)
        {
            notification = new Notification()
            {
                Type = NotificationType.Success
            };

            try
            {
                using (var context = CreateContext())
                {
                    WorkCenter dbItem;
                    dbItem = context.WorkCenter.FirstOrDefault(o => o.WorkCenterID == id);

                    if (dbItem == null)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Can not find Work Center!";

                        return false;
                    }

                    context.WorkCenter.Remove(dbItem);

                    context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;

                return false;
            }
        }

        public override EditForm GetData(int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public EditForm GetData(int userId ,int id, out Notification notification)
        {
            notification = new Notification()
            {
                Type = NotificationType.Success
            };

            EditForm data = new EditForm()
            {
                Data = new DTO.WorkCenterDto(),
                Employees = new List<Support.DTO.Employee>()
            };

            try
            {
                Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                int? companyID = fw_factory.GetCompanyID(userId);

                using (var context = CreateContext())
                {
                    if (id > 0)
                    {
                        data.Data = converter.DB2DTO_WorkCenter(context.WorkCenterMng_WorkCenter_View.FirstOrDefault(o => o.WorkCenterID == id));
                    }

                    data.Branches = AutoMapper.Mapper.Map<List<WorkCenterMng_Branch_View>, List<DTO.BranchDTO>>(context.WorkCenterMng_Branch_View.Where(o => o.CompanyID == companyID).ToList());
                    data.FactoryWarehouses = AutoMapper.Mapper.Map<List<WorkCenterMng_FactoryWarehouse_View>, List<DTO.FactoryWarehouseDTO>>(context.WorkCenterMng_FactoryWarehouse_View.Where(o => o.CompanyID == companyID).ToList());
                }

                Support.DAL.DataFactory dalSupportMng = new Support.DAL.DataFactory();
                data.Employees = dalSupportMng.GetEmployee();
                //data.FactoryWarehouses = dalSupportMng.GetFactoryWarehouse(companyID);
                return data;
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;

                return data;
            }
        }

        public override SearchForm GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public  SearchForm GetDataWithFilter(int userId ,Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            totalRows = 0;
            notification = new Notification()
            {
                Type = NotificationType.Success
            };

            SearchForm data = new SearchForm();

            int? defaultFactoryWarehouseID = null;

            try
            {
                Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                int? companyID = fw_factory.GetCompanyID(userId);

                using (var context = CreateContext())
                {
                    string workCenterUD = filters.ContainsKey("workCenterUD") && filters["workCenterUD"] != null && !string.IsNullOrEmpty(filters["workCenterUD"].ToString().Trim()) ? filters["workCenterUD"].ToString().Trim() : null;
                    string workCenterNM = filters.ContainsKey("workCenterNM") && filters["workCenterNM"] != null && !string.IsNullOrEmpty(filters["workCenterNM"].ToString().Trim()) ? filters["workCenterNM"].ToString().Trim() : null;
                    decimal? operatingTime = filters.ContainsKey("operatingTime") && filters["operatingTime"] != null && !string.IsNullOrEmpty(filters["operatingTime"].ToString().Trim()) ? (decimal?)Convert.ToDecimal(filters["operatingTime"].ToString().Trim()) : null;
                    decimal? defaultCost = filters.ContainsKey("defaultCost") && filters["defaultCost"] != null && !string.IsNullOrEmpty(filters["defaultCost"].ToString().Trim()) ? (decimal?)Convert.ToDecimal(filters["defaultCost"].ToString().Trim()) : null;
                    decimal? capacity = filters.ContainsKey("capacity") && filters["capacity"] != null && !string.IsNullOrEmpty(filters["capacity"].ToString().Trim()) ? (decimal?)Convert.ToDecimal(filters["capacity"].ToString().Trim()) : null;
                    int? responsibleBy = filters.ContainsKey("responsibleBy") && filters["responsibleBy"] != null && !string.IsNullOrEmpty(filters["responsibleBy"].ToString().Trim()) ? (int?)Convert.ToInt32(filters["responsibleBy"].ToString().Trim()) : null;

                    if (filters.ContainsKey("defaultFactoryWarehouseID") && filters["defaultFactoryWarehouseID"] != null)
                    {
                        defaultFactoryWarehouseID = Convert.ToInt32(filters["defaultFactoryWarehouseID"]);
                    }

                    totalRows = context.WorkCenterMng_function_WorkCenterSearchResult(workCenterUD, workCenterNM, operatingTime, defaultCost, capacity, responsibleBy, defaultFactoryWarehouseID, orderBy, orderDirection).Count();
                    data.Data = converter.DB2DTO_WorkCenterSearchResult(context.WorkCenterMng_function_WorkCenterSearchResult(workCenterUD, workCenterNM, operatingTime, defaultCost, capacity, responsibleBy, defaultFactoryWarehouseID, orderBy, orderDirection).ToList());
                    data.DetailData = AutoMapper.Mapper.Map<List<WorkCenterMng_WorkCenterDetailSearchResultVirtual_View>, List<DTO.WorkCenterDetailSearchResultDTO>>(context.WorkCenterMng_function_WorkCenterDetailSearchResult(workCenterUD, workCenterNM, operatingTime, defaultCost, capacity, responsibleBy, defaultFactoryWarehouseID, null, null).ToList());

                    data.FactoryWarehouses = AutoMapper.Mapper.Map<List<WorkCenterMng_FactoryWarehouse_View>, List<DTO.FactoryWarehouseDTO>>(context.WorkCenterMng_FactoryWarehouse_View.Where(o => o.CompanyID == companyID).ToList());
                }
                
                return data;
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;

                return data;
            }
        }

        public InitForm GetInitData(int userID, out Notification notification)
        {
            notification = new Notification()
            {
                Type = NotificationType.Success
            };

            InitForm data = new InitForm()
            {
                Data = new List<Support.DTO.Employee>()
            };

            try
            {
                Framework.DAL.DataFactory frameworkFactory = new Framework.DAL.DataFactory();
                int? companyID = frameworkFactory.GetCompanyID(userID);

                Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
                data.Data = supportFactory.GetEmployee();

                using (WorkCenterEntities context = CreateContext())
                {
                    data.Branches = AutoMapper.Mapper.Map<List<WorkCenterMng_Branch_View>, List<DTO.BranchDTO>>(context.WorkCenterMng_Branch_View.Where(o => o.CompanyID == companyID).ToList());
                    data.FactoryWarehouses = AutoMapper.Mapper.Map<List<WorkCenterMng_FactoryWarehouse_View>, List<DTO.FactoryWarehouseDTO>>(context.WorkCenterMng_FactoryWarehouse_View.Where(o => o.CompanyID == companyID).ToList());
                }

                return data;
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;

                return data;
            }
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            DTO.WorkCenterDto workCenter = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.WorkCenterDto>();

            notification = new Notification()
            {
                Type = NotificationType.Success
            };

            try
            {
                using (var context = CreateContext())
                {
                    DAL.WorkCenter dbItem;

                    if (id == 0)
                    {
                        dbItem = context.WorkCenter.FirstOrDefault(o => o.WorkCenterUD.Equals(workCenter.WorkCenterUD));
                        if (dbItem != null)
                        {
                            notification.Type = NotificationType.Error;
                            notification.Message = workCenter.WorkCenterUD + " is existed in Work Center!";

                            return false;
                        }

                        dbItem = new DAL.WorkCenter();
                        context.WorkCenter.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.WorkCenter.FirstOrDefault(o => o.WorkCenterID == id);

                        if (dbItem == null)
                        {
                            notification.Type = NotificationType.Error;
                            notification.Message = "Can not found data!";

                            return false;
                        }
                    }
                    converter.DTO2DB_WorkCenter(userId, workCenter, ref dbItem);

                    context.WorkCenterDetail.Local.Where(o => o.WorkCenter == null).ToList().ForEach(o => context.WorkCenterDetail.Remove(o));

                    context.SaveChanges();
                    dtoItem = GetData(userId, dbItem.WorkCenterID, out notification);
                }

                return true;
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;

                return false;
            }
        }

        private WorkCenterEntities CreateContext()
        {
            return new WorkCenterEntities(Library.Helper.CreateEntityConnectionString("DAL.WorkCenterModel"));
        }
    }
}

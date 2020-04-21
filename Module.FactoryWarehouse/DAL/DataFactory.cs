using System;
using System.Collections.Generic;
using System.Linq;

namespace Module.FactoryWarehouse.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private FactoryWarehouseEntities CreateContext()
        {
            return new FactoryWarehouseEntities(Library.Helper.CreateEntityConnectionString("DAL.FactoryWarehouseModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.SearchFormData searchFormData = new DTO.SearchFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            int UserID = -1; ;
            string FactoryWarehouseUD = null;
            string FactoryWarehouseNM = null;
            string Description = null;
            int? CompanyID = null;
            int? BranchID = null;
            int? ResponsibleBy = null;

            if (filters.ContainsKey("UserID") && !string.IsNullOrEmpty(filters["UserID"].ToString()))
            {
                UserID = Convert.ToInt32(filters["UserID"].ToString());
            }
            if (filters.ContainsKey("FactoryWarehouseUD") && !string.IsNullOrEmpty(filters["FactoryWarehouseUD"].ToString()))
            {
                FactoryWarehouseUD = filters["FactoryWarehouseUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("FactoryWarehouseNM") && !string.IsNullOrEmpty(filters["FactoryWarehouseNM"].ToString()))
            {
                FactoryWarehouseNM = filters["FactoryWarehouseNM"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("Description") && !string.IsNullOrEmpty(filters["Description"].ToString()))
            {
                Description = filters["Description"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("BranchID") && filters["BranchID"] != null && !string.IsNullOrEmpty(filters["BranchID"].ToString()))
            {
                BranchID = Convert.ToInt32(filters["BranchID"].ToString());
            }

            if (filters.ContainsKey("responsibleBy") && filters["responsibleBy"] != null && !string.IsNullOrEmpty(filters["responsibleBy"].ToString().Trim()))
            {
                ResponsibleBy = Convert.ToInt32(filters["responsibleBy"].ToString());
            }

            CompanyID = fwFactory.GetCompanyID(UserID);

            try
            {
                using (FactoryWarehouseEntities context = CreateContext())
                {
                    totalRows = context.FactoryWarehouse_function_SearchFactoryWarehouse(FactoryWarehouseUD, FactoryWarehouseNM, Description, CompanyID, ResponsibleBy, orderBy, orderDirection, BranchID).Count();
                    var result = context.FactoryWarehouse_function_SearchFactoryWarehouse(FactoryWarehouseUD, FactoryWarehouseNM, Description, CompanyID, ResponsibleBy, orderBy, orderDirection, BranchID);
                    searchFormData.Data = converter.DB2DTO_FactoryWarehouseSearchList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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

        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new DTO.FactoryWarehouse();
            data.Data.FactoryWarehousePallets = new List<DTO.FactoryWarehousePallet>();
            //data.StockOverviewDetails = new List<DTO.StockOverviewDetail>();
            //data.StockOverviews = new List<DTO.StockOverview>();
            data.PalletOverviews = new List<DTO.PalletOverview>();

            //try to get data
            try
            {
                if (id > 0)
                {
                    using (FactoryWarehouseEntities context = CreateContext())
                    {
                        var w = context.FactoryWarehouse_FactoryWarehouse_View.FirstOrDefault(o => o.FactoryWarehouseID == id);
                        if (w == null)
                        {
                            throw new Exception("Can not found the factory to edit");
                        }
                        data.Data = converter.DB2DTO_FactoryWarehouse(w);
                        //data.StockOverviewDetails = converter.DB2DTO_StockOverviewDetail(context.FactoryWarehouse_StockOverviewDetail_View.Where(o => o.FactoryWarehouseID == id).ToList());
                        //data.StockOverviews = converter.DB2DTO_StockOverview(context.FactoryWarehouse_StockOverView_View.Where(o => o.FactoryWarehouseID == id).ToList());
                        data.PalletOverviews = converter.DB2DTO_PalletOverview(context.FactoryWarehouse_PalletOverview_View.Where(o => o.FactoryWarehouseID == id).ToList());
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FactoryWarehouseEntities context = CreateContext())
                {
                    var dbItem = context.FactoryWarehouse.Where(o => o.FactoryWarehouseID == id).FirstOrDefault();
                    context.FactoryWarehouse.Remove(dbItem);
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

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.FactoryWarehouse dtofWarehouse = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.FactoryWarehouse>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.FactoryWarehouse> FactoryWarehouseDTOs = new List<DTO.FactoryWarehouse>();

            try
            {
                using (FactoryWarehouseEntities context = CreateContext())
                {
                    FactoryWarehouse dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new FactoryWarehouse();
                        context.FactoryWarehouse.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.FactoryWarehouse.FirstOrDefault(o => o.FactoryWarehouseID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Warehouse not found!";
                        return false;
                    }
                    else
                    {
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtofWarehouse.ConcurrencyFlag_String)))
                        {
                            throw new Exception(Library.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        //if (dtofWarehouse.FactoryWarehouseUD != "")
                        //{
                        //    var FactoryWarehouseBDs = context.FactoryWarehouse_FactoryWarehouse_View.Where(o => o.FactoryWarehouseID != id).ToList();
                        //    FactoryWarehouseDTOs = converter.DB2DTO_WarehouseList(FactoryWarehouseBDs);
                        //    foreach (DTO.FactoryWarehouse WarehouseData in FactoryWarehouseDTOs)
                        //    {
                        //        if (dtofWarehouse.FactoryWarehouseUD != null)
                        //        {
                        //            if (dtofWarehouse.FactoryWarehouseUD == WarehouseData.FactoryWarehouseUD)
                        //            {
                        //                throw new Exception("This Warehouse Code already exist in the system!");
                        //            }
                        //        }
                        //    }
                        //}

                        dtofWarehouse.CompanyID = fwFactory.GetCompanyID(userId);

                        converter.DTO2DB(dtofWarehouse, ref dbItem);
                        //remove orphan item

                        context.FactoryWarehousePallet.Local.Where(o => o.FactoryWarehouse == null).ToList().ForEach(o => context.FactoryWarehousePallet.Remove(o));

                        dbItem.UpdatedDate = DateTime.Now;
                        dbItem.UpdatedBy = userId;

                        context.SaveChanges();

                        dtoItem = GetData(dbItem.FactoryWarehouseID, out notification).Data;

                        return true;
                    }
                }
            }

            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
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


        //
        // CUSTOM
        //
        public DTO.SearchFilterData GetSearchFilter(int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFilterData data = new DTO.SearchFilterData();
            //data.Companies = new List<Support.DTO.InternalCompany>();

            try
            {
                Framework.DAL.DataFactory frameworkFactory = new Framework.DAL.DataFactory();
                int? companyID = frameworkFactory.GetCompanyID(userId);

                Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
                data.Employees = supportFactory.GetEmployee();

                using (FactoryWarehouseEntities context = CreateContext())
                {
                    data.Branches = AutoMapper.Mapper.Map<List<FactoryWarehouseMng_Branch_View>, List<DTO.BranchDTO>>(context.FactoryWarehouseMng_Branch_View.Where(o => o.CompanyID == companyID).ToList());
                }
                //data.Companies = supportFactory.GetInternalCompany().ToList();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public DTO.EditFormData GetData1(int userId, int id, int branchID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new DTO.FactoryWarehouse();
            data.PalletOverviews = new List<DTO.PalletOverview>();
            data.Employees = new List<Support.DTO.Employee>();
            data.Data.FactoryWarehousePallets = new List<DTO.FactoryWarehousePallet>();
            int? eCompanyID = fwFactory.GetCompanyID(userId);

            //try to get data
            try
            {
                using (FactoryWarehouseEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        var w = context.FactoryWarehouse_FactoryWarehouse_View.FirstOrDefault(o => o.FactoryWarehouseID == id && o.CompanyID == eCompanyID);
                        if (data.Data == null)
                        {
                            throw new Exception("Can not found the profile to edit");
                        }
                        data.Data = converter.DB2DTO_FactoryWarehouse(w);
                    }

                    // Set default Branch.
                    if (data.Data.BranchID == null || data.Data.BranchID == 0)
                    {
                        data.Data.BranchID = branchID;
                    }

                    // Get support Branch.
                    data.Branches = AutoMapper.Mapper.Map<List<FactoryWarehouseMng_Branch_View>, List<DTO.BranchDTO>>(context.FactoryWarehouseMng_Branch_View.Where(o => o.CompanyID == eCompanyID).ToList());
                }
                Support.DAL.DataFactory dalSupportMng = new Support.DAL.DataFactory();
                data.Employees = dalSupportMng.GetEmployee();
            }

            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public DTO.CapacityData GetCapacityData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.CapacityData data = new DTO.CapacityData();
            data.CapacityOverviews = new List<DTO.CapacityOverview>();

            //try to get data
            try
            {
                using (FactoryWarehouseEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        data.CapacityOverviews = converter.DB2DTO_CapacityOverview(context.FactoryWarehouse_CapacityOverview_View.Where(o => o.FactoryWarehouseID == id).ToList());
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }
            return data;
        }

        public DTO.CapacityDetailData GetCapacityDetailData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.CapacityDetailData data = new DTO.CapacityDetailData();
            data.PalletOverviews = new List<DTO.PalletOverview>();

            //try to get data
            try
            {
                using (FactoryWarehouseEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        data.PalletOverviews = converter.DB2DTO_PalletOverview(context.FactoryWarehouse_PalletOverview_View.Where(o => o.FactoryWarehouseID == id).ToList());
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }
            return data;
        }
    }
}

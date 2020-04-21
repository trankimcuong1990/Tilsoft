using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryPayment2BalanceMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private FactoryPayment2BalanceMngEntities CreateContext()
        {
            return new FactoryPayment2BalanceMngEntities(Library.Helper.CreateEntityConnectionString("DAL.FactoryPayment2BalanceMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.FactoryPayment2BalanceSearchResult>();
            totalRows = 0;

            //try to get data
            try
            {
                using (FactoryPayment2BalanceMngEntities context = CreateContext())
                {
                    string Season = null;
                    int UserID = -1;
                    int? SupplierID = null;
                    if (filters.ContainsKey("Season") && !string.IsNullOrEmpty(filters["Season"].ToString()))
                    {
                        Season = filters["Season"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("SupplierID") && !string.IsNullOrEmpty(filters["SupplierID"].ToString()))
                    {
                        SupplierID = Convert.ToInt32(filters["SupplierID"].ToString());
                    }
                    if (filters.ContainsKey("UserID") && !string.IsNullOrEmpty(filters["UserID"].ToString()))
                    {
                        UserID = Convert.ToInt32(filters["UserID"].ToString());
                    }
                    totalRows = context.FactoryPayment2BalanceMngMng_function_SearchBalance(UserID, SupplierID, Season, orderBy, orderDirection).Count();
                    var result = context.FactoryPayment2BalanceMngMng_function_SearchBalance(UserID, SupplierID, Season, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_FactoryPayment2BalanceSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.FactoryPayment2BalanceDTO dtoBalance = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.FactoryPayment2BalanceDTO>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {

                // check permission
                if (fwFactory.CheckDPBalancePermission(userId, id) == 0)
                {
                    throw new Exception("Current user don't have access permission for the selected balance data");
                }

                using (FactoryPayment2BalanceMngEntities context = CreateContext())
                {
                    FactoryPayment2Balance dbItem = context.FactoryPayment2Balance.FirstOrDefault(o => o.FactoryPayment2BalanceID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("DP balance not found!");
                    }

                    // validate before approve
                    // add next season balance
                    FactoryPayment2BalanceMng_FactoryPayment2BalanceSearchResult_View balanceSummary = context.FactoryPayment2BalanceMng_FactoryPayment2BalanceSearchResult_View.FirstOrDefault(o => o.FactoryPayment2BalanceID == id);
                    FactoryPayment2Balance newBalance = new FactoryPayment2Balance();
                    newBalance.SupplierID = dbItem.SupplierID;
                    newBalance.Season = Library.OMSHelper.Helper.GetNextSeason(dbItem.Season);
                    newBalance.BeginBalance = balanceSummary.EndBalance;
                    newBalance.UpdatedDate = DateTime.Now;
                    newBalance.UpdatedBy = userId;
                    context.FactoryPayment2Balance.Add(newBalance);

                    dbItem.isClosed = true;
                    dbItem.UpdatedDate = DateTime.Now;
                    dbItem.UpdatedBy = userId;
                    context.SaveChanges();
                    dtoItem = GetData(userId, dbItem.FactoryPayment2BalanceID, out notification).Data;
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.FactoryPayment2BalanceDTO dtoBalance = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.FactoryPayment2BalanceDTO>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                // check permission
                if (fwFactory.CheckDPBalancePermission(userId, id) == 0)
                {
                    throw new Exception("Current user don't have access permission for the selected balance data");
                }

                using (FactoryPayment2BalanceMngEntities context = CreateContext())
                {
                    FactoryPayment2Balance dbItem = context.FactoryPayment2Balance.FirstOrDefault(o => o.FactoryPayment2BalanceID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("DP balance not found!");
                    }

                    // validate before reset


                    dbItem.isClosed = null;
                    dbItem.UpdatedDate = null;
                    dbItem.UpdatedBy = null;
                    context.SaveChanges();
                    dtoItem = GetData(userId, dbItem.FactoryPayment2BalanceID, out notification).Data;
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }
        }

        //
        // CUSTOM FUNCTION
        //
        public DTO.EditFormData GetData(int userId, int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new DTO.FactoryPayment2BalanceSearchResult();
            data.Details = new List<DTO.Detail>();

            //try to get data
            try
            {
                // check permission
                if (fwFactory.CheckDPBalancePermission(userId, id) == 0)
                {
                    throw new Exception("Current user don't have access permission for the selected balance data");
                }

                using (FactoryPayment2BalanceMngEntities context = CreateContext())
                {
                    string season = string.Empty;
                    int supplierId = 0;
                    data.Data = converter.DB2DTO_FactoryPayment2BalanceSearchResult(context.FactoryPayment2BalanceMng_FactoryPayment2BalanceSearchResult_View.FirstOrDefault(o => o.FactoryPayment2BalanceID == id));
                    season = data.Data.Season;
                    supplierId = data.Data.SupplierID.Value;
                    data.Details = converter.DB2DTO_DetailList(context.FactoryPayment2BalanceMng_Detail_View.Where(o => o.Season == season && o.SupplierID == supplierId).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public DTO.SearchFilterData GetSearchFilter(int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFilterData data = new DTO.SearchFilterData();
            data.Suppliers = new List<Support.DTO.Supplier>();
            data.Seasons = new List<Support.DTO.Season>();

            try
            {
                data.Suppliers = supportFactory.GetSupplier(userId);
                data.Seasons = supportFactory.GetSeason();
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

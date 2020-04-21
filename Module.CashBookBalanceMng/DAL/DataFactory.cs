using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Module.CashBookBalanceMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private DataConverter converter = new DataConverter();
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private string _tempFolder;
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
        public DataFactory() { }
        public DataFactory(string tempFolder)
        {
            this._tempFolder = tempFolder + @"\";
        }
        private CashBookBalanceMngEntities CreateContext()
        {
            return new CashBookBalanceMngEntities(Library.Helper.CreateEntityConnectionString("DAL.CashBookBalanceMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.CashBookBalance>();
            totalRows = 0;

            //try to get data
            try
            {
                using (CashBookBalanceMngEntities context = CreateContext())
                {
                    int? BalanceMonth = null;
                    int? BalanceYear = null;

                    if (filters.ContainsKey("BalanceMonth") && !string.IsNullOrEmpty(filters["BalanceMonth"].ToString()))
                    {
                        BalanceMonth = Convert.ToInt32(filters["BalanceMonth"].ToString());
                    }
                    if (filters.ContainsKey("BalanceYear") && !string.IsNullOrEmpty(filters["BalanceYear"].ToString()))
                    {
                        BalanceYear = Convert.ToInt32(filters["BalanceYear"].ToString());
                    }

                    totalRows = context.CashBookBalanceMng_function_SearchCashBookBalance(BalanceMonth, BalanceYear, orderBy, orderDirection).Count();
                    var result = context.CashBookBalanceMng_function_SearchCashBookBalance(BalanceMonth, BalanceYear, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_CashBookBalanceList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }
        public DTO.SearchFilterData GetSearchFilter(out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }
        public bool CloseEntryBalance(int userId, int Month, int Year, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            // Proccess new.
            //Month = (Month == 1) ? 12 : Month - 1;
            //Year = (Month == 1) ? Year - 1 : Year;

            try
            {
                using (CashBookBalanceMngEntities context = CreateContext())
                {
                    var cashBalance = context.CashBookBalanceMng_CashBookBalance_View.OrderByDescending(o => o.UpdatedDate).FirstOrDefault();

                    Month = (cashBalance.BalanceMonth.Value == 12) ? 1 : (cashBalance.BalanceMonth.Value + 1);
                    Year = (cashBalance.BalanceMonth.Value == 12) ? (cashBalance.BalanceYear.Value + 1) : cashBalance.BalanceYear.Value;

                    var retVal = new SqlParameter()
                    {
                        ParameterName = "@ReturnValue",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Output
                    };

                    object[] parameters =
                    {
                        new SqlParameter()
                        {
                            ParameterName = "@ParameterMonth",
                            SqlDbType = System.Data.SqlDbType.BigInt,
                            Value = Month
                        },
                        new SqlParameter()
                        {
                            ParameterName = "@ParameterYear",
                            SqlDbType = System.Data.SqlDbType.BigInt,
                            Value = Year
                        },
                        new SqlParameter()
                        {
                            ParameterName = "@ParameterUserId",
                            SqlDbType = System.Data.SqlDbType.BigInt,
                            Value = userId
                        },
                        retVal
                    };

                    string command = string.Format("exec @ReturnValue = dbo.CashBookBalanceMng_function_CloseEntryCashBookBalance2 @ParameterMonth, @ParameterYear, @ParameterUserId");

                    int result = context.Database.ExecuteSqlCommand(command, parameters);

                    if ((int)retVal.Value == 98)
                    {
                        notification.Type = Library.DTO.NotificationType.Error;
                        notification.Message = "Cash balance in this time alreay exist!";
                        return false;
                    }

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

        public DTO.EditFormData GetBalanceDetail(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            DTO.EditFormData data = new DTO.EditFormData();
            data.CashBookBalance = new DTO.CashBookBalance();
            data.CashBookBalanceDetails = new List<DTO.CashBookBalanceDetail>();

            //try to get data
            try
            {
                if (id > 0)
                {
                    using (CashBookBalanceMngEntities context = CreateContext())
                    {
                        var CashBookBalance = context.CashBookBalanceMng_CashBookBalance_View.FirstOrDefault(o => o.CashBookBalanceID == id);
                        var CashBookBalanceDetails = context.CashBookBalanceMng_CashBookBalanceDetail_View.Where(o => o.CashBookBalanceID == id).ToList();
                        if (CashBookBalance == null)
                        {
                            throw new Exception("Can not found the Cash Book Balance to view");
                        }
                        data.CashBookBalance = converter.DB2DTO_CashBookBalance(CashBookBalance);
                        data.CashBookBalanceDetails = converter.DB2DTO_CashBookBalanceDetailList(CashBookBalanceDetails);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Library;
using Library.DTO;

namespace Module.BreakdownPriceListMng.DAL
{
    internal partial class DataFactory : Library.Base.DataFactory2<DTO.SearchFormData, DTO.EditFormData>
    {
        Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();
        private BreakdownPriceListMngEntities CreateContext()
        {
            return new BreakdownPriceListMngEntities(Library.Helper.CreateEntityConnectionString("DAL.BreakdownPriceListMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.SearchFormData searchFormData = new DTO.SearchFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            try
            {
                string productionItemUD = null;
                string productionItemNM = null;
                string productionItemNMEN = null;
                int? companyID = fw_factory.GetCompanyID(userId);

                if (filters.ContainsKey("productionItemUD") && !string.IsNullOrEmpty(filters["productionItemUD"].ToString()))
                {
                    productionItemUD = filters["productionItemUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("productionItemNM") && !string.IsNullOrEmpty(filters["productionItemNM"].ToString()))
                {
                    productionItemNM = filters["productionItemNM"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("productionItemNMEN") && !string.IsNullOrEmpty(filters["productionItemNMEN"].ToString()))
                {
                    productionItemNMEN = filters["productionItemNMEN"].ToString().Replace("'", "''");
                }
                using (BreakdownPriceListMngEntities context = CreateContext())
                {
                    totalRows = context.BreakdownPriceListMng_function_SearchBreakdownPriceList(orderBy, orderDirection, productionItemUD, productionItemNM, productionItemNMEN).Count();
                    var result = context.BreakdownPriceListMng_function_SearchBreakdownPriceList(orderBy, orderDirection, productionItemUD, productionItemNM, productionItemNMEN);
                    searchFormData.Data = converter.DB2DTO_Search(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                    searchFormData.CompanyID = companyID.Value;

                    //get exchange rate of USD
                    var dbExchangeRate = context.BreakdownPriceListMng_function_GetExchangeRate(DateTime.Now, "USD").FirstOrDefault();
                    if (dbExchangeRate != null)
                    {
                        searchFormData.ExchangeRate = dbExchangeRate.ExchangeRate;
                    }                    
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return searchFormData;
        }
        public override DTO.EditFormData GetData(int userId, int id, Hashtable param, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }
        public override bool DeleteData(int userID, int id, out Library.DTO.Notification notification)
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
        public override object GetInitData(int userId, out Notification notification)
        {
            throw new NotImplementedException();
        }
        public override object GetSearchFilter(int userId, out Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}

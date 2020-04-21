using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.TransportationFeeMng
{
    public class DataFactory : DALBase.FactoryBase2<DTO.TransportationFeeMng.SearchFormData, DTO.TransportationFeeMng.EditFormData, DTO.TransportationFeeMng.TransportationFee>
    {
        private DataConverter converter = new DataConverter();
        private TransportationFeeEntities CreateContext()
        {
            return new TransportationFeeEntities(DALBase.Helper.CreateEntityConnectionString("TransportationFeeMngModel"));
        }

        public override DTO.TransportationFeeMng.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.TransportationFeeMng.SearchFormData data = new DTO.TransportationFeeMng.SearchFormData();
            data.Data = new List<DTO.TransportationFeeMng.TransportationFeeSearchResult>();
            totalRows = 0;

            int? TransportationFee = null;
            int? Commission = null;
            string Season = null;
            int? offerID=null;


            if (filters.ContainsKey("TransportationFee") && filters["TransportationFee"] != null && !string.IsNullOrEmpty(filters["TransportationFee"].ToString()))
            {
                TransportationFee = Convert.ToInt32(filters["TransportationFee"]);
            }
            if (filters.ContainsKey("Commission") && filters["Commission"] != null && !string.IsNullOrEmpty(filters["Commission"].ToString()))
            {
                Commission = Convert.ToInt32(filters["Commission"]);
            }


            //try to get data
            try
            {
                using (TransportationFeeEntities context = CreateContext())
                {
                    totalRows = context.TransportationFeeMng_function_SearchTransportationFee(TransportationFee, Commission,offerID,Season ,orderBy,orderDirection).Count();
                    var result = context.TransportationFeeMng_function_SearchTransportationFee(TransportationFee, Commission, offerID, Season, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_TransportationFeeSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override DTO.TransportationFeeMng.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.TransportationFeeMng.EditFormData data = new DTO.TransportationFeeMng.EditFormData();
            data.Data = new DTO.TransportationFeeMng.TransportationFee();

            //try to get data
            try
            {
                using (TransportationFeeEntities context = CreateContext())
                {
                    data.Data = converter.DB2DTO_TransportationFee(context.TransportationFeeMng_TransportationFee_View.FirstOrDefault(o => o.SaleOrderID == id));
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
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            try
            {
                using (TransportationFeeEntities context = CreateContext())
                {
                    SaleOrder dbItem = context.SaleOrders.FirstOrDefault(o => o.SaleOrderID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Sale Order not found!";
                        return false;
                    }
                    else
                    {
                        context.SaleOrders.Remove(dbItem);
                        context.SaveChanges();

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

        public override bool UpdateData(int id, ref DTO.TransportationFeeMng.TransportationFee dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int number;
            string indexName;

            try
            {
                using (TransportationFeeEntities context = CreateContext())
                {
                    SaleOrder dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new SaleOrder();
                        context.SaleOrders.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.SaleOrders.FirstOrDefault(o => o.SaleOrderID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Sale order not found!";
                        return false;
                    }
                    else
                    {
                        converter.DTO2BD_TransportationFee(dtoItem, ref dbItem);
                        context.SaveChanges();

                        dtoItem = GetData(dbItem.SaleOrderID, out notification).Data;

                        return true;
                    }
                }
            }
            catch (System.Data.DataException dEx)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                Library.ErrorHelper.DataExceptionParser(dEx, out number, out indexName);
                if (number == 2601 && !string.IsNullOrEmpty(indexName))
                {
                    if (indexName == "SaleOrderIDUnique")
                    {
                        notification.Message = "Sale order already exists!";
                    }
                }
                else
                {
                    notification.Message = dEx.Message;
                }

                return false;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }
        }

        public override bool Approve(int id, ref DTO.TransportationFeeMng.TransportationFee dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.TransportationFeeMng.TransportationFee dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}

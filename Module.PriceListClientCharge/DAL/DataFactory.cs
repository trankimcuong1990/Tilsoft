namespace Module.PriceListClientCharge.DAL
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Library.DTO;
    using Module.PriceListClientCharge.DTO;

    internal class DataFactory : Library.Base.DataFactory<DTO.SearchData, DTO.EditData>
    {
        #region ** Variable & Constant **

        DataConverter converter = new DataConverter();

        Module.Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();

        #endregion

        #region ** Constructor **

        public PriceListClientChargeEntities CreateContext()
        {
            return new PriceListClientChargeEntities(Library.Helper.CreateEntityConnectionString("DAL.PriceListClientChargeModel"));
        }

        #endregion

        #region ** Function & Method **

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override EditData GetData(int id, out Notification notification)
        {
            DTO.EditData data = new EditData();

            // Define type of notification
            notification = new Notification { Type = NotificationType.Success };
            data.Data = new DTO.PriceListClientChargeDto();

            data.SupportCurrency = new List<Support.DTO.TypeOfCurrency>();
            data.SupportCharge = new List<Support.DTO.TypeOfCharge>();
            data.SupportPOL = new List<Support.DTO.POL>();
            data.SupportPOD = new List<Support.DTO.POD>();
            data.SupportContainerType = new List<Support.DTO.ContainerType>();

            try
            {
                if (id > 0)
                {
                    using (var context = CreateContext())
                    {
                        var dbItem = context.PriceListClientCharge_PriceListClientCharge_View.FirstOrDefault(o => o.PriceListClientChargeID == id);

                        if (dbItem == null)
                        {
                            notification.Type = NotificationType.Error;
                            notification.Message = "Can not found the Price List Client Charge";

                            return data;
                        }

                        data.Data = converter.DB2DTO_PriceListClientCharge(dbItem);
                    }
                }

                // Get data support
                data.SupportCurrency = supportFactory.GetTypeOfCurrency();
                data.SupportCharge = supportFactory.GetTypeOfCharges().ToList();
                data.SupportPOL = supportFactory.GetPOL();
                data.SupportPOD = supportFactory.GetPOD();
                data.SupportContainerType = supportFactory.GetContainerType();
            }
            catch (Exception ex)
            {
                notification.Message = ex.Message;
                notification.Type = NotificationType.Error;
            }

            return data;
        }

        public override SearchData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            DTO.SearchData data = new SearchData();

            // Define type of notification, total rows
            notification = new Notification { Type = NotificationType.Success };
            totalRows = 0;

            try
            {
                using (var context = CreateContext())
                {
                    string startDate = null;
                    string endDate = null;
                    string clientID = null;
                    string typeOfCostID = null;
                    string typeOfCurrencyID = null;

                    totalRows = context.PriceListClientCharge_function_PriceListClientChargeSearch(startDate, endDate, clientID, typeOfCostID, typeOfCurrencyID, orderBy, orderDirection).Count();
                    var result = context.PriceListClientCharge_function_PriceListClientChargeSearch(startDate, endDate, clientID, typeOfCostID, typeOfCurrencyID, orderBy, orderDirection);

                    data.Data = converter.DB2DTO_PriceListClientChargeSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            DTO.PriceListClientChargeDto dtoConvert = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.PriceListClientChargeDto>();

            // Define type of notification, convert object
            notification = new Notification { Type = NotificationType.Success };

            try
            {
                using (var context = CreateContext())
                {
                    PriceListClientCharge dbItem;

                    if (id > 0)
                    {
                        dbItem = context.PriceListClientCharge.FirstOrDefault(o => o.PriceListClientChargeID == id);
                    }
                    else
                    {
                        dbItem = new PriceListClientCharge();

                        context.PriceListClientCharge.Add(dbItem);

                        dbItem.CreatedBy = userId;
                        dbItem.CreatedDate = DateTime.Now;
                    }

                    if (dbItem == null)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = string.Format("Price List Client Charge [id = {0}] not found!", id);

                        return false;
                    }

                    // Call method convert object to database
                    converter.DTO2DB_PriceListClientCharge(dtoConvert, dbItem);

                    dbItem.UpdatedBy = userId;
                    dbItem.UpdatedDate = DateTime.Now;

                    // remove orphan detail
                    context.PriceListClientChargeDetail.Local.Where(o => o.PriceListClientCharge == null).ToList()
                        .ForEach(o => context.PriceListClientChargeDetail.Remove(o));

                    context.SaveChanges();

                    // Load again data
                    dtoItem = GetData(dbItem.PriceListClientChargeID, out notification);

                    return true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;

                return false;
            }
        }

        #endregion
    }
}

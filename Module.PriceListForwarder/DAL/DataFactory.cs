namespace Module.PriceListForwarder.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Library.Base;
    using DTO;
    using Library.DTO;
    using System.Collections;
    using Support.DTO;
    using Library;

    internal class DataFactory : DataFactory<SearchData, EditData>
    {
        #region ** Variables & Constants **

        private Support.DAL.DataFactory supportDataFactory = new Support.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        #endregion

        #region ** Constructors **

        private PriceListForwarderEntities CreateContext()
        {
            return new PriceListForwarderEntities(Helper.CreateEntityConnectionString("DAL.PriceListForwarderModel"));
        }

        #endregion

        #region ** Events **

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
            notification = new Notification { Type = NotificationType.Success };

            EditData editData = new EditData()
            {
                Data = new DTO.PriceListForwarder(),
                TypeContainers = new List<ContainerType>(),
                TypeCosts = new List<TypeOfCost>(),
                TypeCurrencys = new List<TypeOfCurrency>(),
                PODs = new List<POD>(),
                POLs = new List<POL>()
            };

            try
            {
                if (id > 0)
                {
                    using (var context = CreateContext())
                    {
                        var dbItem = context.PriceListForwarder_PriceListForwarder_View.FirstOrDefault(o => o.PriceListForwarderID == id);

                        if (dbItem == null)
                        {
                            notification.Type = NotificationType.Error;
                            notification.Message = "Can not found the Price List Forwarder to edit";

                            return editData;
                        }

                        editData.Data = converter.DB2DTOGetData(dbItem);
                    }
                }

                // Get data support: Container, Cost, Currency, POD, POL
                editData.PODs = supportDataFactory.GetPOD().ToList();
                editData.POLs = supportDataFactory.GetPOL().ToList();
                editData.TypeContainers = supportDataFactory.GetContainerType().ToList();
                editData.TypeCosts = supportDataFactory.GetTypeOfCosts().ToList();
                editData.TypeCurrencys = supportDataFactory.GetTypeOfCurrency().ToList();
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
            }

            return editData;
        }

        public override SearchData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };
            totalRows = 0;
            SearchData searchData = new SearchData()
            {
                Data = new List<DTO.PriceListForwarderSearchResult>(),
                TypeCosts = new List<TypeOfCost>(),
                TypeCurrencys = new List<TypeOfCurrency>()
            };

            try
            {
                using (var context = CreateContext())
                {
                    // Condition filters: StartDate, EndDate, ForwarderID, TypeOfCostID, TypeOfCurrencyID
                    string startDate = null;
                    string endDate = null;
                    string forwarderID = null;
                    string typeOfCostID = null;
                    string typeOfCurrencyID = null;

                    // Get total rows, search data
                    totalRows = context.PriceListForwarder_function_SearchPriceListForwarder(startDate, endDate, forwarderID, typeOfCostID, typeOfCurrencyID, orderBy, orderDirection).Count();
                    var result = context.PriceListForwarder_function_SearchPriceListForwarder(startDate, endDate, forwarderID, typeOfCostID, typeOfCurrencyID, orderBy, orderDirection);

                    // Convert DB to DTO
                    searchData.Data = converter.DB2DTOSearchResult(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }

                // Get data support Cost and Currency
                searchData.TypeCosts = supportDataFactory.GetTypeOfCosts().ToList();
                searchData.TypeCurrencys = supportDataFactory.GetTypeOfCurrency().ToList();
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
            }

            return searchData;
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            notification = new Notification { Type = NotificationType.Success };

            DTO.PriceListForwarder dtoConverter = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.PriceListForwarder>();

            try
            {
                using (var context = CreateContext())
                {
                    PriceListForwarder dbItem;

                    if (id > 0)
                    {
                        dbItem = context.PriceListForwarder.FirstOrDefault(o => o.PriceListForwarderID == id);
                    }
                    else
                    {
                        dbItem = new PriceListForwarder();

                        context.PriceListForwarder.Add(dbItem);

                        dbItem.CreatedBy = userId;
                        dbItem.CreatedDate = DateTime.Now;
                    }

                    if (dbItem == null)
                    {
                        notification.Message = string.Format("Price List Forwarder [id={0}] not found!", id);
                        return false;
                    }

                    // converter DTO to DB
                    converter.DTO2DB(dtoConverter, dbItem);

                    dbItem.UpdatedBy = userId;
                    dbItem.UpdatedDate = DateTime.Now;

                    // remove orphan detail
                    context.PriceListForwarderDetail.Local.Where(o => o.PriceListForwarder == null).ToList()
                        .ForEach(o => context.PriceListForwarderDetail.Remove(o));

                    context.SaveChanges();

                    dtoItem = GetData(dbItem.PriceListForwarderID, out notification);

                    return true;
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

        #endregion
    }
}

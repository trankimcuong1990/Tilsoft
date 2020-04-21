using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DAL.WareHouseMng
{
    public class DataFactory : DALBase.FactoryBase<DTO.WareHouseMng.WareHouseSearchResult, DTO.WareHouseMng.WareHouse>
    {
        private DataConverter converter = new DataConverter();
        private DAL.Support.DataFactory supportFactory = new Support.DataFactory();

        private WareHouseMngEntities CreateContext()
        {
            return new WareHouseMngEntities(DALBase.Helper.CreateEntityConnectionString("WareHouseMngModel"));
        }

        public override IEnumerable<DTO.WareHouseMng.WareHouseSearchResult> GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            //try to get data
            try
            {
                using (WareHouseMngEntities context = CreateContext())
                {
                    totalRows = context.WareHouseMng_function_SearchWareHouse(orderBy, orderDirection).Count();
                    return converter.DB2DTO_WareHouseSearchResultList(context.WareHouseMng_function_SearchWareHouse(orderBy, orderDirection).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message };
                return new List<DTO.WareHouseMng.WareHouseSearchResult>();
            }
        }

        public override DTO.WareHouseMng.WareHouse GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            //try to get data
            try
            {
                using (WareHouseMngEntities context = CreateContext())
                {
                    return converter.DB2DTO_WareHouse(context.WareHouseMng_WareHouse_View.Include("WareHouseMng_WareHouseArea_View").FirstOrDefault(o => o.WareHouseID == id));
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message };
                return new DTO.WareHouseMng.WareHouse();
            }
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (WareHouseMngEntities context = CreateContext())
                {
                    WareHouse dbItem = context.WareHouse.FirstOrDefault(o => o.WareHouseID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Warehouse not found!";
                        return false;
                    }
                    else
                    {
                        context.WareHouse.Remove(dbItem);
                        context.SaveChanges();

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message };
                return false;
            }
        }

        public override bool UpdateData(int id, ref DTO.WareHouseMng.WareHouse dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (WareHouseMngEntities context = CreateContext())
                {
                    WareHouse dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new WareHouse();
                        context.WareHouse.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.WareHouse.FirstOrDefault(o => o.WareHouseID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Warehouse not found!";
                        return false;
                    }
                    else
                    {
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoItem.ConcurrencyFlag)))
                        {
                            throw new Exception(DALBase.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        converter.DTO2DB(dtoItem, ref dbItem);
                        dbItem.UpdatedDate = DateTime.Now;

                        // remove orphans
                        context.WareHouseArea.Local.Where(o => o.WareHouse == null).ToList().ForEach(o => context.WareHouseArea.Remove(o));

                        context.SaveChanges();

                        dtoItem = GetData(dbItem.WareHouseID, out notification);

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }

        //
        // custom function 
        //
        public DTO.WareHouseMng.DataContainer GetDataContainer(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            //try to get data
            try
            {
                using (WareHouseMngEntities context = CreateContext())
                {
                    DTO.WareHouseMng.DataContainer dtoItem = new DTO.WareHouseMng.DataContainer();

                    if (id > 0)
                    {
                        DTO.WareHouseMng.WareHouse wareHouseDTOItem = converter.DB2DTO_WareHouse(context.WareHouseMng_WareHouse_View.Include("WareHouseMng_WareHouseArea_View").FirstOrDefault(o => o.WareHouseID == id));
                        dtoItem.WareHouseData = wareHouseDTOItem;
                    }
                    else
                    {
                        dtoItem.WareHouseData = new DTO.WareHouseMng.WareHouse();
                    }

                    // intitialize child collection
                    if (dtoItem.WareHouseData.Areas == null)
                    {
                        dtoItem.WareHouseData.Areas = new List<DTO.WareHouseMng.WareHouseArea>();
                    }

                    // get support data
                    dtoItem.Countries = supportFactory.GetCountry().ToList();

                    return dtoItem;
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message };
                return new DTO.WareHouseMng.DataContainer();
            }
        }
    }
}

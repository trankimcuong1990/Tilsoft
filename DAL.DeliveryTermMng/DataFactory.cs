using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DeliveryTermMng
{
    public class DataFactory : DALBase.FactoryBase<DTO.DeliveryTermMng.DeliveryTermSearchResult, DTO.DeliveryTermMng.DeliveryTerm>
    {
        private DataConverter converter = new DataConverter();
        private DeliveryTermMngEntities CreateContext()
        {
            return new DeliveryTermMngEntities(DALBase.Helper.CreateEntityConnectionString("DeliveryTermMngModel"));
        }

        public override IEnumerable<DTO.DeliveryTermMng.DeliveryTermSearchResult> GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            string DeliveryTermUD = string.Empty;
            string DeliveryTermNM = string.Empty;
            string DeliveryTypeNM = string.Empty;
            bool IsActive = false;
            //try to get data
            try
            {
                using (DeliveryTermMngEntities context = CreateContext())
                {
                    
                    totalRows = context.DeliveryTermMng_function_SearchDeliveryTerm(DeliveryTermUD,DeliveryTermNM,DeliveryTypeNM,IsActive ,orderBy, orderDirection).Count();
                    var result = context.DeliveryTermMng_function_SearchDeliveryTerm(DeliveryTermUD, DeliveryTermNM, DeliveryTypeNM, IsActive,orderBy, orderDirection);
                    return converter.DB2DTO_DeliveryTermSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message };
                return new List<DTO.DeliveryTermMng.DeliveryTermSearchResult>();
            }
        }

        public override DTO.DeliveryTermMng.DeliveryTerm GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            //try to get data
            try
            {
                using (DeliveryTermMngEntities context = CreateContext())
                {
                    return converter.DB2DTO_Material(context.DeliveryTermMng_DeliveryTerm_View.FirstOrDefault(o => o.DeliveryTermID == id));
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message };
                return new DTO.DeliveryTermMng.DeliveryTerm();
            }
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (DeliveryTermMngEntities context = CreateContext())
                {
                    DeliveryTerm dbItem = context.DeliveryTerm.FirstOrDefault(o => o.DeliveryTermID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Delivery Term not found!";
                        return false;
                    }
                    else
                    {
                        context.DeliveryTerm.Remove(dbItem);
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

        public override bool UpdateData(int id, ref DTO.DeliveryTermMng.DeliveryTerm dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (DeliveryTermMngEntities context = CreateContext())
                {
                    DeliveryTerm dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new DeliveryTerm();
                        context.DeliveryTerm.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.DeliveryTerm.FirstOrDefault(o => o.DeliveryTermID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Delivery Term not found!";
                        return false;
                    }
                    else
                    {
                        converter.DTO2BD_Material(dtoItem, ref dbItem);
                        context.SaveChanges();

                        dtoItem = GetData(dbItem.DeliveryTermID, out notification);

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
    }
}

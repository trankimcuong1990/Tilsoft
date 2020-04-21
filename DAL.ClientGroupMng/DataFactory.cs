using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ClientGroupMng
{
    public class DataFactory : DALBase.FactoryBase<DTO.ClientGroupMng.ClientGroupSearchResult, DTO.ClientGroupMng.ClientGroup>
    {
        private DataConverter converter = new DataConverter();
        private ClientGroupMngEntities CreateContext()
        {
            return new ClientGroupMngEntities(DALBase.Helper.CreateEntityConnectionString("ClientGroupMngModel"));
        }

        public override IEnumerable<DTO.ClientGroupMng.ClientGroupSearchResult> GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            //try to get data
            try
            {
                using (ClientGroupMngEntities context = CreateContext())
                {
                    totalRows = context.ClientGroupMng_function_SearchClientGroup(orderBy, orderDirection).Count();
                    var result = context.ClientGroupMng_function_SearchClientGroup(orderBy, orderDirection);
                    return converter.DB2DTO_ClientGroupSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message };
                return new List<DTO.ClientGroupMng.ClientGroupSearchResult>();
            }
        }

        public override DTO.ClientGroupMng.ClientGroup GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            //try to get data
            try
            {
                using (ClientGroupMngEntities context = CreateContext())
                {
                    return converter.DB2DTO_ClientGroup(context.ClientGroupMng_ClientGroup_View.FirstOrDefault(o => o.ClientGroupID == id));
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message };
                return new DTO.ClientGroupMng.ClientGroup();
            }
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ClientGroupMngEntities context = CreateContext())
                {
                    ClientGroup dbItem = context.ClientGroup.FirstOrDefault(o => o.ClientGroupID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Client Group not found!";
                        return false;
                    }
                    else
                    {
                        context.ClientGroup.Remove(dbItem);
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

        public override bool UpdateData(int id, ref DTO.ClientGroupMng.ClientGroup dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ClientGroupMngEntities context = CreateContext())
                {
                    ClientGroup dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new ClientGroup();
                        context.ClientGroup.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.ClientGroup.FirstOrDefault(o => o.ClientGroupID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Client Group not found!";
                        return false;
                    }
                    else
                    {
                        converter.DTO2BD_ClientGroup(dtoItem, ref dbItem);
                        context.SaveChanges();

                        dtoItem = GetData(dbItem.ClientGroupID, out notification);

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

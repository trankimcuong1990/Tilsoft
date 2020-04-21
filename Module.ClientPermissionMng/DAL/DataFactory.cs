using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;
using Module.ClientPermissionMng.DTO;
using Newtonsoft.Json.Linq;

namespace Module.ClientPermissionMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchData, DTO.EditData>
    {
        DataConverter converter = new DataConverter();
        private ClientPermissionMngEntities CreateContext()
        {
            return new ClientPermissionMngEntities(Library.Helper.CreateEntityConnectionString("DAL.ClientPermissionMngModel"));
        }
        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            try
            {
                using (var context = this.CreateContext())
                {
                    ClientPermission productionTeam = context.ClientPermission.FirstOrDefault(o => o.ClientPermissionID == id);

                    if (productionTeam == null)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Can not found data!";

                        return false;
                    }

                    context.ClientPermission.Remove(productionTeam);
                    context.SaveChanges();
                }

                return true;
            }
            catch (System.Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;

                return false;
            }
        }

        public override EditData GetData(int id, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };

            EditData data = new EditData()
            {
                Data = new ClientPermissionDTO() { ClientPermissionDetailDTOs = new List<ClientPermissionDetailDTO>() },
                //Employees = new System.Collections.Generic.List<Support.DTO.Employee>()
            };

            try
            {
                if (id > 0)
                {
                    using (var context = this.CreateContext())
                    {

                        ClientPermissionMng_ClientPermission_View item = context.ClientPermissionMng_ClientPermission_View.Include("ClientPermissionMng_ClientPermissionDetail_View").FirstOrDefault(o => o.ClientPermissionID == id);
                        if (item == null)
                        {
                            notification = new Notification() { Type = NotificationType.Error, Message = "Can not find data." };
                        }
                        else
                        {
                            data.Data = this.converter.DB2DTO_OneClientPermissionView(item);
                        }
                        //Module.Support.DAL.DataFactory support_factory = new Support.DAL.DataFactory();

                        //data.Employees = support_factory.GetEmployee();
                    }
                }
                
            }
            catch (System.Exception ex)
            {                
                notification = new Notification() { Type = NotificationType.Error, Message = Library.Helper.GetInnerException(ex).Message };
            }

            return data;
        }
        public override SearchData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            DTO.SearchData searchFormData = new DTO.SearchData() {Data = new List<ClientPermissionDTO>()};
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            try
            {
                using (var context = this.CreateContext())
                {
                    string ClientUD = filters["ClientUD"]?.ToString().Replace("'", "''");
                    string ClientNM= filters["ClientNM"]?.ToString().Replace("'", "''");
                    totalRows = context.ClientPermission_function_ClientPermissionSearchResult(ClientNM, ClientUD, orderBy, orderDirection).Count();
                    var result = context.ClientPermission_function_ClientPermissionSearchResult(ClientNM, ClientUD, orderBy, orderDirection);
                    //searchFormData.Data = converter.DB2DTO_ClientPermissionView(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                    searchFormData.Data = converter.DB2DTO_ClientPermissionView(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());

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

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            DTO.ClientPermissionDTO clientPermissionDTO = ((JObject)dtoItem).ToObject<DTO.ClientPermissionDTO>();
            notification = new Notification()
            {
                Type = NotificationType.Success
            };

            try
            {
                using (var context = this.CreateContext())
                {
                    ClientPermission clientPermission = new ClientPermission();

                    if (id == 0)
                        context.ClientPermission.Add(clientPermission);

                    if (id > 0)
                    {
                        clientPermission = context.ClientPermission.FirstOrDefault(o => o.ClientPermissionID == id);

                        if (clientPermission == null)
                        {
                            notification.Message = "Can not found data!";
                            return false;
                        }
                    }
                    clientPermission.ClientID = clientPermissionDTO.ClientID;
                    clientPermission.Remark = clientPermissionDTO.Remark;                                       
                    clientPermission.UpdatedBy = userId;
                    clientPermission.UpdatedDate = System.DateTime.Now;
                    List<int> dbClientPermissionDelete = clientPermission.ClientPermissionDetail.Select(y => y.ClientPermissionDetailID).Where(x => !clientPermissionDTO.ClientPermissionDetailDTOs.Select(z => z.ClientPermissionDetailID).Contains(x)).ToList();
                    foreach (DTO.ClientPermissionDetailDTO item in clientPermissionDTO.ClientPermissionDetailDTOs)
                    {
                        if (item.ClientPermissionDetailID<0)
                        {
                            
                            clientPermission.ClientPermissionDetail.Add(converter.DTO2DB_ClientPermissionDetail(item));
                        }
                        else
                        {
                            ClientPermissionDetail detail = clientPermission.ClientPermissionDetail.Where(x => x.ClientPermissionDetailID == item.ClientPermissionDetailID).FirstOrDefault();
                            detail.UserId = item.UserID;
                            detail.ModuleID = item.ModuleID;
                        }
                    }
                    foreach (int iddelete in dbClientPermissionDelete)
                    {
                        ClientPermissionDetail itemdelete = clientPermission.ClientPermissionDetail.Where(x => x.ClientPermissionDetailID == iddelete).FirstOrDefault();
                        if (itemdelete != null)
                            clientPermission.ClientPermissionDetail.Remove(itemdelete);
                    }
                    context.SaveChanges();
                    dtoItem = this.GetData(clientPermission.ClientPermissionID, out notification);
                }

                return true;
            }
            catch (System.Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;

                return false;
            }
        }
        public List<DTO.Employee> QuickSearchEmployee(Hashtable filters, out Library.DTO.Notification notification)
        {
            List<DTO.Employee> data = new List<DTO.Employee>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int totalRows = 0;

            //search info
            string searchQuery = string.Empty;
            if (filters.ContainsKey("searchQuery")) searchQuery = filters["searchQuery"].ToString();

            //pager info
            int pageSize = 20;
            int pageIndex = 1;
            string sortedBy = string.Empty;
            string sortedDirection = string.Empty;

            if (filters.ContainsKey("pageSize")) pageSize = Convert.ToInt32(filters["pageSize"].ToString());
            if (filters.ContainsKey("pageIndex")) pageIndex = Convert.ToInt32(filters["pageIndex"].ToString());
            if (filters.ContainsKey("sortedBy")) sortedBy = filters["sortedBy"].ToString();
            if (filters.ContainsKey("sortedDirection")) sortedDirection = filters["sortedDirection"].ToString();

            try
            {
                using (ClientPermissionMngEntities context = CreateContext())
                {
                    totalRows = context.ClientPermissionMng_function_QuickSearchEmployee(sortedBy, sortedDirection, searchQuery).Count();
                    var result = context.ClientPermissionMng_function_QuickSearchEmployee(sortedBy, sortedDirection, searchQuery);
                    data = converter.DB2DTO_Employee(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
                return data;
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
                return data;
            }
        }
        public List<DTO.ModuleDTO> QuickSearchModule(Hashtable filters, out Library.DTO.Notification notification)
        {
            List<DTO.ModuleDTO> data = new List<DTO.ModuleDTO>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int totalRows = 0;

            //search info
            string searchQuery = string.Empty;
            if (filters.ContainsKey("searchQuery")) searchQuery = filters["searchQuery"].ToString();

            //pager info
            int pageSize = 20;
            int pageIndex = 1;
            string sortedBy = string.Empty;
            string sortedDirection = string.Empty;

            if (filters.ContainsKey("pageSize")) pageSize = Convert.ToInt32(filters["pageSize"].ToString());
            if (filters.ContainsKey("pageIndex")) pageIndex = Convert.ToInt32(filters["pageIndex"].ToString());
            if (filters.ContainsKey("sortedBy")) sortedBy = filters["sortedBy"].ToString();
            if (filters.ContainsKey("sortedDirection")) sortedDirection = filters["sortedDirection"].ToString();

            try
            {
                using (ClientPermissionMngEntities context = CreateContext())
                {
                    totalRows = context.ClientPermissionMng_function_QuickSearchModule(sortedBy, sortedDirection, searchQuery).Count();
                    var result = context.ClientPermissionMng_function_QuickSearchModule(sortedBy, sortedDirection, searchQuery);
                    data = converter.DB2DTO_Module(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
                return data;
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
                return data;
            }
        }
    }
}

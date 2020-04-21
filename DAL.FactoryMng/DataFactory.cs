using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.FactoryMng
{
    public class DataFactory : DALBase.FactoryBase2<DTO.FactoryMng.SearchFormData, DTO.FactoryMng.EditFormData, DTO.FactoryMng.Factory>
    {
        private DataConverter converter = new DataConverter();
        private DAL.Support.DataFactory supportFactory = new Support.DataFactory();

        private FactoryMngEntities CreateContext()
        {
            return new FactoryMngEntities(DALBase.Helper.CreateEntityConnectionString("FactoryMngModel"));
        }

        public override DTO.FactoryMng.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.FactoryMng.SearchFormData data = new DTO.FactoryMng.SearchFormData();
            data.Data = new List<DTO.FactoryMng.FactorySearchResult>();

            totalRows = 0;

            //try to get data
            try
            {
                using (FactoryMngEntities context = CreateContext())
                {
                    totalRows = context.FactoryMng_function_SearchFactory(orderBy, orderDirection).Count();
                    var result = context.FactoryMng_function_SearchFactory(orderBy, orderDirection);
                    data.Data =  converter.DB2DTO_FactorySearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override DTO.FactoryMng.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.FactoryMng.EditFormData data = new DTO.FactoryMng.EditFormData();
            data.Data = new DTO.FactoryMng.Factory();
            data.Locations = new List<DTO.Support.Location>();
            data.Suppliers = new List<DTO.Support.Supplier>();

            //try to get data
            try
            {
                using (FactoryMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        data.Data = converter.DB2DTO_Material(context.FactoryMng_Factory_View.FirstOrDefault(o => o.FactoryID == id));
                    }
                    data.Locations = supportFactory.GetLocation().ToList();
                    data.Suppliers = supportFactory.GetSupplier().ToList();
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
                using (FactoryMngEntities context = CreateContext())
                {
                    Factory dbItem = context.Factory.FirstOrDefault(o => o.FactoryID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Factory not found!";
                        return false;
                    }
                    else
                    {
                        context.Factory.Remove(dbItem);
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

        public override bool UpdateData(int id, ref DTO.FactoryMng.Factory dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FactoryMngEntities context = CreateContext())
                {
                    Factory dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new Factory();
                        context.Factory.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.Factory.FirstOrDefault(o => o.FactoryID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Factory not found!";
                        return false;
                    }
                    else
                    {
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoItem.ConcurrencyFlag_String)))
                        {
                            throw new Exception(DALBase.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        converter.DTO2BD_Material(dtoItem, ref dbItem);
                        context.SaveChanges();

                        // run sync
                        if (id == 0)
                        {
                            context.SyncFactoryAndUserAccessFactory();
                            context.SaveChanges();
                        }

                        dtoItem = GetData(dbItem.FactoryID, out notification).Data;

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

        public override bool Approve(int id, ref DTO.FactoryMng.Factory dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.FactoryMng.Factory dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public DTO.FactoryMng.EditFormData GetData(int id, int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.FactoryMng.EditFormData data = new DTO.FactoryMng.EditFormData();
            data.Data = new DTO.FactoryMng.Factory();
            data.Locations = new List<DTO.Support.Location>();
            data.Suppliers = new List<DTO.Support.Supplier>();

            //try to get data
            try
            {
                using (FactoryMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        data.Data = converter.DB2DTO_Material(context.FactoryMng_Factory_View.FirstOrDefault(o => o.FactoryID == id));
                    }
                    //else
                    //{
                    //    data.Data.UpdatedBy = userId;
                    //    data.Data.UpdatedDate = DateTime.Now.ToString("dd/MM/yyyy");

                    //    var employee = context.Employee.Where(s => s.UserID.HasValue && s.UserID.Value == userId).FirstOrDefault();
                    //    data.Data.UpdatorName = (employee == null) ? string.Empty : employee.EmployeeNM;
                    //}

                    data.Locations = supportFactory.GetLocation().ToList();
                    data.Suppliers = supportFactory.GetSupplier().ToList();
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

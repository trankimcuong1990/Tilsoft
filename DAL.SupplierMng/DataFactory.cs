using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.SupplierMng
{
    public class DataFactory :  DALBase.FactoryBase2<DTO.SupplierMng.SearchFormData, DTO.SupplierMng.EditFormData, DTO.SupplierMng.Supplier>
    {
        private DataConverter converter = new DataConverter();
        private DAL.Support.DataFactory supportFactory = new Support.DataFactory();

        private SupplierMngEntities CreateContext()
        {
            return new SupplierMngEntities(DALBase.Helper.CreateEntityConnectionString("SupplierMngModel"));
        }

        public override DTO.SupplierMng.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SupplierMng.SearchFormData data = new DTO.SupplierMng.SearchFormData();
            data.Data = new List<DTO.SupplierMng.SupplierSearchResult>();

            totalRows = 0;

            //try to get data
            try
            {
                using (SupplierMngEntities context = CreateContext())
                {
                    totalRows = context.SupplierMng_function_SearchSupplier(orderBy, orderDirection).Count();
                    var result = context.SupplierMng_function_SearchSupplier(orderBy, orderDirection);
                    data.Data = converter.DB2DTO_SupplierSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override DTO.SupplierMng.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SupplierMng.EditFormData data = new DTO.SupplierMng.EditFormData();
            data.Data = new DTO.SupplierMng.Supplier();
            data.Data.Factories = new List<DTO.SupplierMng.Factory>();
            data.PaymentTerms = new List<DTO.Support.PaymentTerm>();
            data.DeliveryTerms = new List<DTO.Support.DeliveryTerm>();
            data.ManufacturerCountries = new List<DTO.Support.ManufacturerCountry>();

            //try to get data
            try
            {
                using (SupplierMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        data.Data = converter.DB2DTO_Supplier(context.SupplierMng_Supplier_View.Include("SupplierMng_Factory_View").FirstOrDefault(o => o.SupplierID == id));
                    }
                    
                    data.ManufacturerCountries = supportFactory.GetManufacturerCountry().ToList();
                    data.PaymentTerms = supportFactory.GetPaymentTerm().ToList();
                    data.DeliveryTerms = supportFactory.GetDeliveryTerm().ToList();
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
                using (SupplierMngEntities context = CreateContext())
                {
                    Supplier dbItem = context.Supplier.FirstOrDefault(o => o.SupplierID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Supplier not found!";
                        return false;
                    }
                    else
                    {
                        context.Supplier.Remove(dbItem);
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

        public override bool UpdateData(int id, ref DTO.SupplierMng.Supplier dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (SupplierMngEntities context = CreateContext())
                {
                    Supplier dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new Supplier();
                        context.Supplier.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.Supplier.FirstOrDefault(o => o.SupplierID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Supplier not found!";
                        return false;
                    }
                    else
                    {
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoItem.ConcurrencyFlag_String)))
                        {
                            throw new Exception(DALBase.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        converter.DTO2BD(dtoItem, ref dbItem);
                        context.SaveChanges();

                        dtoItem = GetData(dbItem.SupplierID, out notification).Data;

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

        public override bool Approve(int id, ref DTO.SupplierMng.Supplier dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.SupplierMng.Supplier dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public DTO.SupplierMng.EditFormData GetData(int id, int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SupplierMng.EditFormData data = new DTO.SupplierMng.EditFormData();
            data.Data = new DTO.SupplierMng.Supplier();
            data.Data.Factories = new List<DTO.SupplierMng.Factory>();
            data.PaymentTerms = new List<DTO.Support.PaymentTerm>();
            data.DeliveryTerms = new List<DTO.Support.DeliveryTerm>();
            data.ManufacturerCountries = new List<DTO.Support.ManufacturerCountry>();

            //try to get data
            try
            {
                using (SupplierMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        data.Data = converter.DB2DTO_Supplier(context.SupplierMng_Supplier_View.Include("SupplierMng_Factory_View").FirstOrDefault(o => o.SupplierID == id));
                    }
                    //else
                    //{
                    //    data.Data.UpdatedBy = userId;
                    //    data.Data.UpdatedDate = DateTime.Now.ToString("dd/MM/yyyy");

                    //    var employee = context.Employee.Where(s => s.UserID.HasValue && s.UserID.Value == userId).FirstOrDefault();
                    //    data.Data.UpdatorName = (employee == null) ? string.Empty : employee.EmployeeNM;
                    //}

                    data.ManufacturerCountries = supportFactory.GetManufacturerCountry().ToList();
                    data.PaymentTerms = supportFactory.GetPaymentTerm().ToList();
                    data.DeliveryTerms = supportFactory.GetDeliveryTerm().ToList();
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

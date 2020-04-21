using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;

namespace Module.SupplierMng.DAL
{
    public class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private DataConverter converter = new DataConverter();

        private SupplierMngEntities CreateContext()
        {
            return new SupplierMngEntities(Library.Helper.CreateEntityConnectionString("DAL.SupplierMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.SupplierSearchResult>();

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

        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new DTO.Supplier();
            data.Data.Factories = new List<DTO.Factory>();
            data.Data.supplierBanks = new List<DTO.SupplierBankDTO>();
            data.PaymentTerms = new List<DTO.PaymentTerm>();
            data.DeliveryTerms = new List<DTO.DeliveryTerm>();
            data.ManufacturerCountries = new List<DTO.ManufacturerCountry>();

            //try to get data
            try
            {
                using (SupplierMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        data.Data = converter.DB2DTO_Supplier(context.SupplierMng_Supplier_View
                            .Include("SupplierMng_Factory_View")
                            .Include("SupplierMng_SupplierBank_View").FirstOrDefault(o => o.SupplierID == id));
                    }

                    data.ManufacturerCountries = GetManufacturerCountry().ToList();
                    data.PaymentTerms = GetPaymentTerm().ToList();
                    data.DeliveryTerms = GetDeliveryTerm().ToList();
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

        public bool UpdateData(int id, ref object dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.Supplier dtoSupplier = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.Supplier>();
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
                        dtoSupplier.UpdatedBy = iRequesterID;
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Supplier not found!";
                        return false;
                    }
                    else
                    {
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoSupplier.ConcurrencyFlag_String)))
                        {
                            throw new Exception(Library.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        converter.DTO2BD(dtoSupplier, ref dbItem);

                        context.SupplierBank.Local.Where(o => o.Supplier == null).ToList().ForEach(o => context.SupplierBank.Remove(o));

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

        public DTO.EditFormData GetData(int id, int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new DTO.Supplier();
            data.Data.Factories = new List<DTO.Factory>();
            data.PaymentTerms = new List<DTO.PaymentTerm>();
            data.DeliveryTerms = new List<DTO.DeliveryTerm>();
            data.ManufacturerCountries = new List<DTO.ManufacturerCountry>();

            //try to get data
            try
            {
                using (SupplierMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        data.Data = converter.DB2DTO_Supplier(context.SupplierMng_Supplier_View.Include("SupplierMng_Factory_View").FirstOrDefault(o => o.SupplierID == id));
                    }

                    data.ManufacturerCountries = GetManufacturerCountry().ToList();
                    data.Companies = GetCompany().ToList();
                    data.PaymentTerms = GetPaymentTerm().ToList();
                    data.DeliveryTerms = GetDeliveryTerm().ToList();
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public IEnumerable<DTO.Company> GetCompany()
        {
            //try to get data
            try
            {
                using (SupplierMngEntities context = CreateContext())
                {
                    return converter.DB2DTO_Company(context.List_Company_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Company>();
            }
        }
        public IEnumerable<DTO.ManufacturerCountry> GetManufacturerCountry()
        {
            //try to get data
            try
            {
                using (SupplierMngEntities context = CreateContext())
                {
                    return converter.DB2DTO_ManufacturerCountry(context.List_ManufacturerCountry_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.ManufacturerCountry>();
            }
        }

        public IEnumerable<DTO.PaymentTerm> GetPaymentTerm()
        {
            //try to get data
            try
            {
                using (SupplierMngEntities context = CreateContext())
                {
                    return converter.DB2DTO_PaymentTerm(context.List_PaymentTerm.ToList());
                }
            }
            catch
            {
                return new List<DTO.PaymentTerm>();
            }
        }

        public IEnumerable<DTO.DeliveryTerm> GetDeliveryTerm()
        {
            //try to get data
            try
            {
                using (SupplierMngEntities context = CreateContext())
                {
                    return converter.DB2DTO_DeliveryTerm(context.List_DeliveryTerm.ToList());
                }
            }
            catch
            {
                return new List<DTO.DeliveryTerm>();
            }
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}
